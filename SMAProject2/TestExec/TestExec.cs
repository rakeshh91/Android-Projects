///////////////////////////////////////////////////////////////
// TestExec.cs - Test Requirements for Project #2            //
// Ver 1.2                                                   //
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
 * This package begins the demonstration of meeting requirements.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   TestExec.cs,  DBElement.cs, DBEngine.cs, Display.cs, 
 *   ItemEditor.cs, QueryEngine.cs, PersistEngine.cs, DBFacory.cs UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.2 : 07 Oct 15
 * - third release
 * ver 1.1 : 24 Sep 15
 * ver 1.0 : 18 Sep 15
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Text.RegularExpressions;

namespace Project2Starter
{
    class TestExec
    {
        private DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();
        private DBEngine<string, DBElement<string, List<string>>> dbString = new DBEngine<string, DBElement<string, List<string>>>();
        private DBEngine<string, DBElement<string, List<string>>> dbCategory = new DBEngine<string, DBElement<string, List<string>>>();
        private DBEngine<string, DBElement<string, List<string>>> dependancyDb = new DBEngine<string, DBElement<string, List<string>>>();
        private Dictionary<int, DBElement<int, string>> dictFactory = new Dictionary<int, DBElement<int, string>>();
        private DBFactory<int, DBElement<int, string>> dbFactory;
        private ItemEditor<int, DBElement<int, string>> iEditor;
        private QueryEngine<int, DBElement<int, string>> qEngine;
        private QueryEngine<string, DBElement<string, List<string>>> qEngine2;
        private PersistEngine<int, DBElement<int, string>> pEngine;
        private PersistEngine<string, DBElement<string, List<string>>> pEngineString;
        private Dictionary<string, List<string>> dictCategory = new Dictionary<string, List<string>>();

        void TestR2()
        {
            DBElement<int, string> elem = new DBElement<int, string>();
            elem.name = "India";
            elem.descr = "Country";
            elem.timeStamp = DateTime.Now;
            elem.children.AddRange(new List<int> { 2, 3 });
            elem.payload = "Famous cricket player";
            WriteLine();

            "1) The element is as shown below".title();
            elem.showElement();
            db.insert(1, elem);
            WriteLine();

            "2) The key Value pair is shown below".title();
            db.showDB();
            WriteLine();
        }

        void TestR3()
        {
            "\n1) Inserting key/value pairs to the database".title();
            DBElement<int, string> elem2 = new DBElement<int, string>();                            //Add new key/value pairs to the database
            elem2.name = "Roger federer";
            elem2.descr = "Tennis player";
            elem2.timeStamp = DateTime.Now;
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

            "\n2) Removing key 4 from the database".title();
            db.delete(4);
            db.showDB();
            WriteLine();
        }

        void TestR4()
        {
            "\n1) Editing metadata".title();
            "\nBefore editing metadata for key 1".title();
            db.showDB();
            WriteLine();
            "\nAfter editing metadata for key 1".title();
            iEditor = new ItemEditor<int, DBElement<int, string>>(db);
            iEditor.editMetaData(1, "Sachin Tendulkar", "Cricket player");                     //send the values to be edited to the editMetaData() function
            db.showDB();
            WriteLine();

            "\n2) Adding children".title();
            "\nBefore adding relationship(children) for key 2".title();
            db.showDB();
            WriteLine();
            "\nAfter adding relationship(children) for Key 2".title();
            iEditor.addrelationships(2, new List<int> { 4, 5 });                                //send a new list with children to be added to a key
            db.showDB();
            WriteLine();
            WriteLine();

            "\n3) Deleting children".title();
            "\nBefore deleting relationship(children) for key 2".title();
            db.showDB();
            WriteLine();
            "\nAfter deleting relationship(children) to Key 2".title();                           //send a new list with children to be deleted from a key
            iEditor.deleteRelationships(2, new List<int> { 4, 5 });
            db.showDB();
            WriteLine();
            WriteLine();

            "\n4) Replacing value instance".title();
            DBElement<int, string> elem = new DBElement<int, string>();                         //create a new element for replacing value
            elem.name = "Messi";
            elem.payload = "Plays for Argentina";
            elem.descr = "Football player";
            elem.children.AddRange(new List<int> { 2 });
            elem.timeStamp = DateTime.Now;

            "\nBefore replacing the value instance for key 3".title();
            db.showDB();
            WriteLine();
            "\nAfter replacing the value instance for key 3".title();
            iEditor.replaceValueInstance(3, elem);                                              //send value to be replaced for a key
            db.showDB();
            WriteLine();
        }

