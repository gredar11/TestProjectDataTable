using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TestProjectDataTable
{
    public partial class MainWindow : Window
    {
        ObservableCollection<DataGridRow> _data = new ObservableCollection<DataGridRow>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVindowViewModel();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DataGrid.AutoGenerateColumns = true;
            //DataSet dataSet = new DataSet();

            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //string file = @"E:\Тест C#\test\Objects.xlsx";
            //DataTable dtTable = new DataTable();
            //List<string> rowList = new List<string>();
            //ISheet sheet;
            //using (var stream = new FileStream(file, FileMode.Open))
            //{
            //    stream.Position = 0;
            //    XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
            //    sheet = xssWorkbook.GetSheetAt(0);
            //    IRow headerRow = sheet.GetRow(0);
            //    int cellCount = headerRow.LastCellNum;
            //    for (int j = 0; j < cellCount; j++)
            //    {
            //        ICell cell = headerRow.GetCell(j);
            //        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
            //        {
            //            dtTable.Columns.Add(cell.ToString());
            //        }
            //    }
            //    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //    {
            //        IRow row = sheet.GetRow(i);
            //        if (row == null) continue;
            //        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            if (row.GetCell(j) != null)
            //            {
            //                if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
            //                {
            //                    rowList.Add(row.GetCell(j).ToString());
            //                }
            //            }
            //        }
            //        if (rowList.Count > 0)
            //            dtTable.Rows.Add(rowList.ToArray());
            //        rowList.Clear();
            //    }
            //}
            //DataGrid.ItemsSource = dtTable.DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string file = @"E:\Тест C#\test\Objects.xlsx";
            var table = ((DataView)DataGrid.ItemsSource).Table;
            var memoryStream = new MemoryStream();
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Sheet1");

                List<String> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);
                int columnIndex = 0;

                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                    columnIndex++;
                }

                int rowIndex = 1;
                foreach (DataRow dsrow in table.Rows)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    int cellIndex = 0;
                    foreach (String col in columns)
                    {
                        row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                        cellIndex++;
                    }

                    rowIndex++;
                }
                workbook.Write(fs,false);
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
