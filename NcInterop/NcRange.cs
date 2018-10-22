using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NcInterop.Base;

namespace NcInterop
{
    public class NcRange
    {
        private IntPtr beginPtr;
        private IntPtr countPtr;

        #region Properties
        public IntPtr BeginPtr { get { return beginPtr; } }
        public IntPtr CountPtr { get { return countPtr; } }
        #endregion

        /// <summary>
        /// Standard Range, from Index begin until end (inclusive!)
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public NcRange(int begin, int end)
        {
            if (begin > end)
            {
                throw new NetCDFException("Begin must be less equal to end.");
            }

            if (begin < 0 || end < 0)
            {
                throw new NetCDFException("Begin and end must be positive.");
            }

            beginPtr = (IntPtr) begin;
            countPtr = (IntPtr) (end - begin + 1); //+ 1 -> end Inclusive
        }
        /// <summary>
        /// Standard Range for accessing nc-data. Single Point 
        /// </summary>
        /// <param name="point"></param>
        public NcRange(int point) : this(point, point)
        {
            
        }
    }
}
