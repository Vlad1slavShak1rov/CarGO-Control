using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarGO_Control.Tools
{
    static public class CheckTextBox
    {
        
        public static bool CheckText(TextCompositionEventArgs e)
        {
            string pattern = @"^[a-zA-Zа-яА-Я0-9]*$";
            return Regex.IsMatch(e.Text, pattern);
        }

        public static bool CheckPass(string password)
        {
            bool Upper = false, Number = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    Upper = true;
                }
                if (char.IsNumber(c))
                {
                    Number = true;
                }
                if (!Upper || !Number) return false;
                else break;
            }

            return (Upper && Number);

        }
    }
}
