using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;

namespace AssemblyLib.TreeElements
{
    public class Constructor
    {
        public string Name { get; private set; }

        public string Modifier { get; private set; }

        public string AccessModifier { get; private set; }

        public string ConstructorParameters { get; private set; }

        public string FullName { get; private set; }
        public Constructor(ConstructorInfo constructorInfo)
        {
            Name = constructorInfo.Name;
            List<ParameterInfo> parameters = constructorInfo.GetParameters().ToList();
            ConstructorParameters = "(";
            foreach (ParameterInfo parameter in parameters)
            {
                SetParameters(parameter.ParameterType);
                ConstructorParameters = ConstructorParameters + " " + parameter.Name;
                if (parameters.Last().Name != parameter.Name)
                    ConstructorParameters = ConstructorParameters + ", ";
            }

            ConstructorParameters = ConstructorParameters + ")";
            SetModifier(constructorInfo);
            SetAccessModifier(constructorInfo);
            FullName = AccessModifier + " " + Modifier + " " + ConstructorParameters + " " + Name;
        }
        
        private void SetParameters(Type temptype)
        {
            if (temptype.IsGenericType)
            {
                ConstructorParameters = ConstructorParameters + temptype.Name.Remove(temptype.Name.Length-2, 2).Trim(new char[] {'`'}) + " <";
                List<Type> arguments = temptype.GetGenericArguments().ToList();
                foreach (Type argument in arguments)
                {
                    SetParameters(argument);
                    if (arguments.Last().GetHashCode() != argument.GetHashCode())
                        ConstructorParameters = ConstructorParameters + ", ";
                }
                ConstructorParameters = ConstructorParameters + ">";
            }
            else
            {
                ConstructorParameters = ConstructorParameters + temptype.Name;
            }
        }
        private void SetModifier(ConstructorInfo constructorInfo)
        {
            Modifier = "";
            if (constructorInfo.IsFinal)
                Modifier = Modifier + "final";
            if (constructorInfo.IsAbstract)
                Modifier = Modifier + "abstract";
            if (constructorInfo.IsStatic)
                Modifier = Modifier + "static";
            if (constructorInfo.IsVirtual)
                Modifier = Modifier + " virtual";
        }

        private void SetAccessModifier(ConstructorInfo constructorInfo)
        {
            if (constructorInfo.IsPublic)
                AccessModifier = "public";
            else if (constructorInfo.IsPrivate)
                AccessModifier = "private";
            else if (constructorInfo.IsFamilyAndAssembly)
                AccessModifier = "private protected";
            else if (constructorInfo.IsFamily)
                AccessModifier = "protected";
            else if (constructorInfo.IsAssembly)
                AccessModifier = "internal";
            else
                AccessModifier = "protected internal";
        }
    }
}