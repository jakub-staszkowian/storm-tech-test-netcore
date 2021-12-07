using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Gravatar;
using Todo.Models.TodoItems;

namespace Todo.Services
{
    public static class Gravatar
    {
        private static Dictionary<string, GravatarResponse> _savedResponses = new Dictionary<string, GravatarResponse>();

        public static string GetImageUrl(TodoItemSummaryViewmodel item)
        {
            if (item.GravatarProfile != null)
            {
                return item.GravatarProfile.ThumbnailUrl;
            }

            return $"https://www.gravatar.com/avatar/{GetHash(item.ResponsibleParty.Email)}?s=30";
        }

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static GravatarProfile GetUserData(string emailAddress)
        {
            var url = GetGravatarProfileUrl(emailAddress);

            GravatarResponse result;
            if (_savedResponses.ContainsKey(url))
            {
                result =  _savedResponses[url];
            }
            else
            {
                result = GetResponse(url);
                _savedResponses.Add(url, result);
            }

            return result?.Entry?.FirstOrDefault();
        }

        private static GravatarResponse GetResponse(string url)
        {
            using var httpClient = new HttpClient();
            var request = WebRequest.Create(url);
            request.Timeout = 30000;
            request.Headers.Add("User-Agent", "Test App");
            using var response = request.GetResponse();
            using var content = response.GetResponseStream();
            var data = new StreamReader(content).ReadToEnd();
            try
            {
                return JsonConvert.DeserializeObject<GravatarResponse>(data);
            }
            catch
            {
                return null;
            }
        }

        private static string GetGravatarProfileUrl(string emailAddress)
        {
            var emailHash = GetHash(emailAddress);
            return $"https://gravatar.com/{emailHash}.json";
        }
    }
}