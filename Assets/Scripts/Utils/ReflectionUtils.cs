using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aboba.Utils
{
  public static class ReflectionUtils
  {
    public static void SetPropertyWithoutSetter(object obj, string propName, object newValue)
    {
      var type = obj.GetType();
      var field = GetPrivateField(type, $"<{propName}>k__BackingField");

      var startsWithLowerPropName = $"{propName.First().ToString().ToLower()}{propName.Substring(1)}";
      
      if (field == null)
        field = GetPrivateField(type, startsWithLowerPropName);

      if (field == null)
        field = GetPrivateField(type, $"_{startsWithLowerPropName}");
      
      field!.SetValue(obj, newValue);
    }

    public static IEnumerable<object> RequireCollectionValueOfPrivateField(object instance, string fieldName)
    {
      var fieldInfo = RequirePrivateField(instance.GetType(), fieldName);
      var value = (IEnumerable)fieldInfo.GetValue(instance);
      return value.Cast<object>();
    }

    public static object? RequireValueOfPrivateField(object instance, string fieldName)
    {
      var fieldInfo = RequirePrivateField(instance.GetType(), fieldName);
      var value = fieldInfo.GetValue(instance);
      return value;
    }
    
    public static TValue? RequireValueOfPrivateField<TValue>(object instance, string fieldName)
    {
      var value = RequireValueOfPrivateField(instance, fieldName);

      if(value == null)
        return default;

      try
      {
        return (TValue)value;
      }
      catch (InvalidCastException)
      {
        throw new InvalidCastException($"Не удается привести значение {instance.GetType().Name}.{fieldName} к типу {typeof(TValue).Name}");
      }
    }
    
    public static TValue? RequireValueOfPublicField<TValue>(object instance, string fieldName)
    {
      var fieldInfo = RequirePublicField(instance.GetType(), fieldName);
      var value = fieldInfo.GetValue(instance);
      
      if(value == null)
        return default;

      try
      {
        return (TValue)value;
      }
      catch (InvalidCastException)
      {
        throw new InvalidCastException($"Не удается привести значение {instance.GetType().Name}.{fieldName} к типу {typeof(TValue).Name}");
      }
    }


    private static FieldInfo RequirePrivateField(Type type, string fieldName)
    {
      var fieldInfo = GetPrivateField(type, fieldName);
      
      if (fieldInfo == null)
        throw new NullReferenceException($"Приватное поле с именем '{type.Name}.{fieldName}' не найдено");

      return fieldInfo;
    }
    
    private static FieldInfo RequirePublicField(Type type, string fieldName)
    {
      var fieldInfo = GetPublicField(type, fieldName);
      
      if (fieldInfo == null)
        throw new NullReferenceException($"Публичное поле с именем '{type.Name}.{fieldName}' не найдено");

      return fieldInfo;
    }

    private static FieldInfo? GetPrivateField(Type type, string fieldName) => type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
    
    private static FieldInfo? GetPublicField(Type type, string fieldName) => type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public);
  }
}