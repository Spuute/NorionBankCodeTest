using Application.Helpers.Dates;
namespace ApplicationTests.Helpers.Date;

[TestClass]
public class HolidayDateHelperTests
{
    [TestInitialize]
    public void InitializeTests()
    {
        
    }
    
    [TestMethod]
    public void IsWeekend_Saturday_ShouldReturnTrue()
    {
        // Arrange
        var date = new DateTime(2024, 04, 13);
        
        // Act
        var result = HolidayDateHelper.IsWeekend(date);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsWeekend_Sunday_ShouldReturnTrue()
    {
        // Arrange
        var date = new DateTime(2024, 04, 14);
        
        // Act
        var result = HolidayDateHelper.IsWeekend(date);
        
        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsWeekend_Monday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 01);
        
        // Act 
        var result = HolidayDateHelper.IsWeekend(date);
        
        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsWeekend_Tuesday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 02);
        
        // Act
        var result = HolidayDateHelper.IsWeekend(date);
        
        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsWeekend_Wednesday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 03);
        
        // Act
        var result = HolidayDateHelper.IsWeekend(date);

        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void IsWeekend_Thursday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 04);
        
        // Act
        var result = HolidayDateHelper.IsWeekend(date);

        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void IsWeekend_Friday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 05);
        
        // Act
        var result = HolidayDateHelper.IsWeekend(date);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetPreviousWorkDay_Friday_ShouldReturnThursdag()
    {
        // Arrange
        var date = new DateTime(2024, 04, 05);
        var expected = new DateTime(2024, 04, 04);
        
        // Act
        var result = HolidayDateHelper.GetPreviousWorkDay(date);
        
        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CalculateMidsummer_Year2024_ShouldReturnJune21st()
    {
        // Arrange
        const int year = 2024;
        var expected = new DateTime(2024, 06, 21);

        // Act
        var result = HolidayDateHelper.CalculateMidsummer(year);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CalculateMidsummer_Year2026_ShouldReturnJune19th()
    {
        // Arrange
        const int year = 2026;
        var expected = new DateTime(2026, 06, 19);
        
        // Act
        var result = HolidayDateHelper.CalculateMidsummer(year);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CalculateMidsummer_Year2027_ShouldReturnJune25th()
    {
        // Arrange
        const int year = 2027;
        var expected = new DateTime(2027, 06, 25);
        
        // Act
        var result = HolidayDateHelper.CalculateMidsummer(year);
        
        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void IsChargeableDayBeforeHolidays_DayBeforeMidsummer_ShouldReturnTrue()
    {
        // Arrange
        var date = new DateTime(2027, 06, 24);
        
        // Act
        var result = HolidayDateHelper.IsChargeableDayBeforeHolidays(date);
        
        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsChargeableDayBeforeHolidays_DayBeforeNewYears_ShouldReturnTrue()
    {
        // Arrange
        var date = new DateTime(2027, 12, 30);
        
        // Act
        var result = HolidayDateHelper.IsChargeableDayBeforeHolidays(date);
        
        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsChargeableDayBeforeHolidays_DayBeforeChristmas_ShouldReturnTrue()
    {
        // Arrange
        var date = new DateTime(2027, 12, 23);
        
        // Act
        var result = HolidayDateHelper.IsChargeableDayBeforeHolidays(date);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsChargeableDayBeforeHolidays_AscensionDay_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 05, 09);
        
        // Act
        var result = HolidayDateHelper.IsChargeableDayBeforeHolidays(date);
        
        // Assert
        Assert.IsFalse(result);
    }
}