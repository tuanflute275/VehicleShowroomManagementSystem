namespace VehicleShowroom.Mangement.Application.Utils
{
    public static class Util
    {
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public static string BodyResetPasswordMail(string pass)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Areas\\Admin\\Views\\Shared\\Mail", "ForgotPasswordMail.cshtml")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{Password}}", pass);
            return body;
        }

        public static string BodyRegisterMail(string fullName)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Areas\\Admin\\Views\\Shared\\Mail", "RegisterSuccessMail.cshtml")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{fullName}}", fullName);
            return body;
        }
    }
}
