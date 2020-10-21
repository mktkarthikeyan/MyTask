using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task.Models;

namespace Task.Controllers
{
    public class ProductController : ApiController
    {
        public IList<StockDetails> _StockDetails = new List<StockDetails>()
        {
            new StockDetails()
                {
                    ItemId = 1, ItemName = "Mobile Charger", Price = 300, StockInHand = 15
                },
            new StockDetails()
                {
                    ItemId = 2, ItemName = "Wired Head Phone", Price = 120, StockInHand = 14
                },
            new StockDetails()
                {
                    ItemId = 3, ItemName = "Bluetooth Head Phone", Price = 200, StockInHand = 25
                },
            new StockDetails()
                {
                    ItemId = 4, ItemName = "Bluetooth Speaker", Price = 450, StockInHand = 10
                },
            new StockDetails()
                {
                    ItemId = 5, ItemName = "Mobile Case", Price = 100, StockInHand = 50
                }
        };


        //Here am not using any Database for storing/Update/Delete Products.. If we want to it in DB means we have to connect DB.. 
        public IList<StockDetails> GetAllStockDetails()
        {
            //Return list of all Products  
            return _StockDetails;
        }
        public StockDetails GetStockDetailsbyId(int id)
        {
            //Return a single item details by ItemId  
            var Stock = _StockDetails.FirstOrDefault(e => e.ItemId == id);
            if (Stock == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return Stock;
        }

        public HttpResponseMessage AddProduct(int id, double Price, string Itemname, int Stockinhand)
        {
            //ADD New Product
            var Stock = _StockDetails.FirstOrDefault(e => e.ItemId == id);
            if (Stock == null)
            {
                StockDetails stck = new StockDetails()
                {
                    ItemId = id,
                    ItemName = Itemname,
                    Price = Price,
                    StockInHand = Stockinhand
                };
                _StockDetails.Add(stck);
            }
            else
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable));
            return Request.CreateResponse(HttpStatusCode.Created);
        }
        public HttpResponseMessage DeleteProduct(int id)
        {
            //ADD Stock inHand By Stock Id 
            var Stock = _StockDetails.FirstOrDefault(e => e.ItemId == id);
            if (Stock == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            _StockDetails.Remove(Stock);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage UpdateProduct(int id, string Name, double Fare, int stckcount)
        {
            //ADD Stock inHand By Stock Id 
            var Stock = _StockDetails.FirstOrDefault(e => e.ItemId == id);
            if (Stock == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            int mystock = _StockDetails.IndexOf(Stock);
            _StockDetails.RemoveAt(mystock);
            Stock.ItemName = Name;
            Stock.Price = Fare;
            Stock.StockInHand = stckcount;
            _StockDetails.Add(Stock);

            //If we want to do stock details in SQL server also we can do here
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage AddStock(int id, int AddCount)
        {
            //ADD Stock inHand By Stock Id 
            var Stock = _StockDetails.FirstOrDefault(e => e.ItemId == id);
            if (Stock == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            int mystock = _StockDetails.IndexOf(Stock);
            _StockDetails.RemoveAt(mystock);
            Stock.StockInHand = Stock.StockInHand + AddCount;
            _StockDetails.Add(Stock);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


    }
}
