using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorInspector : Editor
{
    //  順番が可変できるリスト
    ReorderableList reorderableList;
    bool foldout;

    private void OnEnable()
    {
        var prop = serializedObject.FindProperty("tiles");

        reorderableList = new ReorderableList(serializedObject, prop);
        reorderableList.elementHeight = 55;
        reorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
          {
              var element = prop.GetArrayElementAtIndex(index);
              rect.height -= 4;
              rect.y += 2;
              EditorGUI.PropertyField(rect, element);
          };

        var defaultColor = GUI.backgroundColor;

        reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, prop.displayName);
    }

    public override void OnInspectorGUI()
    {
        //  最新情報に更新
        serializedObject.Update();
        //  マップデータ
        MapCreator mapCreator = (MapCreator)target;
        //EditorGUILayout.ObjectField("マップデータ",mapdate, typeof(ScriptableObjectSample), false);
        //mapCreator.

        if (GUILayout.Button("マップに変換"))
        {
            CreateMap();
        }
        EditorGUI.indentLevel++;
        foldout = EditorGUILayout.Foldout( foldout,"Tile" );
		if(foldout)
		{
            EditorGUI.indentLevel--;
			EditorGUILayout.LabelField("タイルのTextureとPrefab ※0番目は消しゴムのままで!!");
            reorderableList.DoLayoutList();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void CreateMap()
    {
        //int xLength = MapDate..GetLength(1);
        //int yLength = MapDate.MapData.GetLength(0);

        var obj = new GameObject("RootObject");
        

        //scriptable.MapData.le
    }
}
