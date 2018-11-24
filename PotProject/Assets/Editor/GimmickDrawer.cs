using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Gimmick))]
public class GimmickDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUIUtility.labelWidth = 50;

            position.height = EditorGUIUtility.singleLineHeight;

            var iconRect = new Rect(position)
            {
                width = 50,
                height = 50
            };

            var objRect = new Rect(position)
            {
                width = position.width - 50,
                x = position.x + 54,
                y = iconRect.y + 5
            };

            var vec2Rect = new Rect(position)
            {
                width = position.width - 50,
                x = position.x + 54,
                y = iconRect.y + 25
            };


            //各プロパティーの SerializedProperty を求める
            var iconProperty = property.FindPropertyRelative("gimmickImage");
            var sizeProrerty = property.FindPropertyRelative("gimmickSize");
            var objProperty = property.FindPropertyRelative("gimmickObj");

            //各プロパティーの GUI を描画
            iconProperty.objectReferenceValue = EditorGUI.ObjectField(iconRect, iconProperty.objectReferenceValue, typeof(Texture), false);
            objProperty.objectReferenceValue = EditorGUI.ObjectField(objRect, objProperty.objectReferenceValue, typeof(GameObject), false);
            sizeProrerty.vector2Value = EditorGUI.Vector2Field(vec2Rect, "Ratio", sizeProrerty.vector2Value);
        }
    }
}
