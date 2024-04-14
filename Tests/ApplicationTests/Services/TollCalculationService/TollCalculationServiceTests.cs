using Application.Interfaces;
using Application.Services.TollFeeCalculationService;
using Core.Entities;
using Moq;

namespace ApplicationTests.Services.TollCalculationService;

[TestClass]
public class TollCalculationServiceTests
{
    private readonly Mock<IHolidayCalculationService> _holidayCalculationServiceMock = new();
    private TollFeeCalculationService _tollFeeCalculationService;


    [TestInitialize]
    public void InitializeTests()
    {
        _tollFeeCalculationService = new TollFeeCalculationService(_holidayCalculationServiceMock.Object);
    }

    [TestMethod]
    public void CalculateTotalTollFeeForDay_ExceedsMaxFeeLimit_ShouldReturnSixty()
    {
        // Arrange
        var vehicle = new Car();
        var passageTimes = new DateTime[]
        {
            new(2024, 04, 12, 08, 00, 00, 000),
            new(2024, 04, 12, 08, 10, 00, 000),
            new(2024, 04, 12, 11, 00, 00, 000),
            new(2024, 04, 12, 13, 00, 00, 000),
            new(2024, 04, 12, 15, 00, 00, 000),
            new(2024, 04, 12, 16, 10, 00, 000),
            new(2024, 04, 12, 17, 20, 00, 000)
        };

        // Act
        var totalFee = _tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTimes);

        // Assert
        Assert.AreEqual(60, totalFee);
    }

    [TestMethod]
    public void CalculateTotalTollFeeForDay_ShouldReturnZero_VehicleIsTollFree()
    {
        // Arrange
        var vehicle = new Motorbike();
        var passageTimes = new DateTime[]
        {
            new(2024, 04, 12, 08, 00, 00, 000),
            new(2024, 04, 12, 08, 10, 00, 000),
            new(2024, 04, 12, 11, 00, 00, 000),
            new(2024, 04, 12, 13, 00, 00, 000)
        };

        // Act
        var totalFee = _tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTimes);

        // Assert
        Assert.AreEqual(0, totalFee);
    }

    [TestMethod]
    public void CalculateTotalFeeForDay_Weekend_ShouldReturnZero()
    {
        // Arrange
        var vehicle = new Car();

        var passageTimes = new DateTime[]
        {
            new(2024, 01, 01, 13, 44, 32, 000)
        };

        _holidayCalculationServiceMock.Setup(x => x.IsWeekend(passageTimes[0]))
            .Returns(true);

        // Act
        var totalFee = _tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTimes);

        // Assert
        Assert.AreEqual(0, totalFee);
    }

    [TestMethod]
    public void CalculateTotalTollFeeForDay_ShouldReturnSingleTollFee_ForSinglePassage()
    {
        // Arrange
        var vehicle = new Car();

        var passageTimes = new DateTime[]
        {
            new(2024, 04, 12, 08, 00, 00)
        };

        // Act
        var totalFee = _tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTimes);

        // Assert
        Assert.AreEqual(13, totalFee);
    }

    [TestMethod]
    public void CalculateTotalTollFeeForDay_ShouldReturnSingleFeeAtHighestAmount_ForMultiplePassagesWithinWindow()
    {
        // Arrange
        var vehicle = new Car();
        var passageTimes = new DateTime[]
        {
            new(2024, 04, 12, 06, 05, 00), // 8 kr
            new(2024, 04, 12, 06, 07, 00), // 8 kr
            new(2024, 04, 12, 06, 59, 00), // 13 kr - Within 60 minutes of first passage, 
            new(2024, 04, 12, 07, 02, 00) // 18kr
        };

        // Act
        var totalFee = _tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTimes);

        // Assert
        Assert.AreEqual(18, totalFee);
    }

    [TestMethod]
    public void CalculateTotalTollFeeForDay_MultiplePassagesWithinHourAndOneJustOutsideWindow_ShouldReturnTwoFees()
    {
        // Arrange
        var vehicle = new Car();
        var passageTimes = new DateTime[]
        {
            new(2024, 04, 12, 06, 05, 00), // 8 kr                                                
            new(2024, 04, 12, 06, 07, 00), // 8 kr                                                
            new(2024, 04, 12, 06, 59, 00), // 13 kr - Within 60 minutes of first passage,         
            new(2024, 04, 12, 07, 02, 00), // 18kr
            new(2024, 04, 12, 07, 05, 01) // 18kr
        };

        // Act
        var result = _tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTimes);

        // Assert
        Assert.AreEqual(36, result);
    }

    [TestMethod]
    public void CalculateTotalTollFeeForDay_TollFreeDay_ShouldReturnZero()
    {
        // Arrange
        var vehicle = new Car();
        var passageTimes = new DateTime[]
        {
            new(2024, 06, 19, 10, 0, 0)
        };

        _holidayCalculationServiceMock.Setup(x => x.IsTollFreeDate(passageTimes[0]))
            .Returns(true);

        // Act
        var result = _tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTimes);

        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void CalculateTotalTollFeeForDay_NightTime_ShouldReturnZero()
    {
        // Arrange
        var vehicle = new Car();
        var passageTime = new DateTime[]
        {
            new(2024, 04, 18, 02, 00, 00)
        };

        // Act
        var result = _tollFeeCalculationService.CalculateTotalTollFeeForDay(vehicle, passageTime);

        // Assert
        Assert.AreEqual(0, result);
    }
}