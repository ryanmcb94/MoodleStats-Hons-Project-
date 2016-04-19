using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleObjects;
using System.Globalization;

namespace HonsService
{
    public class System
    {
        //Define Varibles
        public List<Campus> campus { get; set; }
        private static System sys;
        /***********************
        *Constructors and get instances
        ****************************/
        /// <summary>
        /// Constructor for system.
        /// </summary>
        private System()
        {
            this.campus = LocationDB.getLocationDB().getCampus();
        }

        /// <summary>
        /// Get system instance
        /// </summary>
        /// <returns>System Instance</returns>
        public static System getSystem()
        {
            if(sys ==null)
            {
                sys = new System();
            }
            return sys;
        }
			
        /*****************************
        *Update locationDB
        ******************************/
        /// <summary>
        /// Updates the LocationDB
        /// </summary>
        public void updatesLocationDB()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && DateTime.Now.Add(new TimeSpan(7, 0, 0, 0, 0)).Month != DateTime.Now.Month)
            { //Check if this is the last friday of month.
                updateLocationMonth();
                LocationDB.getLocationDB().removeWeek();
            }
            updateWeek();
            LocationDB.getLocationDB().removeDay();
        }


        /// <summary>
        /// Sort the list by UserID
        /// </summary>
        /// <param name="todaysEvents"></param>
        /// <returns></returns>
        public List<MoodleEvent> sortEventsList(List<MoodleEvent>todaysEvents)
        {
            int max = -1;
            foreach (MoodleEvent evt in todaysEvents)
            {
                if (evt.userID > max)
                {
                    max = evt.userID;
                }
            }
            List<MoodleEvent> tempTodaysEvent = new List<MoodleEvent>();
            for (int i = 0; i < max + 1; i++)
            {
                foreach (MoodleEvent evt in todaysEvents)
                {
                    if (evt.userID == i)
                    {
                        tempTodaysEvent.Add(evt);
                    }
                }
            }
            return tempTodaysEvent;
        }

        /// <summary>
        /// Sort list by UserID
        /// </summary>
        /// <param name="avgs"></param>
        /// <returns></returns>
        public List<LocationAvg> sortLocationAvg(List<LocationAvg> avgs)
        {
            int max = -1;
            foreach(LocationAvg avg in avgs)
            {
                if(avg.userID > max)
                {
                    max = avg.userID;
                }
            }
            List<LocationAvg> tempAvg = new List<LocationAvg>();
            for (int i = 0; i < max++; i++)
            {
                foreach (LocationAvg avg in avgs)
                {
                    if (avg.userID == i)
                    {
                        tempAvg.Add(avg);
                    }
                }
            }
            return tempAvg;

        }
        
        /// <summary>
        /// Updates the location DB taking all of the items
        /// from week and adding them to the week
        /// </summary>
        private void updateWeek()
        {
            ///Get and Sort Events.
            DateTime date = DateTime.Now;
            List<MoodleEvent> todaysEvents = MoodleDB.getMoodleDB().getDaysEvents(); //Change
            todaysEvents = System.getSystem().sortEventsList(todaysEvents);
            
            List<MoodleEvent> userEvents = new List<MoodleEvent>();

            //Fixes Issue with foreach.
            MoodleEvent e = new MoodleEvent(0,0,0,"",DateTime.Now);
            todaysEvents.Add(e);
            int currentID = -1;
            foreach (MoodleEvent evt in todaysEvents)
            {
                if (currentID == evt.userID) //Sane user.
                {
                    userEvents.Add(evt);
                }
                //Current event has diffrent user than the current list.
                //so counts the current list, add to database then deals with
                //current event.
                else if (currentID != evt.userID && userEvents.Count > 0)
                {
                    int countOff = 0;
                    int countOn = 0;
                    Dictionary<MoodleCourse, List<int>> courseAvgs = new Dictionary<MoodleCourse, List<int>>();
                    //Get user locations
                    List<LocationEvent> Locations = LocationDB.getLocationDB().getUserLocation(evt.userID);
                    foreach (MoodleEvent userEvt in userEvents)
                    {
                        
                        //Compare events and location
                        foreach (MoodleEvent moodleEvt in userEvents) //Events
                        {
                            bool found = false;
                            foreach (LocationEvent loc in Locations) //Locations
                            {
                                //Check Time within 10 mins
                                DateTime timeplus = new DateTime();
                                timeplus = loc.time;
                                timeplus.AddMinutes(10);
                                if (Math.Abs((moodleEvt.time - loc.time).TotalMinutes) <= 10 && Math.Abs((moodleEvt.time - loc.time).TotalMinutes) >= 0)
                                {
                                    //Find if on campus
                                    foreach (Campus c in System.getSystem().campus)
                                    {
                                        if (c.isIn(loc.lat, loc.lng)) //Check Location
                                        {
                                            countOn++;
                                            found = true;
                                            bool foundCourse = false;

                                            
                                            MoodleCourse mc = null;
                                            foreach (MoodleCourse course in courseAvgs.Keys)
                                            {
                                                if (course.ID == moodleEvt.courseID)
                                                {
                                                    foundCourse = true;
                                                    mc = course;
                                                    break;
                                                }
                                            }
                                            //Update
                                            if (foundCourse)
                                            {
                                                List<int> list = courseAvgs[mc];
                                                list[0] = list[0]++;
                                                courseAvgs[mc] = list;
                                                mc = null;
                                            }
                                            else //Add Course
                                            {
                                                List<int> list = new List<int>();
                                                list.Add(1);
                                                list.Add(0);
                                                if (moodleEvt.courseID > 0)
                                                {
                                                    mc = MoodleDB.getMoodleDB().getCourse(moodleEvt.courseID);
                                                    if (mc != null)
                                                    {
                                                        courseAvgs.Add(mc, list);
                                                    }
                                                    mc = null;
                                                }
                                            }
                                        }

                                    }
                                    if (!found)
                                    {
                                        countOff++;
                                        bool foundCourse = false;
                                        MoodleCourse mc=null;
                                        foreach (MoodleCourse course in courseAvgs.Keys)
                                        {
                                            if (course.ID == moodleEvt.courseID)
                                            {
                                                mc = course;
                                                foundCourse = true;
                                            }
                                        }
                                        if (evt.courseID > 0)
                                        {
                                            //Update
                                            if (foundCourse)
                                            {
                                                List<int> list = courseAvgs[mc];
                                                list[1] = list[1]++;
                                                courseAvgs[mc] = list;
                                                mc = null;
                                            }
                                            else //course not in list
                                            {
                                                List<int> list = new List<int>();
                                                list.Add(0);
                                                list.Add(1);
                                                courseAvgs.Add(MoodleDB.getMoodleDB().getCourse(moodleEvt.courseID), list);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //get time at campus
                    int timeOn = countOn*10;
                    int timeOff = countOff *10;

                    //Add to Database.
                    //Generic.
                    LocationDB.getLocationDB().addLocationWeekEvents(currentID, timeOn, timeOff);
                    //Per Course.
                    foreach (MoodleCourse course in courseAvgs.Keys)
                    {
                        List<int> list = courseAvgs[course];
                        int on = list[0] * 10;
                        int off = list[1] * 10;
                        //Add to DB.
                        LocationDB.getLocationDB().addLocationWeekEventsCourse(currentID, course.ID, on, off);
                    }
                    userEvents = new List<MoodleEvent>();

                } // End Final
                else if (currentID != evt.userID)
                {
                    currentID = evt.userID;
                    userEvents.Add(evt);
                }
            }
        }

        /// <summary>
        /// Updates all of the item from week 
        /// and adding them to the month
        /// </summary>
        public void updateLocationMonth()
        {
            //Generic Avgs.
            List<LocationAvg> evts = LocationDB.getLocationDB().getWeekAvg();
            evts = this.sortLocationAvg(evts);
            int currentID = 0;
            int countOff = 0;
            int countOn = 0;


            foreach(LocationAvg avg in evts)
            {
                if(avg.userID == currentID)
                {
                    countOff = +avg.off;
                    countOn = +avg.on;
                }
                else if(avg.userID !=currentID && (countOn >0 && countOff >0 || countOn >0 || countOff >0))
                {
                    LocationDB.getLocationDB().addMonthAverage(avg.userID, countOn, countOff);
                    countOff = 0;
                    countOn = 0;
                    currentID = avg.userID;
                    countOff = +avg.off;
                    countOn = +avg.on;
                }
                else
                {
                    currentID = avg.userID;
                }
            }
            //Course Avgs.
			Dictionary<int,List<LocationAvg>> courseAvgs = new Dictionary<int, List<LocationAvg>> ();		currentID = 0;
			countOff = 0;
			countOn = 0;
			List<LocationAvg> courseEvt = LocationDB.getLocationDB ().getCourseWeekAvg();
			evts.Sort ();

			foreach (LocationAvg avg in courseEvt) 
			{
				if (avg.userID == currentID) 
				{
					if(courseAvgs.ContainsKey(avg.courseID))
						{
							courseAvgs [avg.courseID].Add (avg);
						}
						else
						{
						List<LocationAvg> newList = new List<LocationAvg> ();
						newList.Add (avg);
						courseAvgs.Add(avg.courseID,newList);
						}


				}
				else if(avg.userID !=currentID && courseEvt.Count>0)
				{
					foreach(int i in courseAvgs.Keys)
					{
						LocationDB.getLocationDB().addYearAverage(currentID,courseEvt[i].courseID,courseEvt[i].on,courseEvt[i].off);
					}
				}
        	}
		}

        public void updateLocationDBYear()
        {
            List<LocationAvg> evts = LocationDB.getLocationDB().getYearAvg();
            evts = this.sortLocationAvg(evts);
            int currentID = 0;
            int countOff = 0;
            int countOn = 0;


            foreach (LocationAvg avg in evts)
            {
                if (avg.userID == currentID)
                {
                    countOff = +avg.off;
                    countOn = +avg.on;
                }
                else if (avg.userID != currentID && (countOn > 0 && countOff > 0 || countOn > 0 || countOff > 0))
                {

                    LocationDB.getLocationDB().addYearAverage(avg.userID, countOn, countOff);
                    countOff = 0;
                    countOn = 0;
                    currentID = avg.userID;
                    countOff = +avg.off;
                    countOn = +avg.on;
                }
                else
                {
                    currentID = avg.userID;
                }
            }
            //Course Avgs.
            Dictionary<int, List<LocationAvg>> courseAvgs = new Dictionary<int, List<LocationAvg>>(); currentID = 0;
            countOff = 0;
            countOn = 0;
            List<LocationAvg> courseEvt = LocationDB.getLocationDB().getCourseYearAvg();
            evts.Sort();

            foreach (LocationAvg avg in courseEvt)
            {
                if (avg.userID == currentID)
                {
                    if (courseAvgs.ContainsKey(avg.courseID))
                    {
                        courseAvgs[avg.courseID].Add(avg);
                    }
                    else
                    {
                        List<LocationAvg> newList = new List<LocationAvg>();
                        newList.Add(avg);
                        courseAvgs.Add(avg.courseID, newList);
                    }


                }
                else if (avg.userID != currentID && courseEvt.Count > 0)
                {
                    foreach (int i in courseAvgs.Keys)
                    {
                        LocationDB.getLocationDB().addYearAverage(currentID, courseEvt[i].courseID, courseEvt[i].on, courseEvt[i].off);
                    }
                }
            }

        }
        /******************************
        *Get user matches
        ******************************/


        /// <summary>
        /// Gets a user month and there matches avg
        /// </summary>
        /// <param name="ID">UserID</param>
        /// <returnsThe full course avg user then matches then each of the module avgs</returns>
        public Dictionary<MoodleCourse, List<LocationAvg>> getAvgsMonth(int ID)
        {
            MoodleUser user = MoodleDB.getMoodleDB().getUserByID(ID);
            if(user == null)
            {
                return null;
                Console.WriteLine("No User");
            }
            List<MoodleCourse> courses = MoodleDB.getMoodleDB().getUsersCourses(user);
            List<MoodleUser> matches = System.getSystem().getBestMatches(user);
            Dictionary<MoodleCourse, List<LocationAvg>> averages = new Dictionary<MoodleCourse, List<LocationAvg>>();
            List<LocationAvg> avgs = LocationDB.getLocationDB().getAveragesMonth(matches);

            ///Overall
            //User
            LocationAvg userAvg = LocationDB.getLocationDB().getUsersAvgMonth(ID);
            MoodleCourse global = new MoodleCourse(-1, "Global", "Global", "");
            //Match Avg
            int totalOn = 0, totalOff = 0;
            foreach (MoodleUser avg in matches)
            {
                LocationAvg matchAvg = LocationDB.getLocationDB().getUsersAvgMonth(ID);
                if (matchAvg != null)
                {
                    totalOn = totalOn + matchAvg.on;
                    totalOff = totalOff + matchAvg.off;
                }
            }
            int avgMatchOn = 0,avgMatchOff = 0;
            //On
            if (matches.Count > 0 && totalOn > 0)
                avgMatchOn = totalOn / matches.Count;
            if (matches.Count > 0 && totalOff > 0)
                avgMatchOff = totalOff / matches.Count;
            LocationAvg overallAvg = new LocationAvg(-1, DateTime.Now, avgMatchOn, avgMatchOff);
            List<LocationAvg> locationAvgs = new List<LocationAvg>();
            locationAvgs.Add(userAvg);
            locationAvgs.Add(overallAvg);
            averages.Add(global, locationAvgs);

            //Per Course
            //User
            foreach (MoodleCourse course in courses)
            {
                int cTotalOn=0,cTotalOff =0;
                //User
                LocationAvg avg = LocationDB.getLocationDB().getUsersAvgMonth(course.ID, user.ID);

                int count = 0;
                foreach (MoodleUser mUser in matches)
                {
                    foreach(MoodleCourse c in courses)
                    {
                        if (course.ID == c.ID)
                        {
                            LocationAvg tempLA = LocationDB.getLocationDB().getUsersAvgMonth(course.ID, mUser.ID);
                            if (tempLA != null)
                            {
                                cTotalOn = cTotalOn + tempLA.on;
                                cTotalOff = cTotalOff + tempLA.off;
                                count++;
                            }

                        }
                    }

                }

                int cAvgMatchOn = 0,cAvgMatchOff=0;
                if (matches.Count > 0 && cTotalOn > 0)
                {
                    cAvgMatchOn = cTotalOn / matches.Count;
                    cAvgMatchOff = cTotalOff / matches.Count;
                }
                List<LocationAvg> cAvgs = new List<LocationAvg>();
                LocationAvg locAvg = new LocationAvg(-1, DateTime.Now, cAvgMatchOn, cAvgMatchOff);
                cAvgs.Add(avg);
                cAvgs.Add(locAvg);
                averages.Add(course, cAvgs);
            }
            return averages;
        }

        /// <summary>
        /// Gets a user week and there matches avg
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Dictionary<MoodleCourse, List<LocationAvg>> getAvgsWeek(int ID)
        {
            MoodleUser user = MoodleDB.getMoodleDB().getUserByID(ID);
            if(user == null)
            {
                return null;
            }
            List<MoodleCourse> courses = MoodleDB.getMoodleDB().getUsersCourses(user);
            List<MoodleUser> matches = System.getSystem().getBestMatches(user);
            Dictionary<MoodleCourse, List<LocationAvg>> averages = new Dictionary<MoodleCourse, List<LocationAvg>>();

            ///Overall
            //User+
            LocationAvg userAvg = LocationDB.getLocationDB().getUsersAvgWeek(ID);
            MoodleCourse global = new MoodleCourse(-1, "Global", "Global", "");
            //Match Avg
            int totalOn = 0, totalOff = 0;
            foreach (MoodleUser avg in matches)
            {
                LocationAvg matchAvg = LocationDB.getLocationDB().getUsersAvgWeek(avg.ID);
                if (matchAvg != null)
                {
                    totalOn = totalOn + matchAvg.on;
                    totalOff = totalOff + matchAvg.off;
                }
            }
            int avgMatchOn = 0, avgMatchOff = 0;
            //On
            if (matches.Count > 0 && totalOn > 0)
                avgMatchOn = totalOn / matches.Count;
            if (matches.Count > 0 && totalOff > 0)
                avgMatchOff = totalOff / matches.Count;
            LocationAvg overallAvg = new LocationAvg(-1, DateTime.Now, avgMatchOn, avgMatchOff);
            List<LocationAvg> locationAvgs = new List<LocationAvg>();
            locationAvgs.Add(userAvg);
            locationAvgs.Add(overallAvg);
            averages.Add(global, locationAvgs);

            //Per Course
            //User
            foreach (MoodleCourse course in courses)
            {
                int cTotalOn = 0, cTotalOff = 0;
                //User
                LocationAvg avg = LocationDB.getLocationDB().getUsersAvgWeek(course.ID, user.ID);

                int count = 0;
                foreach (MoodleUser mUser in matches)
                {
                    foreach (Moodle c in user.courses)
                    {
                        if (c.ID == course.ID)
                        {
                            LocationAvg tempLA = LocationDB.getLocationDB().getUsersAvgWeek(course.ID, mUser.ID);
                            if (tempLA != null)
                            {
                                cTotalOn = cTotalOn + tempLA.on;
                                cTotalOff = cTotalOff + tempLA.off;
                                count++;
                            }
                        }
                    }
                }

                int cAvgMatchOn = 0, cAvgMatchOff = 0;
                if (matches.Count > 0 && cTotalOn > 0)
                {
                    cAvgMatchOn = cTotalOn / matches.Count;
                    cAvgMatchOff = cTotalOff / matches.Count;
                }
                List<LocationAvg> cAvgs = new List<LocationAvg>();
                LocationAvg locAvg = new LocationAvg(-1, DateTime.Now, cAvgMatchOn, cAvgMatchOff);
                cAvgs.Add(avg);
                cAvgs.Add(locAvg);
                averages.Add(course, cAvgs);
            }
            return averages;
        }

        /// <summary>
        /// Gets a user year and there matches avg
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Dictionary<MoodleCourse, List<LocationAvg>> getAvgsYear(int ID)
        {
            MoodleUser user = MoodleDB.getMoodleDB().getUserByID(ID);
            List<MoodleCourse> courses = MoodleDB.getMoodleDB().getUsersCourses(user);
            List<MoodleUser> matches = System.getSystem().getBestMatches(user);
            Dictionary<MoodleCourse, List<LocationAvg>> averages = new Dictionary<MoodleCourse, List<LocationAvg>>();
            List<LocationAvg> avgs = LocationDB.getLocationDB().getAveragesYear(matches);

            ///Overall
            //User
            LocationAvg userAvg = LocationDB.getLocationDB().getUsersAvgYear(ID);
            MoodleCourse global = new MoodleCourse(-1, "Global", "Global", "");
            //Match Avg
            int totalOn = 0, totalOff = 0;
            foreach (MoodleUser avg in matches)
            {
                LocationAvg matchAvg = LocationDB.getLocationDB().getUsersAvgYear(ID);
                if (matchAvg != null)
                {
                    totalOn = totalOn + matchAvg.on;
                    totalOff = totalOff + matchAvg.off;
                }
            }
            int avgMatchOn = 0, avgMatchOff = 0;
            //On
            if (matches.Count > 0 && totalOn > 0)
                avgMatchOn = totalOn / matches.Count;
            if (matches.Count > 0 && totalOff > 0)
                avgMatchOff = totalOff / matches.Count;
            LocationAvg overallAvg = new LocationAvg(-1, DateTime.Now, avgMatchOn, avgMatchOff);
            List<LocationAvg> locationAvgs = new List<LocationAvg>();
            locationAvgs.Add(userAvg);
            locationAvgs.Add(overallAvg);
            averages.Add(global, locationAvgs);

            //Per Course
            //User
            foreach (MoodleCourse course in courses)
            {
                int cTotalOn = 0, cTotalOff = 0;
                //User
                LocationAvg avg = LocationDB.getLocationDB().getUsersAvgYear(course.ID, user.ID);

                int count = 0;
                foreach (MoodleUser mUser in matches)
                {
                    foreach (MoodleCourse c in user.courses)
                    {
                        if (course.ID == c.ID)
                        {
                            
                            LocationAvg tempLA = LocationDB.getLocationDB().getUsersAvgYear(course.ID, mUser.ID);
                            if (tempLA != null)
                            {
                                cTotalOn = cTotalOn + tempLA.on;
                                cTotalOff = cTotalOff + tempLA.off;
                                count++;
                            }

                        }
                    }
                }

                int cAvgMatchOn = 0, cAvgMatchOff = 0;
                if (matches.Count > 0 && cTotalOn > 0)
                {
                    cAvgMatchOn = cTotalOn / matches.Count;
                    cAvgMatchOff = cTotalOff / matches.Count;
                }
                List<LocationAvg> cAvgs = new List<LocationAvg>();
                LocationAvg locAvg = new LocationAvg(-1, DateTime.Now, cAvgMatchOn, cAvgMatchOff);
                cAvgs.Add(avg);
                cAvgs.Add(locAvg);
                averages.Add(course, cAvgs);
            }
            return averages;
        }
        
        /// <summary>
        /// Returns a list of users that are a best 
        /// match for user will try to return a min of 5 users
        /// </summary>
        /// <param name="user">The user to find matches for</param>
        /// <returns>List of moodle user</returns>
        public List<MoodleUser> getBestMatches(MoodleUser user)
        {
            List<MoodleUser> users = new List<MoodleUser>();
            List<MoodleUser> matches = new List<MoodleUser>(); // List to be returned.
            users = MoodleDB.getMoodleDB().getUserWithSameModules(user);

            //Perfect Match. //Main Loop
            foreach(MoodleUser moUser in users)
            {
                moUser.courses = MoodleDB.getMoodleDB().getUsersCourses(moUser);
                int courseMatch = 0;
                if(moUser.city.ToLower() == user.city.ToLower())
                {

                    foreach(MoodleCourse course in moUser.courses)
                    {
                        foreach(MoodleCourse userCourse in user.courses)
                        {
                            if(course.ID == userCourse.ID)
                            {
                                courseMatch++;
                            }
                        }
                    }
                }
                if(user.courses.Count == courseMatch)
                {
                    if (!matches.Contains(moUser))
                    {
                        matches.Add(moUser);
                    }
                }
            }
            foreach(MoodleUser mu in matches) // FY all matches from main list.
            {
                users.Remove(mu);
            }
            ///2 > Modules match
            int matchLevel = user.courses.Count;
            while(matches.Count <5)
            {    
                foreach(MoodleUser moUser in users)
                {
                    int courseMatch = 0;
                    foreach(MoodleCourse course in moUser.courses)
                    {
                        foreach(MoodleCourse c in user.courses)
                        {
                            if (c.ID == course.ID)
                            courseMatch++;
                        }
                    }
                    if(courseMatch == matchLevel)
                    {
                        matches.Add(moUser);
                    }
                }
                foreach(MoodleUser moUser in matches)
                {
                    users.Remove(moUser);
                }
                matchLevel--;
                if(users.Count == 0 || matchLevel == 0)
                {
                    break;
                }
            }
            return matches;
        }

        /*********************
        *Login and Register
        ***********************/

        /// <summary>
        /// Login the user to Moodle using rest.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public MoodleUser LoginMoodle(string username,string password)
        {
            int id = MoodleDB.getMoodleDB().getUser(username, password);
            return MoodleDB.getMoodleDB().getUserByID(id);
        }

        /// <summary>
        /// Login the user to the locationDB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public MoodleUser Login(string username,string password)
        {
            MoodleUser user = null;
            int id = LocationDB.getLocationDB().login(username, password);
            if(id !=-1)
            {
                user = MoodleDB.getMoodleDB().getUserByID(id);
            }
            return user;
        }

        /// <summary>
        /// Regester the user with locationDB
        /// </summary>
        /// <param name="moodleUsername"></param>
        /// <param name="moodlePassword"></param>
        /// <param name="password"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="location"></param>
        public void Register(string moodleUsername, string moodlePassword, string password, string fName, string lName, string location)
        {
            int ID = MoodleDB.getMoodleDB().getUser(moodleUsername,moodlePassword);
            if(ID !=-1)
            LocationDB.getLocationDB().Register(moodleUsername,moodlePassword,ID,password,fName,lName,location);
        }



    }
}
