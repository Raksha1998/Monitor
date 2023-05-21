using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Monitor
{
    public class ProcessMonitor
    {
        public static void Main(string[] args)
        {
          //Initialization 
            string processName = args[0];
            int maxLifetimeMinutes = int.Parse(args[1]);
            int monitoringFreqMin = int.Parse(args[2]);

            //Invoking Run utility 
            RunUtility(processName, maxLifetimeMinutes, monitoringFreqMin);
            
        }

        /*  Method used to validate command line arguments */
        public static bool CommandLineArgs(string[] args)
        {
            bool result = true;// boolean datatype used to validate command line args

            //condition to check if the maximum lifetime and monitoring frequency are arsing the string to int
            if (!int.TryParse(args[1], out int maxLifetimeMinutes)|| !int.TryParse(args[2], out int monitoringFreqMin))
            {
                Console.WriteLine("Maximum lifetime and monitoring frequency must be valid integers");// error message to show in command prompt
                result = false;// set false if not parsed
            }
            return result;// remain true if parsed
        }

        /*  Method used to perform the functionality of utility to kill and sleep for the frequency given  */
        public static void RunUtility( string processName, int maxLifetimeMinutes, int monitoringFreqMin )
        {
            Console.WriteLine("Monitoring process " + processName + " every " + monitoringFreqMin + " minutes and terminate if exceed " + maxLifetimeMinutes + " minutes...");
            Console.WriteLine("Press Q to stop..");

            //Exit condition to check any key is pressed and read the key using Console and exit loop
            while (!Console.KeyAvailable || Console.ReadKey(intercept: true).Key != ConsoleKey.Q)
            {
                KillProcessRunningLong(processName, maxLifetimeMinutes);//method call to perform
                Thread.Sleep(monitoringFreqMin * 60000);//sleep for every given freq time
            }
            
        }

        /*  Method used Kill the process running longer than the threshold time  */
        public static void KillProcessRunningLong(string processName, int maxLifetimeMinutes)
        {

            Process[] processes_target = Process.GetProcessesByName(processName);//store all process in processes array 
                                                                          
            foreach (var process in processes_target)//check each process if its exceeding its lifetime
            {
                TimeSpan processlifetime = DateTime.Now - process.StartTime;//Calculate the pocess lifetime
                if (processlifetime.Minutes >= maxLifetimeMinutes)// condition to check if process lifetime exceed
                {
                    LogData(process.ProcessName);//calling log data function to record in log file
                    process.Kill();//kill process
                    
                }
            }
        }

        /*  Method used to log the data to the text file */
        public static void LogData(string processName)
        {
            /*Stream Writer used to create file and append file */
            string Log = $"{DateTime.Now}: Process '{processName}' exceeded time limit and was terminated";// log format to be added in text file
            using (StreamWriter sw = File.AppendText("process_log.txt"))//append the string Log to process_log.txt file
            {
                sw.WriteLine(Log);//pass the string Log to append
            }
        }
    }
}
