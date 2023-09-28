using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingProject
{
    //create an event to send the final values to the parking agent.
    public delegate void orderProcessedEvent(string senderId, double finalPrice, int price, int quantity);
    public class OrderProcessing
    {
        //initialize event and random.
        public static event orderProcessedEvent orderProcessed;
        public static Random random = new Random();


        //processes the order to be sent to the parking agent.
        public static void processOrder(OrderClass order)
        {
            //if the card number is not between 5000 and 7000 it will read as invalid.
            if(order.CardNo > 7000 || order.CardNo < 5000)
            {
                Console.WriteLine("{0} has an invalid credit card number.", order.SenderId);
                return;
            }
            else
            {
                //otherwise get the final price from the praking spot price times the number bought. Then a tax and location charge will be applied. Then send the order to be printed out.
                double tax = random.NextDouble() * (.04) + 1.08;
                int locationCharge = random.Next(2, 8);
                double finalPrice = order.Price * order.Quantity * tax + locationCharge;
                finalPrice = Math.Round(finalPrice, 2);
                orderProcessed(order.SenderId, finalPrice, order.Price, order.Quantity);
            }
        }
    }
}
