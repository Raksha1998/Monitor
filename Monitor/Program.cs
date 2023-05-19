using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialising Variables

            string processName = "Notepad";// process name argument
            int maxLifetimeMinutes = 1;// process maximum lifetime alloted
            int monitoringFreqMin = 1;// process monitoring frequency

            Console.WriteLine("Monitoring process " + processName + " every " + monitoringFreqMin + " minutes and terminate if exceed " + maxLifetimeMinutes + " minutes...");
            Console.WriteLine("Press Q to stop..");


            //Process[] pname = Process.GetProcessesByName(processName);//get process information
            //bool isAnyProcessExist = pname.Length > 0;//check if any process exist and set boolean datatype accordingly;

            /*check if any processes exist and if any special key is pressed*/

            //Exit Condition (PRESS Q or Any special Key Pressed condition
            while (!Console.KeyAvailable)
            {
                Process[] pname = Process.GetProcessesByName(processName);//get process information
                bool isAnyProcessExist = pname.Length > 0;//check if any new process exist and set boolean datatype accordingly;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key_info = Console.ReadKey(intercept: true);//read the key and store in key_info
                    if (key_info.Key == ConsoleKey.Q)// check if key pressed is Q
                    {
                        Environment.Exit(0);
                    }
                }
                while (isAnyProcessExist)//if any process exist
                {
                    Process[] processes = Process.GetProcessesByName(processName);//store all process in processes array 
                    //loop to get each process in variable process and kill
                    foreach (var process in processes)// check every process and terminate if exceed lifetime
                    {
                        TimeSpan processlifetime = DateTime.Now - process.StartTime;//Calculate the pocess lifetime
                        if (processlifetime.Minutes >= maxLifetimeMinutes)// condition to check if process lifetime exceed
                        {
                            process.Kill();//kill process
                            LogData(process.ProcessName);//calling log data function to record in log file
                        }
                    }
                    processes = null;// clear all processes to exit loop and check if any key available or new process exist
                    isAnyProcessExist=false;//terminated processes and hence set false since no old process exist
                    //Thread.Sleep(monitoringFreqMin * 60000);//sleep for the frequency given
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
    
