using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssemblyLib.TreeElements
{
    public class NameSpace
    {
        public string Name { get; private set; }
        public NameSpace(Type type)
        {
            Name = type.Namespace;
            AnotherTypes = new List<AnotherType>();
        }
        

        public List<AnotherType> AnotherTypes { get; }
    }
}
