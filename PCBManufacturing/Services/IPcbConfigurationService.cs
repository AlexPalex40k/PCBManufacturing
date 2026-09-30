namespace PCBManufacturing.Services;

/// <summary>
/// Provides operations for loading and saving the current PCB configuration.
/// </summary>
public interface IPcbConfigurationService
{
    /// <summary>
    /// Loads the persisted PCB configuration into the current application state.
    /// </summary>
    void Load();

    /// <summary>
    /// Saves the current PCB configuration to persistent storage.
    /// </summary>
    void Save();
}