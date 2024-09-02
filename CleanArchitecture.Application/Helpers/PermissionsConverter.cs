using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Helpers
{
    public class PermissionsConverter : JsonConverter<Permissions>
    {
        public override Permissions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (Enum.TryParse<Permissions>(reader.GetString(), out var result))
            {
                return result;
            }
            throw new JsonException($"Unable to convert '{reader.GetString()}' to {nameof(Permissions)}.");
        }

        public override void Write(Utf8JsonWriter writer, Permissions value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
