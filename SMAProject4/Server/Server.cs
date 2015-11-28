////////////////////////////////////////////////////////////////////////////////
// Server.cs - CommService server                                             //
// ver 2.4                                                                    //
// Author: Rakesh Nallapeta Eshwaraiah                                        // 
// Source:Jim Fawcett, CSE681 - Software Modeling and Analysis, Project #4    //
////////////////////////////////////////////////////////////////////////////////
/*
 * Additions to C# Console Wizard generated code:
 * - Added reference to ICommService, Sender, Receiver, Utilities, NoSQLDB
 *
 * Note:
 * - This server now receives and then sends back received messages and performs operations requested.
 */

/*
 * Maintenance History:
 * --------------------
 * ver 2.4 : 18 Nov 2015
 * - added handling input requests and operations to be performed on the database
 *    "addition", "deletion", "edit", "persist", "query"
 * ver 2.3 : 29 Oct 2015
 * - added handling of special messages: 
 *   "connection start message", "done", "closeServer"
 * ver 2.2 : 25 Oct 2015
 * - minor changes to display
 * ver 2.1 : 24 Oct 2015
 * - added Sender so Server can echo back messages it receives
 * - added verbose mode to support debugging and learning
 * - to see more detail about what is going on in Sender and Receiver
 *   set Utilities.verbose = true
 * ver 2.0 : 20 Oct 2015
 * - Defined Receiver and used that to replace almost all of the
 *   original Server's functionality.
 * ver 1.0 : 18 Oct 2015
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Starter
{
    using System.Xml.Linq;
    using Util = Utilities;

    class Server
    {
        string address { get; set; } = "localhost";
        string port { get; set; } = "8080";

        //----< quick way to grab ports and addresses from commandline >-----
        public void ProcessCommandLine(string[] args)
        {
            if (args.Length > 0)
                port = args[0];
            if (args.Length > 1)
                address = args[1];
        }

        public void printDatabase(DBEngine<string, DBElement<string, List<string>>> testDict, string messg)
        {/*--------------------printing the database after certain operation--------------------*/
            Console.Write("\n\n  {0}", messg);
            Console.Write("\n ================================================\n");
            testDict.showEnumerableDB();
        }

        public void printQuery(string messg)
        {/*---------------------printing query message after certain query operation----------------*/
            Console.Write("\n\n  {0}", messg);
            Console.Write("\n ==================================================================\n");
        }

        public void displayChildren(List<string> child)
        {/*-------------------------display children from the list---------------------------*/
            if (child != null)
            {
                foreach (var c in child)
                    Console.Write(c + ", ");
                Console.WriteLine();
            }
            else { Console.WriteLine("\n None of the children are present for specified key\n"); }
        }

        public void performOperations(DBEngine<string, DBElement<string, List<string>>> testDict, DBElement<string, List<string>> value)
        {       /*----------Perform operations as per the input given in the XML document--------------*/
            if (value.operation == "addition")
            {
                testDict.insert(value.key, value);          //insert the key/value pairs to the main database
                string s = "Database after inserting key " + value.key + " is";
                printDatabase(testDict, s);
            }
            if (value.operation == "edit")
            {
                testDict.saveValue(value.key, value);       //edit the value for the given key
                string s = "Database after editing key " + value.key + " is";
                printDatabase(testDict, s);
            }
            if (value.operation == "delete")
            {
                testDict.delete(value.key);                 //delete the key/value pair
                string s = "Database after deleting key " + value.key + " is";
                printDatabase(testDict, s);
            }
            if (value.operation == "persist database")
            {
                PersistEngine<string, DBElement<string, List<string>>> persist = new PersistEngine<string, DBElement<string, List<string>>>(testDict);
                var keys = testDict.Keys();
                persist.persistToXMLListPayload(keys);
                printDatabase(testDict, "Persisted database is:");
            }
            if (value.operation == "Query value")
            {
                DBElement<string, List<string>> valueOfKey = testDict.getValueOfKey(value.key);
                printQuery("Querying the database for value of key " + value.key + " is");
                Console.WriteLine("\n\nThe value of the Key {0} is:\n", value.key);
                valueOfKey.showEnumerableElement();
            }
            if (value.operation == "Query children")
            {
                QueryEngine<string, DBElement<string, List<string>>> qEngine = new QueryEngine<string, DBElement<string, List<string>>>(testDict);
                printQuery("Querying the database for value of key " + value.key + " is");
                List<string> children = qEngine.getChildrenOfKey(value.key);
                Console.WriteLine("\nThe children of the Key {0} are:\n", value.key);
                displayChildren(children);
            }
            if (value.operation == "Augment database")
            {
                PersistEngine<string, DBElement<string, List<string>>> persist = new PersistEngine<string, DBElement<string, List<string>>>(testDict);
                string fileName = "C:\\Users\\rakeshh91\\Documents\\Rakesh Documents\\Class Materials\\SMA\\Assignments\\Assignment 4 - Implementation\\CommPrototype\\augmentDatabase.xml";
                persist.augmentDatabaseFromXMLStringList(testDict, fileName);
                printDatabase(testDict, "Database after augmenting is:");
            }
        }

        //--------------check operation and assign the response to be done------------------//

        public void chkOperation(dynamic op, XElement request, DBEngine<string, DBElement<string, List<string>>> testDict)
        {
            if (op == "addition" || op == "edit")
            {
                DBElement<string, List<string>> elem1 = new DBElement<string, List<string>>();
                elem1.name = request.Descendants("Value").Descendants("name").FirstOrDefault().Value;
                elem1.descr = request.Descendants("Value").Descendants("descr").FirstOrDefault().Value;
                elem1.children = request.Descendants("Value").Descendants("children").Descendants("child").Select(child => { return (child.Value); }).ToList();
                elem1.timeStamp = DateTime.Now;
                elem1.payload = request.Descendants("Value").Descendants("payload").Descendants("payloads").Select(payload => { return (payload.Value); }).ToList();
                elem1.operation = request.Descendants("Operation").FirstOrDefault().Value;
                elem1.key = request.Descendants("Key").FirstOrDefault().Value;
                performOperations(testDict, elem1);
            }
            if (op == "delete" || op == "Query value" || op == "Query children")
            {
                DBElement<string, List<string>> elem2 = new DBElement<string, List<string>>();
                elem2.operation = request.Descendants("Operation").FirstOrDefault().Value;
                elem2.key = request.Descendants("Key").FirstOrDefault().Value;
                performOperations(testDict, elem2);
            }
            if (op == "persist database")
            {
                DBElement<string, List<string>> elem3 = new DBElement<string, List<string>>();
                elem3.operation = request.Descendants("Operation").FirstOrDefault().Value;
                performOperations(testDict, elem3);
            }
            if (op == "Augment database")
            {
                DBElement<string, List<string>> elem3 = new DBElement<string, List<string>>();
                elem3.operation = request.Descendants("Operation").FirstOrDefault().Value;
                performOperations(testDict, elem3);
            }
        }

        public string readOperations(DBEngine<string, DBElement<string, List<string>>> testDict, Message msg)
        {       //-----------< Loads the XMl document and parses it. Read the operation from XMl and perform them >----- 
            try
            {    /*----------------Parse loaded XMl and load into DBElement and perform operation----------*/
                StringBuilder sb = new StringBuilder();
                XDocument parsedDoc = XDocument.Parse(msg.content);
                var requests = parsedDoc.Descendants("Request");
                foreach (var request in requests)
                {
                    var op = request.Descendants("Operation").FirstOrDefault().Value;
                    var keyString = request.Descendants("Key").FirstOrDefault().Value;                  
                    string s1 = "\nThe operation " + op + " has been performed on the database for Key " + keyString;
                    sb = sb.Append(s1);
                    chkOperation(op, request, testDict);
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Error is:\t {0}", e.Message);
                return "";
            }
        }

        public void printMessage(Message msg)
        {
            Console.Write("\n============Start of the Message==========\n");
            Console.Write("\n  Received message:");
            Console.Write("\n============================\n");
            Console.Write("\n  Sender is {0}", msg.fromUrl);
            Console.Write("\n  Content is {0}\n", msg.content);
            Console.WriteLine("==================End of Message===============");
        }

        public void printMessage(Server srvr)
        {
            Console.Title = "Server";
            Console.Write(String.Format("\n  Starting CommService server listening on port {0}", srvr.port));
            Console.Write("\n ====================================================\n");
            Console.WriteLine("\n The Message request is");
            Console.Write("\n =================================\n");
        }

        public void printServerThroughput(double timeTaken)
        {
            Console.Write("\n==================================");
            Console.Write("\nThe throughput of the server is");
            Console.Write("\n==================================\n");
            Console.WriteLine(1 / timeTaken);
        }
        static void Main(string[] args)
        {
            Util.verbose = false;
            Server srvr = new Server();
            DBEngine<string, DBElement<string, List<string>>> testDict = new DBEngine<string, DBElement<string, List<string>>>();
            srvr.ProcessCommandLine(args);
            double timeTaken = 0, totalTime = 0, insCount = 0;
            srvr.printMessage(srvr);
            Sender sndr = new Sender(Util.makeUrl(srvr.address, srvr.port));
            Receiver rcvr = new Receiver(srvr.port, srvr.address);
            StringBuilder sb = new StringBuilder();
            Action serviceAction = () =>
            {
                Message msg = null;
                while (true)
                {
                    HiResTimer hiResTimer = new HiResTimer();
                    hiResTimer.Start();
                    msg = rcvr.getMessage();   // note use of non-service method to deQ messages            
                    srvr.printMessage(msg);
                    if (msg.content == "connection start message") continue; // don't send back start message
                    if (msg.fromUrl == "http://localhost:8080/CommService") continue;
                    if (msg.content == "done")
                    { Console.Write("\n  client has {0} finished\n",msg.fromUrl); continue; }
                    if (msg.content == "closeServer")
                    { Console.Write("received closeServer"); break; }
                    msg.content = srvr.readOperations(testDict, msg);
                    Util.swapUrls(ref msg);                     // swap urls for outgoing message
#if (TEST_WPFCLIENT)
                    Message testMsg = new Message();
                    testMsg.toUrl = msg.toUrl;
                    testMsg.fromUrl = msg.fromUrl;
                    testMsg.content = msg.content;
                    sndr.sendMessage(testMsg);
                    hiResTimer.Stop();
                    double diffTime = hiResTimer.ElapsedTimeSpan.TotalSeconds;
                    totalTime = totalTime + diffTime;
                    timeTaken = totalTime / (++insCount);
                    Console.Title = "Server client handling " + (insCount) + " requests";
                    srvr.printServerThroughput(timeTaken);
                    sndr.sendServerThroughput((1 / timeTaken));
#else
                              sndr.sendMessage(msg);
#endif
                }
            };
            if (rcvr.StartService())
                rcvr.doService(serviceAction); // This serviceAction is asynchronous,
            Util.waitForUser(); // so the call doesn't block.
        }
    }
}