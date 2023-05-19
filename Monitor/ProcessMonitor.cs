using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor
{
    public class ProcessMonitor
    {
        static void Main(string[] args)
        {
            //Initialising Variables

            string processName = "Notepad";// process name argument
            int maxLifetimeMinutes = 1;// process maximum lifetime alloted
            int monitoringFreqMin = 1;// process monitoring frequency

            Console.WriteLine("Monitoring process " + processName + " every " + monitoringFreqMin + " minutes and terminate if exceed " + maxLifetimeMinutes + " minutes...");
            Console.WriteLine("Press Q to stop..");

            while(!Console.KeyAvailable|| Console.ReadKey(intercept: true).Key!=ConsoleKey.Q)
            {
                KillProcessRunningLong(processName, maxLifetimeMinutes);//method call to perform
                Thread.Sleep(monitoringFreqMin * 60000);//sleep for every given freq time
            }

        }

        public static void KillProcessRunningLong(string processName, int maxLifetimeMinutes)
        {

            Process[] processes_target = Process.GetProcessesByName(processName);//store all process in processes array 
                                                                          
            foreach (var process in processes_target)//checj each process if its exceeding its lifetime
            {
                TimeSpan processlifetime = DateTime.Now - process.StartTime;//Calculate the pocess lifetime
                if (processlifetime.Minutes >= maxLifetimeMinutes)// condition to check if process lifetime exceed
                {
                    process.Kill();//kill process
                    LogData(process.ProcessName);//calling log data function to record in log file
                }
            }
        }

        static void LogData(string processName)
        {
            /*Stream Writer used to create file and append file */
            string Log = $"{DateTime.Now}: Process '{processName}' exceeded time limit and was terminated";// log format to be added in text file
            using (StreamWriter sw = File.AppendText("process_log.txt"))//append the string Log to process_log.txt file
            {
                sw.WriteLine(Log);
            }
        }
    }
}
