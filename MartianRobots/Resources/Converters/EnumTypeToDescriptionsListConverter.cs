using EnumsNET;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Resources.Converters
{
    public class EnumTypeToDescriptionsListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> OrientationList = new List<string>();

            if(value != null && value is OrientationTypes)
            {
                Type orientation = value.GetType();               
                Array enumValues = orientation.GetEnumValues();

                foreach (var element in enumValues)
                {
                    string description = element.ToString();
                    OrientationList.Add(description);

                }
            }
                     
            return OrientationList;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> OrientationList = new List<string>();

            if (value != null && value is OrientationTypes)
            {
                Type orientation = value.GetType();
                Array enumValues = orientation.GetEnumValues();

                foreach (var element in enumValues)
                {
                    string description = element.ToString();
                    OrientationList.Add(description);

                }
            }

            return OrientationList;

        }
    }
}
