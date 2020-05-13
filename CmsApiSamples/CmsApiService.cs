using CmsApiSamples.Services;

namespace CmsApiSamples
{
    /// <summary>
    /// Common class to work with API.
    /// </summary>
    public class CmsApiService
    {
        private readonly CmsApiProxy _apiProxy;

        #region Properties

        /// <summary>
        /// Accounts-related API service.
        /// </summary>
        public AccountsService AccountsService { get; private set; }

        /// <summary>
        /// Orders-related API service.
        /// </summary>
        public OrdersService OrdersService { get; private set; }

        /// <summary>
        /// Session-related API service.
        /// </summary>
        public SessionService SessionService { get; private set; }

        /// <summary>
        /// Information-related API service.
        /// </summary>
        public InformationService InformationService { get; private set; }

        /// <summary>
        /// Search-related API service.
        /// </summary>
        public SearchService SearchService { get; private set; }

        /// <summary>
        /// Operations-related API service.
        /// </summary>
        public OperationsService OperationsService { get; private set; }

        #endregion

        /// <summary>
        /// Initializes instance of <see cref="CmsApiProxy"/> class.
        /// </summary>
        /// <param name="webSocketsUrl">Web socket URL.</param>
        public CmsApiService(string webSocketsUrl)
        {
            _apiProxy = new CmsApiProxy(webSocketsUrl);
            AccountsService = new AccountsService(_apiProxy);
            OrdersService = new OrdersService(_apiProxy);
            SessionService = new SessionService(_apiProxy);
            InformationService = new InformationService(_apiProxy);
            SearchService = new SearchService(_apiProxy);
            OperationsService = new OperationsService(_apiProxy);
        }

        /// <summary>
        /// Calls dispose of CmsApiProxy instance.
        /// </summary>
        public void CloseWebSocket()
        {
            if (_apiProxy != null)
            {
                _apiProxy.Dispose();
            }
        }
    }
}
