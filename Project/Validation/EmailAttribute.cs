using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project.Validation
{
    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            if (value != null)
            {
                String input = (String)value;
                return Regex.IsMatch(input,
                  @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            }
            else
            {
                return false;
            }
        }
    }
}