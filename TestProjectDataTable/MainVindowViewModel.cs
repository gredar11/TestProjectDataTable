using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using TestProjectDataTable.DataViews;

namespace TestProjectDataTable
{
    public class MainVindowViewModel : BindableBase
    {
        public ObservableCollection<ObjectDataViewModel> SelectedData { get; set; } = new ObservableCollection<ObjectDataViewModel>();
        private DelegateCommand<object> delegateCommand;
        public DelegateCommand<object> ChangeSelectedRow =>
            delegateCommand ?? (delegateCommand = new DelegateCommand<object>(ExecuteChangeSelectedRow));

        void ExecuteChangeSelectedRow(object args)
        {

            IList<DataGridCellInfo> selectedCells = (IList<DataGridCellInfo>) args;
            SelectedData.Clear();
            foreach (var item in selectedCells)
            {
                var itemIndex = item.Column.DisplayIndex;
                var row = (DataRowView)item.Item;
                SelectedData.Add(new ObjectDataViewModel() { PropertyName = item.Column.Header.ToString(), FieldValue = row.Row.ItemArray[itemIndex].ToString() });
            }
        }
        private DelegateCommand<object> _datagridCellChanged;
        public DelegateCommand<object> DataGridCellChanged =>
            _datagridCellChanged ?? (_datagridCellChanged = new DelegateCommand<object>(ExecuteDataGridCellChanged));

        void ExecuteDataGridCellChanged(object args)
        {
            var currentCellInfo = (DataGridCellInfo)args;
            if (currentCellInfo.Column is null)
                return;
            var header = currentCellInfo.Column.Header;
            var objInObservableCollection = SelectedData.Where(x => x.PropertyName == header).FirstOrDefault();
            if(objInObservableCollection != null)
            {
                var indexInRow = currentCellInfo.Column.DisplayIndex;
                objInObservableCollection.FieldValue = (((DataRowView)currentCellInfo.Item).Row.ItemArray[indexInRow]).ToString();
            }
        }
        public MainVindowViewModel()
        {

        }
    }
}
