namespace VehicleShowroom.Management.Application.Utils
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
            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Utils\\Template\\ForgotPasswordMail.cshtml", "ForgotPasswordMail.cshtml")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{Password}}", pass);
            return body;
        }

        public static string BodyRegisterMail(string fullName)
        {
            string body = string.Empty;
            string templatePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "VehicleShowroom.Mangement.Application", "Utils", "Template", "RegisterSuccessMail.cshtml");
            using (StreamReader reader = new StreamReader(templatePath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{fullName}}", fullName);
            return body;
        }
        public static string GenerateSlug(string value)
        {
            string slug = RemoveVietnameseAccents(value.ToLowerInvariant());
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-").Trim('-');
            return slug;
        }

        private static string RemoveVietnameseAccents(string input)
        {
            string[] vietnameseSigns = new string[]
            {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
            };

            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                {
                    input = input.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
                }
            }

            return input;
        }

    }
}
