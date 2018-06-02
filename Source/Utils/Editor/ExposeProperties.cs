using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

// Taken from http://wiki.unity3d.com/index.php/ExposePropertiesInInspector_SetOnlyWhenChanged
// Original author: Mift (mift)
// Variant author: Venryx(venryx)

namespace WolarGames.Variables.Utils
{
    public static class ExposeProperties
    {
        public static void Expose(PropertyField[] properties) {
            var emptyOptions = new GUILayoutOption[0];
            EditorGUILayout.BeginVertical(emptyOptions);
            foreach (PropertyField field in properties) {
                EditorGUILayout.BeginHorizontal(emptyOptions);
                if (field.Type == SerializedPropertyType.Integer) {
                    var oldValue = (int)field.GetValue();
                    var newValue = EditorGUILayout.IntField(field.Name, oldValue, emptyOptions);
                    if (oldValue != newValue)
                        field.SetValue(newValue);
                } else if (field.Type == SerializedPropertyType.Float) {
                    var oldValue = (float)field.GetValue();
                    var newValue = EditorGUILayout.FloatField(field.Name, oldValue, emptyOptions);
                    if (oldValue != newValue)
                        field.SetValue(newValue);
                } else if (field.Type == SerializedPropertyType.Boolean) {
                    var oldValue = (bool)field.GetValue();
                    var newValue = EditorGUILayout.Toggle(field.Name, oldValue, emptyOptions);
                    if (oldValue != newValue)
                        field.SetValue(newValue);
                } else if (field.Type == SerializedPropertyType.String) {
                    var oldValue = (string)field.GetValue();
                    var newValue = EditorGUILayout.TextField(field.Name, oldValue, emptyOptions);
                    if (oldValue != newValue)
                        field.SetValue(newValue);
                } else if (field.Type == SerializedPropertyType.Vector2) {
                    var oldValue = (Vector2)field.GetValue();
                    var newValue = EditorGUILayout.Vector2Field(field.Name, oldValue, emptyOptions);
                    if (oldValue != newValue)
                        field.SetValue(newValue);
                } else if (field.Type == SerializedPropertyType.Vector3) {
                    var oldValue = (Vector3)field.GetValue();
                    var newValue = EditorGUILayout.Vector3Field(field.Name, oldValue, emptyOptions);
                    if (oldValue != newValue)
                        field.SetValue(newValue);
                } else if (field.Type == SerializedPropertyType.Enum) {
                    var oldValue = (Enum)field.GetValue();
                    var newValue = EditorGUILayout.EnumPopup(field.Name, oldValue, emptyOptions);
                    if (oldValue != newValue)
                        field.SetValue(newValue);
                } else if (field.Type == SerializedPropertyType.ObjectReference) {
                    UnityEngine.Object oldValue = (UnityEngine.Object)field.GetValue();
                    UnityEngine.Object newValue = EditorGUILayout.ObjectField(field.Name, oldValue, field.Info.PropertyType, true);
                    if (oldValue != newValue)
                        field.SetValue(newValue);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        public static PropertyField[] GetProperties(object obj) {
            var fields = new List<PropertyField>();

            PropertyInfo[] infos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo info in infos) {
                if (!(info.CanRead && info.CanWrite))
                    continue;

                object[] attributes = info.GetCustomAttributes(true);

                bool isExposed = false;
                foreach (object o in attributes)
                    if (o.GetType() == typeof(ExposePropertyAttribute)) {
                        isExposed = true;
                        break;
                    }
                if (!isExposed)
                    continue;

                var type = SerializedPropertyType.Integer;
                if (PropertyField.GetPropertyType(info, out type)) {
                    var field = new PropertyField(obj, info, type);
                    fields.Add(field);
                }
            }

            return fields.ToArray();
        }
    }

    public class PropertyField
    {
        object obj;
        PropertyInfo info;
        SerializedPropertyType type;

        MethodInfo getter;
        MethodInfo setter;

        public PropertyInfo Info
        {
            get { return info; }
        }

        public SerializedPropertyType Type
        {
            get { return type; }
        }

        public String Name
        {
            get { return ObjectNames.NicifyVariableName(info.Name); }
        }

        public PropertyField(object obj, PropertyInfo info, SerializedPropertyType type) {
            this.obj = obj;
            this.info = info;
            this.type = type;

            getter = this.info.GetGetMethod();
            setter = this.info.GetSetMethod();
        }

        public object GetValue() { return getter.Invoke(obj, null); }
        public void SetValue(object value) { 
            Undo.RecordObject(obj as UnityEngine.Object, "Exposed property changed");
            setter.Invoke(obj, new[] { value }); 
        }

        public static bool GetPropertyType(PropertyInfo info, out SerializedPropertyType propertyType) {
            Type type = info.PropertyType;
            propertyType = SerializedPropertyType.Generic;
            if (type == typeof(int))
                propertyType = SerializedPropertyType.Integer;
            else if (type == typeof(float))
                propertyType = SerializedPropertyType.Float;
            else if (type == typeof(bool))
                propertyType = SerializedPropertyType.Boolean;
            else if (type == typeof(string))
                propertyType = SerializedPropertyType.String;
            else if (type == typeof(Vector2))
                propertyType = SerializedPropertyType.Vector2;
            else if (type == typeof(Vector3))
                propertyType = SerializedPropertyType.Vector3;
            else if (type.IsEnum)
                propertyType = SerializedPropertyType.Enum;
            else if (typeof(MonoBehaviour).IsAssignableFrom(type))
                propertyType = SerializedPropertyType.ObjectReference;
            else if (typeof(ScriptableObject).IsAssignableFrom(type)) 
                propertyType = SerializedPropertyType.ObjectReference;
            return propertyType != SerializedPropertyType.Generic;
        }
    }
}