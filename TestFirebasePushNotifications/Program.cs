using FirebasePushNotifications;
using FirebasePushNotifications.Models;

using System;

namespace TestFirebasePushNotifications
{
    class Program
    {
        private static readonly string serverKey = "";

        static void Main(string[] args)
        {
            var deviceToken1 = "";
            var deviceToken2 = ""; 

            var deviceTokens = new string[] { deviceToken1, deviceToken2 };

            var client = new FCMClient(serverKey);

            var message = new Message()
            {
                RegistrationIds = deviceTokens,

                Notification = new AndroidNotification()
                {
                    Body = "Привет:)",
                    Title = "Заголовок",
                },
            };

            var result = client.Send(message);

            if (result.Exception == null)
            {
                Console.WriteLine($"Успешных отправок {result.MessageResult.SuccessfulTokens.Count}");
                foreach (var token in result.MessageResult.SuccessfulTokens)
                {
                    Console.WriteLine($"Успешные токены: {token}");
                }

                Console.Write($"Неудачных отправок: {result.MessageResult.FailedTokens.Count}");
                foreach (var token in result.MessageResult.FailedTokens)
                {
                    Console.WriteLine($"Неудачные токены: {token}");
                }
            }
            else
            {
                Console.WriteLine($"Ошибка отправки push-соощения: {result.Exception.Message}");
            }

            Console.ReadKey();
        }
    }
}
