using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyLib.TreeElements
{
    public class AnotherType
    {
        public List<Field> Fields { get; }
        public List<Method> Methods { get; }

        public List<Property> Properties { get; }

        public string Name { get; private set; }

        public string DataType { get; private set; }

        public string Modifier { get; private set; }

        public string AccessModifier { get; private set; }

        public AnotherType(Type type)
        {
            Name = type.Name;
            Fields = new List<Field>();
            Methods = new List<Method>();
            Properties = new List<Property>();
            SetTypeName(type);
            SetModifier(type);
            SetAccessModifier(type);
            List<FieldInfo> fields = type.GetFields().ToList();
            List<MethodInfo> methods = type.GetMethods().ToList();
            List<PropertyInfo> properties = type.GetProperties().ToList();
            
        }

        private void SetTypeName(Type type)
        {
            if (type.IsClass && type.BaseType.Name == "MulticastDelegate")
                DataType = "Delegate";
            else if (type.IsClass)
                DataType = "Class";
            else if (type.IsInterface)
                DataType = "Interface";
            else if (type.IsEnum)
                DataType = "Enum";
            else if (type.IsValueType && !type.IsPrimitive)
                DataType = "Struct";
        }

        private void SetModifier(Type type)
        {
            if (type.IsAbstract && type.IsSealed)
                Modifier = "Static";
            else if (type.IsAbstract)
                Modifier = "Abstract";
            else
            {
                Modifier = "";
            }
        }

        private void SetAccessModifier(Type type)
        {
            if (type.IsPublic)
                AccessModifier = "Public";
            else if (type.IsNotPublic)
                AccessModifier = "Internal";
        }
    }
}
