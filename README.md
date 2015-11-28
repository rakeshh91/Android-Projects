# Software-Modelling-and-Analysis
Key/Value in memory NoSQL database. 

In the SMAProject2, a NoSQL in memory database is implemented using C# and Microsoft Visual studio.
Some of the functionalities of this project are,
- Generic type database
- Add/Delete/edit key-value pairs in the database
- Persist contents of the database to an XML file to a Local machine 
- Scheduling the persistence
- Querying the NoSQL database
- Creation of an immutable database from the results of the queries

In the SMAProject4, the developed NoSQL database is used from the remote clients and operations are executed from those clients on the database. Some of the fucntionalities of this project are,
- WCF is used to communicate between clients and a server that exposes the noSQL database through messages that are sent by clients and enqueued by the server
- testing the performance of the system and understanding how the server and the client behaves with varying loads
- display the performance timing information on the WPF client
- to demonstrate that the required operations implemented in Project2 can be executed remotely
