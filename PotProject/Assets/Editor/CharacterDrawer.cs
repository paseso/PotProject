using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Charactor))]
public class CharacterDrawer : PropertyDrawer
{
    private Charactor character;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //元は 1 つのプロパティーであることを示すために PropertyScope で囲む
        using (new EditorGUI.PropertyScope(position, label, property))
        {

            //サムネの領域を確保するためにラベル領域の幅を小さくする
            EditorGUIUtility.labelWidth = 50;

            position.height = EditorGUIUtility.singleLineHeight;

            //var halfWidth = position.width * 0.5f;

            //各プロパティーの Rect を求める
            var iconRect = new Rect(position)
            {
                width = 64,
                height = 64
            };

            var nameRect = new Rect(position)
            {
                width = position.width - 64,
                x = position.x + 64
            };

            var hpRect = new Rect(nameRect)
            {
                y = nameRect.y + EditorGUIUtility.singleLineHeight + 2
            };

            var powerRect = new Rect(hpRect)
            {
                y = hpRect.y + EditorGUIUtility.singleLineHeight + 2
            };

            //各プロパティーの SerializedProperty を求める
            var iconProperty = property.FindPropertyRelative("icon");
            var nameProperty = property.FindPropertyRelative("name");
            var hpProperty = property.FindPropertyRelative("hp");
            var powerProperty = property.FindPropertyRelative("power");

            //各プロパティーの GUI を描画
            iconProperty.objectReferenceValue =
              EditorGUI.ObjectField(iconRect,
                iconProperty.objectReferenceValue, typeof(Texture), false);

            nameProperty.stringValue =
              EditorGUI.TextField(nameRect,
                nameProperty.displayName, nameProperty.stringValue);

            EditorGUI.IntSlider(hpRect, hpProperty, 0, 100);
            EditorGUI.IntSlider(powerRect, powerProperty, 0, 10);

        }
    }
}