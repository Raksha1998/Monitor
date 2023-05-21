using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Monitor;
using NUnit.Framework;
using System.ComponentModel;
using System.Diagnostics;

namespace Monitor_Nunit_Test
{
    public class Tests
    {
                
        //Arrange for test of kill process running long method            
        string processName = "Notepad";
        int maxLifetimeMinutes = 1;



        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        /* Test Valid Command Line Arguments */
        public void TestCommandLineArgsParse()
        {
            //Arrange for test of valid command line arguments
            bool result = ProcessMonitor.CommandLineArgs(new[] { "notepad", "5", "1" });

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        /* Test Invalid Command Line Arguments */
        public void TestCommandLineArgs_Invalid()
        {
            //Arrange for test of Invalid command line arguments
            bool result = ProcessMonitor.CommandLineArgs(new[] { "notepad", "Invalid", "1" });

            //Assert
            Assert.IsFalse(result);
        }

        /* Test to validate the process killing method*/
        [Test]
        public void TestCase_KillProcessRunningLong()
        {
            //start a process
            Process.Start(processName);

            //Act
            //wait for max life time minutes and 5 seconds more to ensure process exceeds maximum lifetime to kill
            Thread.Sleep((maxLifetimeMinutes) * 65000);
            ProcessMonitor.KillProcessRunningLong(processName, maxLifetimeMinutes);

            //Assert
            Process[] processempty = Process.GetProcessesByName(processName);
            Assert.IsEmpty(processempty);
        }

        /* Test to ensure the logging functionality*/
        [Test]
        public void Test_LogData()
        {
            //Arrange                         
            Process.Start(processName);
            ProcessMonitor.KillProcessRunningLong(processName, 0);

            //Act
            ProcessMonitor.LogData(processName);
            string[] lines = File.ReadAllLines(@"C:\temp\Monitor\Monitor\bin\Debug\net6.0\process_log.txt");
            string lastline = lines[lines.Length - 1];
            string Log = $"{DateTime.Now}: Process '{processName}' exceeded time limit and was terminated";// log format to be added in text file

            //Assert
            Assert.That(Log, Is.EqualTo(lastline));
        }

    }
}