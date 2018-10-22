using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace GridlistCFConverter
{
    class OthogonalReducedParser
    {
        const double RES_ERROR = 1E-10;


        private double lonOffset;
        private double latOffset;

        private int nlons;
        private int nlats;


        private NeighborOrdering ordering = NeighborOrdering.DoubleFloor;



        public Coord[] Coords { get; set; } 
        public double Resolution { get; set; }
        public double MinLon { get; set; }
        public double MaxLon { get; set; }
        public double MinLat { get; set; }
        public double MaxLat { get; set; }

        public OthogonalReducedParser(double[] lons, double[] lats)
        {
            Coords = new Coord[lons.Length];

            for (int i = 0; i < lons.Length; i++)
            {
                Coords[i] = new Coord(lons[i], lats[i]);
            }

            MinLon = Coords.First().Lon;
            MaxLon = Coords.First().Lon;
            MinLat = Coords.First().Lat;
            MaxLat = Coords.First().Lat;

            foreach (Coord coord in Coords)
            {
                if (MinLon > coord.Lon)
                    MinLon = coord.Lon;

                if (MaxLon < coord.Lon)
                    MaxLon = coord.Lon;

                if (MinLat > coord.Lat)
                    MinLat = coord.Lat;

                if (MaxLat < coord.Lat)
                    MaxLat = coord.Lat;
            }


            Coord first = Coords[0];

            double min = double.MaxValue;

            for (int i = 1; i < Coords.Length; i++)
            {
                double x = Coords[i].Lon - first.Lon;
                double y = Coords[i].Lat - first.Lat;

                double dist = Math.Sqrt(x * x + y * y);

                if (dist < min)
                {
                    min = dist;
                }

            }


            if (Math.Abs(min) < 1E-12)
            {
                throw new Exception("Invalid spatial resolution, are some coords twice in list ?!");
            }


            Resolution = min;
            int a = (int)(Coords[0].Lon / min);
            int b = (int)(Coords[0].Lat / min);


            lonOffset = min * a - Coords[0].Lon > 0 ? min * a - Coords[0].Lon : -min * a + Coords[0].Lon;
            latOffset = min * b - Coords[0].Lat > 0 ? min * b - Coords[0].Lat : -min * b + Coords[0].Lat;

            nlons = (int)((MaxLon - MinLon) / Resolution);
            nlats = (int)((MaxLat - MinLat) / Resolution);
        }



        public int FindCoordIndex(double lon, double lat)
        {

            double index_scaled_lon = (lon - lonOffset) / Resolution;
            double index_scaled_lat = (lat - latOffset) / Resolution;


            List<Neighbor> possible_neighbors = GetNeighbors(index_scaled_lon, index_scaled_lat);


            // Sort the neighbors to be sure they are in the right order
            // The sorting algorithm is determined by the > operator of neighbor struct
            // Only sort if we have more than 1 element...
            if (possible_neighbors.Count > 1)
            {

                switch (ordering)
                {
                    case NeighborOrdering.DoubleFloor:
                        possible_neighbors.OrderBy(p => p.Lon).ThenBy(p => p.Lat);
                        break;
                    case NeighborOrdering.DoubleCeiling:
                        possible_neighbors.Sort();
                        possible_neighbors.Reverse();
                        break;
                }

            }



            bool found = false;
            int neighbor_index = 0;
            int id = -1;


            // Loop through the neigbors and find a suitable neighbor
            // Here, a major role plays the index of operator overload of neighbor struct overload
            while (!found && neighbor_index < possible_neighbors.Count)
            {
                // Calculate lon and lat which suite gridlist resolution
                double lon_gridlist = possible_neighbors[neighbor_index].Lon * Resolution + lonOffset;
                double lat_gridlist = possible_neighbors[neighbor_index].Lat * Resolution + latOffset;

                // If the distance between lon (of interest) and the nearest neighbor lon_gridlist is greater than the spatial resolution
                //if (std::abs(std::abs(lon_gridlist) - std::abs(lon)) > spatial_resolution || std::abs(std::abs(lat_gridlist) - std::abs(lat)) > spatial_resolution)
                //    dprintf("Warning: Coord (%f,%f) differs too much from nearest neighbor (%f,%f)", lon, lat, lon_gridlist, lat_gridlist);

                int i = 0;
                // Search for the correct lon and lat inside the coord list
                // If no coord is in the list - 1 is returned
                while (!found && i < Coords.Length)
                {
                    bool lon_found = Math.Abs(Coords[i].Lon - lon_gridlist) < RES_ERROR ? true : false;
                    bool lat_found = Math.Abs(Coords[i].Lat - lat_gridlist) < RES_ERROR ? true : false;
                    if (lon_found && lat_found)
                    {
                        id = i;
                        found = true;
                    }
                    i++;
                }
                neighbor_index++;
            }

            return id;

        }
        


        private List<Neighbor> GetNeighbors(double index_scaled_lon, double index_scaled_lat)
        {


            List<Neighbor> possible_neighbors = new List<Neighbor>();


            double d1 = Math.Abs(Math.Abs(index_scaled_lon) - Math.Abs(Auxil.Floor(index_scaled_lon)) - 0.5);
            double d2 = Math.Abs(Math.Abs(index_scaled_lat) - Math.Abs(Auxil.Floor(index_scaled_lat)) - 0.5);

            if (Math.Abs(Math.Abs(index_scaled_lon) - Math.Abs(Auxil.Floor(index_scaled_lon)) - 0.5) < RES_ERROR)
            {

                //Four equal neighbors lon and lat have 0.5 distance to neighbors each
                if (Math.Abs(Math.Abs(index_scaled_lat) - Math.Abs(Auxil.Floor(index_scaled_lat)) - 0.5) < RES_ERROR)
                {
                    possible_neighbors.Add(new Neighbor(index_scaled_lon - 0.5, index_scaled_lat - 0.5));
                    possible_neighbors.Add(new Neighbor(index_scaled_lon + 0.5, index_scaled_lat - 0.5));
                    possible_neighbors.Add(new Neighbor(index_scaled_lon - 0.5, index_scaled_lat + 0.5));
                    possible_neighbors.Add(new Neighbor(index_scaled_lon + 0.5, index_scaled_lat + 0.5));
                }

                else
                {
                    //lat coord is close enough to an exclusive neighbor
                    //only two lons here
                    possible_neighbors.Add(new Neighbor(index_scaled_lon - 0.5, Math.Round(index_scaled_lat)));
                    possible_neighbors.Add(new Neighbor(index_scaled_lon + 0.5, Math.Round(index_scaled_lat)));
                }

            }

            //Lon is close enough to an exclusive neighbor but not lat.
            //again two options
            else if (Math.Abs(Math.Abs(index_scaled_lat) - Math.Abs(Auxil.Floor(index_scaled_lat)) - 0.5) < RES_ERROR)
            {
                possible_neighbors.Add(new Neighbor(Math.Round(index_scaled_lon), index_scaled_lat - 0.5));
                possible_neighbors.Add(new Neighbor(Math.Round(index_scaled_lon), index_scaled_lat + 0.5));
            }

            //Coords are close enough to an coord(lon, lat)  we have only one suitable candidate
            else
            {
                possible_neighbors.Add(new Neighbor(Math.Round(index_scaled_lon), Math.Round(index_scaled_lat)));
            }

            return possible_neighbors;


        }




    }
}
