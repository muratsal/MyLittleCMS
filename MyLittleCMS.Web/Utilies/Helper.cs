using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyLittleCMS.Web.Utilies
{
    public class Helper
    {
        public static string GetHashedString(string textToHash)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] retVal = md5.ComputeHash(Encoding.Unicode.GetBytes(textToHash));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString() ;
            }
        }
    }
}