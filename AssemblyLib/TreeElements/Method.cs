using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssemblyLib.TreeElements
{
    public class Method
    {
        public string Name { get; private set; }

        public string Modifier { get; private set; }

        public string AccessModifier { get; private set; }

        public string MethodParameters { get; private set; }

        public string DataType { get; private set; }
        
        public string FullName { get; private set; }

        public int HashCode { get; private set; }


        public Method(MethodInfo methodInfo)
        {
            HashCode = methodInfo.GetHashCode();
            Name = methodInfo.Name;
            List<ParameterInfo> parameters = methodInfo.GetParameters().ToList();
            MethodParameters = "(";
            foreach (ParameterInfo parameter in parameters)
            {
                SetParameters(parameter.ParameterType);
                MethodParameters = MethodParameters + " " + parameter.Name;
                if (parameters.Last().Name != parameter.Name)
                    MethodParameters = MethodParameters + ", ";
            }

            MethodParameters = MethodParameters + ")";
            SetModifier(methodInfo);
            SetAccessModifier(methodInfo);
            DataType = "";
            SetDataType(methodInfo.ReturnType);
            FullName = AccessModifier + " " + Modifier + " " + DataType + " " + Name + MethodParameters;
        }
        
        private void SetParameters(Type temptype)
        {
            if (temptype.IsGenericType)
            {
                MethodParameters = MethodParameters + temptype.Name.Remove(temptype.Name.Length-2, 2).Trim(new char[] {'`'}) + " <";
                List<Type> arguments = temptype.GetGenericArguments().ToList();
                foreach (Type argument in arguments)
                {
                    SetParameters(argument);
                    if (arguments.Last().FullName != argument.FullName)
                        MethodParameters = MethodParameters + ", ";
                }
                MethodParameters = MethodParameters + ">";
            }
            else
            {
                MethodParameters = MethodParameters + temptype.Name;
            }
        }
        
        
        private void SetModifier(MethodInfo methodInfo)
        {
            Modifier = "";
            if (methodInfo.IsFinal)
                Modifier = Modifier + "final";
            if (methodInfo.IsAbstract)
                Modifier = Modifier + "abstract";
            if (methodInfo.IsStatic)
                Modifier = Modifier + "static";
            if (methodInfo.IsVirtual)
                Modifier = Modifier + " virtual";
        }

        private void SetAccessModifier(MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic)
                AccessModifier = "public";
            else if (methodInfo.IsPrivate)
                AccessModifier = "private";
            else if (methodInfo.IsFamilyAndAssembly)
                AccessModifier = "private protected";
            else if (methodInfo.IsFamily)
                AccessModifier = "protected";
            else if (methodInfo.IsAssembly)
                AccessModifier = "internal";
            else
                AccessModifier = "protected internal";
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
    }
}
