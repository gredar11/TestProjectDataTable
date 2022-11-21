using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestProjectDataTable.Tools;

namespace TestProjectDataTable.ViewModels.Repository
{
    public class ExcelRepository : ITechObjRepository
    {

        public ExcelRepository()
        {

        }

        public IEnumerable<TechnicalObjectViewModel> GetObjects(string filename)
        {
            var listToReturn = new List<TechnicalObjectViewModel>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<string> rowList = new List<string>();
            ISheet sheet;
            using (var stream = new FileStream(filename, FileMode.Open))
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
                    listToReturn.Add(new TechnicalObjectViewModel(rowList.ToArray()));
                    rowList.Clear();
                }
            }
            return listToReturn;
        }

        public void SaveObjects(IEnumerable<TechnicalObjectViewModel> objects, string filename)
        {
            MemberInfo[] members = typeof(TechnicalObjectViewModel).GetProperties();
            YesNoToBooleanConverter booleanConverter = new YesNoToBooleanConverter();

            using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Sheet1");

                List<String> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);
                int columnIndex = 0;

                foreach (var memberInfo in members)
                {
                    row.CreateCell(columnIndex).SetCellValue(memberInfo.Name);
                    columnIndex++;
                }
                
                int rowIndex = 1;
                foreach (var techObject in objects)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    int cellIndex = 0;
                    foreach (var memberInfo in members)
                    {
                        var valueOfMember = memberInfo.GetValue(techObject);
                        if (memberInfo.Name == "IsDefect")
                        {
                            row.CreateCell(cellIndex).SetCellValue(booleanConverter.Convert(valueOfMember, typeof(Boolean), null, null).ToString());
                            cellIndex++;
                            continue;
                        }
                        row.CreateCell(cellIndex).SetCellValue(valueOfMember.ToString());
                        cellIndex++;
                    }
                    rowIndex++;
                }
                workbook.Write(fs, false);
            }
        }
    }
}
