using System.Reflection;

namespace Study402Online.Common.Helpers;

public static class PocoHelper
{
    /// <summary>
    /// 复制属性
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    public static void CopyProperties<TSource, TDestination>(TSource source!!, TDestination destination!!)
        where TDestination : class
        where TSource : class
    {
        // 获取源类型和目标类型
        Type sourceType = source.GetType();
        Type destinationType = destination.GetType();

        // 获取源类型的所有属性
        PropertyInfo[] sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // 遍历源类型中的所有属性
        foreach (PropertyInfo sourceProperty in sourceProperties)
        {
            // 检查目标类型是否包含与源属性名称相同的属性
            PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name, BindingFlags.Public | BindingFlags.Instance);

            if (destinationProperty != null && destinationProperty.CanWrite)
            {
                // 如果目标属性存在且可写，则将源属性值复制到目标属性中
                object value = sourceProperty.GetValue(source);
                destinationProperty.SetValue(destination, value, null);
            }
        }
    }

}