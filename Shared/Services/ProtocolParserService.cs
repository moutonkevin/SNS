using System;
using System.Text.RegularExpressions;
using Shared.Interfaces;

namespace Shared.Services
{
    public class ProtocolParserService : IProtocolParser
    {
        public MessageType GetMessageType(string message)
        {
            var pattern = new Regex(@"Type=([a-zA-Z]+)");
            var match = pattern.Match(message);
            var type = match.Groups[1].Value;

            MessageType typeEnum;

            var isSucess = Enum.TryParse(type, true, out typeEnum);

            Console.WriteLine($" >> The message type received is [{typeEnum.ToString()}]");

            return isSucess ? typeEnum : MessageType.Unknown;
        }

        public string GetBody(string message)
        {
            var pattern = new Regex("Body=([a-zA-Z0-9\\s\\t-]+)");
            var match = pattern.Match(message);
            var body = match.Groups[1].Value;

            Console.WriteLine($" >> The message body received is [{body}]");

            return body;
        }
    }
}