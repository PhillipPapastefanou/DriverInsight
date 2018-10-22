using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcInterop
{
    public class NcDimension
    {
        public string Name { get; set; }
        public int NcIndex { get; set; }
        public int Count { get; set; }

        public NcDimension(string name, int ncindex, int count)
        {
            this.Name = name;
            this.NcIndex = ncindex;
            this.Count = count;
        }
    }

    public class NcDimensionCollection : List<NcDimension>
    {
        public NcDimension this[string name]
        {
            get
            {
                foreach (NcDimension dim in this)
                {
                    if (dim.Name == name)
                    {
                        return dim;
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
                foreach (NcDimension attr in this)
                {
                    names.Add(attr.Name);
                }
                return names;
            }
        }
    }
}
