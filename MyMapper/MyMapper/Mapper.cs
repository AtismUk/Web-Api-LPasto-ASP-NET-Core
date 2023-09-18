using MyMapper.Interface;
using System.Reflection;

namespace MyMapper
{
    public static class Mapper 
    {
        public static ToModel MappingModels<FromModel, ToModel>(FromModel entity) where ToModel : class, new()
        {
            ToModel model = new();
            Type fromType = entity.GetType();
            Type toType = model.GetType();
            foreach (var fromProperty in fromType.GetProperties())
            {
                foreach (var toProperty in toType.GetProperties())
                {
                    if ((toProperty.Name.ToLower() == fromProperty.Name.ToLower()) && (toProperty.GetType() == fromProperty.GetType()))
                    {
                        var targetValue = fromProperty.GetValue(entity);
                        toProperty.SetValue(model, targetValue);
                        break;
                    }
                }
            }
            return model;
        }

        public static void Replace<FromModel, ToModel>(FromModel FromEntity, ToModel ToEntity) where ToModel : class
        {
            Type fromType = FromEntity.GetType();
            Type toType = ToEntity.GetType();
            foreach (var fromProperty in fromType.GetProperties())
            {
                foreach (var toProperty in toType.GetProperties())
                {
                    if ((toProperty.Name.ToLower() == fromProperty.Name.ToLower()) && (toProperty.GetType() == fromProperty.GetType()))
                    {
                        var targetValue = fromProperty.GetValue(FromEntity);
                        toProperty.SetValue(ToEntity, targetValue);
                        break;
                    }
                }
            }
        }

        public static bool CheckNull<TModel>(TModel model) where TModel : class
        {
            Type modelType = model.GetType();
            foreach (var property in modelType.GetProperties())
            {
                if (property.PropertyType.IsValueType)
                {
                    var value = property.GetValue(model);
                    if (property.PropertyType.IsPrimitive && value.Equals(Activator.CreateInstance(property.PropertyType)))
                    {
                        return false;
                    }
                }
                else if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                {
                    var value = property.GetValue(model);
                    if (value == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}