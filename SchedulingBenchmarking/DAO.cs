using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    partial class DAO
    {
        //select all users
        public void FindUsers()
        {
            using (var dbContext = new Model1Container())
            {
                IEnumerable<string> users = from db in dbContext.DbLogs select db.user;

                foreach(string name in users)
                {
                    Console.WriteLine(name); 
                }
            }
        }

        //select all jobs from a user
        public void FindAllJobs(String name)
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
        public void FindAllSubmitsWithin(string user, DateTime start, DateTime end)
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
        //perform the same query as above but restricting the query to only one user
    }
}
