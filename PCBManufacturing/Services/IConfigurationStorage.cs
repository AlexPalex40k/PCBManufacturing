using PCBManufacturing.Models;

namespace PCBManufacturing.Services;

/// <summary>
/// Provides persistent storage for PCB configuration data.
/// </summary>
public interface IConfigurationStorage
{
    /// <summary>
    /// Loads the previously saved PCB configuration.
    /// </summary>
    /// <returns>
    /// The saved configuration, or <see langword="null"/> when no saved
    /// configuration is available.
    /// </returns>
    PcbConfigurationData? Load();

    /// <summary>
    /// Saves the specified PCB configuration.
    /// </summary>
    /// <param name="configuration">The configuration to save.</param>
    void Save(PcbConfigurationData configuration);
}