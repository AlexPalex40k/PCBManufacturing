using System.Text.RegularExpressions;

namespace PCBManufacturing.Services;

/// <summary>
/// Validates postcodes using the application's postcode format rules.
/// </summary>
public sealed class PostcodeValidator : IPostcodeValidator
{
    private static readonly Regex PostcodeRegex = new Regex(@"^\d{4,10}$");

    /// <inheritdoc />
    public bool IsValid(string? postcode)
    {
        return !string.IsNullOrWhiteSpace(postcode) && PostcodeRegex.IsMatch(postcode);
    }
}