using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridlistCFConverter
{
    class Coord
    {
        public Coord(double lon, double lat)
        {
            this.Lon = lon;
            this.Lat = lat;
        }

        public double Lon { get; set; }
        public double Lat { get; set; }
    }


    enum NeighborOrdering
    {
        /// Double floor has the following edges order
        // 3			4
        //
        //		 x
        //
        // 1			2
        //
        DoubleFloor,

        /// Double Ceiling has the following edges order
        // 2			1
        //
        //		 x
        //
        // 4			3
        //
        DoubleCeiling
    };


    class Neighbor : IComparable
    {
        public Neighbor(double lon, double lat)
        {
            this.Lon = lon;
            this.Lat = lat;
        }

        public static bool operator >(Neighbor n1, Neighbor n2)
        {
            if (n1 < n2)
            {
                return false;
            }

            return true;
        }

        public static bool operator <(Neighbor n1, Neighbor n2)
        {
            if (n1.Lon < n2.Lon)
            {
                if (n1.Lat < n2.Lat)
                {
                    return true;
                }
            }

            return false;

        }

        public double Lon { get; set; }
        public double Lat { get; set; }
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
