using System;
using System.IO;
using System.Text.Json;

namespace CubeOS95
{
    public class GameSettingsData
    {
        public string Language { get; set; } = "en-US";
    }

    public static class GameSettings
    {
        private static readonly string SettingsPath = Path.Combine(AppContext.BaseDirectory, "settings.json");

        public static string CurrentLanguage { get; private set; } = "en-US";

        public static GameSettingsData Load()
        {
            if (File.Exists(SettingsPath))
            {
                string json = File.ReadAllText(SettingsPath);
                var data = JsonSerializer.Deserialize(json, GameSettingsJsonContext.Default.GameSettingsData) ?? new GameSettingsData();
                CurrentLanguage = data.Language;
                return data;
            }
            return new GameSettingsData();
        }

        public static void Save(GameSettingsData settings)
        {
            CurrentLanguage = settings.Language;
            string json = JsonSerializer.Serialize(settings, GameSettingsJsonContext.Default.GameSettingsData);
            File.WriteAllText(SettingsPath, json);
        }
    }
}
