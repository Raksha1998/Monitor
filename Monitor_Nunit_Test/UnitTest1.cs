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

        //[Test]
        //public void TestCase_KillProcessRunningLong()
        //{
        //    //start a process
        //    Process.Start(processName);

        //    //Act
        //    //wait for max life time minutes and 5 seconds more to ensure process exceeds maximum lifetime to kill
        //    Thread.Sleep((maxLifetimeMinutes) * 65000);
        //    ProcessMonitor.KillProcessRunningLong(processName, maxLifetimeMinutes);

        //    //Assert
        //    Process[] processempty = Process.GetProcessesByName(processName);
        //    Assert.IsEmpty(processempty);
        //}

        
    }
}