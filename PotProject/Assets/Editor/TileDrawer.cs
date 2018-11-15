using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileDrawer : PropertyDrawer {

    private Tile tile;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUIUtility.labelWidth = 50;

            position.height = EditorGUIUtility.singleLineHeight;

            var halfWidth = position.width * 0.5f;

            var iconRect = new Rect(position)
            {
                width = 64,
                height = 64
            };

            var xRect = new Rect(position)
            {
                width = position.width - 64,
                x = position.x + 64
            };

            var yRect = new Rect(xRect)
            {
                y = xRect.y + EditorGUIUtility.singleLineHeight * 2 + 5
            };

            var iconProperty = property.FindPropertyRelative("icon");
            var xProperty = property.FindPropertyRelative("x");
            var yProperty = property.FindPropertyRelative("y");

            iconProperty.objectReferenceValue = EditorGUI.ObjectField(iconRect, iconProperty.objectReferenceValue, typeof(Texture), false);
            EditorGUI.IntSlider(xRect, xProperty, 1, 3);
            EditorGUI.IntSlider(yRect, yProperty, 1, 3);

        }
    }
}
