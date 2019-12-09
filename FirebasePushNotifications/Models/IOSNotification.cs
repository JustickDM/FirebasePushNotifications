using Newtonsoft.Json;

namespace FirebasePushNotifications.Models
{
	public class IOSNotification: BaseNotification
	{
        /// <summary>
        /// Indicates the badge on the client app home icon.
        /// </summary>
        [JsonProperty(PropertyName = "badge")]
        public string Badge { get; set; }

        /// <summary>
        /// Indicates the action associated with a user click on the notification. Corresponds to category in the APNs payload.
        /// </summary>
        [JsonProperty(PropertyName = "click_action")]
        public string ClickAction { get; set; }

        /// <summary>
        /// Indicates the key to the body string for localization. Corresponds to "loc-key" in the APNs payload.
        /// </summary>
        [JsonProperty(PropertyName = "body_loc_key")]
        public string BodyLocKey { get; set; }

        /// <summary>
        /// Indicates the string value to replace format specifiers in the body string for localization. Corresponds to "loc-args" in the APNs payload.
        /// </summary>
        [JsonProperty(PropertyName = "body_loc_args")]
        public string BodyLocArgs { get; set; }


        /// <summary>
        /// Indicates the key to the title string for localization. Corresponds to "title-loc-key" in the APNs payload.
        /// </summary>
        [JsonProperty(PropertyName = "title_loc_key")]
        public string TitleLocKey { get; set; }


        /// <summary>
        /// Indicates the string value to replace format specifiers in the title string for localization.Corresponds to "title-loc-args" in the APNs payload.
        /// </summary>
        [JsonProperty(PropertyName = "title_loc_args")]
        public string TitleLocArgs { get; set; }
    }
}
