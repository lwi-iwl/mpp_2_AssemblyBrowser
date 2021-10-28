using AssemblyLib.TreeElements;

namespace AssemblyBrowser
{
    public class Element : ViewModel
    {
        public string ElementName { get; set; }
        public string Icon { get; set; }

        public Element(string name, string type)  
        {  
            ElementName = name;
            if (type == "method")
                Icon = "C:\\Users\\nikst\\RiderProjects\\AssemblyBrowser\\AssemblyBrowser\\bin\\Debug\\method.png";
            if (type == "field")
                Icon = "C:\\Users\\nikst\\RiderProjects\\AssemblyBrowser\\AssemblyBrowser\\bin\\Debug\\field.png";
            if (type == "constructor")
                Icon = "C:\\Users\\nikst\\RiderProjects\\AssemblyBrowser\\AssemblyBrowser\\bin\\Debug\\constructor.png";
            if (type == "property")
                Icon = "C:\\Users\\nikst\\RiderProjects\\AssemblyBrowser\\AssemblyBrowser\\bin\\Debug\\property.png";
        }     
    }
}