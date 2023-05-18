using System.Diagnostics;

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

            Console.WriteLine("Monioring process " + processName + " every " + monitoringFreqMin + " minutes and terminate if exceed " + maxLifetimeMinutes + " minutes...");
            Console.WriteLine("Press Q to stop..");


            bool anyKeyPress = false;// boolean datatype to check if any key pressed
            

            while (!anyKeyPress)// if not any key pressed continue...
            {
                if (Console.KeyAvailable)//check if any key pressed
                {
                    ConsoleKeyInfo key_info = Console.ReadKey(intercept: true);//read the key and store in key_info
                    if (key_info.Key == ConsoleKey.Q)// check if key pressed is Q
                    {
                        anyKeyPress = true;
                        //Environment.Exit(0);
                    }
                }
                //loop to get each process in variable process and kill
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    TimeSpan processlifetime = DateTime.Now - process.StartTime;//Calculate the pocess lifetime
                    if (processlifetime.Minutes >= maxLifetimeMinutes)// condition to check if process lifetime exceed
                    {
                        process.Kill();//kill process
                        LogData(process.ProcessName);
                    }
                }
                Thread.Sleep(monitoringFreqMin * 60000);
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
