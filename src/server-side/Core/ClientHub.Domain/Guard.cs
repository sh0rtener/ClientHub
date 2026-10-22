namespace ClientHub.Domain;

public static class Guard
{
    public static void ValidateLength(string value, int maxLength = 100)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (value.Trim().Length > maxLength)
            throw new InvalidPropertyException(
                nameof(value),
                $"не должен превышать {maxLength} символов"
            );
    }

    public static void MustGreaterThan(int value, int min = 0)
    {
        if (value < min)
            throw new InvalidPropertyException(nameof(value));
    }

    public static void MustGreaterThan(decimal value, decimal min = 0)
    {
        if (value < min)
            throw new InvalidPropertyException(nameof(value));
    }

    public static void ValidateEnum<T>(T value)
        where T : struct, Enum
    {
        if (!Enum.IsDefined(value))
            throw new InvalidPropertyException(nameof(value), "неопределенное значение");
    }

    public static void ValidateDate(DateTime value)
    {
        if (value == default)
            throw new InvalidPropertyException(nameof(value));
    }

    public static void SaveAddToCollection<T>(List<T> inner, IEnumerable<T> added)
    {
        ArgumentNullException.ThrowIfNull(added);

        foreach (var value in added)
        {
            if (inner.Any(c => c.Equals(value)))
                continue;

            inner.Add(value);
        }
    }

    public static void SaveRemoveToCollection<T>(List<T> inner, IEnumerable<T> removable)
    {
        ArgumentNullException.ThrowIfNull(removable);

        foreach (var value in removable)
            inner.Remove(value);
    }
}
