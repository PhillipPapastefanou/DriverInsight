using System;
using System.Collections.Generic;
using NcInterop.Types;

namespace NcInterop.Base
{
    public class NcVariable : NcBase
    {
        public string Name { get; set; }
        public NcAttributeCollection Attributes { get; set; }
        public int SizeInBytes { get; set; }
        public NcType Type { get; set; } 
        public NcStorage Storage { get; set; }
        public int NcGrpID { get; set; }
        public int ID { get; set; }
        public int[] DimIDs { get; set; }
        public int NDims
        {
            get { return DimIDs.Length; }
        }
        public int[] ChunckSizes { get; set; }


        public NcVariable()
        {
            
        }

        public T[] GetSingleData<T>(NcRange range)
        {
            if (NDims > 1)
            {
                throw new NetCDFException("This is not a single data variable, as this does contain more then one dimension.");
            }

            IntPtr[] start = new IntPtr[1];
            IntPtr[] count = new IntPtr[1];

            start[0] = range.BeginPtr;
            count[0] = range.CountPtr;

            T[] data = new T[(int)range.CountPtr];
            switch (Type)
            {
                case NcType.NC_BYTE:
                    break;
                case NcType.NC_UBYTE:
                    break;
                case NcType.NC_CHAR:
                    break;
                case NcType.NC_SHORT:
                    break;
                case NcType.NC_USHORT:
                    break;
                case NcType.NC_INT:
                    break;
                case NcType.NC_UINT:
                    break;
                case NcType.NC_FLOAT:
                    float[] dataF = data as float[];
                    res = NetCDF.nc_get_vara_float(this.NcGrpID, this.ID, start, count, dataF);
                    data = dataF as T[];
                    break;
                case NcType.NC_DOUBLE:
                    double[] dataD = data as double[];
                    res = NetCDF.nc_get_vara_double(this.NcGrpID, this.ID, start, count, dataD);
                    data = dataD as T[];
                    break;
                case NcType.NC_INT64:
                    break;
                case NcType.NC_UINT64:
                    break;
                case NcType.NC_STRING:
                    break;
                default:
                    throw new NetCDFException("Invalid Type");
            }
            HandleResult(res);
            return data;
        }
        public T[] GetData<T>(NcRange[] ranges)
        {
            if (ranges.Length != NDims)
            {
                throw new NetCDFException("Variables dimension count does not match the supplied ranges count");
            }

            IntPtr[] start = new IntPtr[NDims];
            IntPtr[] count = new IntPtr[NDims];

            int arrLength = 1;

            for (int i = 0; i < NDims; i++)
            {
                start[i] = ranges[i].BeginPtr;
                count[i] = ranges[i].CountPtr;
                arrLength *= (int) count[i];
            }

            T[] data = new T[arrLength];
            switch (Type)
            {
                case NcType.NC_BYTE:
                    break;
                case NcType.NC_UBYTE:
                    break;
                case NcType.NC_CHAR:
                    break;
                case NcType.NC_SHORT:
                    break;
                case NcType.NC_USHORT:
                    break;
                case NcType.NC_INT:
                    break;
                case NcType.NC_UINT:
                    break;
                case NcType.NC_FLOAT:
                    float[] dataF = new float[arrLength];
                    res = NetCDF.nc_get_vara_float(this.NcGrpID, this.ID, start, count, dataF);

                    if (typeof(T) == typeof(float))
                    { 
                        data = dataF as T[];
                    }

                    if (typeof(T) == typeof(double))
                    {
                        double[] dataDf = new double[arrLength];
                        for (int i = 0; i < dataF.Length; i++)
                        {
                            dataDf[i] = dataF[i];
                        }
                        data = dataDf as T[];
                    }
                    break;
                case NcType.NC_DOUBLE:
                    double[] dataD = data as double[];
                    res = NetCDF.nc_get_vara_double(this.NcGrpID, this.ID, start, count, dataD);
                    data = dataD as T[];
                    break;
                case NcType.NC_INT64:
                    break;
                case NcType.NC_UINT64:
                    break;
                case NcType.NC_STRING:
                    break;
                default:
                    throw new NetCDFException("Invalid Type");
            }
            HandleResult(res);

            return data;
        }
        public float[] GetData(NcRange[] ranges)
        {
            if (ranges.Length != NDims)
            {
                throw new NetCDFException("Variables dimension count does not match the supplied ranges count");
            }

            IntPtr[] start = new IntPtr[NDims];
            IntPtr[] count = new IntPtr[NDims];

            int arrLength = 1;

            for (int i = 0; i < NDims; i++)
            {
                start[i] = ranges[DimIDs[i]].BeginPtr;
                count[i] = ranges[DimIDs[i]].CountPtr;
                arrLength *= (int)count[i];
            }
            float[] dataF = new float[arrLength];

            switch (Type)
            {
                case NcType.NC_BYTE:
                    break;
                case NcType.NC_UBYTE:
                    break;
                case NcType.NC_CHAR:
                    break;
                case NcType.NC_SHORT:
                    break;
                case NcType.NC_USHORT:
                    break;
                case NcType.NC_INT:
                    break;
                case NcType.NC_UINT:
                    break;
                case NcType.NC_FLOAT:
                    res = NetCDF.nc_get_vara_float(this.NcGrpID, this.ID, start, count, dataF);
                    break;
                case NcType.NC_DOUBLE:
                    double[] dataD = new double[arrLength];
                    res = NetCDF.nc_get_vara_double(this.NcGrpID, this.ID, start, count, dataD);
                    ConvertToFloat<double>(dataD, dataF);
                    break;
                case NcType.NC_INT64:
                    break;
                case NcType.NC_UINT64:
                    break;
                case NcType.NC_STRING:
                    break;
                default:
                    throw new NetCDFException("Invalid Type");
            }
            HandleResult(res);
            return dataF;
        }
        private void ConvertToFloat<T>(T[] data, float[] dataF)
        {
            for (int i = 0; i < data.Length; i++)
            {
                dataF[i] = Convert.ToSingle(data[i]);
            }
        }

    }



    public class NcVariableCollection : List<NcVariable>
    {
        public NcVariable this[string name]
        {
            get
            {
                foreach (NcVariable var in this)
                {
                    if (var.Name == name)
                    {
                        return var;
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
                foreach (NcVariable var in this)
                {
                    names.Add(var.Name);
                }
                return names;
            }
        }
    }
}
