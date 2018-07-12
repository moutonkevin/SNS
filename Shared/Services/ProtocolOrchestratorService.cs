using System.Net.Sockets;
using Shared.Interfaces;

namespace Shared.Services
{
    public class ProtocolOrchestratorService : IProtocolOrchestrator
    {
        private readonly IProtocolInterpreter _protocolInterpreter;
        private readonly IProtocolParser _protocolParser;

        public ProtocolOrchestratorService(IProtocolParser protocolParser, IProtocolInterpreter protocolInterpreter)
        {
            _protocolParser = protocolParser;
            _protocolInterpreter = protocolInterpreter;
        }

        public bool Process(TcpClient socket, string message)
        {
            var requestType = _protocolParser.GetMessageType(message);
            var body = _protocolParser.GetBody(message);

            _protocolInterpreter.Interpret(socket, requestType, body);

            return true;
        }
    }
}