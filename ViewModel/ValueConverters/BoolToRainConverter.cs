﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.WPF.Learning.ViewModel.ValueConverters
{
    class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRaining = (bool)value;

            if (isRaining)
            {
                return "Currently raining.";
            }
            else
                return "Sky is clear.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isRaining = (string)value;

            if (isRaining == "Currently raining.")
                return true;

            return false;
        }
    }
}
