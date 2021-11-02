using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace AssemblyLib.TreeElements
{
    public class Field
    {
        public string Name { get; private set; }

        public string DataType { get; private set; }

        public string Modifier { get; private set; }

        public string AccessModifier { get; private set; }
        
        public string FullName { get; private set; }

        public Field(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            SetTypeName(fieldInfo);
            SetModifier(fieldInfo);
            SetAccessModifier(fieldInfo);
            FullName = AccessModifier + Modifier + " " + DataType + " " + Name;
        }
        
        
        private void SetTypeName(FieldInfo fieldInfo)
        {
            Type temptype = fieldInfo.FieldType;
            SetParameters(temptype);
        }

        private void SetParameters(Type temptype)
        {
            if (temptype.IsGenericType)
            {
                DataType = DataType + temptype.Name.Remove(temptype.Name.Length-2, 2).Trim(new char[] {'`'}) + " <";
                List<Type> arguments = temptype.GetGenericArguments().ToList();
                foreach (Type argument in arguments)
                {
                    SetParameters(argument);
                    if (arguments.Last().FullName != argument.FullName)
                        DataType = DataType + ", ";
                }
                DataType = DataType + ">";
            }
            else
            {
                DataType = DataType + temptype.Name;
            }
        }

        private void SetModifier(FieldInfo fieldInfo)
        {
            Modifier = "";
            if (fieldInfo.IsStatic)
                Modifier = Modifier + " static";
            if (fieldInfo.IsInitOnly)
                Modifier = Modifier + " readonly";
        }

        private void SetAccessModifier(FieldInfo fieldInfo)
        {
            if (fieldInfo.IsPublic)
                AccessModifier = "public";
            else if (fieldInfo.IsPrivate)
                AccessModifier = "private";
            else if (fieldInfo.IsFamilyAndAssembly)
                AccessModifier = "private protected";
            else if (fieldInfo.IsFamily)
                AccessModifier = "protected";
            else if (fieldInfo.IsAssembly)
                AccessModifier = "internal";
            else
                AccessModifier = "protected internal";
        }
    }
}
