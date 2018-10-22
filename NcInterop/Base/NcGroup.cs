using System;
using System.Collections.Generic;
using System.Linq;
using NcInterop.Types;

namespace NcInterop.Base
{
    public class NcGroup: NcBase
    {
        #region Fields
        private int ncgrpid;
        private string name;

        private readonly int ndims;
        private readonly int nvars;
        private readonly int ngatts;
        private readonly int unlimdimid;

        private NcAttributeCollection gatts;
        private NcDimensionCollection dimensions;
        private NcVariableCollection vars;
        #endregion

        #region Properties
        public string Name
        { get { return name; } }
        public NcVariableCollection Variabels
        { get { return vars; } }
        public NcDimensionCollection Dimensions
        { get { return dimensions; } }
        public NcAttributeCollection GlobalAtts
        { get { return gatts; } }
        #endregion
        public NcGroup(int ncgrpid, string name)
        {
            this.ncgrpid = ncgrpid;
            this.name = name;
            //********************************************************
            //Loading schema
            res = NetCDF.nc_inq(ncgrpid, out ndims, out nvars, out ngatts, out unlimdimid);
            HandleResult(res);

            //Getting global attributes
            this.gatts = GetAttributes(-1);

            //Getting dimensions
            this.dimensions = GetDimensions();

            //Getting variables for this group
            this.vars = GetVariables();
        }
        public NcGroup(int ncgrpid, string name, NcDimensionCollection dimensions):this(ncgrpid, name)
        {
            this.dimensions = dimensions;
        }
        private NcAttributeCollection GetAttributes(int varId)
        {
            var attList = new NcAttributeCollection();

            int natts;
            NetCDF.nc_inq_varnatts(ncgrpid, varId, out natts);
            int nattrs;
            res = NetCDF.nc_inq_varnatts(ncgrpid, varId, out nattrs);
            HandleResult(res);

            for (int i = 0; i < nattrs; i++)
            {
                // Name
                string aname;
                res = NetCDF.nc_inq_attname(ncgrpid, varId, i, out aname);
                HandleResult(res);

                // Value and Type
                object value = ReadNetCdfAttribute(varId, aname);

                //Add the attribute
                attList.Add(new NcAttribute(aname, varId, value));
            }
            return attList;
        }
        private NcDimensionCollection GetDimensions()
        {
            var dimList = new NcDimensionCollection();
            for (int i = 0; i < ndims; i++)
            {
                string dimName;
                //get name
                res = NetCDF.nc_inq_dimname(ncgrpid, i, out dimName);
                HandleResult(res);
                IntPtr len;
                //get length
                res = NetCDF.nc_inq_dimlen(ncgrpid, i, out len);
                HandleResult(res);
                dimList.Add(new NcDimension(dimName, i, (int) len));
            }
            return dimList;
        }
        private NcVariableCollection GetVariables()
        {
            NcVariableCollection varList = new NcVariableCollection();
            for (int id = 0; id < nvars; id++)
            {
                int nDims;
                // Get the number of dimensions first to set the array size.
                res = NetCDF.nc_inq_varndims(ncgrpid, id, out nDims);
                // Now get the rest of the infos;
                int[] dimIDs = new int[nDims];
                string name;
                NcType type;
                int nAtts;
                res = NetCDF.nc_inq_var(ncgrpid, id, out name, out type, out nDims, dimIDs, out nAtts);
                HandleResult(res);

                //Get size of each var entry
                int sizeInBytes = GetSizeInBytes(type);

                //Get Attributes
                NcAttributeCollection attrs = GetAttributes(id);

                //Get Chunks
                IntPtr[] chunkPtrs = new IntPtr[nDims];
                IntPtr storage;
                res = NetCDF.nc_inq_var_chunking(ncgrpid, id, out storage, chunkPtrs);
                HandleResult(res);

                int[] chunks = new int[nDims];
                for (int i = 0; i < nDims; i++)
                {
                    chunks[i] = (int)chunkPtrs[i];
                }

                NcVariable var = new NcVariable()
                {
                    Name = name,
                    ID = id,
                    NcGrpID = ncgrpid,
                    DimIDs = dimIDs,
                    Attributes = attrs,
                    ChunckSizes = chunks,
                    Type = type,
                    SizeInBytes = sizeInBytes,
                    Storage = (NcStorage)(int)storage
                };

                varList.Add(var);
            }
            return varList;
        }

