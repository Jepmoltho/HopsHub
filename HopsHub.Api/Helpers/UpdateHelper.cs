namespace HopsHub.Api.Helpers;

/// <summary>
/// Source: The source of the new data eg the DTO
/// Target: The target of the update eg the Model
/// </summary>

public static class UpdateHelper
{
    public static void UpdateEntity<TSource, TTarget>(TSource source, TTarget target)
        where TSource : class
        where TTarget : class
    {
        var sourceType = typeof(TSource);
        var targetType = typeof(TTarget);

        var targetProperties = targetType.GetProperties();

        foreach (var targetProperty in targetProperties)
        {
            var sourceProperty = sourceType.GetProperty(targetProperty.Name);

            if (sourceProperty != null && sourceProperty.Name != "Id")
            {
                var sourceValue = sourceProperty.GetValue(source);
                var targetValue = targetProperty.GetValue(target);

                if (sourceValue != null && !Equals(sourceValue, targetValue))
                {
                    targetProperty.SetValue(target, sourceValue);
                }
            }
        }
    }
}

