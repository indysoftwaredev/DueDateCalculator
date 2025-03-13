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
            var result = DueDateCalculator.AddWorkingDays(date, days);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddDays_WhenFriday9AM_1Day_ReturnsMonday9AM()
        {
            var date = new DateTime(2025, 3, 14, 9, 0, 0);
            var days = 1;
            var expected = new DateTime(2025, 3, 17, 9, 0, 0);
            var result = DueDateCalculator.AddWorkingDays(date, days);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddHours_WhenMonday9AM_1Hour_ReturnsMonday10AM()
        {
            var date = new DateTime(2025, 3, 10, 9, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 10, 10, 0, 0);
            var result = DueDateCalculator.AddWorkingHours(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddHours_WhenMonday5PM_1Hour_ReturnsTuesday10AM()
        {
            var date = new DateTime(2025, 3, 10, 17, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 11, 10, 0, 0);
            var result = DueDateCalculator.AddWorkingHours(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddHours_WhenFriday5PM_1Hour_ReturnsMonday10AM()
        {
            var date = new DateTime(2025, 3, 14, 17, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 17, 10, 0, 0);
            var result = DueDateCalculator.AddWorkingHours(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenMonday8AM_1Hour_ReturnsMonday10AM ()
        {
            //case when the date is too early
            var date = new DateTime(2025, 3, 10, 8, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 10, 10, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenMonday6PM_1Hour_ReturnsTuesday10AM()
        {
            //case when the date is too late
            var date = new DateTime(2025, 3, 10, 18, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 11, 10, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenSaturday4PM_1Hour_ReturnsMonday10AM()
        {
            var date = new DateTime(2025, 3, 8, 16, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 10, 10, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenSunday4PM_1Hour_ReturnsMonday10AM()
        {
            var date = new DateTime(2025, 3, 9, 16, 0, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 10, 10, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenTurnaroundTimeIsNegative_ThrowsArgumentException()
        {
            var submitDate = new DateTime(2025, 3, 10, 9, 0, 0);
            var turnaroundTime = -1;

            var exception = Assert.Throws<ArgumentException>(() =>
                DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime));

            Assert.Equal("Turnaround time must be non-negative (Parameter 'turnaroundTime')", exception.Message);
        }

        [Fact]
        public void CalculateDueDate_WhenTurnaroundTimeIsZero_ReturnsSameTimeIfWithinWorkDay()
        {
            //characterization test - maybe an error should be thrown
            var submitDate = new DateTime(2025, 3, 10, 14, 0, 0);
            var turnaroundTime = 0;
            var expected = submitDate;
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenExactly40Hours_ReturnsNextWeekSameTime()
        {
            var submitDate = new DateTime(2025, 3, 10, 10, 0, 0);
            var turnaroundTime = 40; // Full work week
            var expected = new DateTime(2025, 3, 17, 10, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenFriday4PMWith4Hours_ReturnsMonday12PM()
        {
            var submitDate = new DateTime(2025, 3, 14, 16, 0, 0);
            var turnaroundTime = 4; // Spans weekend
            var expected = new DateTime(2025, 3, 17, 12, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenExactlyEndOfWorkDay_MovesToNextDay()
        {
            var submitDate = new DateTime(2025, 3, 10, 17, 0, 0);
            var turnaroundTime = 1;
            var expected = new DateTime(2025, 3, 11, 10, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_WhenMultipleWeeksOfWork_CalculatesCorrectly()
        {
            var submitDate = new DateTime(2025, 3, 10, 9, 0, 0);
            var turnaroundTime = 80; // Two full work weeks
            var expected = new DateTime(2025, 3, 24, 9, 0, 0);
            var result = DueDateCalculator.CalculateDueDate(submitDate, turnaroundTime);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddWorkingDays_WhenSaturday9AM_2Days_Tuesday9AM()
        {
            //fixes bug when starting on a saturday
            var date = new DateTime(2025, 3, 15, 9, 0, 0); // Saturday at 9AM
            var days = 2;
            var expected = new DateTime(2025, 3, 18, 9, 0, 0); // Should be Tuesday at 9AM                        
            var result = DueDateCalculator.AddWorkingDays(date, days);            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_Tuesday212PM_16Hours_ReturnsThursday212PM()
        {
            var date = new DateTime(2025, 3, 11, 14, 12, 0);
            var hours = 16;
            var expected = new DateTime(2025, 3, 13, 14, 12, 0);
            var result = DueDateCalculator.CalculateDueDate(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_Tuesday141230_16Hours_ReturnsThursday141230()
        {
            var date = new DateTime(2025, 3, 11, 14, 12, 30);
            var hours = 16;
            var expected = new DateTime(2025, 3, 13, 14, 12, 30);
            var result = DueDateCalculator.CalculateDueDate(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_Tuesday1659_1Hour_ReturnsWednesday0959()
        {
            var date = new DateTime(2025, 3, 11, 16, 59, 0);
            var hours = 1;
            var expected = new DateTime(2025, 3, 12, 9, 59, 0);
            var result = DueDateCalculator.CalculateDueDate(date, hours);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateDueDate_Tuesday165959_1Hour_ReturnsWednesday095959()
        {
            var date = new DateTime(2025, 3, 11, 16, 59, 59);
            var hours = 1;
            var expected = new DateTime(2025, 3, 12, 9, 59, 59);
            var result = DueDateCalculator.CalculateDueDate(date, hours);
            Assert.Equal(expected, result);
        }


    }
}