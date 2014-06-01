using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project.Validation
{
    public class PostcodeAttribute : ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            if (value != null)
            {
                String input = (String)value;
                return Regex.IsMatch(input,
                  @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$");
            }
            else
            {
                return false;
            }
        }
    }
}