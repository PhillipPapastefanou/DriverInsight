using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using NcInterop;

namespace GridlistCFConverter
{
    class ResolutionInterop
    {
        private NcReader reader;
        private List<string> allVarNames;
        private List<string> allDimNames;

        private int nstation;
        private int nlon;
        private int nlat;
        private int station_dim_id;
        private int lon_dim_id;
        private int lat_dim_id;


      

        private int lonID;
        private int latID;

        private OthogonalReducedParser oma_parser;
        private OrthogonalBitmapTransposer oma_transposer;

        private CVSParser cvs_parser;
        private CVSBitmapTransposer cvs_transposer;

        private static List<string> lonVarAliases = new List<string>() {"lon", "longitude", "base_longitude"};
        private static List<string> latVarAliases = new List<string>() { "lat", "latitude", "base_latitude" };


        private static List<string> stationVarAliases = new List<string>() { "station", "gridcell", "base_station" };


        public bool IsOrthogonal { get; set; }
        public double Resolution { get; set; }
        public double MinLon { get; set; }
        public double MaxLon { get; set; }
        public double MinLat { get; set; }
        public double MaxLat { get; set; }




        public ResolutionInterop(NcReader reader)
        {
            this.reader = reader;

            allVarNames = reader.MainGroup.Variabels.AllNames;
            allDimNames = reader.MainGroup.Dimensions.AllNames;

            
            lonID = FindAliasVarID(lonVarAliases);
            latID = FindAliasVarID(latVarAliases);


            int nlondims = reader.MainGroup.Variabels[lonID].NDims;
            int nlatdims = reader.MainGroup.Variabels[latID].NDims;


            if (lonID == -1)
            {
                throw new Exception("Could not find longitude variable");
            }

            if (latID == -1)
            {
                throw new Exception("Could not find latitude variable");
            }

            // We have the reduced version of the input file
            // Input is read in as orthogonal files

            station_dim_id = FindAliasDimID(stationVarAliases);
            lon_dim_id = FindAliasDimID(lonVarAliases);
            lat_dim_id = FindAliasDimID(latVarAliases);


            if (station_dim_id != -1)
            {
                ReadOMAFormat();
            }

            else if (lon_dim_id != -1 && lat_dim_id != -1)
            {
                ReadCVSFormat();
            }

            else
            {
                throw new Exception("Invalid nc file fromat!");
            }




        }

        public void ReadOMAFormat()
        {
            nstation = reader.MainGroup.Dimensions[station_dim_id].Count;

            NcRange range = new NcRange(0, nstation - 1);
            double[] lons = reader.MainGroup.Variabels[lonID].GetData<double>(new NcRange[] { range });
            double[] lats = reader.MainGroup.Variabels[latID].GetData<double>(new NcRange[] { range });


            oma_parser = new OthogonalReducedParser(lons, lats);
            Resolution = oma_parser.Resolution;
            MinLon = oma_parser.MinLon;
            MaxLon = oma_parser.MaxLon;
            MinLat = oma_parser.MinLat;
            MaxLat = oma_parser.MaxLat;

            IsOrthogonal = true;
        }

        public void ReadCVSFormat()
        {
            nlon = reader.MainGroup.Dimensions[lon_dim_id].Count;
            nlat = reader.MainGroup.Dimensions[lat_dim_id].Count;

            NcRange rangelon = new NcRange(0, nlon - 1);
            NcRange rangelat = new NcRange(0, nlat - 1);

            double[] lons = reader.MainGroup.Variabels[lonID].GetData<double>(new NcRange[] { rangelon });
            double[] lats = reader.MainGroup.Variabels[latID].GetData<double>(new NcRange[] { rangelat });


            cvs_parser = new CVSParser(lons, lats);
            Resolution = cvs_parser.Resolution;
            MinLon = cvs_parser.MinLon;
            MaxLon = cvs_parser.MaxLon;
            MinLat = cvs_parser.MinLat;
            MaxLat = cvs_parser.MaxLat;

            IsOrthogonal = false;
        }


        public int[] FindIndex(double lon, double lat)
        {
            if (lon < MinLon || lon > MaxLon)
            {
                throw new Exception("Longitude coordinate out of Range!");
            }

            if (lat < MinLat || lat > MaxLat)
            {
                throw new Exception("Latitude coordinate out of Range!");
            }



            if (IsOrthogonal)
            {
                return new[] { oma_parser.FindCoordIndex(lon, lat) };
            }

            else
            {
                return new[] { cvs_parser.FindLonIndex(lon), cvs_parser.FindLatIndex(lat)};
            }

            
        }

        public byte[,] FindCoords()
        {
            if (IsOrthogonal)
            {
                OrthogonalReader oreader = new OrthogonalReader(this.reader, oma_parser);
                oma_transposer = new OrthogonalBitmapTransposer(oreader, station_dim_id);
                return oma_transposer.GetPixels();
            }

            else
            {
                CVSReader cvsreader = new CVSReader(this.reader, cvs_parser);
                cvs_transposer = new CVSBitmapTransposer(cvsreader, lonID, latID);
                return cvs_transposer.GetPixels();
            }



            
        }

        public double FindSelectedLon(int lon_index)
        {
            if (IsOrthogonal)
            {
                return oma_transposer.GetLon(lon_index);
            }

            else
            {
                return cvs_transposer.GetLon(lon_index);
            }
        }

        public double FindSelectedLat(int lat_index)
        {
            if (IsOrthogonal)
            {
                return oma_transposer.GetLat(lat_index);
            }

            else
            {
                return cvs_transposer.GetLat(lat_index);
            }
        }


        public int FindSelectedLonIndex(double lon)
        {
            if (IsOrthogonal)
            {
                return oma_transposer.GetLonIndex(lon);
            }

            else
            {
                return cvs_transposer.GetLonIndex(lon);
            }
        }


        public int FindSelectedLatIndex(double lat)
        {
            if (IsOrthogonal)
            {
                return oma_transposer.GetLatIndex(lat);
            }

            else
            {
                return cvs_transposer.GetLatIndex(lat);
            }
        }


        private int FindAliasVarID(List<string> aliases)
        {
            bool foundLon = false;
            int varid = 0;

            int index = -1;

            while (!foundLon && varid < allVarNames.Count)
            {

                string lowername = allVarNames[varid].ToLower();

                foreach (var lonaliase in aliases)
                {
                    if (lowername == lonaliase)
                    {
                        foundLon = true;
                        index = varid;
                        break;
                    }
                }

                varid++;
            }

            return index;
        }


        private int FindAliasDimID(List<string> aliases)
        {
            bool foundLon = false;
            int varid = 0;

            int index = -1;

            while (!foundLon && varid < allDimNames.Count)
            {

                string lowername = allDimNames[varid].ToLower();

                foreach (var lonaliase in aliases)
                {
                    if (lowername == lonaliase)
                    {
                        foundLon = true;
                        index = varid;
                        break;
                    }
                }

                varid++;
            }

            return index;
        }

    }
}
