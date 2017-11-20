using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    class UniqueKeyGenerator
    {
        private static String strGrantedChars = "qwertyuiopasdfghjklzxcvbnm1234567890";
        private static Random rnd;
        public static void initGenerator()
        {
            rnd = new Random(Environment.TickCount * DateTime.Now.Millisecond);
        }
        public static String generateUniqueKey(int nKeyLength)
        {
            if (rnd == null)
            {
                Exception expInit = new Exception("Generator is not initialized!");
                throw expInit;
            }
            String strKey = "";
            String strUsefulChars = mixInputString();
            int nTemp = 0;
            for (int i = 0; i < nKeyLength; i++)
            {
                nTemp = rnd.Next(0, strUsefulChars.Length - 1);
                strKey += strUsefulChars[nTemp];
            }
            return strKey;
        }
        private static String mixInputString()
        {
            String strNewString = "";
            String strGrantedTemp = strGrantedChars;
            int nTemp = 0;
            while (strGrantedTemp.Length > 0)
            {
                nTemp = rnd.Next(0, strGrantedTemp.Length - 1);
                strNewString += strGrantedTemp[nTemp];
                strGrantedTemp = strGrantedTemp.Replace(strGrantedTemp[nTemp].ToString(), "");
            }
            return strNewString;
        }
    }
}