        internal object ReadNetCdfAttribute(int varid, string aname)
        {
            NcType type;
            IntPtr len1;
            int res = NetCDF.nc_inq_att(ncgrpid, varid, aname, out type, out len1);
            int len = (int)len1;
            HandleResult(res);
            object value;
            switch (type)
            {
                case NcType.NC_UBYTE:
                    if (aname == null)
                    {
                        value = null;
                    }
                    else
                    {
                        byte[] bvalue = new byte[len];
                        res = NetCDF.nc_get_att_uchar(ncgrpid, varid, aname, bvalue);
                        if (len == 1)
                        {
                            if (bvalue.GetType() == typeof(bool))
                                value = bvalue[0] > 0; // bool
                            else
                                value = bvalue[0]; // byte
                        }
                        else
                        {
                            if (bvalue.GetType() == typeof(bool[]))
                                value = Array.ConvertAll(bvalue, p => p > 0); // bool[]
                            else
                                value = bvalue; // byte[]
                        }
                        HandleResult(res);
                    }
                    break;
                case NcType.NC_BYTE:
                    sbyte[] sbvalue = new sbyte[len];
                    res = NetCDF.nc_get_att_schar(ncgrpid, varid, aname, sbvalue);
                    if (len == 1)
                        value = sbvalue[0]; // byte
                    else
                        value = sbvalue;
                    HandleResult(res);
                    break;
                case NcType.NC_CHAR:
                case NcType.NC_STRING:
                    int length = len;

                   if (aname == null)
                    {
                        value = null;
                    }
                    else if (aname.GetType() == typeof(string[]))
                    {
                        // Type is string[]
                        if (length == 0)
                        {
                            value = new string[0];
                        }
                        else
                        {
                            string[] strings = new string[length];
                            res = NetCDF.nc_get_att_string(ncgrpid, varid, aname, strings);
                            HandleResult(res);
                            value = strings;
                        }
                    }
                    else // not string[]
                    {
                        string strvalue = NcGetAttText(ncgrpid, varid, aname, len, out res);
                        HandleResult(res);
                        if (strvalue.Contains('\0'))
                            value = strvalue.Replace("\0", string.Empty);
                        else
                            value = strvalue;
                    }
                    break;
                case NcType.NC_SHORT:
                    short[] svalue = new short[len];
                    res = NetCDF.nc_get_att_short(ncgrpid, varid, aname, svalue);
                    if (len == 1)
                        value = svalue[0];
                    else
                        value = svalue;
                    HandleResult(res);
                    break;
                case NcType.NC_USHORT:
                    ushort[] usvalue = new ushort[len];
                    res = NetCDF.nc_get_att_ushort(ncgrpid, varid, aname, usvalue);
                    if (len == 1)
                        value = usvalue[0];
                    else
                        value = usvalue;
                    HandleResult(res);
                    break;
                case NcType.NC_INT:
                    int[] ivalue = new int[len];
                    res = NetCDF.nc_get_att_int(ncgrpid, varid, aname, ivalue);
                    if (len == 1)
                        value = ivalue[0];
                    else
                        value = ivalue;
                    HandleResult(res);
                    break;
                case NcType.NC_UINT:
                    uint[] uivalue = new uint[len];
                    res = NetCDF.nc_get_att_uint(ncgrpid, varid, aname, uivalue);
                    if (len == 1)
                        value = uivalue[0];
                    else
                        value = uivalue;
                    HandleResult(res);
                    break;
                case NcType.NC_INT64:
                    Int64[] livalue = new Int64[len];
                    res = NetCDF.nc_get_att_longlong(ncgrpid, varid, aname, livalue);
                    if (len == 1)
                        value = livalue[0];
                    else
                        value = livalue;
                    HandleResult(res);
                    break;
                case NcType.NC_UINT64:
                    UInt64[] ulivalue = new UInt64[len];
                    res = NetCDF.nc_get_att_ulonglong(ncgrpid, varid, aname, ulivalue);
                    if (len == 1)
                        value = ulivalue[0];
                    else
                        value = ulivalue;
                    HandleResult(res);
                    break;
                case NcType.NC_FLOAT:
                    float[] fvalue = new float[len];
                    res = NetCDF.nc_get_att_float(ncgrpid, varid, aname, fvalue);
                    if (len == 1) value = fvalue[0];
                    else value = fvalue;
                    HandleResult(res);
                    break;
                case NcType.NC_DOUBLE:
                    double[] dvalue = new double[len];
                    res = NetCDF.nc_get_att_double(ncgrpid, varid, aname, dvalue);
                    if (len == 1) value = dvalue[0];
                    else value = dvalue;
                    HandleResult(res);
                    break;
                default:
                    throw new NotSupportedException("Invalid attribute type value");
            }
            return value;
        }
        internal static string NcGetAttText(int ncid, int varid, string aname, int len, out int res)
        {
            string strvalue = null;
            res = NetCDF.nc_get_att_text(ncid, varid, aname, out strvalue, len);
            HandleResult(res);
            return strvalue;
        }
        internal static int NcPutAttText(int ncid, int varid, string aname, string value)
        {
            int res = NetCDF.nc_put_att_text(ncid, varid, aname, value);
            HandleResult(res);
            return res;
        }
        internal int GetSizeInBytes(NcType type)
        {
            switch (type)
            {
                case NcType.NC_BYTE:
                    return 1;
                case NcType.NC_UBYTE:
                    return 1;
                case NcType.NC_CHAR:
                    return 2;
                case NcType.NC_SHORT:
                    return 2;
                case NcType.NC_USHORT:
                    return 2;
                case NcType.NC_INT:
                    return 4;
                case NcType.NC_UINT:
                    return 4;
                case NcType.NC_FLOAT:
                    return 4;
                case NcType.NC_DOUBLE:
                    return 8;
                case NcType.NC_INT64:
                    return 8;
                case NcType.NC_UINT64:
                    return 8;
                case NcType.NC_STRING:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public class NcGroupCollection: List<NcGroup>
    {
        public NcGroup this[string name]
        {
            get
            {
                foreach (NcGroup group in this)
                {
                    if (group.Name == name)
                    {
                        return group;
                    }
                }
                return null;
            }
        }

        public List<string> AllNames
        {
            get
            {
                List<string> names = new List<string>();
                foreach (NcGroup group in this)
                {
                    names.Add(group.Name);
                }
                return names;
            }
        }

    }
}
