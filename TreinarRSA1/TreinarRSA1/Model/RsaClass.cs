using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace TreinarRSA1.Model
{
    class RsaClass
    {
        private int _int_keySize;
        private static RSAParameters _RSAParameter_publicKey;
        private static RSAParameters _RSAParameter_privateKey;
        private static  RSACryptoServiceProvider _RSACryptoServiceProvider_rsa;


        public RsaClass(int? keySize)
        {
            if (!(keySize is null) && keySize.Value > 0)
            {
                _int_keySize = keySize.Value;

                _RSACryptoServiceProvider_rsa = new RSACryptoServiceProvider(_int_keySize);
                _RSAParameter_publicKey = _RSACryptoServiceProvider_rsa.ExportParameters(false);
                _RSAParameter_privateKey = _RSACryptoServiceProvider_rsa.ExportParameters(true); 
            }
            else
            {
                throw new Exception("Impossivel construir o provedor de criptografia sem o tamanho da chave.");
            }
        }

        public string Encript(string str_msg)
        {
            if (!String.IsNullOrEmpty(str_msg))
            {
                byte[] byte_msg = Encoding.UTF8.GetBytes(str_msg);

                _RSACryptoServiceProvider_rsa.ImportParameters(_RSAParameter_publicKey);
                byte[] byte_result = _RSACryptoServiceProvider_rsa.Encrypt(byte_msg, false);

                return Convert.ToBase64String(byte_result);
            }
            else
            {
                throw new Exception("Não dá para criptografar o que não existe.");
            }
        }

        public string Decript(string str_msg)
        {
            if (!String.IsNullOrEmpty(str_msg))
            {
                byte[] byte_msg = Convert.FromBase64String(str_msg);

                _RSACryptoServiceProvider_rsa.ImportParameters(_RSAParameter_privateKey);
                byte[] byte_result = _RSACryptoServiceProvider_rsa.Decrypt(byte_msg, false);

                return Encoding.UTF8.GetString(byte_result);
            }
            else
            {
                throw new Exception("Não dá para descriptografar o que não existe.");
            }
        }

        public void Dispose() => _RSACryptoServiceProvider_rsa.Dispose();

        public static void MostraTempoDeExecucao(float tempo) => Console.WriteLine($"\n\n o processo durou: {tempo.ToString("f2", CultureInfo.InvariantCulture)} millissegundos");
    }
}
