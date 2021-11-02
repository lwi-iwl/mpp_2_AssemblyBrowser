using System.Collections.Generic;
using AssemblyBrowser.NameSpaceElements;
using AssemblyLib.TreeElements;

namespace AssemblyBrowser.Tree
{
    public class TreeNameSpace: ViewModel  
    {

        public TreeNameSpace(NameSpace nameSpace)
        {
            NameSpaceName = nameSpace.Name;
            TreeTypes = new List<TreeType>();
            foreach (AnotherType anotherType in nameSpace.AnotherTypes)
            {
                TreeTypes.Add(new TreeType(anotherType));
            }
        }  
        public List<TreeType> TreeTypes { get; set; }
        public string NameSpaceName { get; set; }
    }  
}