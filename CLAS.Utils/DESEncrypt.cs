﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace CLAS.Utils
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class DESEncrypt
    {
        protected const string EnDecryptString = "ldx117";

        public DESEncrypt()
        {
        }

        #region ========加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, EnDecryptString);
        }

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        /// 加密对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string EncryptModel(object model)
        {
            var result = ObjectStrConvert.SerializeObjectJson(model);
            result = DESEncrypt.Encrypt(result);
            return result;
        }

        #endregion

        #region ========解密========


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, EnDecryptString);
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            Text = Text.Replace("\"", "");
            if (string.IsNullOrEmpty(Text))
                return String.Empty;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// 解密对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T DecryptModel<T>(string key)
        {
            key = DESEncrypt.Decrypt(key);
            var result = ObjectStrConvert.DeserializeObjectJson<T>(key);
            return result;
        }


        #endregion


        public static string PwdStuEncrypt(string p_Pwd)
        {
            return Utils.DESEncrypt.Encrypt(p_Pwd, "22TopStudent");
        }

        public static string PwdStuDecrypt(string p_Pwd)
        {
            return Utils.DESEncrypt.Decrypt(p_Pwd, "22TopStudent");
        }

        public static string PwdEtpEncrypt(string p_Pwd)
        {
            return Utils.DESEncrypt.Encrypt(p_Pwd, "22TopEnterprise");
        }

        public static string PwdEtpDecrypt(string p_Pwd)
        {
            return Utils.DESEncrypt.Decrypt(p_Pwd, "22TopEnterprise");
        }



    }
}

