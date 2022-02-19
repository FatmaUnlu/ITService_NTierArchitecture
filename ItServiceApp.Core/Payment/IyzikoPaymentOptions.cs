using Iyzipay;

namespace ItServiceApp.Core.Payment
{ 
    public class IyzikoPaymentOptions :Options
    {
        public const string Key = "IyzikoOptions"; 
        public string  ThreedsCallbackUrl { get; set; }
    }
}
