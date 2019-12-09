using FirebasePushNotifications.Interfaces;

using Newtonsoft.Json;

namespace FirebasePushNotifications.Models
{
    public class BaseNotification : IBaseNotification
    {
        /// <summary>
        /// Indicates notification title.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Indicates notification body text.
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

		/// <summary>
		/// Indicates a sound to play when the device receives a notification. Sound files can be in the main bundle of the client app or in the Library/Sounds folder of the app's data container.
		/// See the iOS Developer Library for more information.
		/// </summary>
		[JsonProperty(PropertyName = "sound")]
		public string Sound { get; set; }
	}
}
