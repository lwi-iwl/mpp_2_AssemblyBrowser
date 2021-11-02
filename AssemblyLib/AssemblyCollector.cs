using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
            
            List<MethodInfo> methods = (from type in asm.GetTypes()
                where type.IsSealed && !type.IsGenericType && !type.IsNested
                from method in type.GetMethods(BindingFlags.Static
                                               | BindingFlags.Public | BindingFlags.NonPublic)
                where method.IsDefined(typeof(ExtensionAttribute), false)
                select method).ToList();
            foreach (MethodInfo extensionmethod in methods)
            {
                NameSpace nameSpace =
                    NameSpaces.Find(x => x.Name == extensionmethod.GetParameters()[0].ParameterType.Namespace);
                if (nameSpace != null)
                {
                    AnotherType anotherType = nameSpace.AnotherTypes.Find(x =>
                        x.HashCode == extensionmethod.GetParameters()[0].ParameterType.GetHashCode());
                    if (anotherType != null)
                    {
                        anotherType.Methods.Add(new Method(extensionmethod));
                        anotherType.Methods.Last().FullName = "Extension method" + anotherType.Methods.Last().FullName;
                    }
                }
                else
                {
                    NameSpace tempNameSpace = new NameSpace(extensionmethod.GetParameters()[0].ParameterType);
                    AnotherType extensionType = new AnotherType(extensionmethod.GetParameters()[0].ParameterType);
                    extensionType.Constructors.Clear();
                    extensionType.Properties.Clear();
                    extensionType.Fields.Clear();
                    extensionType.Methods.Clear();
                    extensionType.Methods.Add(new Method(extensionmethod));
                    extensionType.Methods.Last().FullName = "Extension method " + extensionType.Methods.Last().FullName;
                    NameSpaces.Add(tempNameSpace);
                    tempNameSpace.AnotherTypes.Add(extensionType);
                }

                foreach (NameSpace tempNameSpace in NameSpaces)
                {
                    AnotherType tempType =
                        tempNameSpace.AnotherTypes.Find(x => x.HashCode == extensionmethod.DeclaringType.GetHashCode());
                    if (tempType != null)
                    {
                        tempType.Methods.RemoveAll(x => x.HashCode == extensionmethod.GetHashCode());
                    }
                        
                }
            }
            
            return NameSpaces;
        }
    }
}
