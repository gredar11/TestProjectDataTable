using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectDataTable.DataViews
{
    public class ObjectDataViewModel : BindableBase
    {
        
        private string fieldName;
        public string PropertyName
        {
            get { return fieldName; }
            set { SetProperty(ref fieldName, value); }
        }
        private string fieldValue;
        public string FieldValue
        {
            get { return fieldValue; }
            set { SetProperty(ref fieldValue, value); }
        }
        public ObjectDataViewModel()
        {

        }
    }
}
