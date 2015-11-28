///////////////////////////////////////////////////////////////
// PersistEngineTest.cs - Test persist engine package        //
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
 * This package replaces persist engine test stub to remove.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   PersistEngine.cs,  DBElement.cs, DBEngine.cs,  
 *   DBFactory.cs, Display.cs, UtilityExtensions.cs
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
    class PersistEngineTest
    {
        static void Main(string[] args)
        {
            "Testing PersistEngine Package".title('=');
            WriteLine();
            DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();
            "\nSave to an XML file".title();
            DBElement<int, string> elem1 = new DBElement<int, string>();
            elem1.name = "Usain Bolt";
            elem1.descr = "Athelte";
            elem1.timeStamp = DateTime.Now;
            elem1.children.AddRange(new List<int> { 2 });
            elem1.payload = "Fastest in the world";
            db.insert(1, elem1);

            DBElement<int, string> elem2 = new DBElement<int, string>();
            elem2.name = "Saina Nehwal";
            elem2.descr = "Badminton Player";
            elem2.timeStamp = DateTime.Now;
            elem2.children.AddRange(new List<int> { 1 });
            elem2.payload = "Famous badminton player";
            db.insert(2, elem2);
            db.showDB();
            WriteLine();

            dynamic allKeys = db.Keys();
            PersistEngine<int,DBElement<int,string>> pEngine = new PersistEngine<int, DBElement<int, string>>(db);
            pEngine.persistToXML(allKeys);
            WriteLine("\n\nAbove database is stored as XML file in local machine");
            WriteLine();

            WriteLine("\nThe persisted XML file along with new key/value pairs are augmented to the database.\n");
            WriteLine("Below shown key/value pairs are augmented to the database.\n");
            pEngine.augmentDatabaseFromXML(db);                                         //Augment the persisted database along with new values to the main database
            pEngine.persistToXML(allKeys);
            db.showDB();
            WriteLine();
            WriteLine();

            "\nPersist database every 5 seconds until its cancelled".title();
            WriteLine();

            pEngine.scheduledSaveDatabase();
            WriteLine();
            WriteLine();

            "\nProject dependancy and realtionships".title();
            WriteLine();
            DBEngine<string, DBElement<string, List<string>>> dependancyDb = new DBEngine<string, DBElement<string, List<string>>>();
            PersistEngine<string, DBElement<string, List<string>>> pEngineString = new PersistEngine<string, DBElement<string, List<string>>>(dependancyDb);
            try
            {
                Console.WriteLine("\nBelow details provide information on dependancy of every package in the project\n");
                pEngine.displayDependancy();
                dependancyDb.showEnumerableDB();
                WriteLine();
            }
            catch (Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
            }
        }
    }
}
