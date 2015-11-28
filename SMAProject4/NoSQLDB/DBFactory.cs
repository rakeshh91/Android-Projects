///////////////////////////////////////////////////////////////
// DBFactory.cs - creation of immutable database             //
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
 * This package implements DBFactory<Key, Value> where Value
 * is the DBElement<key, Data> type.
 *
 * This class is a DBFactory package.
 * It implements creation of immutable database for any query result
 */
/*
 * Maintenance:
 * ------------
 * Required Files: DBFactory.cs, DBElement.cs and
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
    public class DBFactory<Key, Value>
    {
        private Dictionary<Key, Value> dbStore = new Dictionary<Key, Value>();                      //dictionary to store the key/values

        /*--------------Constructor where keys obtained from a query result are stored to a dictionary---------*/
        public DBFactory(Dictionary<Key, Value> dbFactory)
        {
            foreach (Key key in dbFactory.Keys)
            {
                dbStore[key] = dbFactory[key];
            }
        }

        /*--------------------Get the value of the key--------------------------*/
        public bool getValue(Key key, out Value val)
        {
                if (dbStore.Keys.Contains(key))
                {
                    val = dbStore[key];
                    return true;
                }
                val = default(Value);
                return false;
        }

        /*------------------Fetch all the keys---------------------*/
        public IEnumerable<Key> Keys()
        {
            return dbStore.Keys;
        }
    }

#if (TEST_DBFACTORY)

    class TestDBFactory
    {
        static void Main(string[] args)
        {
            "Testing DBFactory Package".title('=');
            WriteLine();

            Write("\n  All testing of DBFactory class moved to DBFactoryTest package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");

            Write("\n\n");
        }
    }
#endif
}
