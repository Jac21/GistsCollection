using System.Text.RegularExpressions;

namespace CreativeExtensionMethods.Strings
{
    public static class StringExtensions
    {
        public static bool IsValidIpAddress(this string ipAddress)
        {
            if (!Regex.IsMatch(ipAddress, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
                return false;

            var ips = ipAddress.Split('.');
            if (ips.Length == 4 || ips.Length == 6)
            {
                return int.Parse(ips[0]) < 256 && int.Parse(ips[1]) < 256
                       & int.Parse(ips[2]) < 256 & int.Parse(ips[3]) < 256;
            }

            return false;
        }

        public static bool IsValidUrl(this string text)
        {
            var rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(text);
        }
    }
}