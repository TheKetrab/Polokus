using System.Text.Json;
using System.Text.Json.Serialization;

namespace Polokus.Core.Extensibility.Externals
{
    public class EnumJsonConverter<T> : JsonConverter<T> where T : Enum
    {

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert != typeof(T))
            {
                throw new JsonException($"Current converter is for type {typeof(T)} but was used to convert type {typeToConvert}");
            }

            string? str = reader.GetString();
            if (Enum.TryParse(typeToConvert, str, out object? result))
            {
                if (result == null)
                {
                    throw new JsonException($"Failed to parse \"{str}\" to enum type {typeToConvert}");
                }

                return (T)result;
            }

            throw new JsonException($"Failed to parse \"{str}\" to enum type {typeToConvert}");

        }


        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
