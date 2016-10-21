using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace EmployeesDiscount.Common
{
    public class CommonMethod
    {
        public string Encryption(string express)

        {

            return FormsAuthentication.HashPasswordForStoringInConfigFile(express, "md5");

        }



        //解密

        public string Decrypt(string ciphertext)

        {

            CspParameters param = new CspParameters();

            param.KeyContainerName = "oa_erp_dowork";

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))

            {

                byte[] encryptdata = Convert.FromBase64String(ciphertext);

                byte[] decryptdata = rsa.Decrypt(encryptdata, false);

                return Encoding.Default.GetString(decryptdata);

            }

        }
    }
}