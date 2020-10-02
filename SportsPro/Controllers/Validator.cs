using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Conversions
{
    // a collection methods
    // every method assumes that the controls have Tag property set
    public static class Validator
    {
        // checks if text box is not empty
        public static bool IsPresent(TextBox tb)
        {
            bool result = true; // innocent intil proven guilty
            if(tb.Text == "")
            {
                MessageBox.Show(tb.Tag + " has to be provided",
                    "Input Error");
                result = false; // guilty
                tb.Focus();
            }
            return result;
        }

        // checks if input in a text box in an int
        public static bool IsInteger(TextBox tb)
        {
            bool result = true;
            int parsedValue;
            if(!Int32.TryParse(tb.Text, out parsedValue))// bad!
            {
                MessageBox.Show(tb.Tag + " has to be a whole number",
                    "Input Error");
                result = false;
                tb.SelectAll();
                tb.Focus();
            }
            return result;
        }

        // checks if input in a text box in a non-negative int
        public static bool IsNonNegativeInteger(TextBox tb)
        {
            bool result = true;
            int parsedValue = Int32.Parse(tb.Text);// we already know it is an int
            if (parsedValue < 0)// bad!
            {
                MessageBox.Show(tb.Tag + " has to be positive or zero",
                    "Input Error");
                result = false;
                tb.SelectAll();
                tb.Focus();
            }
            return result;
        }

        // checks if input in a text box in a double value
        public static bool IsDouble(TextBox tb)
        {
            bool result = true;
            double parsedValue;
            if (!Double.TryParse(tb.Text, out parsedValue))// bad!
            {
                MessageBox.Show(tb.Tag + " has to be a number (can have decimal part)",
                    "Input Error");
                result = false;
                tb.SelectAll();
                tb.Focus();
            }
            return result;
        }

        // checks if input in a text box in a non-negative int
        public static bool IsNonNegativeDouble(TextBox tb)
        {
            bool result = true;
            double parsedValue = Double.Parse(tb.Text);// we already know it is a double
            if (parsedValue < 0)// bad!
            {
                MessageBox.Show(tb.Tag + " has to be positive or zero",
                    "Input Error");
                result = false;
                tb.SelectAll();
                tb.Focus();
            }
            return result;
        }

    }
}
