using Microsoft.VisualStudio.TestPlatform.TestHost;
using Monitor;
using NUnit.Framework;
using System.ComponentModel;
using System.Diagnostics;

namespace Monitor_Nunit_Test
{
    public class Tests
    {
        //Arrange            
        string processName = "Notepad";
        int maxLifetimeMinutes = 1;

        
        [SetUp]
        public void Setup()
        {
            
        }

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

        public void TestCase2_KillProcessRunningLong()
        {
            //start a process
            Process.Start(processName);

            //Act
            //wait for max life time minutes and 5 seconds more to ensure process exceeds maximum lifetime to kill
            Thread.Sleep((maxLifetimeMinutes) * 65000);
            ProcessMonitor.KillProcessRunningLong(processName, maxLifetimeMinutes);

            //Assert
            Process[] processempty = Process.GetProcessesByName(processName);
            Assert.IsTrue(processempty.Length<0);
        }
    }
}