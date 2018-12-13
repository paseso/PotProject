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

        if (GUILayout.Button("マップに変換")) { CreateMap(); }
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
        int xLength = mapCreator.Map.mapDate[0].mapNum.Length;
        int yLength = mapCreator.Map.mapDate.Length;

        //  タイルのサイズを取得
        float tileSize = 2;
        //  ルートオブジェクトの作成
        var rootObj = new GameObject("StageRootObject");
        //  エディター側では左上が始点なので、その分場所の移動
        Vector2 startPos = rootObj.transform.position + new Vector3(tileSize / 2, tileSize * yLength - tileSize / 2, 0);
        //  オブジェクトの生成
        for(int y = 0; y < yLength; y++)
        {
            for(int x = 0; x < xLength; x++)
            {
                int tilenum = mapCreator.Map.mapDate[y].mapNum[x];
                int gimmickNum = mapCreator.Map.gimmickDate[y].mapNum[x];
                int enemyNum = mapCreator.Map.enemyDate[y].mapNum[x];
                //  通常タイルの生成
                if (tilenum != 0)
                {
                    var tileObj = Instantiate(mapCreator.GetTile(tilenum).TileObj);
                    tileObj.transform.parent = rootObj.transform;
                    tileObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                    //  周りにはしごギミックがあったらレイヤーを変更
                    if ((gimmickNum != 0 && mapCreator.GetGimmick(gimmickNum).GimmickObj.GetComponent<GimmickInfo>() != null) || (mapCreator.Map.gimmickDate[y].mapNum[x + 1] != 0 && mapCreator.GetGimmick(mapCreator.Map.gimmickDate[y].mapNum[x + 1]).GimmickObj.GetComponent<GimmickInfo>() != null) || (mapCreator.Map.gimmickDate[y].mapNum[x - 1] != 0 && mapCreator.GetGimmick(mapCreator.Map.gimmickDate[y].mapNum[x - 1]).GimmickObj.GetComponent<GimmickInfo>() != null))
                    {
                        if (mapCreator.GetGimmick(gimmickNum).GimmickObj.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.LADDER)
                        {
                            tileObj.layer = LayerMask.NameToLayer("LadderBrock");
                        }
                    }
                }
                //  ギミックの生成
                if (gimmickNum != 0)
                {
                    var gimmickObj = Instantiate(mapCreator.GetGimmick(gimmickNum).GimmickObj);
                    gimmickObj.transform.parent = rootObj.transform;
                    gimmickObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                }
                //  エネミーやポジションの生成
                if (enemyNum != 0)
                {
                    var enemyObj = Instantiate(mapCreator.GetEnemy(enemyNum).EnemyObj);
                    enemyObj.transform.parent = rootObj.transform;
                    enemyObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                }
            }
        }
    }

    private void CreateMap(MapData data)
    {
        //  配列の長さを取得
        int xLength = data.mapDate[0].mapNum.Length;
        int yLength = data.mapDate.Length;
        //  タイルのサイズを取得
        float tileSize = 2;
        //  ルートオブジェクトの作成
        string rootName = data.name;
        var rootObj = new GameObject(rootName);
        rootObj.transform.position = new Vector3(xLength / 2 * tileSize, yLength / 2 * tileSize, 0);

        //  エディター側では左上が始点なので、その分場所の移動
        Vector2 startPos = rootObj.transform.position + new Vector3(tileSize / 2, tileSize * yLength - tileSize / 2, 0);




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
