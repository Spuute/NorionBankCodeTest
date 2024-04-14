using Application.Services.HolidayCalculationService;

namespace ApplicationTests.Services.HolidayCalculationServiceTests;

[TestClass]
public class HolidayCalculationServiceTests
{
    private HolidayCalculationService _holidayCalculationService;
    
    [TestInitialize]
    public void InitializeTests()
    {
        _holidayCalculationService = new HolidayCalculationService();
    }

    [TestMethod]
    public void IsWeekend_Monday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 15); 
        
        // Act
        var result = _holidayCalculationService.IsWeekend(date);
        
        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsWeekend_Tuesday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 16);
        
        // Act
        var result = _holidayCalculationService.IsWeekend(date);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsWeekend_Wednesday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 17);
        
        // Act
        var result = _holidayCalculationService.IsWeekend(date);
        
        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void IsWeekend_Thursday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 18);
        
        // Act
        var result = _holidayCalculationService.IsWeekend(date);
        
        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void IsWeekend_Friday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 19);
        
        // Act
        var result = _holidayCalculationService.IsWeekend(date);
        
        // Assert
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void IsWeekend_Saturday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 20);
        
        // Act
        var result = _holidayCalculationService.IsWeekend(date);
        
        // Assert
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void IsWeekend_Sunday_ShouldReturnFalse()
    {
        // Arrange
        var date = new DateTime(2024, 04, 21);
        
        // Act
        var result = _holidayCalculationService.IsWeekend(date);
        
        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DynamicData(nameof(TollFreeDates2024))]
    public void IsTollFreeDate_2024SpecificDates_ShouldReturnTrue(int year, int month, int day)
    {
        // Arrange
        var christmas = new DateTime(year, month, day);
        
        // Act
        var result = _holidayCalculationService.IsTollFreeDate(christmas);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    [DynamicData(nameof(SpecificNoneTollFreeDatesBeforeHolidays))]
    public void IsTollFreeDate_SpecificDaysBeforeHolidays_ShouldReturnFalse(int year, int month, int day)
    {
        // Arrange
        var date = new DateTime(year, month, day);
        
        // Act
        var result = _holidayCalculationService.IsTollFreeDate(date);

        // Assert
        Assert.IsFalse(result);
    }
    
    

    public static IEnumerable<object[]> TollFreeDates2024
    {
        get
        {
            return new[]
            {
                [2024, 01, 01],
                [2024, 01, 05],
                [2024, 03, 28],
                [2024, 03, 29],
                [2024, 04, 01],
                [2024, 04, 30],
                [2024, 05, 01],
                [2024, 05, 08],
                [2024, 05, 09],
                [2024, 06, 05],
                [2024, 06, 06],
                [2024, 06, 21],
                [2024, 11, 01],
                [2024, 12, 24],
                [2024, 12, 25],
                [2024, 12, 26],
                new object[] { 2024, 12, 31 }
            };
        }
    }
    
    public static IEnumerable<object[]> SpecificNoneTollFreeDatesBeforeHolidays
    {
        get
        {
            return new[]
            {
                [2024, 06, 20],
                [2024, 12, 23],
                new object[] { 2024, 12, 30 }
            };
        }
    }
}