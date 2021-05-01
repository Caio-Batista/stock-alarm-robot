using NUnit.Framework;
using System.IO;
using System;
using StockAlarmRobot;

namespace StockAlarmRobotTests
{
    public class Tests
    {
        private const string Expected = "Created client SMTP successfuly";

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void TestMethod1()
        {
            using (var sw = new StringWriter())
            {
                
                var fakeReader = new FakeFileReader();
                var path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.
                Parent.FullName, "default.robot.config.test");


                Console.SetOut(sw);
                string[] args = { path };
                Program myProgram = new Program(fakeReader);
                Program.Main(args);

                var result = sw.ToString().Trim();
                Console.WriteLine(result);
                Assert.IsTrue(result.Contains(Expected));
            }
        }
    }
}
