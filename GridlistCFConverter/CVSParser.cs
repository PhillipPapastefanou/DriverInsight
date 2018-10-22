using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridlistCFConverter
{
    class CVSParser
    {
        public double Resolution { get; set; }
        public double MinLon { get; set; }
        public double MaxLon { get; set; }
        public double MinLat { get; set; }
        public double MaxLat { get; set; }
        public double[] Lons { get; private set; }
        public double[] Lats { get; private set; }

        private bool lon_descending;
        private bool lat_descending;

        const double RES_ERROR = 1E-10;

        private int nlons;
        private int nlats;

       

        public CVSParser(double[] lons, double[] lats)
        {
            Lons = lons;
            Lats = lats;

            nlons = lons.Length;
            nlats = lats.Length;

            MinLon = lons.Min(); 
            MaxLon = lons.Max();

            MinLat = lats.Min(); 
            MaxLat = lats.Max();

            if (lons.Length == 1)
            {
                Resolution = 0.0;
            }

            else
            {
                Resolution = Math.Abs(lons[0] - lons[1]);

                if (lons[0] - lons[1] < 0)
                {
                    lon_descending = true;
                }
                if (lats[0] - lats[1] < 0)
                {
                    lat_descending = true;
                }

            }
        }



        public int FindLonIndex(double lon)
        {

            double a = (nlons - 1) / (MaxLon - MinLon);
            double b = -MinLon * (nlons - 1) / (MaxLon - MinLon);

            if (!lon_descending)
            {
                a = -a;
                //b = b;
            }


            double lon_d = a * lon + b;
             
            return Auxil.Floor(lon_d);

        }


        public int[] FindLonIndex(double[] lons)
        {
            int[] vals = new int[lons.Length];
            for (int i = 0; i < lons.Length; i++)
            {
                vals[i] = FindLonIndex(lons[i]);
            }
            return vals;
        }

        public int FindLatIndex(double lat)
        {
            double a = (nlats - 1) / (MaxLat - MinLat);
            double b = -MinLat * (nlats - 1) / (MaxLat - MinLat);

            if (!lat_descending)
            {
               // a = -a;
                //b = b;
            }

            double lat_d = a * lat + b;
            return Auxil.Floor(lat_d);
        }

        public int[] FindLatIndex(double[] lats)
        {
            int[] vals = new int[lats.Length];
            for (int i = 0; i < lats.Length; i++)
            {
                vals[i] = FindLatIndex(lats[i]);
            }
            return vals;
        }
    }
}
