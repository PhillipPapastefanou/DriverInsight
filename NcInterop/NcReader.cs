using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NcInterop.Base;
using NcInterop.Types;

namespace NcInterop
{
    public class NcReader : NcBase
    {
        private string filename;

        private int ncid;

        private readonly int ngroups;

        private NcGroup mainGroup;
        private NcGroupCollection groups;

        private int defaultCacheSize = 32 * 1024 * 1024; // 32 Mb
        private int defaultCacheNElems = 1009;
        private float defaultCachePreemption = 0.75f;

        #region Properties
        public NcGroup MainGroup
        { get { return mainGroup; } }
        public NcGroupCollection Groups
        { get { return groups; } }

        #endregion

        public NcReader(string filename)
        {
            this.filename = filename;
            //res = NetCDF.nc_open_chunked(filename, CreateMode.NC_NOWRITE, out ncid, new IntPtr(defaultCacheSize), new IntPtr(defaultCacheNElems),
            res = NetCDF.nc_open(filename, CreateMode.NC_NOWRITE, out ncid);
            HandleResult(res);


            //********************************************************
            // Loading Groups
            //********************************************************
            res = NetCDF.nc_inq_grps(ncid, out ngroups, null);
            HandleResult(res);

            //Main group
            this.mainGroup = new NcGroup(ncid, "main");
           
            //Check if we have netcdf-4 groups
            if (ngroups > 0)
            {
                groups = new NcGroupCollection();

                //Repeaat the nc_inq_grps processes to get the group NC-files.
                IntPtr[] groupNcIdsT = new IntPtr[ngroups];
                res = NetCDF.nc_inq_grps(ncid, out ngroups, groupNcIdsT);
                HandleResult(res);
                
                //Adding groups
                for (int i = 0; i < ngroups; i++)
                {
                    //Get name
                    string name;
                    int id = (int) groupNcIdsT[i];
                    res = NetCDF.nc_inq_grpname(id, out name);

                    NcGroup grp = new NcGroup(id, name, this.mainGroup.Dimensions);
                    groups.Add(grp);
                }

            }

        }

        ~NcReader()
        {
            NetCDF.nc_close(ncid);
        }
    }
}