        void TestR5()
        {
            WriteLine();
            "\nSave to an XML file".title();
            db.showDB();

            dynamic allKeys = db.Keys();
            pEngine = new PersistEngine<int, DBElement<int, string>>(db);
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
        }

        void TestR6()
        {
            "\nPersist database every 5 seconds until its cancelled".title();
            WriteLine();

            pEngine.scheduledSaveDatabase();
            WriteLine();
            WriteLine();
        }

        void TestR7()
        {
            "\n1) Fetch Value for key 3".title();
            var val = db.getValueOfKey(3);
            val.showElement();
            WriteLine();

            qEngine = new QueryEngine<int, DBElement<int, string>>(db);
            "\n2) Fetch the children of key 1".title();
            var children = qEngine.getChildrenOfKey(1);
            displayChildren(children);

            "\n3) Fetch the keys which starts with r in the below database with key/value pairs".title();             //Taking new database with string keys
            DBElement<string, List<string>> elem = new DBElement<string, List<string>>();
            elem.name = "Christ college";
            elem.descr = "College where the person studied";
            elem.timeStamp = DateTime.Now;
            elem.children.AddRange(new List<string> { "rakesh" });
            elem.payload = new List<string> { "One", "Two", "Three", "Four" };
            dbString.insert("saahith", elem);

            DBElement<string, List<string>> elem1 = new DBElement<string, List<string>>();
            elem1.name = "PESIT college";
            elem1.descr = "College where the person pursued undergraduation";
            elem1.timeStamp = DateTime.Now.AddDays(-2);
            elem1.children.AddRange(new List<string> { "saahith" });
            elem1.payload = new List<string> { "Five", "Six", "Seven", "Eight" };
            dbString.insert("rakesh", elem1);
            dbString.showEnumerableDB();
            WriteLine();

            qEngine2 = new QueryEngine<string, DBElement<string, List<string>>>(dbString);
            string pattern1 = @"(^r)";
            var spKeys = qEngine2.getListStringKeyPattern(pattern1);
            displayStringKeys(spKeys);

            "\n4) Fetch keys with metadata pattern 'Name'".title();
            string pattern = "Name";
            var mdKeys = qEngine.getListMetaDataPattern(pattern);
            displayKeys(mdKeys);

            "\n5) Fetch keys which are created between two dates 29th sept 2015 and 15th oct 2015".title();
            DateTime time1 = new DateTime(2015, 9, 29);
            DateTime time2 = new DateTime(2015, 10, 15);
            var tpKeys = qEngine.getListTimePattern(time1, time2);
            displayKeys(tpKeys);
        }

