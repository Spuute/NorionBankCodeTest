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
    public void IsTollFreeDate_Christmas_ShouldReturnTrue()
    {
        // Arrange
        var christmas = new DateTime(2024, 12, 24);
        
        // Act
        var result = _holidayCalculationService.IsTollFreeDate(christmas);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsTollFreeDate_DayBeforeChristmas_ShouldReturnFalse()
    {
        // Arrange
        var dayBeforeChristmas = new DateTime(2024, 12, 23);
        
        // Act
        var result = _holidayCalculationService.IsTollFreeDate(dayBeforeChristmas);

        // Assert
        Assert.IsFalse(result);
    }
}