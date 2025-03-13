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

        [Fact]
        public void CalculateDueDate_WhenMonday5PM_1Hour_ReturnsTuesday10AM()
        {
            var submitDate = new DateTime(2025, 3, 10, 17, 0, 0);
            var turnaroundTime = 1;
            var dueDate = new DateTime(2025, 3, 11, 10, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(dueDate, result);
        }

        [Fact]
        public void AddDays_WhenMonday9AM_1Day_ReturnsTuesday9AM()
        {
            var date = new DateTime(2025, 3, 10, 9, 0, 0);
            var days = 1;
            var expected = new DateTime(2025, 3, 11, 9, 0, 0);
            var result = DueDateCalculator.AddDays(date, days);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddDays_WhenFriday9AM_1Day_ReturnsMonday9AM()
        {
            var date = new DateTime(2025, 3, 14, 9, 0, 0);
            var days = 1;
            var expected = new DateTime(2025, 3, 17, 9, 0, 0);
            var result = DueDateCalculator.AddDays(date, days);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddHours_WhenMonday9AM_1Hour_ReturnsMonday10AM()
        {
            var date = new DateTime(2025, 3, 10, 9, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 10, 10, 0, 0);
            var result = DueDateCalculator.AddHours(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddHours_WhenMonday5PM_1Hour_ReturnsTuesday10AM()
        {
            var date = new DateTime(2025, 3, 10, 17, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 11, 10, 0, 0);
            var result = DueDateCalculator.AddHours(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddHours_WhenFriday5PM_1Hour_ReturnsMonday10AM()
        {
            var date = new DateTime(2025, 3, 14, 17, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 17, 10, 0, 0);
            var result = DueDateCalculator.AddHours(date, hours);
            Assert.Equal(expected, result);
        }
    }
}