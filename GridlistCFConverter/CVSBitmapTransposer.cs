using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridlistCFConverter
{
    class CVSBitmapTransposer
    {
        private CVSReader cvsreader;
        private int lonID;
        private int latID;

        private int[] lonIndexes;
        private int[] latIndexes;

        private double[] lons;
        private double[] lats;


        private double[] worldLons;
        private double[] worldLats;


        public CVSBitmapTransposer(CVSReader cvsreader, int lonID, int latID)
        {
            this.cvsreader = cvsreader;
            this.lonID = lonID;
            this.latID = latID;
            this.lons = cvsreader.Parser.Lons;
            this.lats = cvsreader.Parser.Lats;
        }

        public double GetLon(int lon_index)
        {
            return worldLons[lon_index];
        }
        public double GetLat(int lat_index)
        {
            return worldLats[lat_index];
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
            double[] flatValues = cvsreader.GetFlatData(lonID, latID);

            double missing_value = this.cvsreader.MissingValue;

            //The Bitmap just behaves like CVS parser
            worldLons = new double[720];
            worldLats = new double[360];

            for (int i = 0; i < worldLons.Length; i++)
            {
                worldLons[i] = -179.75 + 0.5 * i;
            }

            for (int i = 0; i < worldLats.Length; i++)
            {
                worldLats[i] = -89.75 + 0.5 * i;
            }

            // Figure out the "existing" datapoints first
            byte[,] dataPoints = new byte[lons.Length, lats.Length];

            int u = 0;
            for (int w = 0; w < lons.Length; w++)
            {
                for (int h = 0; h < lats.Length; h++)
                {
                    //int i = w * lats.Length + h;
                    int i = h * lons.Length + w;
                    
                    if (Math.Abs(flatValues[i] - missing_value) < 0.5)
                    {
                        dataPoints[w, h] = 0;
                    }

                    else
                    {
                        dataPoints[w, h] = 1;
                        u++;
                    }

                }
            }
            


            byte[,] worldPoints = new byte[worldLons.Length, worldLats.Length];

            for (int w = 0; w < lons.Length; w++)
            {
                for (int h = 0; h < lats.Length; h++)
                {
                    int worldW = ScaleLonToWorldIndex(lons[w]);
                    int worldH = ScaleLatToWorldIndex(lats[h]);

                    worldPoints[worldW, worldH] = dataPoints[w, h];
                }
            }

            return worldPoints;
        }

        private int  ScaleLonToWorldIndex(double lon)
        {
            return Auxil.Floor(359.5 + 2.0 * lon);
        }


        private int ScaleLatToWorldIndex(double lat)
        {
            return Auxil.Floor(179.5 + 2.0 * lat);
        }






    }
}
