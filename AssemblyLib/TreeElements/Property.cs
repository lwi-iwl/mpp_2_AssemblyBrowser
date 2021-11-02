using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssemblyLib.TreeElements
{
    public class Property
    {
        public string Name { get; private set; }

        public string Modifier { get; private set; }

        public string AccessModifier { get; private set; }

        public string DataType { get; private set; }

        public string Accessors { get; private set; }

        public string FullName { get; private set; }


        public Property(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            SetModifier(propertyInfo);
            SetAccessModifier(propertyInfo);
            SetAccessors(propertyInfo);
            DataType = "";
            SetDataType(propertyInfo.PropertyType);
            FullName = AccessModifier + " " + Modifier + " " + DataType + " " + Name + Accessors;
        }

        private void SetModifier(PropertyInfo propertyInfo)
        {
            Modifier = "";
            if (propertyInfo.GetAccessors().Length != 0)
            {
                if (propertyInfo.GetAccessors()[0].IsFinal)
                    Modifier = Modifier + "final";
                if (propertyInfo.GetAccessors()[0].IsAbstract)
                    Modifier = Modifier + "abstract";
                if (propertyInfo.GetAccessors()[0].IsStatic)
                    Modifier = Modifier + "static";
                if (propertyInfo.GetAccessors()[0].IsVirtual)
                    Modifier = Modifier + " virtual";
            }
        }

        private void SetAccessModifier(PropertyInfo propertyInfo)
        {
            List<MethodInfo> accessors = propertyInfo.GetAccessors().ToList();
            bool flag = false;
            foreach (MethodInfo accessor in accessors)
            {
                if (accessor.IsPublic)
                {
                    AccessModifier = "public";
                    flag = true;
                }
                    
            }
            if (!flag)
                foreach (MethodInfo accessor in accessors)
                {
                    if (accessor.IsFamilyAndAssembly)
                    {
                        AccessModifier = "private protected";
                        flag = true;
                    }
                }
            if (!flag)
                foreach (MethodInfo accessor in accessors)
                {
                    if (accessor.IsFamily)
                    {
                        AccessModifier = "protected";
                        flag = true;
                    }
                }
            if (!flag)
                foreach (MethodInfo accessor in accessors)
                {
                    if (accessor.IsAssembly)
                    {
                        AccessModifier = "internal";
                        flag = true;
                    }
                }
            if (!flag)
                foreach (MethodInfo accessor in accessors)
                {
                    if (accessor.IsPrivate)
                    {
                        AccessModifier = "private";
                    }
                }
        }

        private void SetDataType(Type temptype)
        {
            if (temptype.IsGenericType)
            {
                DataType = DataType + temptype.Name.Remove(temptype.Name.Length-2, 2).Trim(new char[] {'`'}) + " <";
                List<Type> arguments = temptype.GetGenericArguments().ToList();
                foreach (Type argument in arguments)
                {
                    SetDataType(argument);
                    if (arguments.Last().GetHashCode() != argument.GetHashCode())
                        DataType = DataType + ", ";
                }
                DataType = DataType + ">";
            }
            else
            {
                DataType = DataType + temptype.Name;
            }
        }

        private void SetAccessors(PropertyInfo propertyInfo)
        {
            Accessors = "{";
            MethodInfo accessor = propertyInfo.GetMethod;
            if (accessor != null)
            {
                SetMethodAccessModifier(accessor);
                Accessors = Accessors + " " + accessor.Name;
            }
            
            accessor = propertyInfo.SetMethod;
            if (accessor != null)
            {
                Accessors = Accessors + ", ";
                SetMethodAccessModifier(accessor);
                Accessors = Accessors + " " + accessor.Name;
            }

            Accessors = Accessors + "}";
        }
        
        private void SetMethodAccessModifier(MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic)
            {
                if (AccessModifier != "public")
                    Accessors = Accessors + "public";
            }
            else if (methodInfo.IsPrivate)
            {
                if (AccessModifier != "private")
                    Accessors = Accessors + "private";
            }
            else if (methodInfo.IsFamilyAndAssembly)
            {
                if (AccessModifier!="private protected")
                    Accessors = Accessors + "private protected";
            }
            else if (methodInfo.IsFamily)
            {
                if (AccessModifier != "protected")
                    Accessors = Accessors + "protected";
            }
            else if (methodInfo.IsAssembly)
            {
                if (AccessModifier!="internal")
                    Accessors = Accessors + "internal";
            }
            else if (AccessModifier!="protected internal")
                Accessors = Accessors + "protected internal";
        }
    }
}