using System;
using System.Text.RegularExpressions;
using TestServer.Interfaces;

namespace TestServer.Services
{
    public class ProtocolParserService : IProtocolParser
    {
        public MessageType GetMessageType(string message)
        {
            var pattern = new Regex(@"Type=([a-zA-Z]+)");
            var match = pattern.Match(message);
            var type = match.Groups[1].Value;

            MessageType typeEnum;
            return Enum.TryParse(type, true, out typeEnum) ? typeEnum : MessageType.Unknown;
        }

        public string GetBody(string message)
        {
            var pattern = new Regex("Body=([a-zA-Z0-9\\s\\t]+)");
            var match = pattern.Match(message);
            var body = match.Groups[1].Value;

            return body;
        }


    }
}
