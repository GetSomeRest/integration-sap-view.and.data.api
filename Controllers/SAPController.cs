using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Http;
using AdnWebAPI.Models;
using Newtonsoft.Json;
using SAP.IW.GWPAM.Common.Configuration;

namespace AdnWebAPI.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////
    // API Controller
    //
    ////////////////////////////////////////////////////////////////////////////////
    public class SAPController : ApiController
    {
         // GET api/sap
        public IEnumerable<SAPProduct> GetProducts(string productIdList)
        {
            SAPConnector server = new SAPConnector();

            return server.GetProducts(productIdList);
        }

        // GET api/sap?productId=id
        public SAPProduct GetProductById(string productId)
        {
            SAPConnector server = new SAPConnector();

            return server.GetProductById(productId);
        }
 
        // POST api/values
        public void Post(SAPProduct product)
        {
            SAPConnector server = new SAPConnector();

            bool result = server.UpdateProduct(product);
        }
 
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }
 
        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }

    ////////////////////////////////////////////////////////////////////////////////
    // SAP Logic
    //
    ////////////////////////////////////////////////////////////////////////////////
    class SAPConnector
    {
        public IEnumerable<SAPProduct> GetProducts(string productIdList)
        {
            var products = new List<SAPProduct>();

            try
            {
                var productIds = productIdList.Split(new char[] { ';' });

                var service = GetService();

                var query = (from ZGWSAMPLE_SRV.Product product
                    in service.ProductCollection
                    select product);

                foreach (ZGWSAMPLE_SRV.Product product in query)
                {
                    if (productIds.Contains(product.ProductId))
                    {
                        products.Add(new SAPProduct(product));
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return products;
            }
        }

        public SAPProduct GetProductById(string productId)
        {
            try
            {
                var service = GetService();

                var query = (from ZGWSAMPLE_SRV.Product product
                    in service.ProductCollection
                    where product.ProductId == productId
                    select product);

                var res = query.FirstOrDefault();

                if (res != null)
                    return new SAPProduct(res);

                return new SAPProduct(
                    productId,
                    "Unknown Product");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);

                return new SAPProduct(
                   productId,
                   "Unknown Product");
            }
        }

        public bool UpdateProduct(SAPProduct product)
        {
            try
            {
                var service = GetService();

                var query = (from ZGWSAMPLE_SRV.Product p
                    in service.ProductCollection
                    where p.ProductId == product.ProductId
                    select p);

                var updatedProduct = query.FirstOrDefault();

                if (updatedProduct != null)
                {
                    updatedProduct.Price = 
                        decimal.Parse(product.Price);

                    updatedProduct.CurrencyCode = 
                        SAPProduct.getCurrency(product.Currency);

                    service.UpdateObject(updatedProduct);
                    service.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);

                return false;
            }
        }

        private ZGWSAMPLE_SRV.ZGWSAMPLE_SRV GetService()
        {
            // Now creates the Service Context. Authentication occurs here
            var service = new ZGWSAMPLE_SRV.ZGWSAMPLE_SRV(
                new Uri("https://sapes1.sapdevcenter.com:443/sap/opu/odata/sap/ZGWSAMPLE_SRV/"));

            return service;
        }
    }    

    class Logger
    {
        static public void Log(Exception ex)
        {
            var workingdir = HttpContext.Current.Server.MapPath("~");

            var logFile = Path.Combine(
                workingdir, 
                @"log\error.log");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);

            using (StreamWriter outfile = new StreamWriter(logFile))
            {
                outfile.Write(sb.ToString());
            }
        }
    }
}
