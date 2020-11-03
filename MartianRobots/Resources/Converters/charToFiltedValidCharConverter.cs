using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Resources.Converters
{
    public class charToFiltedValidCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char[] validInstructionIdentificators = { 'L', 'R', 'F' };
            string CorrectCharacteres = null;

            string inputInstructions = value as string;

            if (inputInstructions != null)
            {
                for (int j = 0; j < inputInstructions.Length; j++)
                {
                    if (validInstructionIdentificators.Contains(inputInstructions[j]))
                        CorrectCharacteres += inputInstructions[j];
                }

            }

            return CorrectCharacteres;

        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char[] validInstructionIdentificators = {'L', 'R', 'F' };
            string CorrectCharacteres = null;

            string inputInstructions = value as string;
            
            if (inputInstructions != null)
            {
                for (int j = 0; j < inputInstructions.Length; j++)
                {
                    if (validInstructionIdentificators.Contains(inputInstructions[j]))
                        CorrectCharacteres += inputInstructions[j];
                }

            }

            return CorrectCharacteres;

        }
    }
}
