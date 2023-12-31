﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PRORAM
{
    public class MyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            bool control = false;
            double val = 0;
            string text = (value == null) ? "" : value.ToString();
            try
            {                
                val = double.Parse(text);
                control = true;
            }
            catch (Exception e)
            {

                control = false;
            }
            if (control)
                return val;

            else
                return null;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double val = 0.0;
            try
            {
                val = double.Parse(value.ToString());
                return value.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
