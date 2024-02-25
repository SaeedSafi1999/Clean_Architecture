using Core.Application.Services.EncryptServices.Settings;
using Core.Domain.DTOs.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.EncryptServices
{
    internal class EncryptService : IEncryptService
    {
        private readonly ICryptographySetting _setting;

        public EncryptService(ICryptographySetting setting)
        {
            _setting = setting;
        }

        public IServiceResponse<string> Decrypt(string text)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                var key = aes.Key;
                var iv = aes.IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(text)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            var result = reader.ReadToEnd();
                            return new ServiceRespnse<string>().OK(result);
                        }
                    }
                }
            }
        }

        public IServiceResponse<string> Encrypt(string text)
        {
            var Errors = new Hashtable();
            if (string.IsNullOrEmpty(text))
            {
                Errors.Add("text","empty");
                return new ServiceRespnse<string>().Failed(System.Net.HttpStatusCode.NotFound,Errors);
            }
                
            byte[] iv = new byte[16];
            byte[] array;
            using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_setting.Key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            return new ServiceRespnse<string>().OK(Convert.ToBase64String(array));

        }

    }

}
