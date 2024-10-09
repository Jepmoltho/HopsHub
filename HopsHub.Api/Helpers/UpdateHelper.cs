namespace HopsHub.Api.Helpers;

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
                    sourceProperty.SetValue(source, targetValue);
                    //targetProperty.SetValue(target, sourceValue);
                }
            }
        }
    }
}

