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
                    Console.WriteLine(job);
                }

            }
        }
        
        //select all jobs from a user within the past X days
        //select all jobs submitted by a user within a given time period (this includes the both the time and the date)
        //return the number of jobs within a given period grouped by their status (queued,running,ended, error). Here the activity log can be useful.
        //perform the same query as above but restricting the query to only one user
    }
}
