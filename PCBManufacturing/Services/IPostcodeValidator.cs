namespace PCBManufacturing.Services;

/// <summary>
/// Validates postcodes used for PCB order delivery.
/// </summary>
public interface IPostcodeValidator
{
    /// <summary>
    /// Determines whether the specified postcode is valid.
    /// </summary>
    /// <param name="postcode">The postcode to validate.</param>
    /// <returns>
    /// <see langword="true"/> if the postcode is valid; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    bool IsValid(string? postcode);
}