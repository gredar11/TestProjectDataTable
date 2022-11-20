using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDataTable.ViewModels.Repository
{
    public interface ITechObjRepository
    {
        public IEnumerable<TechnicalObjectViewModel> GetObjects(string filename);
        public void SaveObjects(IEnumerable<TechnicalObjectViewModel> objects, string filename);
    }
}
