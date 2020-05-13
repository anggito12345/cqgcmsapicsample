using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsApiSamples.Protocol.Extensions;
using CmsApiSamples.Protocol.CMSApi;
using System;

namespace CmsApiSamples.Services
{
    /// <summary>
    /// Class to demonstrate usage of Operation related CMS API messages.
    /// </summary>
    public sealed class OperationsService
    {
        private readonly CmsApiProxy _api;

        public OperationsService(CmsApiProxy api)
        {
            _api = api;
        }

        /// <summary>
        /// Clones user.
        /// </summary>
        public async Task<ServiceResult> CloneUser(uint requestId, string sourceUserId, string newUserName,
            string newUserFirstName, string newUserLastName, string newUserMiddleInitial,
            string addressCountry, string addressState, string addressCity, string addressZip, string address, string addressAddress2,
            IReadOnlyCollection<string> emails, IReadOnlyCollection<string> phones, IReadOnlyCollection<string> faxes)
        {
            Address newUserAddress = Address.CreateBuilder()
                .SetCountry(addressCountry)
                .SetIfNotNull(addressState, b => b.SetState)
                .SetIfNotNull(addressCity, b => b.SetCity)
                .SetIfNotNull(addressZip, b => b.SetZip)
                .SetIfNotNull(address, b => b.SetAddress_)
                .SetIfNotNull(addressAddress2, b => b.SetAddress2)
                .Build();

            ContactInformation newUserContactInformation = ContactInformation.CreateBuilder()
                .SetIf(!emails.IsNullOrEmpty(),
                       b => b.AddRangeEmail(emails.Select(email => Email.CreateBuilder()
                                                                       .SetIfNotNull(email, x => x.SetEmail_)
                                                                       .Build())))
                .SetIf(!phones.IsNullOrEmpty(),
                       b => b.AddRangePhone(phones.Select(phone => Phone.CreateBuilder()
                                                                       .SetIfNotNull(phone, x => x.SetNumber)
                                                                       .Build())))
                .SetIf(!faxes.IsNullOrEmpty(),
                       b => b.AddRangeFax(faxes.Select(fax => Phone.CreateBuilder()
                                                                 .SetIfNotNull(fax, x => x.SetNumber)
                                                                 .Build())))
                .Build();

            CloneUser cloneUser = Protocol.CMSApi.CloneUser.CreateBuilder()
                .SetSourceUserId(sourceUserId)
                .SetNewUserUsername(newUserName)
                .SetNewUserFirstName(newUserFirstName)
                .SetNewUserLastName(newUserLastName)
                .SetIfNotNull(newUserMiddleInitial, b => b.SetNewUserMiddleInitial)
                .SetNewUserAddress(newUserAddress)
                .SetNewUserContactInformation(newUserContactInformation)
                .Build();

            ClientMessage clientMessage = createClientMessage(requestId, cloneUser);
            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        private ClientMessage createClientMessage(uint requestId, CloneUser cloneUser)
        {
            OperationRequest operationRequest = OperationRequest.CreateBuilder()
                .SetCloneUser(cloneUser)
                .SetId(requestId)
                .Build();

            //Initialize client message.
            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddOperationRequest(operationRequest)
                .Build();

            return clientMessage;
        }
        public async Task<ServiceResult> updateBalanceRequestService(uint requestId)
        {
            UpdateBalanceRecord updateBalanceRecord = UpdateBalanceRecord.CreateBuilder()
               .SetBalanceId(215077617)
               .SetEndCashBalance(-201)
               .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder().SetUpdateBalanceRecord(updateBalanceRecord).Build();

            TradeRoutingRequest tradeRoutingRequest = Protocol.CMSApi.TradeRoutingRequest.CreateBuilder()
               .SetId(requestId)
               .SetAccountScopeRequest(accountScopeRequest)
               .Build();

            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddTradeRoutingRequest(tradeRoutingRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        public async Task<ServiceResult> createBalanceRequestService(uint requestId)
        {
            CreateBalanceRecord createBalanceRecord = CreateBalanceRecord.CreateBuilder().SetAccountId(17004670)
                .SetCurrency("EUR")
                .SetEndCashBalance(1000)
                .Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder().SetCreateBalanceRecord(createBalanceRecord).Build();

            TradeRoutingRequest tradeRoutingRequest = Protocol.CMSApi.TradeRoutingRequest.CreateBuilder()
               .SetId(requestId)
               .SetAccountScopeRequest(accountScopeRequest)
               .Build();

            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddTradeRoutingRequest(tradeRoutingRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        public async Task<ServiceResult> balanceRequestService(uint requestId)
        {
            BalanceRecordsRequest balanceRecordsRequest = BalanceRecordsRequest.CreateBuilder().SetAccountId(17004670).Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder().SetBalanceRecordsRequest(balanceRecordsRequest).Build();

            TradeRoutingRequest tradeRoutingRequest = Protocol.CMSApi.TradeRoutingRequest.CreateBuilder()
               .SetId(requestId)
               .SetAccountScopeRequest(accountScopeRequest)
               .Build();

            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddTradeRoutingRequest(tradeRoutingRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }

        public async Task<ServiceResult> updateAccountService(uint requestId)
        {
            AccountInfoRequest accountInfoRequest = AccountInfoRequest.CreateBuilder().SetAccountId(17004670).Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder().SetAccountInfoRequest(accountInfoRequest).Build();

            TradeRoutingRequest tradeRoutingRequest = Protocol.CMSApi.TradeRoutingRequest.CreateBuilder()
               .SetId(requestId)
               .SetAccountScopeRequest(accountScopeRequest).Build();

            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddTradeRoutingRequest(tradeRoutingRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);            

            return new ServiceResult(clientMessage, serverMessage);

        }
        public async Task<ServiceResult> createAccountService(uint requestId)
        {            

            Account account = Protocol.CMSApi.Account.CreateBuilder().SetName("SIMRahul")
                .SetCustomerId("16894552")
                .SetCurrency("SGD")
                .SetBrokerageAccountNumber("PStet1234567")
                .SetAccountTypeId("0")
                .SetSalesSeriesId("2100467")
                .SetClass(1)    
                .Build();

            CreateAccount createAccount = Protocol.CMSApi.CreateAccount.CreateBuilder().SetAccount(account).Build();

            AccountScopeRequest accountScopeRequest = AccountScopeRequest.CreateBuilder().SetCreateAccount(createAccount).Build();

            TradeRoutingRequest tradeRoutingRequest = Protocol.CMSApi.TradeRoutingRequest.CreateBuilder()
                .SetId(requestId)
                .SetAccountScopeRequest(accountScopeRequest).Build();

            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddTradeRoutingRequest(tradeRoutingRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            Console.WriteLine("ID:" + serverMessage.GetTradeRoutingResult(0).AccountScopeResult.CreateAccountResult.Id);

            return new ServiceResult(clientMessage, serverMessage);
        }

        public async Task<ServiceResult> createCustomerMethod(uint requestId)
        {
            

            Customer customer = Protocol.CMSApi.Customer.CreateBuilder().SetName("Rahul").SetBrokerageId("2571").SetLegalType(1).Build();


            CreateCustomer createCustomer = Protocol.CMSApi.CreateCustomer.CreateBuilder().SetCustomer(customer).Build();            

            OperationRequest operationRequest = OperationRequest.CreateBuilder()
                .SetId(requestId)
                .SetCreateCustomer(createCustomer)                
                .Build();

            ClientMessage clientMessage = ClientMessage.CreateBuilder()
                .AddOperationRequest(operationRequest)
                .Build();

            ServerMessage serverMessage = await _api.SendMessage(clientMessage);

            return new ServiceResult(clientMessage, serverMessage);
        }
    }
}