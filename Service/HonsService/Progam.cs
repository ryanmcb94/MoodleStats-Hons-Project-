using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Text;
using CryptSharp;
using System.Net;
using System.Net.Security;
using System.IO;
using MoodleObjects;

namespace HonsService
{
    public class Progam
    {
        static void Main(string[] args)
        {
            MoodleUser user = MoodleDB.getMoodleDB().getUserByID(3);
            List<MoodleUser> users = MoodleDB.getMoodleDB().getUserWithSameModules(user);

            Console.ReadLine();
        }
        static void locationsget()
        {
            List<LocationEvent> evts = LocationDB.getLocationDB().getUserLocation(1);

        }
        static void mapTetst()
        {

            double x = 555601.5;
            double y = 31247.8;
            List<Campus> cam = LocationDB.getLocationDB().getCampus();
            foreach (Campus c in cam)
            {
                if (c.isIn(x, y))
                {
                    Console.WriteLine(c.name);
                }
            }
        }
        static void addLocations()
        {
            List<MoodleEvent> events = MoodleDB.getMoodleDB().getDaysEvents();
            Random rnd = new Random();
            int craig = 0;
            int merch = 0;
            int off = 0;
            foreach (MoodleEvent evt in events)
            {


                int num = rnd.Next(1,6);
                if (num == 1) //craig
                {
                    LocationDB.getLocationDB().addUserLocation(evt.userID, 555507.3, 31423.4, evt.time);
                    craig++;
                }
                else if (num == 2) //Merch
                {
                    LocationDB.getLocationDB().addUserLocation(evt.userID, 555559.2, 31252.3, evt.time.AddMinutes(2));
                    merch++;
                }
                else//off
                {
                    LocationDB.getLocationDB().addUserLocation(evt.userID, 554039.9, 34650.7, evt.time.AddMinutes(2));
                    off++;
                }
                Console.WriteLine("Craig: {0}, Merch: {1} and Off: {2}",craig,merch,off);
            }
        }
        static void bestMatches()
        {
            MoodleUser user = MoodleDB.getMoodleDB().getUserByID(4);
            List<MoodleUser> users = System.getSystem().getBestMatches(user);
            foreach (MoodleUser mu in users)
            {
                Console.WriteLine(mu.toString());
            }
        }
    }
} 