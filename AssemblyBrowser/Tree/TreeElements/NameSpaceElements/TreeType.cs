using System.Collections.Generic;
using AssemblyLib.TreeElements;

namespace AssemblyBrowser.NameSpaceElements
{
    public class TreeType: ViewModel  
    {

        public TreeType(AnotherType anotherType)
        {
            TreeTypeName = anotherType.FullName;
            if (anotherType.DataType == "class")
                TypeIcon = "C:\\Users\\nikst\\RiderProjects\\AssemblyBrowser\\AssemblyBrowser\\bin\\Debug\\class.png";
            if (anotherType.DataType == "interface")
                TypeIcon = "C:\\Users\\nikst\\RiderProjects\\AssemblyBrowser\\AssemblyBrowser\\bin\\Debug\\interface.png";
            Elements = new List<Element>();

            foreach (Field field in anotherType.Fields)
            {
                Elements.Add(new Element(field.FullName, "field"));
            }
            
            foreach (Constructor constructor in anotherType.Constructors)
            {
                Elements.Add(new Element(constructor.FullName, "constructor"));
            }
            
            foreach (Property property in anotherType.Properties)
            {
                Elements.Add(new Element(property.FullName, "property"));
            }
            
            foreach (Method method in anotherType.Methods)
            {
                Elements.Add(new Element(method.FullName, "method"));
            }
            
        }  
  
        public List<Element> Elements
        {
            get;
            set;
        }

        public string TreeTypeName  
        {  
            get;  
            set;  
        }
        
        public string TypeIcon  
        {  
            get;  
            set;  
        }
    }  
}