namespace ClientHub.Caching;

public static class Constants
{
    public static string EmptyGetFilterName => "filter";
    public static string EmptyIndividualEntrepreneursFilter => $"{EmptyGetFilterName}:IP";
    public static string EmptyLegalEntityFilter => $"{EmptyGetFilterName}:LE";
    public static string IndividualEntrepreneurs => "IP:";
    public static string LegalEntity => "LE:";
}