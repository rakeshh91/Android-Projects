///////////////////////////////////////////////////////////////////////////////
// DBEngine.cs - used to maintain the database and perform basic operations  //
// Ver 1.3                                                                   //
// Application: Demonstration for CSE681-SMA, Project#2                      //
// Language:    C#, ver 6.0, Visual Studio 2015                              //
// Platform:    Dell XPS2700, Core-i7, Windows 10                            //
// Source:      Jim Fawcett, CST 4-187, Syracuse University                  //
//              (315) 443-3948, jfawcett@twcny.rr.com                        //
// Author:      Rakesh Nallapeta Eshwaraiah                                  //
///////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package implements DBEngine<Key, Value> where Value
 * is the DBElement<key, Data> type.
 *
 * This class is a DBEngine package.
 * It implements requirements for the db, e.g., insert elements, remove elements,
 * fetch value of a key, checking whether key is present in the database
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
 * ver 1.3 : 20 Nov 15
 * - fourth release
 *  ver 1.2 : 24 Sep 15
 * - removed extensions methods and tests in test stub
 * - testing is now done in DBEngineTest.cs to avoid circular references
 * ver 1.1 : 15 Sep 15
 * - fixed a casting bug in one of the extension methods
 * ver 1.0 : 08 Sep 15
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
    public class DBEngine<Key, Value>
    {
        private Dictionary<Key, Value> dbStore;

        public DBEngine()
        {
            dbStore = new Dictionary<Key, Value>();
        }

        /*-------------Function to insert the key and value-----------------*/
        public bool insert(Key key, Value val)
        {
            try
            {
                if (dbStore.Keys.Contains(key))
                    return false;
                dbStore[key] = val;
                return true;
            }
            catch (Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
                return false;
            }
        }

        /*-----------------Check whether key is present in the database----------*/
        public bool Contains(Key key)
        {
            try
            {
                if (dbStore.Keys.Contains(key))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
                return false;
            }
            
        }

        /*------------Function to save the new value instance for a key------------*/
        public bool saveValue(Key key, Value val)
        {
            try
            {
                if (dbStore.Keys.Contains(key))
                {
                    dbStore[key] = val;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
                return false;
            }
        }

        /*-------------Function to delete the key--------------------------*/
        public bool delete(Key key)
        {
            try
            {
                if (dbStore.Keys.Contains(key))
                {
                    dbStore.Remove(key);
                    return true;
                }
                else
                {
                    WriteLine("Key is not present in the database");
                    return false;
                }
            }
            catch (Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
                return false;
            }
        }

        /*--------------Function to get the value for a specified key--------------*/
        public bool getValue(Key key, out Value val)
        {
            try
            {
                if (dbStore.Keys.Contains(key))
                {
                    val = dbStore[key];
                    return true;
                }
                val = default(Value);
                return false;
            }
            catch (Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
                val = default(Value);
                return false;
            }
        }

        /*----------Function to fetch all the keys-----------*/
        public IEnumerable<Key> Keys()
        {
            return dbStore.Keys;
        }

        /*--------------------Get the value of the key--------------------------*/
        public Value getValueOfKey(Key key)
        {
            try
            {
                if (dbStore.Keys.Contains(key))
                {
                    return dbStore[key];
                }
                else
                {
                    WriteLine("Key is not present in the database");
                    return default(Value);
                }
            }
            catch (Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
                return default(Value);
            }
        }
    }

#if (TEST_DBENGINE)

    class TestDBEngine
    {
        static void Main(string[] args)
        {
            "Testing DBEngine Package".title('=');
            WriteLine();

            Write("\n  All testing of DBEngine class moved to DBEngineTest package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");

            Write("\n\n");
        }
    }
#endif
}
