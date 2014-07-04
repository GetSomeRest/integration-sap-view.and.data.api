using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AdnWebAPI.Models
{
    public enum kCurrencyCodeEnum
    {
        kUnknown = -1,
        kEUR = 0,
        kUSD = 1,
        kJPY = 2,
        kMXN = 3,
        kARS = 4,
        kGBP = 5,
        kCAD = 6,
        kBRL = 7,
        kCHF = 8,
        kZAR = 9,
        kINR = 10,
        kPLN = 11,
        kCNY = 12,
        kDKK = 13,
        kRUB = 14
    }

    [DataContract]
    public class SAPProduct
    {
        public SAPProduct()
        {

        }

        public SAPProduct(
            string productID,
            string name)
        {
            ProductId = productID;
            Name = name;
        }

        public SAPProduct(ZGWSAMPLE_SRV.Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Price = product.Price.ToString();
            SupplierId = product.SupplierId;
            SupplierName = product.SupplierName;
            Currency = getCurrencyCode(product.CurrencyCode);
        }

        [DataMember]
        public string ProductId
        { get; set; }

        [DataMember]
        public string Name
        { get; set; }

        [DataMember]
        public string Price
        { get; set; }

        [DataMember]
        public string SupplierId
        { get; set; }

        [DataMember]
        public string SupplierName
        { get; set; }

        [DataMember]
        public kCurrencyCodeEnum Currency
        { get; set; }

        public static kCurrencyCodeEnum getCurrencyCode(string currency)
        {
            switch (currency)
            {
                case "EUR": return kCurrencyCodeEnum.kEUR;
                case "USD": return kCurrencyCodeEnum.kUSD;
                case "JPY": return kCurrencyCodeEnum.kJPY;
                case "MXN": return kCurrencyCodeEnum.kMXN;
                case "ARS": return kCurrencyCodeEnum.kARS;
                case "GBP": return kCurrencyCodeEnum.kGBP;
                case "CAD": return kCurrencyCodeEnum.kCAD;
                case "BRL": return kCurrencyCodeEnum.kBRL;
                case "CHF": return kCurrencyCodeEnum.kCHF;
                case "ZAR": return kCurrencyCodeEnum.kZAR;
                case "INR": return kCurrencyCodeEnum.kINR;
                case "PLN": return kCurrencyCodeEnum.kPLN;
                case "CNY": return kCurrencyCodeEnum.kCNY;
                case "DKK": return kCurrencyCodeEnum.kDKK;
                case "RUB": return kCurrencyCodeEnum.kRUB;

                default: return kCurrencyCodeEnum.kUnknown;
            }
        }

        public static string getCurrency(kCurrencyCodeEnum code)
        {
            switch (code)
            {
                case kCurrencyCodeEnum.kEUR: return "EUR";
                case kCurrencyCodeEnum.kUSD: return "USD";
                case kCurrencyCodeEnum.kJPY: return "JPY";
                case kCurrencyCodeEnum.kMXN: return "MXN";
                case kCurrencyCodeEnum.kARS: return "ARS";
                case kCurrencyCodeEnum.kGBP: return "GBP";
                case kCurrencyCodeEnum.kCAD: return "CAD";
                case kCurrencyCodeEnum.kBRL: return "BRL";
                case kCurrencyCodeEnum.kCHF: return "CHF";
                case kCurrencyCodeEnum.kZAR: return "ZAR";
                case kCurrencyCodeEnum.kINR: return "INR";
                case kCurrencyCodeEnum.kPLN: return "PLN";
                case kCurrencyCodeEnum.kCNY: return "CNY";
                case kCurrencyCodeEnum.kDKK: return "DKK";
                case kCurrencyCodeEnum.kRUB: return "RUB";

                default: return "Unknown";
            }
        }
    }
}