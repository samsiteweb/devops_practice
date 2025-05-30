using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.UtcDateTimeOffsetConverter", Version = "1.0")]

namespace RequestManagement.Infrastructure.Persistence.Configurations.Converters;

/// <summary>
/// Postgres requires DateTimeOffset to be stored as Utc , this converter ensures that is the case
/// </summary>
public class UtcDateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public UtcDateTimeOffsetConverter() : base(nonUtc => nonUtc.ToUniversalTime(), utc => utc)
    {
    }
}