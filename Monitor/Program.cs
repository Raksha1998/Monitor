using System.Diagnostics;

namespace Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialising Variables

            string processName = "Notepad";// process name argument
            int maxLifetimeMinutes = 2;// process maximum lifetime alloted
            int monitoringFreqMin = 1;// process monitoring frequency

            Console.WriteLine("Monioring process " + processName + "every " + monitoringFreqMin + " minutes and terminate if exceed "+maxLifetimeMinutes+" minutes...");

            //loop to get each process in variable process and kill
            foreach (var process in Process.GetProcessesByName(processName))
            {
                TimeSpan processlifetime = DateTime.Now - process.StartTime;//Calculate the pocess lifetime
                if (processlifetime.Minutes > maxLifetimeMinutes)// condition to check if process lifetime exceeds the maximum lifetime
                {
                    process.Kill();//kill process
                }
            }



        }
    }
}
