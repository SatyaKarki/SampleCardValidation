using Card.Helper.ExceptionLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Helper
{
   public static class HelperClass
    {
        public static ResponseModel Response(bool success, string message, dynamic output)
        {
            return new ResponseModel()
            {
                success = success,
                message = message,
                output = output
            };
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static T ToEnum<T>(this int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum<T>();
        }
       
    }
}
