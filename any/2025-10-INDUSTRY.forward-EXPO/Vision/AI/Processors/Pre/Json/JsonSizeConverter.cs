namespace App.AI
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class JsonSizeConverter : JsonConverter<Size>
    {
        public JsonSizeConverter()
            : base()
        {
        }

        public override Size Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int? with = null;
            int? height = null;

            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException("Expected property name for Size.");

                string name = reader.GetString()!;

                if (!reader.Read())
                    throw new JsonException("Unexpected end of JSON while reading Size.");

                switch (name.ToLowerInvariant()) {
                    case "width":
                        with = reader.TokenType switch {
                            JsonTokenType.Number => reader.GetInt32(),
                            JsonTokenType.String => int.Parse(reader.GetString()!, CultureInfo.InvariantCulture),
                            _ => throw new JsonException("Width must be a number.")
                        };
                        break;

                    case "height":
                        height = reader.TokenType switch {
                            JsonTokenType.Number => reader.GetInt32(),
                            JsonTokenType.String => int.Parse(reader.GetString()!, CultureInfo.InvariantCulture),
                            _ => throw new JsonException("Height must be a number.")
                        };
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            if (with is null || height is null)
                throw new JsonException("Size requires both width and height.");

            return new Size(with.Value, height.Value);
        }

        public override void Write(Utf8JsonWriter writer, Size value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("width", value.Width);
            writer.WriteNumber("height", value.Height);
            writer.WriteEndObject();
        }
    }
}
