using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Project.Application.Helpers
{
    public static class PublicHelper
    {
        public const string SessionCaptcha = "_Captcha";

        public const string RequiredValidationErrorMessage = "{0} را وارد نکرده اید";
        public const string NotValidValidationErrorMessage = "{0} نامعتبر است";

        public static bool IsValidMime(IFormFile? file, string[] mimeTypes)
        {
            if (file == null)
            {
                return false;
            }
            string mimeType = file.ContentType.ToLower();
            if (mimeTypes.Contains(mimeType))
            {
                return true;
            }
            return false;
        }
        public static string FilterUrl(string Url)
        {
            var m = Url.Replace(" ", "_");
            m = m.Replace("‌", "_");
            m = m.Replace("@", "_");
            m = m.Replace("!", "_");
            m = m.Replace("$", "_");
            m = m.Replace("%", "_");
            m = m.Replace("^", "_");
            m = m.Replace("&", "_");
            m = m.Replace("*", "_");
            m = m.Replace("(", "_");
            m = m.Replace(")", "_");
            m = m.Replace("+", "_");
            m = m.Replace("=", "_");
            m = m.Replace("-", "_");
            m = m.Replace(":", "_");
            m = m.Replace(";", "_");
            m = m.Replace("'", "_");
            m = m.Replace("?", "_");
            m = m.Replace("/", "_");
            m = m.Replace(".", "_");
            m = m.Replace("<", "_");
            m = m.Replace(">", "_");


            return m;
        }

        public static bool IsValidImage(IFormFile? file)
        {
            if (file == null)
            {
                return false;
            }
            string mimeType = file.ContentType.ToLower();
            if (mimeType != "image/jpg" &&
                 mimeType != "image/jpeg" &&
                 mimeType != "image/png")
            {
                return false;
            }

            return true;
        }

        public static bool IsValidVideo(IFormFile? file)
        {
            if (file == null)
            {
                return false;
            }
            string mimeType = file.ContentType.ToLower();
            if (mimeType != "video/mp4")
            {
                return false;
            }

            return true;
        }

        public static bool IsValidVoice(IFormFile? file)
        {
            if (file == null)
            {
                return false;
            }
            string mimeType = file.ContentType.ToLower();
            if (mimeType != "audio/mp3" &&
                 mimeType != "audio/x-m4a")
            {
                return false;
            }

            return true;
        }

        private static readonly Random random = new();

        public static int GetRandomInt()
        {
            int from = 11111, to = 99999;
            return random.Next(from, to);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string GetDisplayAttributeFrom(this Enum enumValue)
        {
            MemberInfo info = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First();
            if (info != null && info.CustomAttributes.Any())
            {
                DisplayAttribute nameAttr = info.GetCustomAttribute<DisplayAttribute>();
                return nameAttr != null ? nameAttr.Name : enumValue.ToString();
            }
            return enumValue.ToString();
        }
    }
}