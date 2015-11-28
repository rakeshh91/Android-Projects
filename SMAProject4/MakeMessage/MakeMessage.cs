//////////////////////////////////////////////////////////////////////////////
// MessageMaker.cs - Construct ICommService Messages                        //
// ver 1.1                                                                  //
// Author: Rakesh Nallapeta Eshwaraiah                                      //
// Source: Jim Fawcett, CSE681 - Software Modeling and Analysis, Project #4 //
//////////////////////////////////////////////////////////////////////////////
/*
 * Purpose:
 *----------
 * This is a placeholder for application specific message construction
 *
 * Additions to C# Console Wizard generated code:
 * - references to ICommService and Utilities
 */
/*
 * Maintenance History:
 * --------------------
 * ver 1.1 : 20 Nov 2015
 * - added message maker function to set the content of the message to every request sent from the client
 * ver 1.0 : 29 Oct 2015
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project4Starter
{
    public class MessageMaker
    {
        public static int msgCount { get; set; } = 0;
        public Message makeMessage(string fromUrl, string toUrl)
        {
            XDocument xmlDoc = XDocument.Load("C:\\Users\\rakeshh91\\Desktop\\WriterClientInput.xml");
            Message msg = new Message();
            msg.fromUrl = fromUrl;
            msg.toUrl = toUrl;
            //msg.content = String.Format("\n  message #{0}", ++msgCount);
            msg.content = xmlDoc.ToString();
            return msg;
        }
        //--------------custom message creation for every request-------------//
        public Message makeMessage(string fromUrl, string toUrl, XElement request)
        {
            Message msg = new Message();
            msg.fromUrl = fromUrl;
            msg.toUrl = toUrl;
            msg.content = request.ToString();
            return msg;
        }

        static void Main(string[] args)
        {
            MessageMaker mm = new MessageMaker();
            Message msg = mm.makeMessage("fromFoo", "toBar");
            Utilities.showMessage(msg);
            Console.Write("\n\n");
        }

    }
}
