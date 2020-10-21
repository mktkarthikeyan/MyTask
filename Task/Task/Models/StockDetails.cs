using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Models
{
    public class StockDetails
    {

        public int ItemId
        {
            get;
            set;
        }
        public string ItemName
        {
            get;
            set;
        }
        public double Price
        {
            get;
            set;
        }
        public int StockInHand
        {
            get;
            set;
        }

    }
}