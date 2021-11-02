using System;
using System.Collections.Generic;
using AssemblyLib;
using AssemblyLib.TreeElements;
using NUnit.Framework;

namespace TestProject3
{
    [TestFixture]
    public class Tests
    {
        private AssemblyCollector _assemblyCollector;
        private List<NameSpace> _nameSpaces;
        public Tests()
        {
            _assemblyCollector = new AssemblyCollector();
            _nameSpaces = _assemblyCollector.getTree("C:\\Users\\nikst\\RiderProjects\\AssemblyBrowser\\Exe\\Test.exe");
        }

        [Test]
        public void namespacesQuantity()
        {
            Assert.That(_nameSpaces.Count, Is.EqualTo(3));
        }
        
        [Test]
        public void ClassesInterfacesQuantity()
        {
            Assert.That(_nameSpaces[0].AnotherTypes.Count, Is.EqualTo(4));
            Assert.That(_nameSpaces[1].AnotherTypes.Count, Is.EqualTo(1));
            Assert.That(_nameSpaces[2].AnotherTypes.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void PropertiesQuantity()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Properties.Count, Is.EqualTo(1));
            Assert.That(_nameSpaces[0].AnotherTypes[1].Properties.Count, Is.EqualTo(0));
            Assert.That(_nameSpaces[0].AnotherTypes[2].Properties.Count, Is.EqualTo(0));
            Assert.That(_nameSpaces[0].AnotherTypes[3].Properties.Count, Is.EqualTo(0));
            Assert.That(_nameSpaces[1].AnotherTypes[0].Properties.Count, Is.EqualTo(1));
            Assert.That(_nameSpaces[2].AnotherTypes[0].Properties.Count, Is.EqualTo(0));
        }

        [Test]
        public void FieldsQuantity()
        {   
            Assert.That(_nameSpaces[0].AnotherTypes[0].Fields.Count, Is.EqualTo(2));
            Assert.That(_nameSpaces[0].AnotherTypes[1].Fields.Count, Is.EqualTo(0));
            Assert.That(_nameSpaces[0].AnotherTypes[2].Fields.Count, Is.EqualTo(2));
            Assert.That(_nameSpaces[0].AnotherTypes[3].Fields.Count, Is.EqualTo(0));
            Assert.That(_nameSpaces[1].AnotherTypes[0].Fields.Count, Is.EqualTo(2));
            Assert.That(_nameSpaces[2].AnotherTypes[0].Fields.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void ConstructorsQuantity()
        {   
            Assert.That(_nameSpaces[0].AnotherTypes[0].Constructors.Count, Is.EqualTo(2));
            Assert.That(_nameSpaces[0].AnotherTypes[1].Constructors.Count, Is.EqualTo(0));
            Assert.That(_nameSpaces[0].AnotherTypes[2].Constructors.Count, Is.EqualTo(1));
            Assert.That(_nameSpaces[0].AnotherTypes[3].Constructors.Count, Is.EqualTo(0));
            Assert.That(_nameSpaces[1].AnotherTypes[0].Constructors.Count, Is.EqualTo(1));
            Assert.That(_nameSpaces[2].AnotherTypes[0].Constructors.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void MethodsQuantity()
        {   
            Assert.That(_nameSpaces[0].AnotherTypes[0].Methods.Count, Is.EqualTo(7));
            Assert.That(_nameSpaces[0].AnotherTypes[1].Methods.Count, Is.EqualTo(1));
            Assert.That(_nameSpaces[0].AnotherTypes[2].Methods.Count, Is.EqualTo(7));
            Assert.That(_nameSpaces[0].AnotherTypes[3].Methods.Count, Is.EqualTo(6));
            Assert.That(_nameSpaces[1].AnotherTypes[0].Methods.Count, Is.EqualTo(8));
            Assert.That(_nameSpaces[2].AnotherTypes[0].Methods.Count, Is.EqualTo(1));
        }

        [Test]
        public void NameSpaceName()
        {
            Assert.That(_nameSpaces[0].Name, Is.EqualTo("Test"));
        }
        
        [Test]
        public void ClassType()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].DataType, Is.EqualTo("class"));
        }
        
        [Test]
        public void InterfaceType()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[1].DataType, Is.EqualTo("interface"));
        }
        
        [Test]
        public void ClassModifier()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Modifier, Is.EqualTo(""));
        }
        
        [Test]
        public void ClassName()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Name, Is.EqualTo("Class2"));
        }
        
        [Test]
        public void ClassAccessModifier()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].AccessModifier, Is.EqualTo("public"));
        }
        
        [Test]
        public void FieldModifier()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Fields[0].Modifier, Is.EqualTo(""));
        }
        
        [Test]
        public void FieldName()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Fields[0].Name, Is.EqualTo("_field1"));
        }
        
        [Test]
        public void FieldTypeName()
        {
            Assert.That(_nameSpaces[1].AnotherTypes[0].Fields[0].DataType, Is.EqualTo("Dictionary <List <List <Int32>>, Int32>"));
        }
        
        [Test]
        public void FieldAccessModifier()
        {
            Assert.That(_nameSpaces[1].AnotherTypes[0].Fields[0].AccessModifier, Is.EqualTo("private"));
        }
        
        [Test]
        public void MethodAccessModifier()
        {
            Assert.That(_nameSpaces[1].AnotherTypes[0].Methods[0].AccessModifier, Is.EqualTo("public"));
        }
        
        [Test]
        public void MethodModifier()
        {
            Assert.That(_nameSpaces[1].AnotherTypes[0].Methods[1].Modifier, Is.EqualTo(" virtual"));
        }
        
        [Test]
        public void MethodName()
        {
            Assert.That(_nameSpaces[1].AnotherTypes[0].Methods[0].Name, Is.EqualTo("Methodf"));
        }
        
        [Test]
        public void MethodDataType()
        {
            Assert.That(_nameSpaces[1].AnotherTypes[0].Methods[0].DataType, Is.EqualTo("Dictionary <List <List <Int32>>, Int32>"));
        }
        
        [Test]
        public void MethodParameters()
        {
            Assert.That(_nameSpaces[1].AnotherTypes[0].Methods[0].MethodParameters, Is.EqualTo("(Int32 i, Dictionary <List <List <Int32>>, Int32> dictionary)"));
        }

        [Test]
        public void ConstructorName()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Constructors[0].Name, Is.EqualTo(".ctor Class2"));
        }
        
        [Test]
        public void ConstructorModifier()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Constructors[0].Modifier, Is.EqualTo(""));
        }
        
        [Test]
        public void ConstructorAccessModifier()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Constructors[0].AccessModifier, Is.EqualTo("public"));
            Assert.That(_nameSpaces[0].AnotherTypes[0].Constructors[1].AccessModifier, Is.EqualTo("private"));
        }

        [Test]
        public void ConstructorParameters()
        {
            Assert.That(_nameSpaces[0].AnotherTypes[0].Constructors[1].ConstructorParameters, Is.EqualTo("(Int32 i)"));
        }
    }
}