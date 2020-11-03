using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Resources.Converters
{
    public class InstructionListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string instructions = null;

            List<IInstruction> instructionList = value as List<IInstruction>;

            if(instructionList != null)
            {
               
                foreach (IInstruction instruction in instructionList)
                {
                    instructions += instruction.Identificator;
                }

            }

            return instructions;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
