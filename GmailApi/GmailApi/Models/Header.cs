using System;
using Newtonsoft.Json;

namespace GmailApi.Models
{
    public class Header
    {
        public Header()
        {
            Value = string.Empty;
        }

        /// <summary>
        /// The name of the header before the : separator. For example, To.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Returns wehter the header is an extension header, which starts with 'X-'.
        /// </summary>
        public bool IsExtensionHeader
        {
            get { return Name.StartsWith("X-", StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// The value of the header after the : separator. For example, someuser@example.com.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Get the Internet Message Format (IMF) header if valid.
        /// </summary>
        public HeaderName ImfHeader
        {
            get
            {
                return JsonConvert.DeserializeObject<HeaderName>(string.Concat("\"", Name, "\""));
            }
        }

        public override string ToString()
        {
            return string.Concat(Name, ": ", Value);
        }
    }
}