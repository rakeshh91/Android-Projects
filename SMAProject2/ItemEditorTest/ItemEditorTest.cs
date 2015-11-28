///////////////////////////////////////////////////////////////
// ItemEditorTest.cs - Test Item editor package              //
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
 * This package replaces Item editor test stub to remove.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   DBFactory.cs, Display.cs, ItemEditor.cs,  DBElement.cs, DBEngine.cs,  
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
    class ItemEditorTest
    {
        static void Main(string[] args)
        {
            "Testing ItemEditor Package".title('=');
            WriteLine();

            DBEngine<int,DBElement<int,string>> db = new DBEngine<int, DBElement<int, string>>();
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

            ItemEditor<int, DBElement<int,string>> iEditor = new ItemEditor<int, DBElement<int,string>>(db);
            "\n1) Editing metadata".title();
            "\nBefore editing metadata for key 1".title();
            db.showDB();
            WriteLine();
            "\nAfter editing metadata for key 1".title();
            iEditor = new ItemEditor<int, DBElement<int, string>>(db);
            iEditor.editMetaData(1, "Sachin Tendulkar", "Cricket player");                     //send the values to be edited to the editMetaData() function
            db.showDB();
            WriteLine();
            WriteLine();

            "\n2) Adding children".title();
            "\nBefore adding relationship(children) for key 1".title();
            db.showDB();
            WriteLine();
            "\nAfter adding relationship(children) for Key 1".title();
            iEditor.addrelationships(1, new List<int> { 3 });                                //send a new list with children to be added to a key
            db.showDB();
            WriteLine();
            WriteLine();

            "\n3) Deleting children".title();
            "\nBefore deleting relationship(children) for key 1".title();
            db.showDB();
            WriteLine();
            "\nAfter deleting relationship(children) to Key 1".title();                           //send a new list with children to be deleted from a key
            iEditor.deleteRelationships(1, new List<int> { 3 });
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

            "\nBefore replacing the value instance for key 2".title();
            db.showDB();
            WriteLine();
            "\nAfter replacing the value instance for key 2".title();
            iEditor.replaceValueInstance(2, elem);                                              //send value to be replaced for a key
            db.showDB();
            WriteLine();

        }
    }
}
