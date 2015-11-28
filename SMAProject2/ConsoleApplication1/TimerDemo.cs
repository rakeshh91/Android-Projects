///////////////////////////////////////////////////////////////
// TimerDemo.cs - define timer methods for scheduling        //
// Ver 1.0                                                  //
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
 * This package implements methods to implement 
 * timer/scheduler for persistence.
 */
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   TimerDemo.cs, UtilityExtensions
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
using System.Timers;

namespace Project2Starter
{
    public static class TimerExtensions
    {

    }
    public class TimerDemo
    {
        public Timer schedular { get; set; } = new Timer();

        public TimerDemo()
        {
            schedular.Interval = 5000;
            schedular.AutoReset = true;

            schedular.Elapsed += (object source, ElapsedEventArgs e) =>
            {
                Console.Write("\n  Database persistence event occurred at {0}", e.SignalTime);
            };
        }

        static void Main(string[] args)
        {
            "Demonstrate Timer - needed for scheduled persistance in Project #2".title('=');
            Console.Write("\n\n  press any key to exit\n");

            TimerDemo td = new TimerDemo();
            td.schedular.Enabled = true;
            Console.ReadKey();
            Console.Write("\n\n");
        }
    }
}
