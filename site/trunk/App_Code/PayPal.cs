using PayPalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace EShop
{
    public static class PayPal
    {
        static string APIUserName
        {
            get
            {
                return "";
            }
        }
        static string APIPassword
        {
            get
            {
                return "";
            }
        }
        static string APISignature
        {
            get
            {
                return "";
            }
        }
        static string EndpointUrl
        {
            get
            {
                return "https://api-3t.paypal.com/2.0/";
            }
        }
        static public OrderInfo Charge(string firstName, string lastName, string address1, string address2, string city, string state, string zip, string country, string ccType, string ccNumber, string ccAuthCode,
            int ccExpireYear, int ccExpireMonth, decimal priceFinal, decimal priceTax, decimal priceShipping)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            CreditCardTypeType cardType = CreditCardTypeType.Visa;
            switch(ccType)
            {
                case "Visa":
                case "MasterCard":
                case "Discover":
                    cardType = (CreditCardTypeType)Enum.Parse(typeof(CreditCardTypeType), ccType.ToString());
                    break;
                case "AMEX":
                    cardType = CreditCardTypeType.Amex;
                    break;
                default:
                    cardType = CreditCardTypeType.Visa;
                    break;
            }
            CountryCodeType countryCode = CountryCodeType.US;
            try
            { countryCode = (CountryCodeType)Enum.Parse(typeof(CountryCodeType), country); }
            catch
            { countryCode = (CountryCodeType)Enum.Parse(typeof(CountryCodeType), Robo.Country.NameToCode(country)); }
            finally { countryCode = CountryCodeType.US; }

            using (var client = new PayPalAPIAAInterfaceClient(new BasicHttpsBinding(), new EndpointAddress(EndpointUrl)))
            {
                var credentials = new CustomSecurityHeaderType()
                {
                    Credentials = new UserIdPasswordType()
                    {
                        Username = APIUserName,
                        Password = APIPassword,
                        Signature = APISignature
                    }
                };
                DoDirectPaymentReq req = new DoDirectPaymentReq()
                {
                    DoDirectPaymentRequest = new DoDirectPaymentRequestType()
                    {
                        Version = "121.0",
                        DoDirectPaymentRequestDetails = new DoDirectPaymentRequestDetailsType()
                        {
                            CreditCard = new CreditCardDetailsType()
                            {
                                CardOwner = new PayerInfoType()
                                {
                                    PayerName = new PersonNameType()
                                    {
                                        FirstName = firstName,
                                        LastName = lastName
                                    },
                                    Address = new AddressType()
                                    {
                                        CityName = city,
                                        StateOrProvince = state,
                                        PostalCode = zip,
                                        Country = countryCode,
                                        Street1 = address1,
                                        Street2 = address2
                                    }
                                },
                                CreditCardNumber = ccNumber,
                                CreditCardType = cardType,
                                CreditCardTypeSpecified = true,
                                CVV2 = ccAuthCode,
                                ExpMonth = ccExpireMonth,
                                ExpMonthSpecified = true,
                                ExpYear = ccExpireYear,
                                ExpYearSpecified = true
                            },
                            PaymentAction = PaymentActionCodeType.Sale,
                            PaymentDetails = new PaymentDetailsType()
                            {
                                OrderTotal = new BasicAmountType()
                                {
                                    currencyID = CurrencyCodeType.USD,
                                    Value = priceFinal.ToString("F2")
                                },
                                ShippingTotal = new BasicAmountType()
                                {
                                    currencyID = CurrencyCodeType.USD,
                                    Value = priceShipping.ToString("F2")
                                },
                                TaxTotal = new BasicAmountType()
                                {
                                    currencyID = CurrencyCodeType.USD,
                                    Value = priceTax.ToString("F2")
                                }
                            }
                        }
                    }
                };
                var response = client.DoDirectPayment(ref credentials, req);
                // check errors
                string errors = CheckErrors(response);
                OrderInfo info = new OrderInfo();
                if (errors == String.Empty)
                {
                    info.Ack = response.Ack.ToString();
                    info.TransactionID = response.TransactionID;
                    info.CVV2Code = response.CVV2Code;
                }
                else
                {
                    info.Ack = errors;
                }
                return info;
            }
        }
        public static OrderInfo Charge(decimal total, string PayPalToken, string payerId)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (var client = new PayPalAPIAAInterfaceClient(new BasicHttpsBinding(), new EndpointAddress(EndpointUrl)))
            {
                var credentials = new CustomSecurityHeaderType()
                {
                    Credentials = new UserIdPasswordType()
                    {
                        Username = APIUserName,
                        Password = APIPassword,
                        Signature = APISignature
                    }
                };
                DoExpressCheckoutPaymentReq req = new DoExpressCheckoutPaymentReq()
                {
                    DoExpressCheckoutPaymentRequest = new DoExpressCheckoutPaymentRequestType()
                    {
                        Version = "121.0",
                        DoExpressCheckoutPaymentRequestDetails = new DoExpressCheckoutPaymentRequestDetailsType()
                        {
                            PaymentAction = PaymentActionCodeType.Sale,
                            PaymentActionSpecified = true,
                            Token = PayPalToken,
                            PayerID = payerId,
                            PaymentDetails = new PaymentDetailsType[1] {
                                new PaymentDetailsType()
                                {
                                    OrderTotal = new BasicAmountType()
                                    {
                                        currencyID = CurrencyCodeType.USD,
                                        Value = total.ToString("F2")
                                    },
                                    ShippingTotal = new BasicAmountType()
                                    {
                                        currencyID = CurrencyCodeType.USD,
                                        Value = "0.00"
                                    },
                                    TaxTotal = new BasicAmountType()
                                    {
                                        currencyID = CurrencyCodeType.USD,
                                        Value = "0.00"
                                    },
                                    ItemTotal = new BasicAmountType()
                                    {
                                        currencyID = CurrencyCodeType.USD,
                                        Value = total.ToString("F2")
                                    }
                                }
                            }
                        }
                    }
                };
                DoExpressCheckoutPaymentResponseType resp = client.DoExpressCheckoutPayment(ref credentials, req);
                string errors = CheckErrors(resp);
                OrderInfo info = new OrderInfo();
                if (errors == String.Empty)
                {
                    info.Ack = resp.Ack.ToString();
                    info.TransactionID = resp.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].TransactionID;
                    info.ReceiptID = resp.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].ReceiptID;
                }
                else
                {
                    info.Ack = errors;
                }
                return info;
            }
        }
        public static void GoToPayPalSite(string email, double totalItemsPrice)
        {
            string host = HttpContext.Current.Request.Url.Host;
            string location = HttpContext.Current.Request.Url.Scheme + "://" + host;
            if (host.Contains("localhost"))
                location += ":" + HttpContext.Current.Request.Url.Port;

            string cancelUrl = location + "/Cart";

            string token = SetExpressCheckout(email, totalItemsPrice, location, cancelUrl, true, true);
            if (token.StartsWith("ERROR"))
                throw new Exception(token);

            string url = "";

            url = "https://www.paypal.com/cgi-bin/webscr?" + "cmd=_express-checkout&token=" + token;

            HttpContext.Current.Response.Redirect(url);
        }
        public static PayerInfoType GetExpressCheckout(string token)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (var client = new PayPalAPIAAInterfaceClient(new BasicHttpsBinding(), new EndpointAddress(EndpointUrl)))
            {
                var credentials = new CustomSecurityHeaderType()
                {
                    Credentials = new UserIdPasswordType()
                    {
                        Username = APIUserName,
                        Password = APIPassword,
                        Signature = APISignature
                    }
                };
                GetExpressCheckoutDetailsReq req = new GetExpressCheckoutDetailsReq()
                {
                    GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType()
                    {
                        Version = "121.0",
                        Token = token
                    }
                };
                var resp = client.GetExpressCheckoutDetails(ref credentials, req);
                string errors = CheckErrors(resp);
                if (errors == String.Empty)
                    return resp.GetExpressCheckoutDetailsResponseDetails.PayerInfo;
                return new PayerInfoType() { PayerID = errors };
            }
        }
        public static string SetExpressCheckout(string email, double total, string returnUrl, string cancelUrl, bool allowGuestCheckout, bool showCreditCardAndAddress)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (var client = new PayPalAPIAAInterfaceClient(new BasicHttpsBinding(), new EndpointAddress(EndpointUrl)))
            {
                var credentials = new CustomSecurityHeaderType()
                {
                    Credentials = new UserIdPasswordType()
                    {
                        Username = APIUserName,
                        Password = APIPassword,
                        Signature = APISignature
                    }
                };
                SetExpressCheckoutReq req = new SetExpressCheckoutReq()
                {
                    SetExpressCheckoutRequest = new SetExpressCheckoutRequestType()
                    {
                        Version = "121.0",
                        SetExpressCheckoutRequestDetails = new SetExpressCheckoutRequestDetailsType()
                        {
                            BuyerEmail = email,
                            OrderTotal = new BasicAmountType()
                            {
                                currencyID = CurrencyCodeType.USD,
                                Value = total.ToString()
                            },
                            ReturnURL = returnUrl,
                            CancelURL = cancelUrl,
                            SolutionType = allowGuestCheckout ? SolutionTypeType.Sole : SolutionTypeType.Mark,
                            SolutionTypeSpecified = allowGuestCheckout,
                            LandingPage = LandingPageType.Billing,
                            LandingPageSpecified = showCreditCardAndAddress
                        }
                    }
                };
                var resp = client.SetExpressCheckout(ref credentials, req);
                string errors = CheckErrors(resp);
                if (errors == String.Empty)
                    return resp.Token;
                else return errors;
            }
        }
        private static string CheckErrors(AbstractResponseType resp)
        {
            string text = "";
            if (resp.Ack != AckCodeType.Success)
            { }
            string result;
            if (resp.Errors != null)
            {
                if (resp.Errors.Length <= 0)
                {
                    result = text;
                    return result;
                }
                text = "ERROR: ";
                for (int i = 0; i < resp.Errors.Length; i++)
                {
                    string text2 = text;
                    text = string.Concat(new string[] { text2, resp.Errors[i].LongMessage, " (", resp.Errors[i].ErrorCode, ")", Environment.NewLine });
                }
            }
            result = text;
            return result;
        }
    }
    public class OrderInfo
    {
        public string Ack;

        public string CVV2Code;

        public string ReceiptID;

        public string TransactionID;
    }
}