using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NcInterop;
using NcInterop.Base;

namespace GridlistCFConverter
{
    class OrthogonalReader
    {
        private NcReader reader;
        private OthogonalReducedParser parser;
        private int stationDimIndex;

        public Coord[] Coords { get; }

        public OrthogonalReader(NcReader reader, OthogonalReducedParser parser)
        {
            this.reader = reader;
            this.parser = parser;

            Coords = parser.Coords;
        }


        public double[] GetFlatData(int stationDimID)
        {

            bool foundVar = false;
            int varID = -1;
            //At which dimension of the variable is the station dimension
            int varDimID = -1;

            foreach (NcVariable var in reader.MainGroup.Variabels)
            {
                int subID = 0;
                foreach (var dimID in var.DimIDs)
                {
                    if (dimID == stationDimID)
                    {
                        foundVar = true;
                        varID = var.ID;
                        varDimID = subID;
                        break;
                    }
                    subID++;
                }
            }


            NcVariable varSuit = reader.MainGroup.Variabels[varID];
            int[] dimIDs = varSuit.DimIDs;

            NcRange[] range = new NcRange[dimIDs.Length];

            for (int i = 0; i < range.Length; i++)
            {
                if (i == varDimID)
                {
                    range[i] = new NcRange(0, parser.Coords.Length - 1);
                }
                else
                {
                    range[i] = new NcRange(0, 0);
                }
            }

            return varSuit.GetData<double>(range);

        }
    }
}
