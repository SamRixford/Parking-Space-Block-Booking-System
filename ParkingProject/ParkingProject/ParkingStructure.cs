using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ParkingProject
{
    //declare pricecut event.
    public delegate void priceCutEvent(int price, string senderId);
    internal class ParkingStructure
    {
        //created values to facilitate new random values, which agent decides on the price cut, and the current price for the price cut event.
        static Random random = new Random();
        private static int runs = 0;
        private static int currentAgent = 0;
        private static int currentPrice = 40;
        public static event priceCutEvent priceCut;

        //return price.
        public int getPrice() { return currentPrice; }

        //runs a parking agent thread for a total of 20 price cuts and changes the price. Once 20 are through, ends the code.
        public void priceManage()
        {
            while (runs < 20)
            {
                Thread.Sleep(random.Next(500, 1000));
                int price = PricingModel();
                changePrice(price);
            }
            Program.structureThreadRun = false;
        }

        //generates a random price between 10 and 40.
        private int PricingModel()
        {
            int price = random.Next(10, 40);
            return price;
        }
        
        //changes the price of the current agent.
        public static void changePrice(int price)
        {
            //if the current agent is number 5 (6th) then reset it to 0 to get the actual first agent.
            if(currentAgent == 5)
            {
                currentAgent = 0;
            }

            //if a price cut event occurs, and the price is lower than the current price, run the priceCut event and add a count to total price cuts.
            if(priceCut != null)
            {
                if (price < currentPrice)
                {
                    priceCut(price, Program.parkingAgents[currentAgent].Name);
                    currentAgent++;
                    runs++;
                }

                if (price != currentPrice)
                {
                    currentPrice = price;
                }
            }

        }

        //called from parking agent when ordering, gets the order from the buffer and creates a thread for the order to run through which calls the OrderProcessing file.
        public void processOrder()
        {
            OrderClass order = Program.buffer.getOneCell();
            Thread thread = new Thread(() => OrderProcessing.processOrder(order));
            thread.Start();
        }
    }
}