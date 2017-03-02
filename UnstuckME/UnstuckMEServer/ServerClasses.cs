using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using UnstuckME_Classes;
using UnstuckMEInterfaces;

namespace UnstuckMEServer
{
    public class ConnectedClient
    {
        public IClient connection;
        public UserInfo User;
        public RemoteEndpointMessageProperty returnAddress;
        public OperationContext ChannelInfo;
    }

    public class ConnectedServerAdmin
    {
        public IServer connection;
        public AdminInfo Admin;
    }

    public class UnstuckMEPassword
    {
        public string Password { get; set; }
        public string Salt { get; set; }
    }

    public class UnstuckMEHashing
    {
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        static byte[] GenerateSaltedHash(string stringPassword, string stringSalt)
        {
            byte[] plainText = GetBytes(stringPassword);
            byte[] salt = GetBytes(stringSalt);
			byte[] return_bytestring = new byte[plainText.Length + salt.Length];

			using (HashAlgorithm algorithm = new SHA256Managed())
			{
				byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

				for (int i = 0; i < plainText.Length; i++)
				{
					plainTextWithSaltBytes[i] = plainText[i];
				}
				for (int i = 0; i < salt.Length; i++)
				{
					plainTextWithSaltBytes[plainText.Length + i] = salt[i];
				}

				return_bytestring = algorithm.ComputeHash(plainTextWithSaltBytes);
			}

            return return_bytestring;
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
            {
                inputPassword += element;
            }

            returnPassword.Password = inputPassword;
            return returnPassword;
        }

        public static string RecreateHashedPassword(string password, string salt)
        {
            byte[] bytePassword = GenerateSaltedHash(password, salt);
            string inputPassword = "";

            foreach (byte element in bytePassword)
            {
                inputPassword += element;
            }

            return inputPassword;
        }
    }
}