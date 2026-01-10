using Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace Application.Common.Extensions
{
    public static class SessionExtensions
    {
        private const string SessionKey = "CurrentUser";

        public static void SetCurrentUser(this ISession session, UserSession user)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false
            };

            var json = JsonSerializer.Serialize(user, options);

            session.SetString(SessionKey, json);
        }

        public static UserSession? GetCurrentUser(this ISession session)
        {
            var json = session.GetString(SessionKey);

            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<UserSession>(json);
        }
    }
}
