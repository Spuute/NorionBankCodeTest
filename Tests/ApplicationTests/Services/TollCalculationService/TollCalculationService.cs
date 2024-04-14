using Application.Interfaces;
using Application.Services.TollFeeCalculationService;
using Core.Entities;
using Moq;

namespace ApplicationTests.Services.TollCalculationService;

[TestClass]
public class TollCalculationService
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
}