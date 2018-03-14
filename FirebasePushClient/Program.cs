using System;

namespace FirebasePushClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string SERVER_KEY = "YourServerKey";
            const string SENDER_ID = "YourSenderId";

            string deviceRegId = "Your Device Registration Id";
            string title = "Your Title";
            string message = "Your Message";

            PushNotificationManager pushNotificationManager = new PushNotificationManager(SERVER_KEY, SENDER_ID);
            var response = pushNotificationManager.NotifyAsync(deviceRegId, title, message);
            Console.WriteLine(response.Result);
            Console.ReadKey();
        }
    }
}
