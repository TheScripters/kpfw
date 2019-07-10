using CryptSharp;
using System.Text;

namespace kpfw.Services.Authentication
{
    /// <summary>
    /// Summary description for Auth
    /// </summary>
    public class Auth
    {
        private readonly string _md5Hash;
        private readonly string _hash;
        private readonly string _pwd;
        private readonly string _dbHash;

        /// <summary>
        /// Instantiate Auth object
        /// </summary>
        /// <param name="password">User provided password</param>
        /// <param name="hash">Database hash. For older accounts, this will be MD5. For newer/updated accounts, it will be more secure.</param>
        public Auth(string password, string hash)
        {
            _pwd = password.ToBase64();
            _dbHash = hash;
            _md5Hash = CalculateMD5Hash(password);
            _hash = Crypter.Sha512.Crypt(password);
        }

        /// <summary>
        /// Instantiate Auth object
        /// </summary>
        /// <param name="password">User provided password</param>
        public Auth(string password)
        {
            _pwd = password.ToBase64();
            _md5Hash = CalculateMD5Hash(password);
            _hash = Crypter.Sha512.Crypt(password);
        }

        /// <summary>
        /// See if the password is correct
        /// </summary>
        /// <param name="isMD5">Lets the application know if the database is still using an MD5 hash</param>
        /// <returns>True if the password is correct, false if it is not</returns>
        public bool Compare(out bool isMD5)
        {
            bool isValid = false;
            isMD5 = _dbHash.Length == 32 && !_dbHash.Contains("$");
            if (!isMD5)
                isValid = Crypter.CheckPassword(_pwd.FromBase64(), _dbHash);
            else
                isValid = _md5Hash == _dbHash;

            return isValid;
        }

        /// <summary>
        /// Returns hash of a given password (not MD5). This can be used to update a user's password as well.
        /// </summary>
        /// <returns>Hash as a string</returns>
        public string GetNewHash()
        {
            return _hash;
        }

        private static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
    }
}