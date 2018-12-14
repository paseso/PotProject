using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorInspector : Editor
{
    //  順番が可変できるリスト
    ReorderableList tileReorderableList;
    ReorderableList gimmickReorderList;
    ReorderableList enemyReorderableList;
    bool tileFoldOut;
    bool gimmickFoldOut;
    bool enemyFoldOut;
    MapCreator mapCreator;

    private void OnEnable()
    {
        var tileProp = serializedObject.FindProperty("tiles");
        var gimmickProp = serializedObject.FindProperty("gimmicks");
        var enemyProp = serializedObject.FindProperty("enemies");

        tileReorderableList = new ReorderableList(serializedObject, tileProp);
        tileReorderableList.elementHeight = 55;
        tileReorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
          {
              var element = tileProp.GetArrayElementAtIndex(index);
              rect.height -= 4;
              rect.y += 2;
              EditorGUI.PropertyField(rect, element);
          };
        gimmickReorderList = new ReorderableList(serializedObject, gimmickProp);
        gimmickReorderList.elementHeight = 55;
        gimmickReorderList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = gimmickProp.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, element);
        };
        enemyReorderableList = new ReorderableList(serializedObject, enemyProp);
        enemyReorderableList.elementHeight = 55;
        enemyReorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = enemyProp.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, element);
        };

        var defaultColor = GUI.backgroundColor;

        tileReorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, tileProp.displayName);
    }

    public override void OnInspectorGUI()
    {
        //  最新情報に更新
        serializedObject.Update();
        //  マップデータ
        mapCreator = (MapCreator)target;
        mapCreator.Map = (MapData)EditorGUILayout.ObjectField("マップデータ", mapCreator.Map, typeof(MapData), false);
        mapCreator.Player = (GameObject)EditorGUILayout.ObjectField("Player", mapCreator.Player, typeof(GameObject), false);
        mapCreator.Pot = (GameObject)EditorGUILayout.ObjectField("Pot", mapCreator.Pot, typeof(GameObject), false);

        //if (GUILayout.Button("マップに変換")) { mapCreator.CreateMap(); }
        if (GUILayout.Button("マップのタイルID表示")) { PrintlistNum(); }
        tileFoldOut = EditorGUILayout.Foldout( tileFoldOut,"Tile" );
		if(tileFoldOut)
		{
			EditorGUILayout.LabelField("タイルのTextureとPrefab");
            EditorGUILayout.HelpBox("※0番目は消しゴムのままで!!", MessageType.Warning);
            tileReorderableList.DoLayoutList();
        }
        gimmickFoldOut = EditorGUILayout.Foldout( gimmickFoldOut, "Gimmick");
        if (gimmickFoldOut)
        {
            gimmickReorderList.DoLayoutList();
        }
        enemyFoldOut = EditorGUILayout.Foldout(enemyFoldOut, "Enemy");
        if (enemyFoldOut)
        {
            enemyReorderableList.DoLayoutList();
        }

        serializedObject.ApplyModifiedProperties();
    }


    private void PrintlistNum()
    {
        //  配列の長さを取得
        int xLength = mapCreator.Map.mapDate[0].mapNum.Length;
        int yLength = mapCreator.Map.mapDate.Length;

        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                Debug.Log("TileID : " + mapCreator.Map.mapDate[y].mapNum[x]);
            }
        }
    }
}
