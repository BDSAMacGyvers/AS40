using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SchedulingBenchmarking
{
    [TestClass]
    public class DAOtest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            /*
            // Delete contents of table
            using (var dbContext = new Model1Container())
            {
            context.Entities.DeleteAllOnSubmit(context.Entities);
            context.SubmitChanges();
            }
             */
            
            // Insert test contents

            // DateTime er i format year, month, day, hour, minute, second
            DAO.AddEntry(new DateTime(2012, 10, 9, 12, 15, 45), "Submitted", "morten", 1);
            DAO.AddEntry(new DateTime(2012, 10, 9, 12, 16, 45), "Cancelled", "morten", 1);

            DAO.AddEntry(new DateTime(2012, 5, 29, 15, 45, 9), "Submitted", "ellen", 2);
            DAO.AddEntry(new DateTime(2012, 5, 29, 15, 47, 3), "Running", "ellen", 2);
            DAO.AddEntry(new DateTime(2012, 5, 29, 15, 48, 15), "Failed", "ellen", 2);

            DAO.AddEntry(new DateTime(2012, 2, 16, 8, 38, 29), "Submitted", "christoffer", 3);
            DAO.AddEntry(new DateTime(2012, 2, 16, 8, 45, 9), "Running", "christoffer", 3);
            DAO.AddEntry(new DateTime(2012, 2, 16, 8, 50, 14), "Terminated", "christoffer", 3);
        }
        /*
        [TestMethod]
        public void TestAddEntry()
        {
            DAO.AddEntry(new DateTime(2012, 4, 16, 8, 50, 14), "Submitted", "theEasterBuny", 4);
        }
        */

        [TestMethod]
        public void TestFindUsers()
        {
            List<string> users = DAO.FindUsers();

            Assert.IsTrue(users.Contains("morten"));
            Assert.IsTrue(users.Contains("ellen"));
            Assert.IsTrue(users.Contains("christoffer"));
            Assert.IsFalse(users.Contains("thejulemanden"));
        }

        [TestMethod]
        public void TestFindAllJobs()
        {
            
                int preCount = DAO.FindAllJobs("christoffer").Count();
                DAO.AddEntry(new DateTime(2012, 2, 16, 8, 38, 29), "Submitted", "christoffer", 3);
                DAO.AddEntry(new DateTime(2012, 2, 16, 8, 45, 9), "Running", "christoffer", 3);
                DAO.AddEntry(new DateTime(2012, 2, 16, 8, 50, 14), "Terminated", "christoffer", 3);
                int postCount = DAO.FindAllJobs("christoffer").Count();
                Assert.AreEqual(preCount + 3, postCount);
                Assert.IsFalse(preCount == postCount);
            
        }

        [TestMethod]
        public void TestFindAllSubmitsWithin()
        {

        }

        [TestMethod]
        public void TestGetLastXDays()
        {
            List<int> jobsForUser= DAO.GetLastXDays(120, "morten");

            Assert.IsTrue(jobsForUser.Contains(1));
            Assert.IsFalse(jobsForUser.Contains(3));
        }

        [TestMethod]
        public void TestNrOfJobsWithin()
        {
            //return the number of jobs within a given period grouped by their status (queued,running,ended, error). Here the activity log can be useful.
            var result = DAO.NrOfJobsWithin(DateTime.Today.AddDays(-2), DateTime.Today);
            //As there is only five different states
            Assert.IsFalse(result.Count() > 5);
            int submittedCount = 0;
            int cancelledCount = 0;
            foreach (Entry ent in result) 
            {
                if (ent.State.Equals("Submitted")) submittedCount = ent.Count;
                if (ent.State.Equals("Cancelled")) cancelledCount = ent.Count;
            }
            DAO.AddEntry(DateTime.Today, "Submitted", "morten", 1);
            var result2 = DAO.NrOfJobsWithin(DateTime.Today.AddDays(-2), DateTime.Today);
            //As there is only five different states
            Assert.IsFalse(result2.Count() > 5);
            foreach (Entry ent in result2)
            {
                // assert that submitted count has increased by one
                if (ent.State.Equals("Submitted")) Assert.IsTrue(ent.Count - submittedCount == 1);
                // assert that cancelledCount hasn't changed
                if (ent.State.Equals("Cancelled")) Assert.IsFalse(ent.Count - cancelledCount == 1);
            }
        }

        [TestMethod]
        public void NrOfJobsWithinOne()
        {

        }

    }
}
