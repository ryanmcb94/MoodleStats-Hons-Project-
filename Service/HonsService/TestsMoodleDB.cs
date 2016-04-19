using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoodleObjects;

namespace HonsService
{
    [TestClass]
    public class TestsMoodleDB
    {

        [TestMethod]
        public void GetCourseByID()
        {
            int ID = 1;

            MoodleCourse course =MoodleDB.getMoodleDB().getCourse(ID);
            Assert.AreEqual(course.ID, ID);
            Assert.IsNotNull(course.desc);
        }

        [TestMethod]
        public void GetUserByID()
        {
            int ID = 1;

            MoodleUser user = MoodleDB.getMoodleDB().getUserByID(ID);
            Assert.AreEqual(user.ID, ID);
            Assert.IsNotNull(user.fName);
        }





    }
}