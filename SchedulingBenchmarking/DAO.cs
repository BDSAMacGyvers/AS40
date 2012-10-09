using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    public partial class DAO
    {
        // add entry 
        public static void AddEntry(DateTime timeStamp, string jobState, string user, int jobId)
        {
            using (var dbContext = new Model1Container())
            {
                dbContext.Database.Connection.Open();
                DbLog logEntry = new DbLog();

                logEntry.timeStamp = timeStamp;
                logEntry.jobState = jobState;
                logEntry.user = user;
                logEntry.jobId = jobId;

                dbContext.DbLogs.Add(logEntry);
                dbContext.SaveChanges();
            } 
        }

        //select all users
        public static List<string> FindUsers()
        {
            using (var dbContext = new Model1Container())
            {
                IEnumerable<string> users = (from db in dbContext.DbLogs select db.user).Distinct();

                /*
                foreach(string name in users)
                {
                    Console.WriteLine(name); 
                }*/

                return users.ToList();
            }
        }

        //select all jobs from a user
        public static void FindAllJobs(String name)
        {
            using (var dbContext = new Model1Container())
            {
                IEnumerable<Job> jobs = from db in dbContext.DbLogs where db.user == name 
                                        select new Job() { jobId = db.jobId };
                
                foreach (Job job in jobs)
                {
                    Console.WriteLine(name+": " +job.jobId);
                }

            }
        }

        
        //select all jobs from a user within the past X days
        
        //select all jobs submitted by a user within a given time period (this includes both the time and the date)
        public static void FindAllSubmitsWithin(string user, DateTime start, DateTime end)
        {
            using (var dbContext = new Model1Container())
            {
                IEnumerable<int> submits = from db in dbContext.DbLogs where 
                                  db.user == user && start < db.timeStamp && db.timeStamp < end 
                                  && db.jobState == "Submitted" select db.jobId;

                Console.WriteLine(user + " has within " + start + " and " + end + ":");
                foreach (int sub in submits)
                {
                    Console.WriteLine(sub);
                }
            }
            
        }
        //return the number of jobs within a given period grouped by their status (queued,running,ended, error). Here the activity log can be useful.
        public static void NrOfJobsWithin(DateTime start, DateTime end)
        {
            using (var dbContext = new Model1Container())
            {
                var jobsByState = from db in dbContext.DbLogs
                                  where start < db.timeStamp && db.timeStamp < end
                                  group db by db.jobState into JobByState
                                  select new { count = JobByState.Count(), state = JobByState.Key };
  
                Console.WriteLine("Between "+start+" and "+end+" the following nr of jobs have been processd");
                foreach (var state in jobsByState)
                {
                    Console.WriteLine("Nr of jobs in state: "+state.state+" : "+state.count);                    
                }
            }

        }
        
        //perform the same query as above but restricting the query to only one user
        public static void NrOfJobsWithinOne(DateTime start, DateTime end, string user)
        {
            using (var dbContext = new Model1Container())
            {
                var jobsByState = from db in dbContext.DbLogs
                                  where start < db.timeStamp && db.timeStamp < end && user == db.user
                                  group db by db.jobState into JobByState
                                  select new { count = JobByState.Count(), state = JobByState.Key };

                Console.WriteLine("Between " + start + " and " + end + " the following nr of jobs have been processed for "+user);
                foreach (var state in jobsByState)
                {
                    Console.WriteLine("Nr of jobs in state: " + state.state + " : " + state.count);
                }
            }
        }

    }
}
