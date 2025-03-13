using DueDateCalculator;

namespace DueDateCalculator.Tests
{
    public class DueDateCalculatorTests
    {
        [Fact]
        public void TestFixtureIsWorking()
        {
            Assert.True(true);
        }

        [Fact]
        public void CalculateDueDate_WhenMonday9AM_1Hour_ShouldReturnMonday10AM()
        {
            var submitDate = new DateTime(2025, 3, 10, 9, 0, 0);
            var turnaroundTime = 1;
            var dueDate = new DateTime(2025, 3, 10, 10, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(dueDate, result);
        }

        [Fact]
        public void CalculateDueDate_WhenMonday9AM_8Hours_ShouldReturnTuesday9AM()
        {
            var submitDate = new DateTime(2025, 3, 10, 9, 0, 0);
            var turnaroundTime = 8;
            var dueDate = new DateTime(2025, 3, 11, 9, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(dueDate, result);
        }

        [Fact]
        public void CalculateDueDate_WhenFriday5PM_8Hour_ReturnsMonday5PM()
        {
            var submitDate = new DateTime(2025, 3, 14, 17, 0, 0);
            var turnaroundTime = 8;
            var dueDate = new DateTime(2025, 3, 17, 17, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(dueDate, result);
        }
    }
}