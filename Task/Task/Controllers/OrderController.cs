using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task.Models;
using Task.Controllers;
namespace Task.Controllers
{
    public class OrderController : ApiController
    {
        ProductController pdtCtrl = new Controllers.ProductController();
        public HttpResponseMessage OrderProduct(int Orderid, int OrderCount)
        {
            //ADD Stock inHand By Stock Id 
            var Stock = pdtCtrl._StockDetails.FirstOrDefault(e => e.ItemId == Orderid);
            if (Stock == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else if (Stock.StockInHand < OrderCount)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable));

            int mystock = pdtCtrl._StockDetails.IndexOf(Stock);
            pdtCtrl._StockDetails.RemoveAt(mystock);
            Stock.StockInHand = Stock.StockInHand - OrderCount;
            pdtCtrl._StockDetails.Add(Stock);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage CancelOrder(int Orderid, int OrderCount)
        {
            //ADD Stock inHand By Stock Id 
            var Stock = pdtCtrl._StockDetails.FirstOrDefault(e => e.ItemId == Orderid);
            if (Stock == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
           
            int mystock = pdtCtrl._StockDetails.IndexOf(Stock);
            pdtCtrl._StockDetails.RemoveAt(mystock);
            Stock.StockInHand = Stock.StockInHand + OrderCount;
            pdtCtrl._StockDetails.Add(Stock);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
