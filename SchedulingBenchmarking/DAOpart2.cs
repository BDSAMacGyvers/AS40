using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingBenchmarking
{
    public partial class DAO
    {
         //select all jobs from a user within the past X days
        public static List<int> GetLastXDays(int x, string name)
        {
            using (var dbContext = new Model1Container())
            {
                DateTime span = DateTime.Today.AddDays(-x);
                var lastTenDays = from db in dbContext.DbLogs
                                  where (db.timeStamp > span) && db.user == name
                                  select db.jobId;

                return lastTenDays.ToList();
            }

        }


    }
}
