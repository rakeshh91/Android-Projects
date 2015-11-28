////////////////////////////////////////////////////////////////////////////
// ICommService.cs - Contract for WCF message-passing service             //
// ver 1.2                                                                //
// Author: Rakesh Nallapeta Eshwaraiah                                    //
// Source:Jim Fawcett, CSE681 - Software Modeling and Analysis, Project #4//
////////////////////////////////////////////////////////////////////////////
/*
 * Additions to C# Console Wizard generated code:
 * - Added reference to System.ServiceModel
 * - Added using System.ServiceModel
 * - Added reference to System.Runtime.Serialization
 * - Added using System.Runtime.Serialization
 */
/*
 * Maintenance History:
 * --------------------
 * ver 1.2 : 20 Nov 2015
 * - added service and data contracts for communicating performance information like latency and throughput
 * ver 1.1 : 29 Oct 2015
 * - added comment in data contract
 * ver 1.0 : 18 Oct 2015
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Project4Starter
{
    [ServiceContract(Namespace = "Project4Starter")]
    public interface ICommService
    {
        [OperationContract(IsOneWay = true)]
        void sendMessage(Message msg);
        //------------------operation contracts for performance measures of reader,writer and server-----------//
        [OperationContract(IsOneWay = true)]
        void getPerformanceResultsReader(double avgReaderLatency);
        [OperationContract(IsOneWay = true)]
        void getPerformanceResultsWriter(double avgWriterLatency);
        [OperationContract(IsOneWay = true)]
        void getThroughputServer(double serverThroughput);
        [OperationContract]
        TimingInfo getPerformanceWPF();
    }

    [DataContract]
    public class Message
    {
        [DataMember]
        public string fromUrl { get; set; }
        [DataMember]
        public string toUrl { get; set; }
        [DataMember]
        public string content { get; set; }  // will hold XML defining message information
    }

    //------------Data contract for performance measure-------------//
    [DataContract]
    public class TimingInfo
    {
        [DataMember]
        public double wClientLatency { get; set; }
        [DataMember]
        public double rClientLatency { get; set; }
        [DataMember]
        public double srvrThroughput { get; set; }
    }
}
