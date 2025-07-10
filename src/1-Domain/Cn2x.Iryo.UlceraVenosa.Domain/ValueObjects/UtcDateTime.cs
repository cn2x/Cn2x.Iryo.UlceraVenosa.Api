using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Domain.ValueObjects;


using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

public class UtcDateTime : ValueObject {
    public DateTime Value { get; private set; }

    [JsonConstructor]
    public UtcDateTime(DateTime value) {
        if (value.Kind != DateTimeKind.Utc)
            throw new ArgumentException("DateTime must be in UTC.", nameof(value));

        Value = value;
    }

    public static UtcDateTime From(DateTime utcDateTime) {
        return new UtcDateTime(DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc));
    }

    public DateTime ToBrasiliaTime() {
        var brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "E. South America Standard Time"
                : "America/Sao_Paulo"
        );

        return TimeZoneInfo.ConvertTimeFromUtc(Value, brasiliaTimeZone);
    }

    public override string ToString() {
        return Value.ToString("o", CultureInfo.InvariantCulture); // ISO 8601
    }

    protected override IEnumerable<object> GetEqualityComponents() {
        yield return Value;
    }

    // 👇 Implicit cast operators
    public static implicit operator DateTime(UtcDateTime utc) {
        return utc?.Value ?? default;
    }

    public static implicit operator UtcDateTime(DateTime value) {
        return new UtcDateTime(DateTime.SpecifyKind(value, DateTimeKind.Utc));
    }
}
