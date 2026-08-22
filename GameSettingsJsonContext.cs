using System.Text.Json.Serialization;

namespace CubeOS95
{
    [JsonSerializable(typeof(GameSettingsData))]
    internal partial class GameSettingsJsonContext : JsonSerializerContext
    {
    }
}
