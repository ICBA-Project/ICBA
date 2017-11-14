using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace ICBA.Services
{
    public static class SlackService
    {
        private static readonly Uri _uri = new Uri(@"https://hooks.slack.com/services/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");
        private const string SlackUsername = "ICBA-Sensors-Information";
        private static readonly string welcomingMessage = $"I will be reporting anything out of the ordinary happening with your sensors!";
        public static string valueBelowMin = "Current value of the sensor is below the set Minimum";
        public static string valueAboveMax = "Current value of the sensor is above the set Maximum";
        private const string SlackChannel = "#sensorsfeedback";
        private const string WebExceptionString = "Bad WebClient";

        public static void PostMessage(string text, string username = SlackUsername, string channel = SlackChannel)
        {
            Payload payload = new Payload()
            {
                Channel = channel,
                Username = username,
                Text = text
            };
            PostMessage(payload);
        }
        public static void PostMessage(Payload payload)
        {
            try
            {
                string payloadJson = JsonConvert.SerializeObject(payload);
                using (WebClient client = new WebClient())
                {
                    NameValueCollection data = new NameValueCollection
                    {
                        ["payload"] = payloadJson
                    };
                    var response = client.UploadValues(_uri, "POST", data);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(WebExceptionString);
                throw ex;
            }
        }
        public static void PostWelcomingMessage()
        {
            PostMessage(username: SlackUsername,
                               text: welcomingMessage,
                               channel: SlackChannel);
        }
        public class Payload
        {
            [JsonProperty("channel")]
            public string Channel { get; set; }
            [JsonProperty("username")]
            public string Username { get; set; }
            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}
