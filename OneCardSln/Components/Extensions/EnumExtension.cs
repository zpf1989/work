﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Components.Extensions
{
    public static class EnumExtension
    {
        public static string GetString<TEnum>(this Enum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }

        public static string GetDescription(this Enum value)
        {
            string str = value.ToString();
            var field = value.GetType().GetField(str);
            object[] attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs == null || attrs.Length < 1)
            {
                return str;
            }
            var da = (DescriptionAttribute)attrs[0];
            if (da == null)
            {
                return str;
            }
            return da.Description;
        }
    }
}
