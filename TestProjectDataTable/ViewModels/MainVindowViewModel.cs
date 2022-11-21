using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using TestProjectDataTable.ViewModels.Repository;

namespace TestProjectDataTable.ViewModels
{
    public class MainVindowViewModel : BindableBase
    {
        private ITechObjRepository _techObjRepository;
        public MainVindowViewModel()
        {
        }
        private string _loadedFilename;
        public string LoadedFileName
        {
            get { return _loadedFilename; }
            set { SetProperty(ref _loadedFilename, value); }
        }
        public ObservableCollection<TechnicalObjectViewModel> SelectedData { get; set; } = new ObservableCollection<TechnicalObjectViewModel>();
        private TechnicalObjectViewModel _selectedRow;
        public TechnicalObjectViewModel SelectedRow
        {
            get { return _selectedRow; }
            set { SetProperty(ref _selectedRow, value); }
        }

        private DelegateCommand _addDataFromExcelCommand;
        public DelegateCommand AddDataFromExcelCommand =>
            _addDataFromExcelCommand ?? (_addDataFromExcelCommand = new DelegateCommand(ExecuteAddDataFromExcelCommand));
        private void ExecuteAddDataFromExcelCommand()
        {
            SelectedData.Clear();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls|Csv files (*.csv)|*.csv";
            if (fileDialog.ShowDialog()!.Value)
            {
                LoadedFileName = fileDialog.FileName;
                if (Path.GetExtension(LoadedFileName) == ".csv")
                {
                    _techObjRepository = new CsvRepository();
                }
                else
                {
                    _techObjRepository = new ExcelRepository();
                }
            }
            else
            {
                return;
            }
            var objects = _techObjRepository.GetObjects(LoadedFileName);
            SelectedData.AddRange(objects);
        }
        private DelegateCommand<object> _delegateCommand;
        public DelegateCommand<object> ChangeSelectedRow =>
            _delegateCommand ?? (_delegateCommand = new DelegateCommand<object>(ExecuteChangeSelectedRow));
        private void ExecuteChangeSelectedRow(object args)
        {
            SelectedRow = (TechnicalObjectViewModel)args;
        }
        private DelegateCommand _saveObjectsToFile;
        public DelegateCommand SaveObjectsToFile =>
            _saveObjectsToFile ?? (_saveObjectsToFile = new DelegateCommand(ExecuteSaveObjectsToFile));

        void ExecuteSaveObjectsToFile()
        {
            _techObjRepository.SaveObjects(SelectedData, LoadedFileName);
            MessageBox.Show(Application.Current.MainWindow, $"Файл {LoadedFileName} перезаписан.");
        }
    }
}