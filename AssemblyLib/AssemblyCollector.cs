using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AssemblyLib.TreeElements;

namespace AssemblyLib
{
    public class AssemblyCollector
    {
        public AssemblyCollector()
        {
            NameSpaces = new List<NameSpace>();
        }

        public List<NameSpace> NameSpaces { get; }

        public List<NameSpace> getTree(string path)
        {
            Assembly asm = Assembly.LoadFrom(path);
            Type[] types = asm.GetTypes();
            foreach (Type type in types)
            {
                NameSpace nameSpace = NameSpaces.Find(x => x.Name == type.Namespace);
                if (nameSpace == null)
                {
                    NameSpace tempSpace = new NameSpace(type);
                    tempSpace.AnotherTypes.Add(new AnotherType(type));
                    NameSpaces.Add(tempSpace);
                }
                else
                {
                    nameSpace.AnotherTypes.Add(new AnotherType(type));
                }
        }
            return NameSpaces;
        }
    }
}
