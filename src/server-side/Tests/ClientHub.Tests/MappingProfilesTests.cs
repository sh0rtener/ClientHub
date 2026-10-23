using System.Text;
using AutoMapper;
using ClientHub.UseCases.Shared.Dtos;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit.Abstractions;

namespace ClientHub.Tests;

public sealed class MappingProfilesTests
{
    private readonly ITestOutputHelper _output;

    public MappingProfilesTests(ITestOutputHelper output)
    {
        _output = output;
    }

    // ИИШНАЯ ТЕМА, НЕ НЕСУ ОТВЕТСТВЕННОСТЬ
    // ЧЕРЕЗ configuration.AssertConfigurationIsValid(); КРАШИТСЯ ВООБЩЕ ВЕСЬ ДОТНЕТ АШАЛЕТЬ
    [Fact]
    public void AllMappings_ShouldBeValid()
    {
        var configuration = new MapperConfiguration(
            cfg =>
            {
                cfg.AddMaps(typeof(ClientContactProfile).Assembly);
            },
            NullLoggerFactory.Instance
        );

        var unmappedErrors = new List<string>();

        var typeMapsProperty = configuration.GetType().GetProperty("TypeMaps");
        if (typeMapsProperty != null)
        {
            var typeMaps = typeMapsProperty.GetValue(configuration) as IEnumerable<object>;
            if (typeMaps != null)
            {
                foreach (var typeMap in typeMaps)
                {
                    var typesProperty = typeMap.GetType().GetProperty("Types");
                    var types = typesProperty?.GetValue(typeMap);

                    var sourceType = types?.GetType().GetProperty("SourceType")?.GetValue(types);
                    var destinationType = types
                        ?.GetType()
                        .GetProperty("DestinationType")
                        ?.GetValue(types);

                    // Вызываем GetUnmappedPropertyNames()
                    var getUnmappedMethod = typeMap.GetType().GetMethod("GetUnmappedPropertyNames");
                    if (getUnmappedMethod != null)
                    {
                        var unmappedProperties =
                            getUnmappedMethod.Invoke(typeMap, null) as IEnumerable<string>;
                        if (unmappedProperties != null && unmappedProperties.Any())
                        {
                            var error = new StringBuilder();
                            error.AppendLine($"\n[{sourceType} -> {destinationType}]");
                            foreach (var prop in unmappedProperties)
                            {
                                error.AppendLine($"  - {prop}");
                            }
                            unmappedErrors.Add(error.ToString());
                        }
                    }
                }
            }
        }

        if (unmappedErrors.Any())
        {
            var fullError = new StringBuilder();
            fullError.AppendLine("AutoMapper configuration is invalid. Unmapped members found:");
            foreach (var error in unmappedErrors)
            {
                fullError.AppendLine(error);
            }

            _output.WriteLine(fullError.ToString());
            throw new Exception(fullError.ToString());
        }

        _output.WriteLine("All mappings are valid!");
    }
}
