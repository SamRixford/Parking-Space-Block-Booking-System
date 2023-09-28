using ParkingProject;
using System;
using System.Threading;

public class Program
{
    //Declare a thread array of parking agents, a buffer, and a boolean value to tell when to stop running the structure thread.
    public static Thread[] parkingAgents;
    public static MultiCellBuffer buffer;
    public static bool structureThreadRun = true;

    static void Main(string[] args)
    {
        //initiate a strucuture and agent to create threads with.
        ParkingStructure structure = new ParkingStructure();
        ParkingAgent agent = new ParkingAgent();

        //initiate a multicellbuffer to buffer the threads. 
        buffer = new MultiCellBuffer(3);

        //create a parking structure thread. in this case there is just 1 structure so we can declare it this way. If we want more we can declare it the same way as the parkingAgent threads below.
        Thread parkingStructureThread = new Thread(new ThreadStart(structure.priceManage));
        parkingStructureThread.Start();

        //allow for events to happen such as priceCut, order, and orderProcessed which call a function in the agent, structure, and agent respectively..
        ParkingStructure.priceCut += new priceCutEvent(agent.parkingSale);
        ParkingAgent.order += new orderEvent(structure.processOrder);
        OrderProcessing.orderProcessed += new orderProcessedEvent(agent.orderProcessed);
        
        //create and start 5 parkingAgent threads. 
        parkingAgents = new Thread[5];

        for (int i = 0; i < 5; i++)
        {
            parkingAgents[i] = new Thread(new ThreadStart(agent.orderFunction));
            parkingAgents[i].Name = (i+ 1).ToString();
            parkingAgents[i].Start();
        }

    }








}
