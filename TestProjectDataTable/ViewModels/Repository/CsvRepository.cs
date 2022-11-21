using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestProjectDataTable.Tools;

namespace TestProjectDataTable.ViewModels.Repository
{
    public class CsvRepository : ITechObjRepository
    {
        public IEnumerable<TechnicalObjectViewModel> GetObjects(string filename)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<TechnicalObjectViewModel> objects = new List<TechnicalObjectViewModel>();
            using (StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("windows-1251")))
            {
                string[] allLines = sr.ReadToEnd().Split("\r\n");
                for (int i = 1; i < allLines.Length; i++)
                {
                    if (allLines[i].Length < 6)
                        continue;
                    string[] objectInfo = allLines[i].Split(';');
                    objects.Add(new TechnicalObjectViewModel(objectInfo));
                }
            }
            return objects;
        }

        public void SaveObjects(IEnumerable<TechnicalObjectViewModel> objects, string filename)
        {
            MemberInfo[] members = typeof(TechnicalObjectViewModel).GetProperties();
            YesNoToBooleanConverter booleanConverter = new YesNoToBooleanConverter();
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.GetEncoding("windows-1251")))
            {

                int columnIndex = 0;
                foreach (var memberInfo in members)
                {
                    sw.Write(memberInfo.Name + ";");
                    columnIndex++;
                }
                sw.WriteLine();
                foreach (var techObject in objects)
                {
                    foreach (var memberInfo in members)
                    {
                        var valueOfMember = memberInfo.GetValue(techObject);
                        if(memberInfo.Name == "IsDefect")
                        {
                            sw.Write(booleanConverter.Convert(valueOfMember, typeof(Boolean), null, null) + ";");
                            continue;
                        }
                        sw.Write(valueOfMember.ToString().Replace('.',',') + ";");
                    }
                    sw.WriteLine();
                }
                sw.Close();
            }

        }
    }
}

