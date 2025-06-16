namespace DevTest_API.Configuration
{
    public static class Base64Utils
    {
        public static string Encode(string plainText)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
        }

        public static string Decode(string base64Encoded)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Encoded));
        }
    }
}
