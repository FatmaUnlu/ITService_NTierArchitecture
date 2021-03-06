using AutoMapper;
using ItServiceApp.Core.Identity;
using ItServiceApp.Core.Payment;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MUsefulMethods;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ItServiceApp.Business.Services.Payment
{
    public class IyzikoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IyzikoPaymentOptions _options;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public IyzikoPaymentService(IConfiguration configuration, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            var section = configuration.GetSection(IyzikoPaymentOptions.Key);

            _options = new IyzikoPaymentOptions()
            {
                ApiKey = section["ApiKey"],
                SecretKey = section["SecretKey"],
                BaseUrl = section["BaseUrl"],
                ThreedsCallbackUrl = section["ThreedsCallbackUrl"],
            };
        }

        private string GenerateConversationId()
        {
            return StringHelpers.GenerateUniqueCode();
        }

        private CreatePaymentRequest InitialPaymentRequest(PaymentModel model)
        {
            var paymentRequest = new CreatePaymentRequest
            {

                Installment = model.Installment,
                Locale = Locale.TR.ToString(),
                ConversationId = GenerateConversationId(),
                Price = model.Price.ToString(new CultureInfo("en-US")),
                PaidPrice = model.PaidPrice.ToString(new CultureInfo("en-US")),
                Currency = Currency.TRY.ToString(),
                BasketId = StringHelpers.GenerateUniqueCode(),
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.SUBSCRIPTION.ToString(),
                PaymentCard = _mapper.Map<PaymentCard>(model.CardModel),
                Buyer = _mapper.Map<Buyer>(model.CustomerModel),
                BillingAddress = _mapper.Map<Address>(model.AddressModel)
            };

            var basketItems = new List<BasketItem>();

            foreach (var basketModel in model.BasketModel)
            {
                basketItems.Add(_mapper.Map<BasketItem>(basketModel));
            }

            paymentRequest.BasketItems = basketItems;

            //var buyer = new Buyer
            //{
            //    Id = user.Id,
            //    Name = user.Name,
            //    Surname = user.Surname,
            //    GsmNumber = user.PhoneNumber,
            //    Email = user.Email,
            //    IdentityNumber = "11111111110",
            //    LastLoginDate = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}",
            //    RegistrationDate = $"{user.CreatedDate:yyyy-MM-dd HH:mm:ss}",
            //    RegistrationAddress = "Cihannüma Mah. Barbaros Bulvarı No:9 Beşiktaş",
            //    Ip = model.Ip,
            //    City = "Istanbul",
            //    Country = "Turkey",
            //    ZipCode = "34732",
            //    //request.Buyer = buyer
            //};


            //Address billingAddress = new Address //faturalandırma adresi
            //    {
            //        ContactName = $"{user.Name} {user.Surname}",
            //        City = "Istanbul",
            //        Country = "Turkey",
            //        Description = "Cihannüma Mah. Barbaros Bulvarı No:9 Beşiktaş",
            //        ZipCode = "34732",
            //    };
            //    paymentRequest.BillingAddress = billingAddress;


            //var firstBasketItem = new BasketItem
            //{
            //    Id = "BI101",
            //    Name = "Binocular",

            //    Category1 = "Collectibles",
            //    Category2 = "Accessories",
            //    ItemType = BasketItemType.VIRTUAL.ToString(),
            //    Price = model.Price.ToString(new CultureInfo("en-US"))
            //};
            //basketItems.Add(firstBasketItem);
            //

            return paymentRequest;
        }
        public InstallmentModel CheckInstallments(string binNumber, decimal price)
        {
            var conversationId = GenerateConversationId();
            var request = new RetrieveInstallmentInfoRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = conversationId,
                BinNumber = binNumber.Substring(0,6),
                Price = price.ToString(new CultureInfo("en-US"))
            };
            var result = InstallmentInfo.Retrieve(request, _options);
            if (result.Status == "failure")
            {
                throw new Exception(result.ErrorMessage);
            }
            if (result.ConversationId != conversationId)
            {
                throw new Exception("Hatalı istek oluturuldu");

            }
            var resultModel = _mapper.Map<InstallmentModel>(result.InstallmentDetails[0]);
            Console.WriteLine();
            return resultModel;
        }



        public PaymentResponseModel Pay(PaymentModel model)
        {
            var request = this.InitialPaymentRequest(model);
            var payment = Iyzipay.Model.Payment.Create(request, _options);

            return _mapper.Map<PaymentResponseModel>(payment);
        }
    }

    internal class PaymnentResponseModel
    {
    }
}
