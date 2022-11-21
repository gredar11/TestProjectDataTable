using Prism.Mvvm;
using System;
using System.Globalization;
using System.Linq;

namespace TestProjectDataTable.ViewModels
{
    public class TechnicalObjectViewModel : BindableBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private double _width;

        public double Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        private double _distance;

        public double Distance
        {
            get { return _distance; }
            set { SetProperty(ref _distance, value); }
        }

        private double _angle;

        public double Angle
        {
            get { return _angle; }
            set { SetProperty(ref _angle, value); }
        }

        private double _height;

        public double Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        private bool _isDefect;

        public bool IsDefect
        {
            get { return _isDefect; }
            set { SetProperty(ref _isDefect, value); }
        }

        public TechnicalObjectViewModel(string[] args)
        {
            Name = args[0];
            NumberFormatInfo nfi = new NumberFormatInfo();
            if (args.Skip(1).Take(4).Any(x => x.Contains(',')))
                nfi.NumberDecimalSeparator = ",";
            else
                nfi.NumberDecimalSeparator = ".";
            Distance = double.Parse(args[1], nfi);
            Angle = double.Parse(args[2], nfi);
            Width = double.Parse(args[3], nfi);
            Height = double.Parse(args[4], nfi);
            IsDefect = ConvertToBool(args[5]);
        }

        private bool ConvertToBool(string val)
        {
            switch (val.ToString().ToLower())
            {
                case "yes":
                    return true;

                case "no":
                    return false;
            }
            return true;
            throw new Exception($"Can't convert {val} to Boolean value.");
        }
    }
}