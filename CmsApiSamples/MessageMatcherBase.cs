using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketClientNet
{
    /// <summary>
    /// Matches received messages with tasks that are related to sent messages.
    /// </summary>
    public abstract class MessageMatcherBase<TClientMessage, TServerMessage>
    {
        // List of sent messages for that response is expected.
        private readonly List<Tuple<TClientMessage, TaskCompletionSource<TServerMessage>>> _sentMessagesTasks =
            new List<Tuple<TClientMessage, TaskCompletionSource<TServerMessage>>>();

        /// <summary>
        /// Registers message and related task in list of messages for matching.
        /// </summary>
        public void Register(TClientMessage message, TaskCompletionSource<TServerMessage> task)
        {
            _sentMessagesTasks.Add(new Tuple<TClientMessage, TaskCompletionSource<TServerMessage>>(message, task));
        }

        /// <summary>
        /// Returns task that is related to previously registered ClientMessage with wich provided ServerMessage is matched.
        /// </summary>
        public TaskCompletionSource<TServerMessage> Match(TServerMessage message)
        {
            Tuple<TClientMessage, TaskCompletionSource<TServerMessage>> match =
                _sentMessagesTasks.SingleOrDefault(m => isMatched(message, m.Item1));

            if (match != null)
            {
                _sentMessagesTasks.Remove(match);
                return match.Item2;
            }
            return null;
        }

        protected abstract bool isMatched(TServerMessage receivedMessage, TClientMessage sentMessage);
    }
}