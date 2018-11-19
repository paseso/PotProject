using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(MapCreator))]
public class MapCreatorInspector : Editor
{
    //  順番が可変できるリスト
    ReorderableList reorderableList;
    bool foldout;
    MapCreator mapCreator;

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
        mapCreator = (MapCreator)target;
        mapCreator.Map = (MapDate)EditorGUILayout.ObjectField("マップデータ", mapCreator.Map, typeof(MapDate), false);
        mapCreator.tilePrefab = (GameObject)EditorGUILayout.ObjectField("TilePrefab", mapCreator.tilePrefab, typeof(GameObject), false);

        if (GUILayout.Button("マップに変換")) { CreateMap(); }
        if (GUILayout.Button("test")) { PrintlistNum(); }
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
    /// <summary>
    /// マップを生成
    /// </summary>
    private void CreateMap()
    {
        if (!mapCreator.Map)
        {
            EditorUtility.DisplayDialog("Error", "マップデータがセットされていません", "OK");
            return;
        }

        //  配列の長さを取得
        int xLength = mapCreator.Map.MapDataList.GetLength(1);
        int yLength = mapCreator.Map.MapDataList.GetLength(0);

        //  タイルのサイズを取得
        float tileSize = mapCreator.tilePrefab.GetComponent<SpriteRenderer>().sprite.texture.width / 100;
        //  ルートオブジェクトの作成
        var rootObj = new GameObject("StageRootObject");
        //  エディター側では左上が始点なので、その分場所の移動
        Vector2 startPos = rootObj.transform.position + new Vector3(0, tileSize * -yLength, 0);

        for(int y = 0; y < yLength; y++)
        {
            for(int x = 0; x < xLength; x++)
            {
                if (mapCreator.Map.MapDataList[y,x] != 0)
                {
                    var TileObj = Instantiate(mapCreator.tilePrefab);
                    TileObj.transform.parent = rootObj.transform;
                    TileObj.transform.position = startPos + new Vector2(tileSize * x, tileSize * y);
                }
            }
        }
    }
    private void PrintlistNum()
    {
        //  配列の長さを取得
        int xLength = mapCreator.Map.MapDataList.GetLength(1);
        int yLength = mapCreator.Map.MapDataList.GetLength(0);

        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                Debug.Log(mapCreator.Map.MapDataList[y, x]);
            }
        }
    }
}
