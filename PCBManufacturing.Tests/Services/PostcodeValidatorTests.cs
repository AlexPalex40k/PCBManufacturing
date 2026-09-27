using PCBManufacturing.Services;

namespace PCBManufacturing.Tests.Services;

public sealed class PostcodeValidatorTests
{
    [Theory]
    [InlineData("11000")]
    [InlineData("1234")]
    [InlineData("1234567890")]
    public void IsValid_WithValidPostcode_ReturnsTrue(string postcode)
    {
        var validator = new PostcodeValidator();

        var result = validator.IsValid(postcode);

        Assert.True(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("123")]
    [InlineData("12345678901")]
    [InlineData("12A45")]
    public void IsValid_WithInvalidPostcode_ReturnsFalse(string? postcode)
    {
        var validator = new PostcodeValidator();

        var result = validator.IsValid(postcode);

        Assert.False(result);
    }
}