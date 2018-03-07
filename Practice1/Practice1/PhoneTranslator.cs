using System.Text;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace Practice1
{
    public static class PhoneTranslator
    {
        public static String ToNumber(String strRaw)
        {
            if (
                String.IsNullOrWhiteSpace(strRaw)
            )
            {
                return "";
            }
            else
            {
                strRaw = strRaw.ToUpperInvariant();
            }

            StringBuilder stbNewnumbre = new StringBuilder();
            foreach(Char chrChar in strRaw)
            {
                if(
                    " -0123456789".Contains(chrChar)
                )
                {
                    stbNewnumbre.Append(chrChar);
                }
                else
                {
                    int? intResult = TranslateToNumber(chrChar);
                    if(
                        intResult != null
                    )
                    {
                        stbNewnumbre.Append(intResult);
                    }
                }
            }
            return stbNewnumbre.ToString();
        }

        static bool Contains (this String strKey, Char chrChar)
        {
            return strKey.IndexOf(chrChar) >= 0;
        }

        static int? TranslateToNumber(Char chrChar)
        {
            /*CASE*/
            if (
                "ABC".Contains(chrChar)
            )
            {
                return 2;
            }else if (
                "DEF".Contains(chrChar)
            ){
                return 3;
            }else if(
                "GHI".Contains(chrChar)
            ){
                return 4;
            }else if(
                "JKL".Contains(chrChar)
            ){
                return 5;
            }else if(
                "MNO".Contains(chrChar)
            ){
                return 6;
            }else if (
                "PQRS".Contains(chrChar)
            )
            {
                return 7;
            }else if (
                "TUV".Contains(chrChar)
            )
            {
                return 8;
            }else if (
                "WXYZ".Contains(chrChar)
            )
            {
                return 9;
            }
            /*END-CASE*/

            return null;
        }

    }
}
