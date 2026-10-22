namespace ClientHub.Domain;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
        return ReferenceEquals(left, right) || left.Equals(right);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !(EqualOperator(left, right));
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
}

public abstract class DefaultStringValueObject : ValueObject
{
    public string Value { get; }

    public DefaultStringValueObject(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        value = value.Trim();
        Guard.ValidateLength(value);
        Validate(value);
        Value = value;
    }

    public DefaultStringValueObject(string value, int maxLength)
    {
        ArgumentNullException.ThrowIfNull(value);
        value = value.Trim();
        Guard.ValidateLength(value, maxLength);
        Validate(value);
        Value = value;
    }

    public virtual void Validate(string value) { }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
