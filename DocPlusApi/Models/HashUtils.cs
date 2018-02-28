using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace DocPlusApi.Models
{
    public class HashUtils
    {

        //Get file MD5 folder type
        public static string GetMD5Hash(FileStream fileStream)
        {
            try
            {
                MD5 md5Provider = new MD5CryptoServiceProvider();
                byte[] buffer = md5Provider.ComputeHash(fileStream);
                md5Provider.Clear();
                return BitConverter.ToString(buffer).Replace("-","");
            }
            catch
            {
                return null;
            }

        }

        // get string MD-5 type
        public static string GetMD5Hash(string str)
        {
            try
            {
                MD5 md5Provider = new MD5CryptoServiceProvider();
                byte[] buffer = md5Provider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
                md5Provider.Clear();
                return BitConverter.ToString(buffer).Replace("-", "");
            }
            catch
            {
                return null;
            }
        }


        //Byte to MD5 type
        public static string GetMD5Hash(byte[] bytes)
        {
            try
            {
                MD5 md5Provider = new MD5CryptoServiceProvider();
                byte[] buffer = md5Provider.ComputeHash(bytes);
                md5Provider.Clear();
                return BitConverter.ToString(buffer).Replace("-", "");
            }
            catch
            {
                return null;
            }
        }
    }
}