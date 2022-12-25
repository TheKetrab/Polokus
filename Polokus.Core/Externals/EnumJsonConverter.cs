using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Polokus.Core.Externals
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
