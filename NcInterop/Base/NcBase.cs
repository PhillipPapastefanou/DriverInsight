using NcInterop.Types;

namespace NcInterop.Base
{
    public class NcBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected int res;

        /// <summary>
        /// Handles the result code for NetCDF operations.
        /// In case of an error throws the NetCDFException exception.
        /// </summary>
        /// <exception cref="NetCDFException" />
        internal static void HandleResult(int resultCode)
        {
            if (resultCode == (int)ResultCode.NC_NOERR)
                return;
            throw new NetCDFException(resultCode);
        }
    }
}
