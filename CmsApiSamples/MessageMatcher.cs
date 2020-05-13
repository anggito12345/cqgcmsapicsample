using System.Linq;
using CmsApiSamples.Protocol.CMSApi;
using WebSocketClientNet;

namespace CmsClient
{
    internal class MessageMatcher : MessageMatcherBase<ClientMessage, ServerMessage>
    {
        protected override bool isMatched(ServerMessage receivedMessage, ClientMessage sentMessage)
        {
            if (receivedMessage.HasLogonResult && sentMessage.HasLogon
                || receivedMessage.HasLoggedOff && sentMessage.HasLogoff)
            {
                // There can be only one logon/logoff message.
                return true;
            }

            if (receivedMessage.OperationResultCount == 1 && sentMessage.OperationRequestCount == 1 &&
                receivedMessage.OperationResultList.Single().RequestId ==
                sentMessage.OperationRequestList.Single().Id)
            {
                return true;
            }

            if (receivedMessage.InformationResultCount == 1 && sentMessage.InformationRequestCount == 1 &&
                receivedMessage.InformationResultList.Single().RequestId ==
                sentMessage.InformationRequestList.Single().Id)
            {
                return true;
            }

            if (receivedMessage.SearchResultCount == 1 && sentMessage.SearchRequestCount == 1 &&
                receivedMessage.SearchResultList.Single().RequestId ==
                sentMessage.SearchRequestList.Single().Id)
            {
                return true;
            }

            if (receivedMessage.TradeRoutingResultCount == 1 && sentMessage.TradeRoutingRequestCount == 1 &&
                receivedMessage.TradeRoutingResultList.Single().RequestId ==
                sentMessage.TradeRoutingRequestList.Single().Id)
            {
                return true;
            }

            return false;
        }
    }
}