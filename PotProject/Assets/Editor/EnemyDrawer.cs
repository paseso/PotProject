using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Enemy))]
public class EnemyDrawer : PropertyDrawer {

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
                y = iconRect.y + 15
            };

            //各プロパティーの SerializedProperty を求める
            var iconProperty = property.FindPropertyRelative("enemyImage");
            var objProperty = property.FindPropertyRelative("enemyObj");

            //各プロパティーの GUI を描画
            iconProperty.objectReferenceValue = EditorGUI.ObjectField(iconRect, iconProperty.objectReferenceValue, typeof(Texture), false);
            objProperty.objectReferenceValue = EditorGUI.ObjectField(objRect, objProperty.objectReferenceValue, typeof(GameObject), false);

        }
    }
}
