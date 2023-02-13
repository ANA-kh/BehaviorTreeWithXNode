using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
 
public static class SerializedPropertyUtility
{
    /// <summary>
    /// SerializedProperty から FieldInfo を取得する
    /// </summary>
    public static FieldInfo GetFieldInfo(this SerializedProperty property)
    {
        FieldInfo GetField(Type type, string path)
        {
            return type.GetField(path, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        }
 
        var parentType = property.serializedObject.targetObject.GetType();
        var splits = property.propertyPath.Split('.');
        var fieldInfo = GetField(parentType, splits[0]);
        for (var i = 1; i < splits.Length; i++)
        {
            if (splits[i] == "Array")
            {
                i += 2;
                if (i >= splits.Length)
                    continue;
 
                var type = fieldInfo.FieldType.IsArray
                    ? fieldInfo.FieldType.GetElementType()
                    : fieldInfo.FieldType.GetGenericArguments()[0];
                 
                fieldInfo = GetField(type, splits[i]);
            }
            else
            {
                fieldInfo = i + 1 < splits.Length && splits[i + 1] == "Array"
                    ? GetField(parentType, splits[i])
                    : GetField(fieldInfo.FieldType, splits[i]);
            }
 
            if (fieldInfo == null)
                throw new Exception("Invalid FieldInfo. " + property.propertyPath);
 
            parentType = fieldInfo.FieldType;
        }
        
        return fieldInfo;
    }
     
    /// <summary>
    /// SerializedProperty から Field の Type を取得する 
    /// </summary>
    /// <param name="property">SerializedProperty</param>
    /// <param name="isArrayListType">array や List の場合要素の Type を取得するか</param>
    public static Type GetPropertyType(this SerializedProperty property, bool isArrayListType = false)
    {
        var fieldInfo = property.GetFieldInfo();
 
        // 配列の場合は配列のTypeを返す
        if (isArrayListType && property.isArray && property.propertyType != SerializedPropertyType.String)
            return fieldInfo.FieldType.IsArray
                ? fieldInfo.FieldType.GetElementType()
                : fieldInfo.FieldType.GetGenericArguments()[0];
        return fieldInfo.FieldType;
    }
}