        void TestR8()
        {
            "\nCreation of immutable database".title();
            WriteLine();
            try
            {
                var keys = qEngine.getListKeyPattern();
                if (keys != null)
                {
                    foreach (var key in keys)
                    {
                        var val = db.getValueOfKey(key);
                        dictFactory.Add(key, val);                                                              //add keys and values to the dictionary
                    }
                    dbFactory = new DBFactory<int, DBElement<int, string>>(dictFactory);                        //store the dictionary in the 
                    WriteLine("\nThe below key/value pairs obtained from query which fetch even keys are saved as an immutable database\n");
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
                WriteLine("\n" + e.Message + "\n");
            }

        }

        void TestR9()
        {
            "\nProject dependancy and realtionships".title();
            WriteLine();
            try
            {
                Console.WriteLine("\nBelow details provide information on dependancy of every package in the project\n");
                pEngineString = new PersistEngine<string, DBElement<string, List<string>>>(dependancyDb);
                pEngineString.displayDependancy();
                dependancyDb.showEnumerableDB();
                WriteLine();
            }
            catch (Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
            }
        }

        void displayCategory(List<string> list)
        {
            WriteLine("\n category: ");
            foreach (var cat in list)
            {
                Write(cat + ",");
            }
            WriteLine("\n");
        }

        void TestR12()
        {
            try
            {
                "\nThe database is shown below:".title();
                WriteLine("\n Key: Apple");
                DBElement<string, List<string>> elem1 = new DBElement<string, List<string>>();
                elem1.name = "Apple";
                elem1.descr = "Type of fruit";
                elem1.timeStamp = DateTime.Now;
                elem1.children.AddRange(new List<string> { "three" });
                elem1.payload = new List<string> { "Seven" };
                elem1.category = new List<string> { "fruit", "drink" };
                dbCategory.insert("Apple", elem1);
                elem1.showEnumerableElement();
                displayCategory(elem1.category);
                WriteLine("\n Key: Banana");
                DBElement<string, List<string>> elem2 = new DBElement<string, List<string>>();
                elem2.name = "Banana";
                elem2.descr = "Type of fruit";
                elem2.timeStamp = DateTime.Now;
                elem2.children.AddRange(new List<string> { "two" });
                elem2.payload = new List<string> { "Five", "Six" };
                elem2.category = new List<string> { "fruit" };
                dbCategory.insert("Banana", elem2);
                elem2.showEnumerableElement();
                displayCategory(elem2.category);
                WriteLine("\n Key: Blackberry");
                DBElement<string, List<string>> elem3 = new DBElement<string, List<string>>();
                elem3.name = "Blackberry";
                elem3.descr = "Type of fruit or phone";
                elem3.timeStamp = DateTime.Now;
                elem3.children.AddRange(new List<string> { "one" });
                elem3.payload = new List<string> { "Eight" };
                elem3.category = new List<string> { "fruit", "phone" };
                dbCategory.insert("Blackberry", elem3);
                elem3.showEnumerableElement();
                displayCategory(elem3.category);
                dictCategory = new Dictionary<string, List<string>>();
                dictCategory.Add("fruit", new List<string> { "Apple", "Banana" });
                List<string> values = new List<string>();
                dictCategory.TryGetValue("fruit", out values);
                dynamic result = qEngine.getKeyForCategory(dbCategory, values);
                "\nMatched keys for category 'fruit'".title();
                displayStringKeys(result);
            }
            catch (Exception e) { WriteLine("\n" + e.Message + "\n"); }
        }

        void stopForExecution()
        {
            WriteLine("\nPress any key for executing next requirement\n");
            Console.ReadKey();
        }

        /*-------------Display Keys------------------*/
        void displayKeys(List<int> keys)
        {
            WriteLine("\nThe keys are:\n");
            try
            {
                if (keys != null)
                {
                    foreach (var key in keys)
                        Write(key + ", ");
                    WriteLine();
                }
                else
                {
                    Console.WriteLine("\nNone of the keys are present\n");
                }
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); }
        }

        /*--------------Display children-------------------*/
        void displayChildren(List<int> child)
        {
            WriteLine("\nThe children are:\n");
            if (child != null)
            {
                foreach (var c in child)
                    Write(c + ", ");
                WriteLine();
            }
            else { Console.WriteLine("\n None of the children are present for specified key\n"); }
        }

        /*--------------Display keys of type string-----------*/
        void displayStringKeys(List<string> keys)
        {
            WriteLine("\nThe keys are:\n");
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
            TestExec exec = new TestExec();
            "Demonstrating Project#2 Requirements".title('=');
            exec.TestR2();
            WriteLine("\n=========================================End of requirement #2==========================================\n");
            exec.stopForExecution();
            "\nDemonstrating Project#3 Requirements".title('=');
            WriteLine();
            exec.TestR3();
            WriteLine("\n=========================================End of requirement #3==========================================\n");
            exec.stopForExecution();
            "\nDemonstrating Project#4 Requirements".title('=');
            WriteLine();
            exec.TestR4();
            WriteLine("\n=========================================End of requirement #4==========================================\n");
            exec.stopForExecution();
            "\nDemonstrating Project#5 Requirements".title('=');
            WriteLine();
            exec.TestR5();
            WriteLine("\n=========================================End of requirement #5==========================================\n");
            exec.stopForExecution();
            "\nDemonstrating Project#6 Requirements".title('=');
            WriteLine();
            exec.TestR6();
            WriteLine("\n=========================================End of requirement #6==========================================\n");
            exec.stopForExecution();
            "\nDemonstrating Project#7 Requirements".title('=');
            WriteLine();
            exec.TestR7();
            WriteLine("\n=========================================End of requirement #7==========================================\n");
            exec.stopForExecution();
            "\nDemonstrating Project#8 Requirements".title('=');
            WriteLine();
            exec.TestR8();
            WriteLine("\n=========================================End of requirement #8==========================================\n");
            exec.stopForExecution();
            "\nDemonstrating project#9 requirements".title('=');
            WriteLine();
            exec.TestR9();
            WriteLine("\n=========================================End of requirement #9==========================================\n");
            exec.stopForExecution();
            WriteLine();
            "Demonstrating project#12 requirements".title('=');
            WriteLine();
            exec.TestR12();
            ReadLine();
        }
    }
}
