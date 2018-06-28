namespace DotNetCoreWebApiBestPractices.Crypto
{
    /// <summary>
    /// CryptoHelper
    /// </summary>
    public class PasswordManager
    {
        // Method for hashing the password
        public string HashPassword(string password)
        {
            return CryptoHelper.Crypto.HashPassword(password);
        }

        // Method to verify the password hash against the given password
        public bool VerifyPassword(string hash, string password)
        {
            return CryptoHelper.Crypto.VerifyHashedPassword(hash, password);
        }
    }
}