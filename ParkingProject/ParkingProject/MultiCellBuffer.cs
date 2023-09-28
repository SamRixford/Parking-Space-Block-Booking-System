using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingProject
{
    public class MultiCellBuffer
    {
        //create a data strucutre to store objects and a semaphore to control resources.
        private OrderClass[] buffer;
        private Semaphore semaphore;

        //initialize the data structure and the semaphore.
        public MultiCellBuffer(int n)
        {
            buffer = new OrderClass[n];
            semaphore = new Semaphore(n, n);
        }

        //set a cell as being used by an agent with a given order.
        public void setOneCell(OrderClass order)
        {
            //wait and lock the buffer.
            semaphore.WaitOne();
            lock (buffer)
            {
                //if the buffer is full wait.
                if (buffer[2] != null)
                {
                    Monitor.Wait(buffer);
                }

                //otherwise enter in an available index with an order.
                for(int i = 0; i <3; i++)
                {
                    if (buffer[i] == null)
                    {
                        buffer[i] = order;
                        i = 3;
                    }
                }
                //notify the thread.
                Monitor.Pulse(buffer);

            }
            //release semaphore.
            semaphore.Release();

        }

        //gets a order from the buffer.
        public OrderClass getOneCell()
        {
            //wait and lock buffer.
            semaphore.WaitOne();
            OrderClass order = null;
            lock (buffer)
            {
                //if there is nothing in the buffer wait.
                while (buffer[0] == null)
                {
                    Monitor.Wait(buffer);
                }
                //otherwise take the first of the final indexes for the parking agent.
                for (int i =2; i > -1; i--)
                {
                    if (buffer[i] != null)
                    {
                        order = buffer[i];
                        buffer[i] = null;
                        break;
                    }
                }
                //notify the thread.
                Monitor.Pulse(buffer);
            }
            //end the semaphore and return the order.
            semaphore.Release();
            return order;
        }


    }
}
