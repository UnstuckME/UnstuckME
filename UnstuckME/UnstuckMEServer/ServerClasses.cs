using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using UnstuckMEInterfaces;
using UnstuckME_Classes;

namespace UnstuckMEServer
{
    public class ConnectedClient
    {
        public IClient Connection;
        public UserInfo User;
        public RemoteEndpointMessageProperty ReturnAddress;
        public OperationContext ChannelInfo;
    }

    public class ConnectedServerAdmin
    {
        public IServer Connection;
        public AdminInfo Admin;
    }

    public class UnstuckMEPassword
    {
        public string Password { get; set; }
        public string Salt { get; set; }
    }

    public class UnstuckMEHashing
    {
        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private static byte[] GenerateSaltedHash(string stringPassword, string stringSalt)
        {
            byte[] plainText = GetBytes(stringPassword);
            byte[] salt = GetBytes(stringSalt);
			byte[] returnBytestring = new byte[plainText.Length + salt.Length];

			using (HashAlgorithm algorithm = new SHA256Managed())
			{
				byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

				for (int i = 0; i < plainText.Length; i++)
				    plainTextWithSaltBytes[i] = plainText[i];
			    for (int i = 0; i < salt.Length; i++)
			        plainTextWithSaltBytes[plainText.Length + i] = salt[i];

			    returnBytestring = algorithm.ComputeHash(plainTextWithSaltBytes);
			}

            return returnBytestring;
        }
        public static UnstuckMEPassword GetHashedPassword(string password)
        {
            UnstuckMEPassword returnPassword = new UnstuckMEPassword();
			//create the account
			using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
			{
				byte[] tokenData = new byte[32];
				rng.GetBytes(tokenData);
				returnPassword.Salt = Convert.ToBase64String(tokenData);
			}

            byte[] bytePassword = GenerateSaltedHash(password, returnPassword.Salt);
            string inputPassword = string.Empty;

            foreach (byte element in bytePassword)
                inputPassword += element;

            returnPassword.Password = inputPassword;
            return returnPassword;
        }

        public static string RecreateHashedPassword(string password, string salt)
        {
            byte[] bytePassword = GenerateSaltedHash(password, salt);
            string inputPassword = string.Empty;

            foreach (byte element in bytePassword)
                inputPassword += element;

            return inputPassword;
        }
    }
}