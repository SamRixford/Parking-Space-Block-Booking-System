using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingProject
{
    //creates event orderEvent.
    public delegate void orderEvent();
    public class ParkingAgent
    {
        //creates an event, random value, and the current price which is set at 40 (maxx value).
        public static event orderEvent order;
        public static Random random = new Random();
        private int currentPrice = 40;

        //Ran when initiating the threads of parking agent. Creates an order with the threads as long as the structure is running.
        public void orderFunction()
        {
            while (Program.structureThreadRun)
            {
                Thread.Sleep(random.Next(1500, 3000));
                createOrder(Thread.CurrentThread.Name);
            }
        }

        //creates an order to send to the buffer.
        private void createOrder(string senderId)
        {
            //random card number between 5000 and 7000 as well as purchasing more spots if the rates are cheaper.
            int cardNo = random.Next(5000, 7000);
            int quantity = 0;
            if (currentPrice < 20)
            {
                quantity = random.Next(20, 30);
            }
            else if (currentPrice < 30)
            {
                quantity = random.Next(10, 20);
            }
            else if (currentPrice < 40)
            {
                quantity = random.Next(1, 10);
            }

            //create an order with the given values for the respective agent.
            OrderClass newOrder = new OrderClass(senderId, cardNo, quantity, currentPrice);
            Console.WriteLine("Agent {0}'s order has been created", senderId);

            //send to the buffer and call the order event which triggers the processing of the order on the server side (parking structure).
            Program.buffer.setOneCell(newOrder);
            order();

        }

        // called from order processing when the final values are calculated to print out the agents values.
        public void orderProcessed(string senderId, double totalPrice, int price, int quantity)
        {
            Console.WriteLine($"Agent {senderId}'s order has been processed. The final sale is ${totalPrice}" + $" (Agent {senderId} bought {quantity} parking spaces for {price} dollars a piece");
        }

        //when a new price cut happens, change the price and create an order.
        public void parkingSale(int newPrice, string senderId)
        {
            currentPrice = newPrice;
            Console.WriteLine("Parking is going on sale");
            createOrder(senderId);
        }
    }
}
