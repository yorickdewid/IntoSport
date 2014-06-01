using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project.Validation
{
    public class PrijsAttribute : ValidationAttribute
    {
        public override bool IsValid(Object value)
        {
            if (value != null && (double)value != 0)
            {
                String input = String.Format("{0}", value);//(String)value;
                if (input.Contains("."))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}