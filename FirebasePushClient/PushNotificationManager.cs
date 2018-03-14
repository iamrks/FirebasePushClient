using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FirebasePushClient
{
    class PushNotificationManager
    {
        const string URL = "https://fcm.googleapis.com/fcm/send";
        string SERVER_KEY = string.Empty;
        string SENDER_ID = string.Empty;

        private PushNotificationManager()
        {
        }

        public PushNotificationManager(string serverKey, string senderId)
        {
            SERVER_KEY = serverKey;
            SENDER_ID = senderId;
        }

        public async Task<bool> NotifyAsync(string deviceRegId, string title, string body)
        {
            try
            {
                var serverKey = $"key={SERVER_KEY}";
                var senderId = $"id={SENDER_ID}";

                var payload = new
                {
                    deviceRegId,
                    priority = "high",
                    notification = new { title, body }
                };

                var jsonBody = JsonConvert.SerializeObject(payload);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, URL))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine($"Error while sending notification. Status Code: {result.StatusCode}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception : {ex}");
            }

            return false;
        }
    }
}
