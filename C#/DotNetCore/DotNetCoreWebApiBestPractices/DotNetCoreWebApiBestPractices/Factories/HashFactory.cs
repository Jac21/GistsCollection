using System.Security.Cryptography;
using System.Text;
using DotNetCoreWebApiBestPractices.Models;

namespace DotNetCoreWebApiBestPractices.Factories
{
    public static class HashFactory
    {
        /// <summary>
        /// Primitive hash function to generate ETAG for Owner resource
        /// 
        /// This is just one example. To avoid the overhead of computing a hash every time, 
        /// the easiest approach is to use a built-in database mechanism, like a row version, high precision time stamp, or GUID.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static string GetHash(Owner owner)
        {
            if (owner == null)
            {
                return string.Empty;
            }

            var ownerText = $"{owner.Id}|{owner.IsActive}|{owner.Name}";
            using (var md5 = MD5.Create())
            {
                byte[] retval = md5.ComputeHash(Encoding.Unicode.GetBytes(ownerText));

                StringBuilder sb = new StringBuilder();

                foreach (var t in retval)
                {
                    sb.Append(t.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}