///////////////////////////////////////////////////////////////
// QueryEngine.cs - used to query the database               //
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
 * This package implements QueryEngine<Key, Value> where Value
 * is the DBElement<key, Data> type.
 *
 * This class is a QueryEngine package.
 * This package is used to query the database
 */
/*
 * Maintenance:
 * ------------
 * Required Files: DBEngine.cs and
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
using System.Text.RegularExpressions;

namespace Project2Starter
{
    public class QueryEngine<Key, Value>
    {
        private DBEngine<Key, Value> db;

        /*--------------Constructor where keys obtained from a query result are stored to a dictionary---------*/
        public QueryEngine(DBEngine<Key, Value> dbEngine)
        {
            db = dbEngine;
        }

        /*----------------------------------Category implementation----------------------------*/
        public List<string> getKeyForCategory(DBEngine<string, DBElement<string, List<string>>> dbCat, List<string> category)
        {
            dynamic keys = dbCat.Keys();
            List<string> matchedKeys = new List<string>();
            foreach(var cat in category)
            {
                foreach(var key in keys)
                {
                    if(cat == key)
                        matchedKeys.Add(key);
                }
            }
            return matchedKeys;
        }

        /*-------------------Get the chidren of a particular key-----------------*/
        public List<Key> getChildrenOfKey(Key key)
        {
            try
            {
                if (db.Contains(key))
                {
                    dynamic elem1 = db.getValueOfKey(key);
                    return elem1.children;
                }
                else
                {
                    WriteLine("\nKey is not present in the dictionary\n");
                    return (default(List<Key>));
                }
            }
            catch (Exception e)
            {
                WriteLine("\n" + e.Message + "\n");
                return default(List<Key>);
            }
        }

        /*-------------------Query for particular pattern in keys------------------*/
        public List<int> getListKeyPattern()
        {
            try
            {
                List<Key> keyList = new List<Key>(db.Keys());
                List<int> ikeys = new List<int>();
                List<int> kpKeys = new List<int>();

                if (keyList != null)
                {
                    foreach (var item in keyList)
                    {
                        ikeys.Add(Convert.ToInt32(item));
                    }
                    foreach (int i in ikeys)
                    {
                        if (i % 2 == 0)                                                     //returning the keys which are even
                        {
                            kpKeys.Add(i);
                        }
                    }
                }
                else
                {
                    WriteLine("\nKeys are not present in the dictionary\n");
                }
                return kpKeys;
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); return (default(List<int>)); }
        }

        /*-----------Query for particular string pattern in key------------------*/
        public List<Key> getListStringKeyPattern(string reg)
        {
            try
            {
                List<Key> keys = new List<Key>(db.Keys());
                List<Key> spKeys = new List<Key>();

                if (keys != null)
                {
                    foreach (var item in keys)
                    {
                        if (Regex.IsMatch(item.ToString(), reg))
                        {
                            spKeys.Add(item);
                        }
                    }
                }
                else
                {
                    WriteLine("\nKeys are not present in the dictionary\n");
                }
                return spKeys;
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); return (default(List<Key>)); }
        }

        /*----------------Query for particular pattern in metadata-----------------*/
        public List<Key> getListMetaDataPattern(string pattern)
        {
            try
            {
                List<Key> keys = new List<Key>(db.Keys());
                List<Key> mdKeys = new List<Key>();

                if (keys != null)
                {
                    foreach (var item in keys)
                    {
                        dynamic elem1 = db.getValueOfKey(item);
                        if (((Regex.IsMatch(elem1.name, pattern)) || (Regex.IsMatch(elem1.descr, pattern))))
                        {
                            mdKeys.Add(item);
                        }
                    }
                }
                else
                {
                    WriteLine("\nKeys are not present in the dictionary\n");
                }
                return mdKeys;
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); return (default(List<Key>)); }

        }

        /*----------------Query for the data between any two specific time period--------------*/
        public List<Key> getListTimePattern(DateTime startTime, DateTime endTime)
        {
            try
            {
                List<Key> keys = new List<Key>(db.Keys());
                List<Key> tpKeys = new List<Key>();

                if (keys != null)
                {
                    if (startTime != null)
                    {
                        foreach (var item in keys)
                        {
                            dynamic elem1 = db.getValueOfKey(item);
                            if (endTime != null)
                            {
                                if (elem1.timeStamp >= startTime && elem1.timeStamp <= endTime)  //check whether key falls between two specified dates
                                    tpKeys.Add(item);
                            }
                            else if (elem1.timeStamp >= startTime)                              //if endTime is not given then take all keys created after start
                                tpKeys.Add(item);
                            else
                                Console.WriteLine("\nNo keys are present within the time interval\n");
                        }
                    }
                    else
                        Console.WriteLine("\nStart time is not mentioned\n");
                }
                return tpKeys;
            }
            catch (Exception e) { Console.WriteLine("\n" + e.Message + "\n"); return (default(List<Key>)); }

        }
    }

#if (TEST_QUERYENGINE)

    class TestQueryEngine
    {
        static void Main(string[] args)
        {
            "Testing QueryEngine Package".title('=');
            WriteLine();

            Write("\n  All testing of QueryEngine class moved to QueryEngineTest package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");

            Write("\n\n");
        }
    }
#endif
}

