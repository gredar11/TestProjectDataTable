using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TestProjectDataTable.ViewModels;

namespace TestProjectDataTable
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<DataGridRow> _data = new ObservableCollection<DataGridRow>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVindowViewModel();
        }
    }
}