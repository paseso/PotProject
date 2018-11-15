using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Tile))]
public class TileDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //元は 1 つのプロパティーであることを示すために PropertyScope で囲む
        using (new EditorGUI.PropertyScope(position, label, property))
        {

            //サムネの領域を確保するためにラベル領域の幅を小さくする
            EditorGUIUtility.labelWidth = 50;

            position.height = EditorGUIUtility.singleLineHeight;

            //各プロパティーの Rect を求める
            var iconRect = new Rect(position)
            {
                width = 50,
                height = 50
            };
            var objRect = new Rect(position)
            {
                width = position.width - 50,
                x = position.x + 54,
                y = iconRect.y + 10
            };

            //各プロパティーの SerializedProperty を求める
            var iconProperty = property.FindPropertyRelative("tileImage");
            var objProperty = property.FindPropertyRelative("tileObj");

            //各プロパティーの GUI を描画
            iconProperty.objectReferenceValue =
              EditorGUI.ObjectField(iconRect,
                iconProperty.objectReferenceValue, typeof(Texture), false);
            objProperty.objectReferenceValue =
                EditorGUI.ObjectField(objRect,
                objProperty.objectReferenceValue, typeof(GameObject), false);
        }
    }

}
