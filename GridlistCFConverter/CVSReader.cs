using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NcInterop;
using NcInterop.Base;

namespace GridlistCFConverter
{
    class CVSReader
    {

        private NcReader reader;
        private int lonDimIndex;
        private int latDimIndex;

        public double MissingValue { get; set; }

        public CVSParser Parser { get; set; }

        public CVSReader(NcReader reader, CVSParser parser)
        {
            this.reader = reader;
            this.Parser = parser;

            object missgValue =reader.MainGroup.GlobalAtts["missing_value"].Value;

            MissingValue = Convert.ToDouble(missgValue, CultureInfo.InvariantCulture);
        }

        public double[] GetFlatData(int lonDimID, int latDimID)
        {
            bool foundVar = false;

            int varID = -1;
            //At which dimension of the variable is the station dimension
            int varLonDimID = -1;
            int varLatDimID = -1;

            foreach (NcVariable var in reader.MainGroup.Variabels)
            {
                int subID = 0;
                bool foundLon = false;
                bool foundLat = false;

                foreach (var dimID in var.DimIDs)
                {
                    
                    if (dimID == lonDimID)
                    {
                        foundLon = true;
                        varLonDimID = dimID;
                    }

                    if (dimID == latDimID)
                    {
                        foundLat = true;
                        varLatDimID = dimID;
                    }
                    subID++;
                }

                if (foundLat && foundLon)
                {

                    varID = var.ID;
                    foundVar = true;
                    break;
                }
            }


            NcVariable varSuit = reader.MainGroup.Variabels[varID];
            int[] dimIDs = varSuit.DimIDs;


            NcRange[] range = new NcRange[dimIDs.Length];


            for (int i = 0; i < range.Length; i++)
            {
                if (dimIDs[i] == varLonDimID)
                {
                    range[i] = new NcRange(0, Parser.Lons.Length - 1);
                }
                else if(dimIDs[i] == varLatDimID)
                {
                    range[i] = new NcRange(0, Parser.Lats.Length - 1);
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
