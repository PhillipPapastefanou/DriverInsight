using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridlistCFConverter
{
    class OrthogonalBitmapTransposer
    {
        private OrthogonalReader orthoReader;
        private int stationID;

        private int[] lonIndexes;
        private int[] latIndexes;

        private double[] lons;
        private double[] lats;

        public OrthogonalBitmapTransposer(OrthogonalReader orthoReader, int stationID)
        {
            this.orthoReader = orthoReader;
            this.stationID = stationID;
        }

        public double GetLon(int lon_index)
        {
            return lons[lon_index];
        }
        public double GetLat(int lat_index)
        {
            return lats[lat_index];
        }

        public int GetLonIndex(double lon)
        {
            return Auxil.Floor(359.5 + 2.0 * lon);
        }

        public int GetLatIndex(double lat)
        {
            return Auxil.Floor(179.5 + 2.0 * lat);
        }


        public byte[,] GetPixels()
        {

            //The Bitmap just behaves like CVS parser
            lons = new double[720];
            lats = new double[360];

            for (int i = 0; i < lons.Length; i++)
            {
                lons[i] = -179.75 + 0.5 * i;
            }

            for (int i = 0; i < lats.Length; i++)
            {
                lats[i] = -89.75 + 0.5 * i;
            }


            CVSParser parser = new CVSParser(lons, lats);
            Coord[] coords = orthoReader.Coords;
            double[] lonsOfInterest = new double[coords.Length];
            double[] latsOfInterest = new double[coords.Length];
            for (int i = 0; i < coords.Length; i++)
            {
                lonsOfInterest[i] = coords[i].Lon;
                latsOfInterest[i] = coords[i].Lat;
            }


            lonIndexes = parser.FindLonIndex(lonsOfInterest);
            latIndexes = parser.FindLatIndex(latsOfInterest);


            // We acutally do not need values here!
            //double[] flatValues = orthoReader.GetFlatData(stationID);


            byte[,] dataPoints = new byte[lons.Length, lats.Length];

            for (int i = 0; i < coords.Length; i++)
            {
                dataPoints[lonIndexes[i], latIndexes[i]] = 1;
            }

            return dataPoints;
        }
    }
}
