using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CmsApiSamples.Protocol.CMSApi;
using CmsClient;
using Google.ProtocolBuffers;
using SuperSocket.ClientEngine;
using WebSocket4Net;
using WebSocketClientNet;

namespace CmsApiSamples
{
    /// <summary>
    /// Proxy class that provides access to CMS API.
    /// </summary>
    /// <remarks>In current implementation supports only corpuscular messages.</remarks>
    public class CmsApiProxy : IDisposable
    {
        #region Fields

        /// <summary>
        /// 4 MByte. Default size from Microst.WebSockets.
        /// </summary>
        private const int MAX_MESSAGE_SIZE = 4194304;

        /// <summary>
        /// Websocket instance.
        /// </summary>
        private readonly WebSocket _clientSocket;

        private readonly MessageMatcherBase<ClientMessage, ServerMessage> _messageMatcher;
        private readonly ServerMessage.Builder _serverMessageBuilder;
        /// <summary>
        /// Flag, indicating if connection is open.
        /// </summary>
        private bool _isOpened;

        /// <summary>
        /// Messages queue to support async processing.
        /// </summary>
        private readonly ConcurrentQueue<Tuple<byte[], Exception>> _queue = new ConcurrentQueue<Tuple<byte[], Exception>>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize web socket.
        /// </summary>
        public CmsApiProxy(string webSocketsUrl)
        {
            _clientSocket = new WebSocket(webSocketsUrl);
            _clientSocket.AllowUnstrustedCertificate = true;
            _clientSocket.ReceiveBufferSize = MAX_MESSAGE_SIZE;
            _clientSocket.Opened += onClientSocketOpened;
            _clientSocket.Error += onClientSocketError;
            _clientSocket.DataReceived += onClientSocketDataReceived;
            _clientSocket.Closed += onClientSocketClosed;
            _serverMessageBuilder = ServerMessage.CreateBuilder();
            _messageMatcher = new MessageMatcher();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Handles Closed event.
        /// </summary>
        private void onClientSocketClosed(object sender, EventArgs e)
        {
            _isOpened = false;
        }

        /// <summary>
        /// Handles DataRecieved event.
        /// </summary>
        private void onClientSocketDataReceived(object sender, DataReceivedEventArgs e)
        {
            ServerMessage serverMessage;
            try
            {
                serverMessage = _serverMessageBuilder.Clone()
                    .MergeFrom(e.Data)
                    .Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Received messaged cannot be deserialized. {0}", ex);
                return;
            }

            TaskCompletionSource<ServerMessage> matchedTask = _messageMatcher.Match(serverMessage);
            if (matchedTask != null)
            {
                matchedTask.SetResult(serverMessage);
                return;
            }
            // If unknown/unexpected response was received log it and ignore it.
            Console.WriteLine("Received message cannot be matched with any ClientMessage. {0}", serverMessage);
        }

        /// <summary>
        /// Handles OnError event.
        /// </summary>
        private void onClientSocketError(object sender, ErrorEventArgs e)
        {
            // here we should process this error.
            _queue.Enqueue(new Tuple<byte[], Exception>(null, e.Exception));
        }

        /// <summary>
        /// Handles Opened event.
        /// </summary>
        private void onClientSocketOpened(object sender, EventArgs e)
        {
            _isOpened = true;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Sends client message and wait response.
        /// </summary>
        /// <param name="message">Client message which will be sent.</param>
        /// <returns>Server message response.</returns>
        public async Task<ServerMessage> SendMessage(ClientMessage message, bool responseExpected = true)
        {
            //Sends the message.
            if (_clientSocket.State != WebSocketState.Open
                && _clientSocket.State != WebSocketState.Connecting)
            {
                await OpenConnection();
            }
            TaskCompletionSource<ServerMessage> messageTask = new TaskCompletionSource<ServerMessage>();
            if (responseExpected)
            {
                _messageMatcher.Register(message, messageTask);
            }

            Console.WriteLine("The message {0} {1}", message.ToByteArray(), message);

            byte[] serializedMessage = message != null ? message.ToByteArray() : new byte[0];

            _clientSocket.Send(new List<ArraySegment<byte>> { new ArraySegment<byte>(serializedMessage) });

            //Returns server message.
            if (!responseExpected)
            {
                // When response is not expected task is considered completed right after send.
                messageTask.SetResult(null);
            }
            return await messageTask.Task;
        }

        /// <summary>
        /// Opens web socket connection to API.
        /// </summary>
        /// <returns>True if connection successfully opened.</returns>
        public async Task<bool> OpenConnection()
        {
            Task<bool> t = new Task<bool>(() =>
            {
                _clientSocket.Open();
                while (!_isOpened)
                {
                    Thread.Sleep(10);
                }
                return _isOpened;
            });
            t.Start();
            await t;

            return t.Result;
        }

        /// <summary>
        /// Disposes socket connection.
        /// </summary>
        public void Dispose()
        {
            if (_isOpened)
            {
                _clientSocket.Close();
                _isOpened = false;
            }
        }

        #endregion
    }
}

