using System;
using System.Collections.Generic;
using NcInterop.Types;

namespace NcInterop.Base
{
    public class NcAttribute
    {
        private int varID;
        private string name;
        private object value;
        private Type type;

        public int VarID
        {
            get { return varID; }
            set { varID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public object Value
        {
            get { return value; }
        }

        public NcAttribute(string name, int varID, object value)
        {
            this.name = name;
            this.varID = varID;
            this.value = value;
        }
    }

    public class NcAttributeCollection : List<NcAttribute>
    {
        public NcAttribute this[string name]
        {
            get
            {
                foreach (NcAttribute attr in this)
                {
                    if (attr.Name == name)
                    {
                        return attr;
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
                foreach (NcAttribute attr in this)
                {
                    names.Add(attr.Name);
                }
                return names;
            }
        }
    }
}
