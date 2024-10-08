using System;

namespace Backend.Helper;

public static class GlobalHelper
{
    public static string GenerateStudentNumber(){
        return (DateTime.Now.Year+'-'+ new Random().Next(0, 1000)).ToString();
    }

    public static bool CompareObject<TNew, TOld>(TNew New, TOld Old)
    {

        var newProperties = typeof(TNew).GetProperties();
        var oldProperties = typeof(TOld).GetProperties();

        string[] properties = ["CreatedBy", "UpdateBy", "CreatedDate", "UpdatedDate"];

        var destPropertyDict = oldProperties.ToDictionary(p => p.Name);

        foreach (var property in newProperties)
        {
            if (!properties.Contains(property.Name) && destPropertyDict.TryGetValue(property.Name, out var oldValue))
            {
                if (oldValue.GetValue(Old) != property.GetValue(New))
                {
                    return false;
                }
            }

        }

        return true;
    }


    public static TDestination MapperFields<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (source == null || destination == null)
            throw new ArgumentNullException("Source or Destination cannot be null");

        var sourceProperties = typeof(TSource).GetProperties();
        var destinationProperties = typeof(TDestination).GetProperties();

        var destPropertyDict = destinationProperties.ToDictionary(p => p.Name);

        foreach (var property in sourceProperties)
        {
            if (destPropertyDict.TryGetValue(property.Name, out var destProperty) && destProperty.CanWrite)
            {
                var value = property.GetValue(source);
                destProperty.SetValue(destination, value);
            }
        }

        return destination;
    }

    public static TSource SetCreatedUpdated<TSource>(TSource source)
    {
        var sourceType = source.GetType();

        var createdByProperty = sourceType.GetProperty("CreatedBy");
        if (createdByProperty?.GetValue(source) == null)
        {
            createdByProperty?.SetValue(source, "default");
            sourceType.GetProperty("CreatedDate")?.SetValue(source, DateTime.UtcNow);
        }

        sourceType.GetProperty("UpdatedBy")?.SetValue(source, "default");
        sourceType.GetProperty("UpdatedDate")?.SetValue(source, DateTime.UtcNow);

        return source;
    }

}
