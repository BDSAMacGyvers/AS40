using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    public class Logger
    {
        /// <summary>
        /// Method invoked by any state change in BenchMarkSystem. Publishes a running commentary 
        /// when any job is submitted, cancelled, run, failed or terminated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            using (var dbContext = new Model1Container())
            {
                DbLog logEntry = new DbLog();
                logEntry.timeStamp = DateTime.Today;

                logEntry.jobState = e.State.ToString();
                dbContext.DbLogs.Add(logEntry);
                
                dbContext.SaveChanges();

                Console.WriteLine("Job state {0}", e.State);
            } 
        }


    }
}
