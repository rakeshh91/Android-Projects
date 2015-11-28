//////////////////////////////////////////////////////////////////////////////////
// WriterClient.cs - CommService client sends and receives messages             //
// ver 2.2                                                                      //
// Author : Rakesh Nallapeta Eshwaraiah                                         //
// Source : Jim Fawcett, CSE681 - Software Modeling and Analysis, Project #4    //
//////////////////////////////////////////////////////////////////////////////////
/*
 * Additions to C# Console Wizard generated code:
 * - Added using System.Threading
 * - Added reference to ICommService, Sender, Receiver, Utilities, MakeMessage
 *
 * Note:
 * - in this incantation the client has Sender and now has Receiver to
 *   retrieve Server echo-back messages.
 * - If you provide command line arguments they should be ordered as:
 *   remotePort, remoteAddress, localPort, localAddress
 */
/*
 * Maintenance History:
 * --------------------
 * ver 2.2 : 20 Nov 2015
 * - updated to support write operations to be performed on database by sending message requests
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

    class Client
    {
        string localUrl { get; set; }// = "http://localhost:8081/CommService";
        string remoteUrl { get; set; } = "http://localhost:8080/CommService";

        //----< retrieve urls from the CommandLine if there are any >--------

        public void processCommandLine(string[] args)
        {
            if (args.Length == 0)
                return;
            localUrl = Util.processCommandLineForLocal(args, localUrl);
            remoteUrl = Util.processCommandLineForRemote(args, remoteUrl);
        }
        //--------------print sending message----------//
        public void printMessage(Message msg)
        {
            Console.Write("\n============Start of the Message==========\n");
            Console.Write("\n  Sending Message:");
            Console.Write("\n==========================\n");
            Console.WriteLine(msg.content); ;
            Console.WriteLine("================End of message=============");
        }

        public void startRcvrService(Receiver rcvr)
        {
            if (rcvr.StartService())
            {
                rcvr.doService(rcvr.defaultServiceAction());
            }
        }

        static void Main(string[] args)
        {
            Console.Write("\n  starting CommService client");
            Console.Write("\n =============================\n");
            Console.Title = "Writer Client to write on the NOSQL database";
            HiResTimer timerWriter = new HiResTimer();
            Client clnt = new Client();
            clnt.processCommandLine(args);
            string localPort = args[0];
            clnt.localUrl = "http://localhost:"+localPort+"/CommService";
            string localAddr = Util.urlAddress(clnt.localUrl);
            Receiver rcvr = new Receiver(localPort, localAddr);
            rcvr.setTimerFromClient(timerWriter);
            clnt.startRcvrService(rcvr);
            Sender sndr = new Sender(clnt.localUrl);  // Sender needs localUrl for start message
            MessageMaker writerInput = new MessageMaker();
            Message msg = new Message();
            string fileName = ".\\WriterClientInput.xml";
            int numberOfRequestsWriters = Convert.ToInt32(args[1]);
            XDocument xmldoc = XDocument.Load(fileName);
            var requests = xmldoc.Descendants("Request");
            int requestCount = 0, i = 0; List<XElement> totalrequest = new List<XElement>();
            while (i < numberOfRequestsWriters){    //set the specified number of requests to send
                totalrequest.Add(requests.ElementAt(i)); i++;}
            timerWriter.Start();
            foreach (var request in totalrequest){
                msg = new Message();
                msg = writerInput.makeMessage(clnt.localUrl, clnt.remoteUrl, request);
                Console.Title = "Writer Client to write to NoSQl database: Writing " + (++requestCount) + " requests";
                if (!sndr.Connect(msg.toUrl)){
                    Console.Write("\n  could not connect in {0} attempts", sndr.MaxConnectAttempts);
                    sndr.shutdown();
                    rcvr.shutDown();
                    return;
                }
                while (true) { 
                        clnt.printMessage(msg);
                        if (!sndr.sendMessage(msg))
                            return;
                        Thread.Sleep(100);
                        break;
                    }                
                sndr.sendLatencyWriter(rcvr.avgLatency);}
            msg.content = "done";
            sndr.sendMessage(msg);
            Util.waitForUser();         
            rcvr.shutDown();
            sndr.shutdown();
            Console.Write("\n\n");
        }
    }
}
