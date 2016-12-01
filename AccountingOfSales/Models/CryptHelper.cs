using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class CryptHelper
    {
        /// <summary>
        /// Получает хэш-значение MD5 из входной строки
        /// </summary>
        /// <param name="input">Строка из которой необходимо получить хэш-значение</param>
        /// <returns></returns>
        public static string CreateHashMD5(string input)
        {
            byte[] hash = Encoding.ASCII.GetBytes(input);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;
        }
    }
}
