﻿using UnityEngine;
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
        mapCreator.Map = (MapDate)EditorGUILayout.ObjectField("マップデータ", mapCreator.Map, typeof(MapDate), false);
        mapCreator.tilePrefab = (GameObject)EditorGUILayout.ObjectField("TilePrefab", mapCreator.tilePrefab, typeof(GameObject), false);

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
        float tileSize = mapCreator.tilePrefab.GetComponent<SpriteRenderer>().sprite.texture.width / 100;
        //  ルートオブジェクトの作成
        var rootObj = new GameObject("StageRootObject");
        //  エディター側では左上が始点なので、その分場所の移動
        Vector2 startPos = rootObj.transform.position + new Vector3(tileSize / 2, tileSize * yLength - tileSize / 2, 0);
        //  オブジェクトの生成
        for(int y = 0; y < yLength; y++)
        {
            for(int x = 0; x < xLength; x++)
            {
                if (mapCreator.Map.mapDate[y].mapNum[x] != 0)
                {
                    var tileObj = Instantiate(mapCreator.GetTile(mapCreator.Map.mapDate[y].mapNum[x]).TileObj);
                    //  ギミックを配置するポジションにタイルがあったらレイヤーを変更
                    if (mapCreator.GetGimmick(mapCreator.Map.gimmickDate[y].mapNum[x]).GimmickObj != null && mapCreator.GetGimmick(mapCreator.Map.gimmickDate[y].mapNum[x]).GimmickObj.GetComponent<GimmickInfo>() != null)
                    {
                        if (mapCreator.GetGimmick(mapCreator.Map.gimmickDate[y].mapNum[x]).GimmickObj.GetComponent<GimmickInfo>().type == GimmickInfo.GimmickType.LADDER)
                        {
                            Debug.Log("レイヤー変更");
                            tileObj.layer = LayerMask.NameToLayer("LadderBrock");
                        }
                    }
                    tileObj.transform.parent = rootObj.transform;
                    tileObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                }
                if (mapCreator.Map.gimmickDate[y].mapNum[x] != 0)
                {
                    var gimmickObj = Instantiate(mapCreator.GetGimmick(mapCreator.Map.gimmickDate[y].mapNum[x]).GimmickObj);
                    gimmickObj.transform.parent = rootObj.transform;
                    gimmickObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                }
                if (mapCreator.Map.enemyDate[y].mapNum[x] != 0)
                {
                    var enemyObj = Instantiate(mapCreator.GetEnemy(mapCreator.Map.enemyDate[y].mapNum[x]).EnemyObj);
                    enemyObj.transform.parent = rootObj.transform;
                    enemyObj.transform.position = startPos + new Vector2(tileSize * x, -tileSize * y);
                }
            }
        }
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
