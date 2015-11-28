///////////////////////////////////////////////////////////////
// QueryEngineTest.cs - Test Query Engine test package       //
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
 * This package replaces Query Engine test stub to remove.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   DBFactory.cs, Display.cs, QueryEngine.cs,  DBElement.cs, DBEngine.cs,  
 *   DBExtensions.cs, UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.0 : 07 Oct 15
 * - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project2Starter
{
    class QueryEngineTest
    {
        public static void displayKeys(List<int> keys)
        {
            Console.WriteLine("\nThe keys are:\n");
            try
            {
                if (keys != null)
                {
                    foreach (var key in keys)
                        Write(key + ", ");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("\nNone of the keys are present\n");
                }
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); }
        }

        /*--------------Display children-------------------*/
        public static void displayChildren(List<int> child)
        {
            Console.WriteLine("\nThe children are:\n");
            if (child != null)
            {
                foreach (var c in child)
                    Write(c + ", ");
                Console.WriteLine();
            }
            else { Console.WriteLine("\n None of the children are present for specified key\n"); }
        }

        /*--------------Display keys of type string-----------*/
        public static void displayStringKeys(List<string> keys)
        {
            Console.WriteLine("\nThe keys are:\n");
            if (keys != null)
            {
                foreach (var key in keys)
                    Write(key + ", ");
                WriteLine();
            }
            else { Console.WriteLine("\n None of the keys are present with the pattern mentioned\n"); }
        }

        static void Main(string[] args)
        {
            "Testing ItemEditor Package".title('=');
            Console.WriteLine();

            DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();

            DBElement<int, string> elem1 = new DBElement<int, string>();
            elem1.name = "India";
            elem1.descr = "Country";
            elem1.timeStamp = DateTime.Now;
            elem1.children.AddRange(new List<int> { 2, 3 });
            elem1.payload = "Famous cricket player";
            db.insert(1, elem1);
            

            DBElement<int, string> elem2 = new DBElement<int, string>();                            //Add new key/value pairs to the database
            elem2.name = "Roger federer";
            elem2.descr = "Tennis player";
            elem2.timeStamp = DateTime.Now.AddDays(-15);
            elem2.children.AddRange(new List<int> { 3 });
            elem2.payload = "Famous tennis player";
            db.insert(2, elem2);

            DBElement<int, string> elem3 = new DBElement<int, string>();
            elem3.name = "Usain Bolt";
            elem3.descr = "Athelte";
            elem3.timeStamp = DateTime.Now;
            elem3.children.AddRange(new List<int> { 1 });
            elem3.payload = "Fastest in the world";
            db.insert(3, elem3);
            Console.WriteLine();
            db.showDB();

            "\n1) Fetch Value for key 3".title();
            var val = db.getValueOfKey(3);
            val.showElement();
            Console.WriteLine();

            QueryEngine<int,DBElement<int,string>> qEngine = new QueryEngine<int, DBElement<int, string>>(db);
            "\n2) Fetch the children of key 1".title();
            var children = qEngine.getChildrenOfKey(1);
            displayChildren(children);

            DBEngine<string, DBElement<string, List<string>>> db2 = new DBEngine<string, DBElement<string, List<string>>>();
            "\n 3) Fetch the keys which starts with r in the below database with key/value pairs".title();             //Taking new database with string keys
            DBElement<string, List<string>> elem4 = new DBElement<string, List<string>>();
            elem4.name = "Christ college";
            elem4.descr = "College where the person studied";
            elem4.timeStamp = DateTime.Now;
            elem4.children.AddRange(new List<string> { "rakesh" });
            elem4.payload = new List<string> { "One", "Two", "Three", "Four" };
            db2.insert("saahith", elem4);

            DBElement<string, List<string>> elem5 = new DBElement<string, List<string>>();
            elem5.name = "PESIT college";
            elem5.descr = "College where the person pursued undergraduation";
            elem5.timeStamp = DateTime.Now.AddDays(-2);
            elem5.children.AddRange(new List<string> { "saahith" });
            elem5.payload = new List<string> { "Five", "Six", "Seven", "Eight" };
            db2.insert("rakesh", elem5);
            db2.showEnumerableDB();
            WriteLine();

            QueryEngine<string, DBElement<string, List<string>>> qEngine2 = new QueryEngine<string, DBElement<string, List<string>>>(db2);
            string pattern1 = @"(^r)";
            var spKeys = qEngine2.getListStringKeyPattern(pattern1);
            displayStringKeys(spKeys);

            "\n4) Fetch keys with metadata pattern 'Country'".title();
            string pattern = "Country";
            var mdKeys = qEngine.getListMetaDataPattern(pattern);
            displayKeys(mdKeys);

            "\n5) Fetch keys which are created between two dates 29th sept 2015 and 15th oct 2015".title();
            DateTime time1 = new DateTime(2015, 9, 29);
            DateTime time2 = new DateTime(2015, 10, 15);
            var tpKeys = qEngine.getListTimePattern(time1, time2);
            displayKeys(tpKeys);
        }
    }
}
