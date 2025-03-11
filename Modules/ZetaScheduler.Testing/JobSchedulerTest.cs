using Moq;
using System;
using ZetaScheduler.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZetaScheduler.Testing
{
    [TestClass]
    public class JobSchedulerTest
    {
        private Mock<IJob> jobMock;

        private string jobMessage;

        [TestMethod]
        public void BasicSchedulerTest()
        {
            jobMock = new Mock<IJob>();
            jobMock.Setup(x => x.Execute())
                .Callback(() => jobMessage = "executed");

            var scheduler = new Core.ZetaScheduler();

            scheduler.Schedule(jobMock.Object, DateTime.Now.AddMilliseconds(500));

            Task.Delay(600).Wait();

            jobMock.Verify(x => x.Execute(), Times.Once);
            Assert.AreEqual("executed", jobMessage);
        }

        [TestMethod]
        public void MultipleJobSchedulerTest()
        {
            Queue<string> strings = new Queue<string>();
            strings.Enqueue("executed1");
            strings.Enqueue("executed2");
            strings.Enqueue("executed3");

            jobMock = new Mock<IJob>();
            jobMock.Setup(x => x.Execute())
                .Callback(() => jobMessage = strings.Dequeue());

            var scheduler = new Core.ZetaScheduler();

            scheduler.Schedule(jobMock.Object, DateTime.Now.AddMilliseconds(500));
            scheduler.Schedule(jobMock.Object, DateTime.Now.AddMilliseconds(1000));
            scheduler.Schedule(jobMock.Object, DateTime.Now.AddMilliseconds(1500));

            Task.Delay(2000).Wait();

            jobMock.Verify(x => x.Execute(), Times.Exactly(3));
            Assert.IsEmpty(strings);
        }

        [TestMethod]
        public void RecurringJobTest()
        {
            int counter = 0;
            jobMock = new Mock<IJob>();
            jobMock.Setup(x => x.Execute())
                .Callback(() => counter++);

            var scheduler = new Core.ZetaScheduler();

            scheduler.ScheduleRecurringJob(
                jobMock.Object,
                DateTime.Now.AddMilliseconds(500),
                TimeSpan.FromMilliseconds(500)
            );

            Task.Delay(2000).Wait();
            scheduler.Dispose();

            jobMock.Verify(x => x.Execute(), Times.AtLeast(3));
            Assert.IsTrue(counter >= 3);
        }
    }
}
