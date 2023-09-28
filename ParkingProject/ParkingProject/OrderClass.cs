using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingProject
{
    //order includes the sender id, card number, quantity and pricing.
    public class OrderClass
    {
        private string senderId;
        private int cardNo;
        private int quantity;
        private int price;

        //include a get and set for each value as well as a constructor taking all values.
        public string SenderId
        {
            get { return senderId; }
            set { senderId = value; }
        }

        public int CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        public OrderClass(string senderId, int cardNo, int quantity, int price) 
        {
            this.senderId = senderId;
            this.cardNo = cardNo;
            this.quantity = quantity;
            this.price = price;
        }
    }
}
