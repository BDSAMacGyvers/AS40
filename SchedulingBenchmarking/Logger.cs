using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
            DateTime ts = DateTime.Now;
            string js = e.State.ToString();
            string u = e.Job.Owner.Name;
            int ji = e.Job.jobId;

            DAO.AddEntry(ts, js, u, ji);
        }
    }
}
