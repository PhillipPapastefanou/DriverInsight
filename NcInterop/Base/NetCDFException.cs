using System;

namespace NcInterop.Base
{
    public class NetCDFException : Exception
    {
        private int resultCode;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultCode"></param>
        public NetCDFException(int resultCode) :
            base(NetCDF.nc_strerror(resultCode))
        {
            this.resultCode = resultCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public NetCDFException(string message) : base(message)
        {
            this.resultCode = -1;
        }
        /// <summary>
        /// Gets the unmanaged NetCDF result code, describing the fault.
        /// </summary>
        public int ResultCode
        {
            get { return resultCode; }
        }
    }
}
