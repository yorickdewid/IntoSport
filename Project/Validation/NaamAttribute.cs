using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project.Validation
{
    public class NaamAttribute : ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            if (value != null)
            {
                String input = (String)value;
                return Regex.IsMatch(input, @"^[\s?a-zA-Z'`-]+$");
            }
            else
            {
                return false;
            }
        }
    }
}