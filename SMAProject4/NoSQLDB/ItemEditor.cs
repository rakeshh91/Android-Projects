///////////////////////////////////////////////////////////////
// ItemEditor.cs - used to modify the database contents      //
// Ver 1.1                                                   //
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
 * This package implements ItemEditor<Key, Value> where Value
 * is the DBElement<key, Data> type.
 *
 * This class is a ItemEditor package.
 * This package is used to modify the metadata contents and replacing value instance 
 * for any specified key
 */
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs, DBElement.cs and
 *                 UtilityExtensions.cs only if you enable the test stub
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
  * ver 1.1 : 20 Nov 15
 * - second release
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

namespace Project4Starter
{
    public class ItemEditor<Key,Value>
    {
        private DBEngine<Key,Value> db;

        /*--------------Constructor where keys obtained from a query result are stored to a dictionary---------*/
        public ItemEditor(DBEngine<Key,Value> dbEngine)
        {
            db = dbEngine;
        }

        /*-------------------Function to edit the name and description metadata---------------*/
        public bool editMetaData(Key key, string name, string description)
        {
            try
            {
                if (db.Contains(key))
                {
                    dynamic elem1 = db.getValueOfKey(key);
                    elem1.name = name;
                    elem1.descr = description;
                    return true;
                }
                else
                {
                    WriteLine("\nKey is not present in the dictionary\n");
                    return false;
                }
            }
            catch(Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
                return false;
            }
            
        }

        /*------------------fucntion to add children to a particular key---------------------*/
        public bool addrelationships(Key key, List<Key> children)
        {
            try
            {
                if (db.Contains(key))
                {
                    dynamic elem1 = db.getValueOfKey(key);
                    elem1.children.AddRange(children);
                    return true;
                }
                else
                {
                    WriteLine("\nkey is not present in the dictionary\n");
                    return false;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
        }

        /*------------------Function to delete children of a particular key------------------*/
        public bool deleteRelationships(Key key, List<Key> deletechildren)
        {
            try
            {
                if (db.Contains(key))
                {
                    dynamic elem1 = db.getValueOfKey(key);
                    foreach (Key item in deletechildren)
                    {
                        if (elem1.children.Contains(item))
                        {
                            elem1.children.Remove(item);
                        }
                    }
                    return true;
                }
                else
                {
                    WriteLine("\nKey is not present in the dictionary\n");
                    return false;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
        }

        /*-----------------Function to replace the instance of the value----------------------*/
        public bool replaceValueInstance(Key key, Value val)
        {
            try
            {
                if (db.Contains(key))
                {
                    db.saveValue(key, val);
                    return true;
                }
                else
                {
                    WriteLine("\nKey is not present in the dictionary\n");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString()); return false;
            }

        }
    }

#if (TEST_ITEMEDITOR)

    class TestItemEditor
    {
        static void Main(string[] args)
        {
            "Testing ItemEditor Package".title('=');
            WriteLine();

            Write("\n  All testing of ItemEditor class moved to ItemEditor package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");

            Write("\n\n");
        }
    }
#endif
}

