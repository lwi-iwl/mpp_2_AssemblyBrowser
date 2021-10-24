using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using AssemblyLib;
using AssemblyLib.TreeElements;
using Microsoft.Win32;

namespace AssemblyBrowser
{
    public class Model: INotifyPropertyChanged
    {

        public AssemblyCollector AssemblyCollector { get; }

        public Model()
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
                           }
                           catch (Exception e)
                           {
                               MessageBox.Show(e.Message);
                           }
                       }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}