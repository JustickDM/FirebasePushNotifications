using FirebasePushNotifications.Interfaces;
using FirebasePushNotifications.Models;
using FirebasePushNotifications.Serialization;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FirebasePushNotifications
{
    public class FCMClient
    {
        private const string _firebaseGoogleUrl = "https://fcm.googleapis.com/fcm/send";

        private ISerializer _serializer;

        private string _serverKey;
        private string _senderId;

        public FCMClient(string serverKey, ISerializer serializer, string senderId = null)
        {
            _serverKey = serverKey;
            _senderId = senderId ?? null;
            _serializer = serializer;
        }

        public FCMClient(string serverKey, string senderId = null) : this(serverKey, new CustomJsonSerializer())
        {

        }

        public Response Send(Message message)
        {
            var tokensArray = message.RegistrationIds.ToArray() ?? new string[0];

            var request = WebRequest.Create(_firebaseGoogleUrl);
            request.Method = "post";
            request.Headers.Add(string.Format("Authorization: key={0}", _serverKey));
            if (_senderId != null)
            {
                request.Headers.Add(string.Format("Sender: id={0}", _senderId));
            }
            request.ContentType = "application/json";

            var postBody = _serializer.Serialize(message);
            var byteArray = Encoding.UTF8.GetBytes(postBody);

            request.ContentLength = byteArray.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                try
                {
                    var tResponse = request.GetResponse();
                    using (var dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null)
                        {
                            using (var tReader = new StreamReader(dataStreamResponse))
                            {
                                var sResponseFromServer = tReader.ReadToEnd();
                                var response = _serializer.Deserialize<MessageResponse>(sResponseFromServer);

                                var failedTokens = FindFailedTokens(response, tokensArray).ToArray();
                                var successfulTokens = tokensArray.Where(x => !failedTokens.Contains(x)).ToArray();

                                response.FailedTokens = failedTokens;
                                response.SuccessfulTokens = successfulTokens;

                                return new Response() { MessageResult = response, Exception = null };
                            }
                        }
                    }
                }
                catch (WebException e)
                {
                    return new Response() { MessageResult = null, Exception = e };
                }
            }

            return new Response() { MessageResult = null, Exception = null }; ;
        }

        private IEnumerable<string> FindFailedTokens(MessageResponse response, string[] tokensArray)
        {
            for (var i = 0; i < response.Results.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(response.Results.ElementAt(i).Error))
                {
                    yield return tokensArray[i];
                }
            }
        }
    }
}
