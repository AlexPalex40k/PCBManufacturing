using System.IO;
using System.Text.Json;
using PCBManufacturing.Models;

namespace PCBManufacturing.Services;

/// <summary>
/// Stores PCB configuration data in a local JSON file.
/// </summary>
public sealed class JsonConfigurationStorage : IConfigurationStorage
{
    private readonly string _filePath;

    public JsonConfigurationStorage()
    {
        var directory = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
            "PCBManufacturing");

        Directory.CreateDirectory(directory);

        _filePath = Path.Combine(directory, "configuration.json");
    }

    /// <inheritdoc />
    public PcbConfigurationData? Load()
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }

        try
        {
            var json = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<PcbConfigurationData>(json);
        }
        catch (JsonException)
        {
            return null;
        }
        catch (IOException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public void Save(PcbConfigurationData configuration)
    {
        var json = JsonSerializer.Serialize(
            configuration,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_filePath, json);
    }
}