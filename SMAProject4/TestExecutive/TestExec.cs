//////////////////////////////////////////////////////////////////////////////////
// TestExec.cs - Client used to show that we can use remote database and perform//
//               operations. Requirement 2, Requirement 3 and Requirement 4 exec//
// ver 2.2                                                                      //
// Author : Rakesh Nallapeta Eshwaraiah                                         //
// Source : Jim Fawcett, CSE681 - Software Modeling and Analysis, Project #4    //
//////////////////////////////////////////////////////////////////////////////////
/*
 * Additions to C# Console Wizard generated code:
 * - Added using System.Threading
 * - Added reference to ICommService, Sender, Receiver, Utilities, MakeMessage
 *
 */
/*
 * Maintenance History:
 * --------------------
 * ver 2.2 : 20 Nov 2015
 * - updated to support write and read operations to be performed on database by sending message requests
 * ver 2.1 : 29 Oct 2015
 * - fixed bug in processCommandLine(...)
 * - added rcvr.shutdown() and sndr.shutDown() 
 * ver 2.0 : 20 Oct 2015
 * - replaced almost all functionality with a Sender instance
 * - added Receiver to retrieve Server echo messages.
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
using System.Threading;

namespace Project4Starter
{
    using System.Xml.Linq;
    using Util = Utilities;
    ///////////////////////////////////////////////////////////////////////
    // Client class sends and receives messages in this version
    // - commandline format: /L http://localhost:8085/CommService 
    //                       /R http://localhost:8080/CommService
    //   Either one or both may be ommitted

    class TestExec
    {
        string localUrl { get; set; } = "http://localhost:8083/CommService";
        string remoteUrl { get; set; } = "http://localhost:8080/CommService";

        //----< retrieve urls from the CommandLine if there are any >--------
        public void processCommandLine(string[] args)
        {
            if (args.Length == 0)
                return;
            localUrl = Util.processCommandLineForLocal(args, localUrl);
            remoteUrl = Util.processCommandLineForRemote(args, remoteUrl);
        }

        public void sendEachRequest(XElement request,TestExec clnt, Message msg, Sender sndr, Receiver rcvr, MessageMaker testExecInput)
        {//send each request to construct xml request message and send it to the server
            msg = new Message();
            msg = testExecInput.makeMessage(clnt.localUrl, clnt.remoteUrl, request);
            if (!sndr.Connect(msg.toUrl))
            {
                Console.Write("\n  could not connect in {0} attempts", sndr.MaxConnectAttempts);
                sndr.shutdown();
                rcvr.shutDown();
                return;
            }
            while (true)
            {
                //msg.content = "Message #" + (++counter).ToString();
                Console.Write("\n============Start of the Message==========\n");
                Console.Write("\n  Sending Message:");
                Console.Write("\n==========================\n");
                Console.WriteLine(msg.content);
                Console.WriteLine("==================End of Message===============");
                if (!sndr.sendMessage(msg))
                    return;
                Thread.Sleep(100);
                break;
            }
        }

        public void startRcvrService(Receiver rcvr)
        {
            if (rcvr.StartService())
            {
                rcvr.doService(rcvr.defaultServiceAction());
            }
        }

        public void getReadersWritersCount(XDocument xmldoc, ref int readers, ref int writers, ref int portReader, ref int portWriter, ref int numberOfRequests, ref string readerDisplay, ref string writerDisplay,ref int numberOfRequestsWriters)
        {/*------------get number of readers, writers and the port numbers and number of reader requests to send from the XML file loaded to the client---*/
            readers = Convert.ToInt32(xmldoc.Descendants("Readers").Descendants("nr").FirstOrDefault().Value);
            writers = Convert.ToInt32(xmldoc.Descendants("Writers").Descendants("nw").FirstOrDefault().Value);
            portReader = Convert.ToInt32(xmldoc.Descendants("Readers").Descendants("rPort").FirstOrDefault().Value);
            portWriter = Convert.ToInt32(xmldoc.Descendants("Writers").Descendants("wPort").FirstOrDefault().Value);
            numberOfRequests = Convert.ToInt32(xmldoc.Descendants("Readers").Descendants("NumberofRequests").FirstOrDefault().Value);
            readerDisplay = xmldoc.Descendants("Readers").Descendants("ReaderDisplay").FirstOrDefault().Value;
            readerDisplay = xmldoc.Descendants("Writers").Descendants("WriterDisplay").FirstOrDefault().Value;
            numberOfRequestsWriters = Convert.ToInt32(xmldoc.Descendants("Writers").Descendants("NumberofRequestsWriters").FirstOrDefault().Value);
        }
        static void Main(string[] args)
        {
            Console.Write("\n  starting CommService client");
            Console.Write("\n =============================\n");
            Console.Title = "Test Executive Client to display the results of remote NoSQL database operations";
            TestExec clnt = new TestExec();
            clnt.processCommandLine(args);
            HiResTimer timerTestExec = new HiResTimer();
            string localPort = Util.urlPort(clnt.localUrl);
            string localAddr = Util.urlAddress(clnt.localUrl);
            Receiver rcvr = new Receiver(localPort, localAddr);
            rcvr.setTimerFromClient(timerTestExec);
            clnt.startRcvrService(rcvr);
            Console.WriteLine("\n The Message request is");
            Console.Write("\n =================================\n");
            Sender sndr = new Sender(clnt.localUrl);  // Sender needs localUrl for start message
            MessageMaker testExecInput = new MessageMaker(); 
            Message msg = new Message();
            string fileName = ".\\TestExecInput.xml"; 
            XDocument xmldoc = XDocument.Load(fileName);
            var requests = xmldoc.Descendants("Request");
            int requestCount = 0, readers = 0, writers = 0, portReader = 0, portWriter = 0, numberOfRequests = 0, numberOfRequestsWriters = 0;
            string readerDisplay = "", writerDisplay = "";
            clnt.getReadersWritersCount(xmldoc, ref readers, ref writers, ref portReader, ref portWriter, ref numberOfRequests, ref readerDisplay, ref writerDisplay, ref numberOfRequestsWriters);
            timerTestExec.Start();
            foreach (var request in requests)
            {   //send each request to construct message and send it to the server
                clnt.sendEachRequest(request,clnt,msg,sndr,rcvr,testExecInput);
                Console.Title = "TestExec Client to display the NoSQl database operations: Operations " + (++requestCount) + " are Sent";
            }
            msg.content = "done";
            Util.waitForUser();
            rcvr.shutDown();
            sndr.shutdown();
            Console.Write("\n\n");
            if(args.Count() != 0)
            {
                readers = Convert.ToInt32(args[0]);
                writers = Convert.ToInt32(args[1]);
                numberOfRequests = Convert.ToInt32(args[2]);
                numberOfRequestsWriters = Convert.ToInt32(args[3]);
            }
            for(int i = 1; i <= writers; i++)
            {
                System.Diagnostics.Process.Start(".\\Client\\bin\\Debug\\Client.exe",""+(portWriter++) + " " + numberOfRequestsWriters + " "+writerDisplay);
            }
            for(int j = 1; j <= readers; j++)
            {
                System.Diagnostics.Process.Start(".\\Client2\\bin\\Debug\\Client2.exe",(""+(portReader++)+" "+numberOfRequests+" "+readerDisplay));
            }
        }
            
    }
}
