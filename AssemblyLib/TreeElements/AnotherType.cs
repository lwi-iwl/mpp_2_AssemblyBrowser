using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssemblyLib.TreeElements
{
    public class AnotherType
    {
        public List<Field> Fields { get; }
        public List<Method> Methods { get; }

        public List<Property> Properties { get; }
        
        public List<Constructor> Constructors { get; }

        public string Name { get; private set; }

        public string DataType { get; private set; }

        public string Modifier { get; private set; }

        public string AccessModifier { get; private set; }
        
        public string FullName { get; private set; }

        public int HashCode { get; private set; }

        public AnotherType(Type type)
        {
            HashCode = type.GetHashCode();
            Name = type.Name;
            Fields = new List<Field>();
            Methods = new List<Method>();
            Properties = new List<Property>();
            Constructors = new List<Constructor>();
            SetTypeName(type);
            SetModifier(type);
            SetAccessModifier(type);
            
            List<FieldInfo> fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static 
                                                    | BindingFlags.Public).Where
                                                    (x=>!x.Name.Contains("Backing")).ToList();
            List<MethodInfo> methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public).Where(m => !m.IsSpecialName).ToList();
            List<PropertyInfo> properties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | 
                                                               BindingFlags.Static | BindingFlags.Public).ToList();
            List<ConstructorInfo> constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic |
                                                                      BindingFlags.Static
                                                                      | BindingFlags.Public).ToList();
            foreach (FieldInfo field in fields)
            {
                Fields.Add(new Field(field));
            }
            foreach (ConstructorInfo constructor in constructors)
            {   
                Constructors.Add(new Constructor(constructor));
            }

            foreach (MethodInfo method in methods)
            {
                Methods.Add(new Method(method));
            }
            foreach (PropertyInfo property in properties)
            {
                Properties.Add(new Property(property));
            }
            FullName = AccessModifier + Modifier + " " + DataType + " " + Name;
        }

        private void SetTypeName(Type type)
        {
            if (type.IsClass && type.BaseType.Name == "multicastdelegate")
                DataType = "delegate";
            else if (type.IsClass)
                DataType = "class";
            else if (type.IsInterface)
                DataType = "interface";
            else if (type.IsEnum)
                DataType = "enum";
            else if (type.IsValueType && !type.IsPrimitive)
                DataType = "struct";
        }

        private void SetModifier(Type type)
        {
            Modifier = "";
            if (type.IsAbstract && type.IsSealed)
                Modifier = Modifier + " static";
            if (type.IsAbstract)
                Modifier = Modifier + " abstract";
        }

        private void SetAccessModifier(Type type)
        {
            if (type.IsPublic)
                AccessModifier = "public";
            else if (type.IsNotPublic)
                AccessModifier = "internal";
        }
        
        
    }
}
