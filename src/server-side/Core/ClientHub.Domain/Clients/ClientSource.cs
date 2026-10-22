namespace ClientHub.Domain.Clients;

public sealed class ClientSource : ValueObject
{
    public string Source { get; }
    public string SourceUrl { get; }

    public ClientSource(string source, string sourceUrl)
    {
        Guard.ValidateLength(source);
        Guard.ValidateLength(sourceUrl);

        Source = source.Trim();
        SourceUrl = sourceUrl.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Source;
        yield return SourceUrl;
    }
}
