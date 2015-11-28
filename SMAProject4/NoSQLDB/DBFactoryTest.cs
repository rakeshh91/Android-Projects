///////////////////////////////////////////////////////////////
// DBFactoryTest.cs - Test DBFactory test package            //
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
 * This package replaces DBFactory test stub to remove.
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

namespace Project4Starter
{

    
    class DBFactoryTest
    {
        public void insertData(DBEngine<int, DBElement<int, string>> db)
        {
            DBElement<int, string> elem1 = new DBElement<int, string>();
            elem1.name = "India";
            elem1.descr = "Country";
            elem1.timeStamp = DateTime.Now;
            elem1.children.AddRange(new List<int> { 2, 3 });
            elem1.payload = "Famous cricket player";
            db.insert(1, elem1);

            DBElement<int, string> elem2 = new DBElement<int, string>();
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

            DBElement<int, string> elem4 = new DBElement<int, string>();
            elem4.name = "Saina Nehwal";
            elem4.descr = "Badminton Player";
            elem4.timeStamp = DateTime.Now;
            elem4.children.AddRange(new List<int> { 2 });
            elem4.payload = "Famous badminton player";
            db.insert(4, elem4);
            db.showDB();
            WriteLine();
        }
#if (TEST_DBFACTORY)
        static void Main(string[] args)
        {
            "Testing DBFactory Package".title('=');
            WriteLine();
            DBFactoryTest dbft = new DBFactoryTest();
            "\nCreation of immutable database".title();
            WriteLine();

            "\nOriginal database".title();
            DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();
            dbft.insertData(db);
            

            "\n Fetch all the keys which are even from the above database".title();
            try
            {
                QueryEngine<int, DBElement<int, string>> qEngine = new QueryEngine<int, DBElement<int, string>>(db);
                Dictionary<int, DBElement<int, string>> dictFactory = new Dictionary<int, DBElement<int, string>>();
                DBFactory<int, DBElement<int, string>> dbFactory;
                var keys = qEngine.getListKeyPattern();
                if (keys != null)
                {
                    foreach (var key in keys)
                    {
                        var val = db.getValueOfKey(key);
                        dictFactory.Add(key, val);                                                              //add keys and values to the dictionary
                    }
                    dbFactory = new DBFactory<int, DBElement<int, string>>(dictFactory);                        //store the dictionary in the 
                    WriteLine("\nThe below key/value pairs with even keys pattern are saved as an immutable database\n");
                    dbFactory.showDB();                                                                         //display the immutable database
                    WriteLine();
                    WriteLine();
                }
                else
                {
                    WriteLine("\nNo keys are obtained from a query for creation of immutable database\n");
                }
                WriteLine();
                WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message + "\n");
            }

        }
#endif
    }
}
