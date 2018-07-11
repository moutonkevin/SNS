using System.Net.Sockets;
using TestServer.Interfaces;

namespace TestServer.Services
{
    public class ProtocolOrchestratorService : IProtocolOrchestrator
    {
        private readonly IProtocolParser _protocolParser;
        private readonly IProtocolInterpreter _protocolInterpreter;

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
