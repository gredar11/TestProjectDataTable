using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace TestProjectDataTable.ViewModels
{
    public class MainVindowViewModel : BindableBase
    {
        private TechnicalObjectViewModel _selectedRow;
        private DelegateCommand addDataFromExcelCommand;
        private DelegateCommand<object> delegateCommand;
        public MainVindowViewModel()
        {
        }

        public DelegateCommand AddDataFromExcelCommand =>
            addDataFromExcelCommand ?? (addDataFromExcelCommand = new DelegateCommand(ExecuteAddDataFromExcelCommand));

        public DelegateCommand<object> ChangeSelectedRow =>
            delegateCommand ?? (delegateCommand = new DelegateCommand<object>(ExecuteChangeSelectedRow));

        public ObservableCollection<TechnicalObjectViewModel> SelectedData { get; set; } = new ObservableCollection<TechnicalObjectViewModel>();
        public TechnicalObjectViewModel SelectedRow
        {
            get { return _selectedRow; }
            set { SetProperty(ref _selectedRow, value); }
        }

        private void ExecuteAddDataFromExcelCommand()
        {
            SelectedData.Clear();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string file = @"E:\Тест C#\test\Objects.xlsx";
            List<string> rowList = new List<string>();
            ISheet sheet;
            using (var stream = new FileStream(file, FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                rowList.Add(row.GetCell(j).ToString());
                            }
                        }
                    }
                    SelectedData.Add(new TechnicalObjectViewModel(rowList.ToArray()));
                    rowList.Clear();
                }
            }
        }

        private void ExecuteChangeSelectedRow(object args)
        {
            SelectedRow = (TechnicalObjectViewModel)args;
        }
    }
}