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

            Console.WriteLine("Monioring process " + processName + " every " + monitoringFreqMin + " minutes and terminate if exceed "+maxLifetimeMinutes+" minutes...");
            Console.WriteLine("Press any key to stop..");
            
            bool anyKeyPress = false;// boolean datatype to check if any key pressed

            while (!anyKeyPress)// if not any key pressed continue...
            {
                if (Console.KeyAvailable)//check if any key pressed
                {
                    anyKeyPress = true;//set any key press boolean datatype to true to exit from the loop
                }
                 //loop to get each process in variable process and kill
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    TimeSpan processlifetime = DateTime.Now - process.StartTime;//Calculate the pocess lifetime
                    if (processlifetime.Minutes > maxLifetimeMinutes)// condition to check if process lifetime exceed
                    {
                        process.Kill();//kill process
                    }
                }
            }
        }
    }
}
