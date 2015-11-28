///////////////////////////////////////////////////////////////
// PersistEngine.cs - used to persist database to XML file   //
// Ver 1.0                                                   //
// Application: Demonstration for CSE681-SMA, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    Dell XPS2700, Core-i7, Windows 10            //
// Source:      Jim Fawcett, CST 4-187, Syracuse University  //
//              (315) 443-3948, jfawcett@twcny.rr.com        //
// Author:      Rakesh Nallapeta Eshwaraiah                  //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package implements PersistEngine<Key, Value> where Value
 * is the DBElement<key, Data> type.
 *
 * This class is a PersistEngine package.
 * This package is used to persist the database contents to XML file
 */
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs, TimerDemo.cs and
 *                 UtilityExtensions.cs only if you enable the test stub
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 07 Oct 15
 * - first release
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Timers;

namespace Project2Starter
{
    public class PersistEngine<Key, Value>
    {
        int num = 0;
        private DBEngine<Key, Value> db;

        /*--------------Constructor where keys obtained from a query result are stored to a dictionary---------*/
        public PersistEngine(DBEngine<Key, Value> dbEngine)
        {
            db = dbEngine;
        }

        /*-----------------Function to persist the database contents to XML doc------------------*/
        public void persistToXML(dynamic keys)
        {
            try
            {
                XDocument xmlDoc = new XDocument();
                XElement database = new XElement("Database");
                xmlDoc.Add(database);
                XElement record = new XElement("Record");
                database.Add(record);

                foreach (var item in keys)
                {
                    XElement key = new XElement("Key", item);
                    database.Add(key);
                    dynamic elem2 = db.getValueOfKey(item);
                    XElement value = new XElement("Value");
                    database.Add(value);
                    XElement name = new XElement("name", elem2.name);
                    value.Add(name);
                    XElement descr = new XElement("descr", elem2.descr);
                    value.Add(descr);
                    XElement timeStamp = new XElement("timeStamp", elem2.timeStamp);
                    value.Add(timeStamp);
                    XElement children = new XElement("children");
                    value.Add(children);
                    foreach (var c in elem2.children)
                    {
                        XElement child = new XElement("child", c);
                        children.Add(child);
                    }
                    XElement payload = new XElement("payload", elem2.payload);
                    value.Add(payload);
                }
                saveXML(xmlDoc);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message + "\n");
            }
        }

        /*--------------save the xml document to the local machine-------------*/
        public void saveXML(dynamic xmlDoc)
        {
            try
            {
                num = num + 1;
                xmlDoc.Save(".\\persistedDatabase_Version_" + num + ".xml");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message + "\n");
            }
        }

        /*-----------------Function to augment persisted xml file to the database-----------------*/

        public void augmentDatabaseFromXML(dynamic db)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(".\\test-doc2.xml");   //path from where xml has to be loaded

                var values = from y in xmlDoc.Descendants("Record")                                             //fetch values from the xml
                             select new DBElement<int, string>()
                             {
                                 name = y.Descendants("name").FirstOrDefault().Value,
                                 descr = y.Descendants("descr").FirstOrDefault().Value,
                                 timeStamp = Convert.ToDateTime(y.Descendants("timeStamp").FirstOrDefault().Value),
                                 children = y.Descendants("children").Descendants("child").Select(child => { return Convert.ToInt32(child.Value); }).ToList(),
                                 payload = y.Descendants("payload").FirstOrDefault().Value,
                                 key = Convert.ToInt32(y.Descendants("Key").First().Value)
                             };
                foreach (var value in values)
                {
                    db.insert(value.key, value);                                                                //insert the key/value pairs to the main database
                }
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); }
        }


        /*-------------------Function to implement scheduled save of database after every 5 seconds-------------------*/
        public void scheduledSaveDatabase()
        {
            try
            {
                TimerDemo timer = new TimerDemo();
                timer.schedular.Enabled = true;                                                                //Elapsed event is triggered
                Console.Write("\n\n  Press any key to stop database persistence\n");

                timer.schedular.Elapsed += (object source, ElapsedEventArgs e) =>                              //persist database to xml every 5 sec
                {
                    var keys = db.Keys();
                    persistToXML(keys);
                };
                Console.ReadKey();                                                                             //persisted until a key is pressed by user 
                timer.schedular.Enabled = false;
            }
            catch(Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
            }                                                                          
        }

        /*------------------------Function to get the project dependancy from the Xml file----------------*/
        public void displayDependancy()
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(".\\projectDependancy.xml");

                var dependancy = from y in xmlDoc.Descendants("Project")                                                //fetch values from the xml
                                 select new DBElement<string, List<string>>()
                                 {
                                     key = y.Descendants("Package").FirstOrDefault().Value,
                                     name = y.Descendants("Package").FirstOrDefault().Value,
                                     descr = y.Descendants("Package").FirstOrDefault().Value+" dependancy packages",
                                     children = y.Descendants("Relationship").Descendants("package").Select(package => { return (package.Value); }).ToList(),
                                     payload = y.Descendants("Relationship").Descendants("package").Select(package => { return (package.Value); }).ToList(),
                                 };
                foreach (dynamic value in dependancy)
                {
                    db.insert(value.key, value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nThe error is {0}.\n", e.Message);
            }
        }

    }

#if (TEST_PERSISTENGINE)

    class TestPersistEngine
    {
        static void Main(string[] args)
        {
            "Testing PersistEngine Package".title('=');
            WriteLine();

            Write("\n  All testing of PersistEngine class moved to PersistEngineTest package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");

            Write("\n\n");
        }
    }
#endif
}
