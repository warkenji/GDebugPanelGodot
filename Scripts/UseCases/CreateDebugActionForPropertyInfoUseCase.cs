using System;
using System.Reflection;
using GDebugPanelGodot.DebugActions.Containers;
using GDebugPanelGodot.Extensions;
using Godot;

namespace GDebugPanelGodot.UseCases;

public static class CreateDebugActionForPropertyInfoUseCase
{
    public static void Execute(DebugActionsSection section, object optionsObject, PropertyInfo propertyInfo)
    {
        MethodInfo? getter = propertyInfo.GetGetMethod();
        MethodInfo? setter = propertyInfo.GetSetMethod();

        bool hasSetter = setter != null;
        bool hasPublicGetter = getter != null && getter.IsPublic;
        bool hasPublicSetter = hasSetter && setter.IsPublic;
        bool hasPublicGetterAndSetter = hasPublicGetter && hasPublicSetter;

        if (hasPublicGetterAndSetter)
        {
            if (propertyInfo.PropertyType == typeof(bool))
            {
                section.AddToggle(
                    propertyInfo.Name,
                    val => propertyInfo.SetValue(optionsObject, val),
                    () => (bool)propertyInfo.GetValue(optionsObject)!
                );
            }
            else if(propertyInfo.PropertyType == typeof(Color))
            {
                section.AddColor(
                    propertyInfo.Name,
                    val => propertyInfo.SetValue(optionsObject, val),
                    () => (Color)propertyInfo.GetValue(optionsObject)!
                );
            }
            else if(propertyInfo.PropertyType == typeof(string))
            {
                section.AddString(
                    propertyInfo.Name,
                    val => propertyInfo.SetValue(optionsObject, val),
                    () => (string)propertyInfo.GetValue(optionsObject)!
                );
            }
            else if(propertyInfo.PropertyType == typeof(int))
            {
                section.AddInt(
                    propertyInfo.Name,
                    val => propertyInfo.SetValue(optionsObject, val),
                    () => (int)propertyInfo.GetValue(optionsObject)!
                );
            }
            else if(propertyInfo.PropertyType == typeof(float))
            {
                section.AddFloat(
                    propertyInfo.Name,
                    val => propertyInfo.SetValue(optionsObject, val),
                    () => (float)propertyInfo.GetValue(optionsObject)!
                );
            }
            else if(propertyInfo.PropertyType.IsAssignableTo(typeof(Enum)))
            {
                section.AddEnum(
                    propertyInfo.Name,
                    propertyInfo.PropertyType,
                    val => propertyInfo.SetValue(optionsObject, val),
                    () => propertyInfo.GetValue(optionsObject)!
                );
            }
        }
        else if (hasPublicGetter && propertyInfo.PropertyType == typeof(string))
        {
            if (hasSetter)
            {
                section.AddInfoDynamic(
                    () => (string)propertyInfo.GetValue(optionsObject)!
                );
            }
            else
            {
                section.AddInfo(
                    (string)propertyInfo.GetValue(optionsObject)!
                );
            }
        }
    }
}