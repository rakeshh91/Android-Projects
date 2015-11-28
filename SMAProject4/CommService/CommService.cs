////////////////////////////////////////////////////////////////////////////
// CommService.cs - Implementation of WCF message-passing service         //
// ver 1.2                                                                //
// Author: Rakesh Nallapeta Eshwaraiah                                    //
// Source:Jim Fawcett, CSE681 - Software Modeling and Analysis, Project #4//
////////////////////////////////////////////////////////////////////////////
/*
 * Additions to the C# Console Wizard code:
 * - added reference to System.ServiceModel
 * - added using System.ServiceModel
 * - added reference to Project4Starter.ICommService
 * - copied BlockingQueue.cs into project folder
 * - Added BlockingQueue.cs to project
 */
/*
 * Maintenance History:
 * --------------------
 * ver 1.2 : 20 Nov 2015
 * - updated functions to receive performance details from the receiver and send it to the client
 * ver 1.1 : 24 Oct 2015
 * - added verbose mode to support debugging and learning
 * - to see more detail about what is going on in Sender and Receiver
 *   set Utilities.verbose = true
 * ver 1.0 : 18 Oct 2015
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Project4Starter
{
    using SWTools;
    using Util = Utilities;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CommService : ICommService
    {
        // static rcvrQueue is shared by all instances of this class

        private static SWTools.BlockingQueue<Message> rcvrQueue =
          new SWTools.BlockingQueue<Message>();
        private static double readerLatency = 0;
        private static double writerLatency = 0;
        private static double srvrThroughput = 0;
        //----< called by clients, will only block briefly >-----------------

        public void sendMessage(Message msg)
        {
            if (Util.verbose)
                Console.Write("\n  this is CommService.sendMessage");
            rcvrQueue.enQ(msg);
        }

        //----< called by server, blocks caller while empty >----------------
        /*
         * Note: this is NOT a service method - see interface definition
         */
        public Message getMessage()
        {
            if (Util.verbose)
                Console.Write("\n  this is CommService.getMessage");
            return rcvrQueue.deQ();
        }
        //-----------------get average latency of the reader client from receiver------------//
        public void getPerformanceResultsReader(double avgReaderLatency)
        {
            readerLatency = avgReaderLatency;
        }

        //-----------------get average latency of the writer client from receiver------------//
        public void getPerformanceResultsWriter(double avgWriterLatency)
        {
            writerLatency = avgWriterLatency;
        }

        //-----------------get throughput of the server from the server------------//
        public void getThroughputServer(double serverThroughput)
        {
            srvrThroughput = serverThroughput;
        }
        //-----------------get performance measure of the reader,writer and the server---------//
        public TimingInfo getPerformanceWPF()
        {
            return new TimingInfo
            {
                rClientLatency = readerLatency,
                wClientLatency = writerLatency,
                srvrThroughput = srvrThroughput
            };
        }
    }
}
