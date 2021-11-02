using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AssemblyBrowser.Tree;
using AssemblyLib;
using AssemblyLib.TreeElements;
using Microsoft.Win32;

namespace AssemblyBrowser
{
    public class ViewModel: INotifyPropertyChanged
    {

        public AssemblyCollector AssemblyCollector { get; }

        public ViewModel()
        {
            AssemblyCollector = new AssemblyCollector();
        }

        public List<NameSpace> NameSpaces { get; private set; }


        private Command _loadCommand;
        public Command LoadCommand
        {
            get
            {
                return _loadCommand ??
                       (_loadCommand = new Command(obj =>
                       {
                           try
                           {
                               OpenFileDialog openFileDialog = new OpenFileDialog();
                               if (openFileDialog.ShowDialog() == true)
                                   NameSpaces = AssemblyCollector.getTree(openFileDialog.FileName);
                               newTree(NameSpaces);
                           }
                           catch (Exception e)
                           {
                               MessageBox.Show(e.Message);
                           }
                       }));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propname)
        {
            if (PropertyChanged != null)  
            {  
                PropertyChanged(this, new PropertyChangedEventArgs(propname));  
            }  
        }
        
        
  
        public void newTree(List<NameSpace> nameSpaces)
        {
            TreeNameSpaces = new List<TreeNameSpace>();
            foreach (NameSpace nameSpace in nameSpaces)
            {
                TreeNameSpaces.Add(new TreeNameSpace(nameSpace));
            }
            
            OnPropertyChanged("TreeNameSpaces"); 
        }  
  
        public List<TreeNameSpace> TreeNameSpaces { get; set; }
    }
}