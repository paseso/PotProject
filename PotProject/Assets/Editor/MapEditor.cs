﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public class MapEditor : EditorWindow {

    //  参考サイト   https://qiita.com/shirasaya0201/items/ee32f35ad3caac428368

    private const string ASSET_PATH = "Assets/Resources/MapData/";
    private const int gridNum = 20;
    private Tile[] tiles;
    private Gimmick[] gimmicks;
    private Enemy[] enemies;
    private Sprite[] bgImages;
    private int toolberInt;
    private int[,] mapData;
    private int[,] gimmickData;
    private int[,] enemyData;
    private Rect[,] gridRect;
    private Rect rect;
    private Rect firstRect;
    private Rect lastRect;
    private Vector2 scrollPos = Vector2.zero;

    //  選択中のボタンの種類
    private int SelectNum = 0;

    private enum BackGround
    {
        NONE = 0,
        NORMAL,
        ICE,
        THUNDER,
        EMPTY1,
        EMPTY2
    };

    private BackGround backGround;
    

    [MenuItem("Editor/MapEditor")]
    private static void Open()
    {
        //  生成
        MapEditor window = GetWindow<MapEditor>("MapEditor");
        //  最小サイズ設定
        window.minSize = new Vector2(320, 500);
        window.maxSize = new Vector2(320, 500);
        //  マップクリエイター側からデータを拾う
        window.Init();
    }

    public void Init()
    {
        MapCreator mapCreator = FindObjectOfType<MapCreator>();
        tiles = mapCreator.GetTiles();
        gimmicks = mapCreator.GetGimmicks();
        enemies = mapCreator.GetEnemies();
        bgImages = mapCreator.GetBackImages();
        mapData = new int[gridNum, gridNum];
        gimmickData = new int[gridNum, gridNum];
        enemyData = new int[gridNum, gridNum];
        gridRect = new Rect[gridNum, gridNum];
        //gridColor = Color.white;
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        Open();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        //  マップデータを書き出し
        if (GUILayout.Button("ファイル出力")) { ExportMapData(); }
        //  マップの読み込み
        if (GUILayout.Button("ファイル入力")) { ImportMapData(); }
        //  グリッドをリセットをする
        if (GUILayout.Button("リセット")) { ResetMapData(); }
        GUILayout.EndHorizontal();

        //  グリッドのサイズを設定
        gridRect = CreateGrid();
        DrawBackGround();
        //  グリッドの描画
        DrawGridAll();

        // クリックされた場所の配列にデータを追加
        SetMapDate();

        // グリッドに画像を描画する
        DrawTileTextures();
        //  グリッド分の空白を挿入
        GUILayout.Space(280);
        //  背景の属性設定
        backGround = (BackGround)EditorGUILayout.EnumPopup("背景", backGround);
        //  タイルの種類を分ける
        toolberInt = GUILayout.Toolbar(toolberInt, new string[] { "地面", "ギミック", "エネミー" });
        //  タイルのボタンを描画
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        DrawTileButtons();
        EditorGUILayout.EndScrollView();
    }

    //  データを配列に格納
    private void SetMapDate()
    {
        Event e = Event.current;
        if (e.type == EventType.MouseDown || e.type == EventType.MouseDrag)
        {
            //  クリックされた場所
            Vector2 pos = Event.current.mousePosition;
            //  グリッド範囲内か、そうでないか
            bool isWithin = true;
            //  範囲外だったら
            if ((pos.x <= gridRect[0, 0].x) || (pos.x >= gridRect[0, gridNum - 1].xMax) || (pos.y <= gridRect[0, 0].y) || (pos.y >= gridRect[gridNum - 1, 0].yMax))
            {
                isWithin = false;
            }
            //  範囲内だったら配列にデータを設定
            if (isWithin)
            {
                int xx;
                // x位置を先に計算して、計算回数を減らす
                for (xx = 0; xx < gridNum; xx++)
                {
                    Rect r = gridRect[0, xx];
                    if (r.x <= pos.x && pos.x <= r.x + r.width)
                    {
                        break;
                    }
                }
                // 後はy位置だけ探す
                for (int yy = 0; yy < gridNum; yy++)
                {
                    Rect r = gridRect[yy, 0];
                    if (r.y <= pos.y && pos.y <= r.y + r.height)
                    {
                        //  配列に代入
                        //  条件によって代入する配列を変化
                        switch (toolberInt)
                        {
                            case 0:
                                mapData[yy, xx] = SelectNum;
                                break;
                            case 1:
                                if (SelectNum < gimmicks.Length)
                                {
                                    gimmickData[yy, xx] = SelectNum;
                                }                                
                                break;
                            case 2:
                                if (SelectNum < enemies.Length)
                                {
                                    enemyData[yy, xx] = SelectNum;
                                }
                                break;
                            default:
                                Debug.LogError("SetMapDateでエラー");
                                break;
                        }
                        Repaint();
                        break;
                    }
                }
            }
        }
    }

    //  ScriptableObjectにデータの書き出し
    private void ExportMapData()
    {
        // 保存先のファイルパスを取得する
        var fullPath = EditorUtility.SaveFilePanel("マップデータの保存", ASSET_PATH, "default_name", "asset");

        // パスが入っていれば選択されたということ（キャンセルされたら入ってこない）
        if (!string.IsNullOrEmpty(fullPath))
        {
            //  フルパスから相対パスへ
            string path = "Assets" + fullPath.Substring(Application.dataPath.Length);
            //  保存処理
            MapData tmp = ScriptableObject.CreateInstance<MapData>();
            //  配列を動的に確保　※そうしないと確保されない
            for (int i = 0; i < tmp.mapDate.Length; i++)
            {
                tmp.mapDate[i] = new MapArray();
                tmp.mapDate[i].mapNum = new int[gridNum];
                tmp.gimmickDate[i] = new MapArray();
                tmp.gimmickDate[i].mapNum = new int[gridNum];
                tmp.enemyDate[i] = new MapArray();
                tmp.enemyDate[i].mapNum = new int[gridNum];
            }
            //  値の代入
            //  背景の情報の代入
            if (backGround == BackGround.NONE)
            {
                #if UNITY_EDITOR
                EditorUtility.DisplayDialog("Error", "背景が設定されていません", "OK");
                #endif
                return;
            }
            tmp.backGroundNum = (int)backGround;
            //  タイルの情報の代入
            for (int i = 0; i < tmp.mapDate.Length; i++)
            {
                for (int j = 0; j < tmp.mapDate[i].mapNum.Length; j++)
                {
                    tmp.mapDate[i].mapNum[j] = mapData[i, j];
                    tmp.gimmickDate[i].mapNum[j] = gimmickData[i, j];
                    tmp.enemyDate[i].mapNum[j] = enemyData[i, j];
                }
            }
            AssetDatabase.CreateAsset(tmp, path);
            //  インスペクターから設定できないようにする
            tmp.hideFlags = HideFlags.NotEditable;
            //  更新通知
            EditorUtility.SetDirty(tmp);
            //  保存
            AssetDatabase.SaveAssets();
            //  エディタを最新の状態にする
            AssetDatabase.Refresh();
        }
    }

    //  ScriptableObjectのマップデータの読み込み
    private void ImportMapData()
    {
        string fullPath = EditorUtility.OpenFilePanel("マップデータの選択", ASSET_PATH, "asset");
        // パスが入っていれば選択されたということ（キャンセルされたら入ってこない）
        if (!string.IsNullOrEmpty(fullPath))
        {
            //  フルパスから相対パスへ
            string path = "Assets" + fullPath.Substring(Application.dataPath.Length);
            //  パスからデータを取得
            MapData data = AssetDatabase.LoadAssetAtPath<MapData>(path);

            if (data == null)
            {
                Debug.Log("Dataの読み込みエラー");
            }

            //  値の代入
            for (int i = 0; i < data.mapDate.Length; i++)
            {
                for (int j = 0; j < data.mapDate.Length; j++)
                {
                    mapData[i,j] = data.mapDate[i].mapNum[j];
                    gimmickData[i,j] = data.gimmickDate[i].mapNum[j];
                    enemyData[i,j] = data.enemyDate[i].mapNum[j];
                }
            }
            backGround = (BackGround)data.backGroundNum;
        }
    }

    //  グリッドの初期化
    private void ResetMapData()
    {
        mapData = new int[gridNum, gridNum];
        gimmickData = new int[gridNum, gridNum];
        enemyData = new int[gridNum, gridNum];
        backGround = BackGround.NONE;
    }

    //  グリッドのサイズを設定
    private Rect[,] CreateGrid()
    {
        //  EditorGUILayout.GetControlRectを呼ぶとSpace()が勝手に入るようでfor文で回したときバグの原因だった
        rect = EditorGUILayout.GetControlRect();
        float edgeX = 20f;
        float tmpX = edgeX;
        float maxGridSize = rect.xMax - edgeX * 2;
        float gridSize = maxGridSize / gridNum;
        float y = rect.y + 10;
        Rect[,] resultRects = new Rect[gridNum, gridNum];
        for (int yy = 0; yy < gridNum; yy++)
        {
            edgeX = tmpX;
            for(int xx = 0; xx < gridNum; xx++)
            {
                Rect r = new Rect(new Vector2(edgeX, y), new Vector2(gridSize, gridSize));
                resultRects[yy, xx] = r;
                edgeX += gridSize;
            }
            y += gridSize;
        }
        //  最初と最後のRectを保存する
        firstRect = resultRects[0, 0];
        lastRect = resultRects[gridNum - 1, gridNum - 1];
        //  Rect[]を返す
        return resultRects;
    }

    private void DrawBackGround()
    {
        switch (backGround)
        {
            case BackGround.NONE:
                break;
            case BackGround.NORMAL:
                GUI.DrawTexture(new Rect(new Vector2(firstRect.x, firstRect.y), new Vector2(lastRect.xMax - firstRect.x, lastRect.yMax - firstRect.y)), bgImages[0].texture);
                break;
            case BackGround.ICE:
                GUI.DrawTexture(new Rect(new Vector2(firstRect.x, firstRect.y), new Vector2(lastRect.xMax - firstRect.x, lastRect.yMax - firstRect.y)), bgImages[1].texture);
                break;
            case BackGround.THUNDER:
                GUI.DrawTexture(new Rect(new Vector2(firstRect.x, firstRect.y), new Vector2(lastRect.xMax - firstRect.x, lastRect.yMax - firstRect.y)), bgImages[2].texture);
                break;
            case BackGround.EMPTY1:
                GUI.DrawTexture(new Rect(new Vector2(firstRect.x, firstRect.y), new Vector2(lastRect.xMax - firstRect.x, lastRect.yMax - firstRect.y)), bgImages[3].texture);
                break;
            case BackGround.EMPTY2:
                GUI.DrawTexture(new Rect(new Vector2(firstRect.x, firstRect.y), new Vector2(lastRect.xMax - firstRect.x, lastRect.yMax - firstRect.y)), bgImages[4].texture);
                break;
            default:
                break;
        }
    }

    //  全てのマス目のグリッドの表示
    private void DrawGridAll()
    {
        for (int y = 0; y < gridNum; y++)
        {
            for (int x = 0; x < gridNum; x++)
            {
                DrawGrig(gridRect[y, x]);
            }
        }
    }

    //  Rect通りにグリッドを表示
    private void DrawGrig(Rect _rect)
    {
        //  グリッドの色を設定
        Handles.color = (toolberInt == 0) ? new Color(1,1,1,0.25f) : (toolberInt == 1) ? new Color(1, 0, 0, 0.25f) : new Color(0,1,0,0.25f);
        // upper line
        Handles.DrawLine(
            new Vector2(_rect.position.x, _rect.position.y),
            new Vector2(_rect.position.x + _rect.size.x, _rect.position.y));

        // bottom line
        Handles.DrawLine(
            new Vector2(_rect.position.x, _rect.position.y + _rect.size.y),
            new Vector2(_rect.position.x + _rect.size.x, _rect.position.y + _rect.size.y));

        // left line
        Handles.DrawLine(
            new Vector2(_rect.position.x, _rect.position.y),
            new Vector2(_rect.position.x, _rect.position.y + _rect.size.y));

        // right line
        Handles.DrawLine(
            new Vector2(_rect.position.x + _rect.size.x, _rect.position.y),
            new Vector2(_rect.position.x + _rect.size.x, _rect.position.y + _rect.size.y));
    }

    private void DrawTileTextures()
    {
        for (int y = 0; y < gridNum; y++)
        {
            for (int x = 0; x < gridNum; x++)
            {
                switch (toolberInt)
                {
                    case 0:
                        //  ノーマルタイルだけ表示
                        if (mapData[y, x] != 0) { GUI.DrawTexture(gridRect[y, x], tiles[mapData[y, x]].TileImage); }
                        break;
                    case 1:
                        //  ノーマルタイルとギミックタイルを表示
                        //  GUI.DrawTextureではアルファ値がいじれなかったので、こちらはGraphics.DrawTextureを使用
                        if (mapData[y, x] != 0) { Graphics.DrawTexture(gridRect[y, x], tiles[mapData[y, x]].TileImage, new Rect(0, 0, 1, 1), 0, 0, 0, 0, new Color(0.5f, 0.5f, 0.5f, 0.25f)); }
                        if (gimmickData[y, x] != 0) { GUI.DrawTexture(gridRect[y, x], gimmicks[gimmickData[y, x]].GimmickImage); }
                        break;
                    case 2:
                        //  ノーマルタイルとポジションタイルを表示
                        if (mapData[y, x] != 0) { Graphics.DrawTexture(gridRect[y, x], tiles[mapData[y, x]].TileImage, new Rect(0, 0, 1, 1), 0, 0, 0, 0, new Color(0.5f, 0.5f, 0.5f, 0.25f)); }
                        if (enemyData[y, x] != 0) { Graphics.DrawTexture(gridRect[y, x], enemies[enemyData[y, x]].EnemyImage, new Rect(0, 0, 1, 1), 0, 0, 0, 0, new Color(0.5f, 0.5f, 0.5f, 0.25f)); }
                        break;
                    default:
                        Debug.Log("ツールバーの値が不正です");
                        break;
                }
            }
        }
    }

    private void DrawTileButtons()
    {
        float x = 20.0f;
        float y = 00.0f;
        float w = 50.0f;
        float h = 50.0f;
        float maxW = 300.0f;
        GUILayout.Space(5);

        switch (toolberInt)
        {
            case 0:
                if (tiles.Length > 0)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        if (x > maxW)
                        {
                            x = 20.0f;
                            y += h;
                            EditorGUILayout.EndHorizontal();
                        }
                        if (x == 20.0f)
                        {
                            EditorGUILayout.BeginHorizontal();
                        }
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button(tiles[i].TileImage, GUILayout.MaxWidth(w), GUILayout.MaxHeight(h), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))
                        {
                            //  0の場合は要素の削除・それ以外はタイルの描画用のID
                            SelectNum = i;
                        }
                        GUILayout.FlexibleSpace();
                        x += w;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                break;
            case 1:
                if (gimmicks.Length > 0)
                {
                    for (int i = 0; i < gimmicks.Length; i++)
                    {
                        if (x > maxW)
                        {
                            x = 20.0f;
                            y += h;
                            EditorGUILayout.EndHorizontal();
                        }
                        if (x == 20.0f)
                        {
                            EditorGUILayout.BeginHorizontal();
                        }
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button(gimmicks[i].GimmickImage, GUILayout.MaxWidth(w), GUILayout.MaxHeight(h), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))
                        {
                            //  0の場合は要素の削除・それ以外はタイルの描画用のID
                            SelectNum = i;
                        }
                        GUILayout.FlexibleSpace();
                        x += w;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                break;
            case 2:
                if (enemies.Length > 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (x > maxW)
                        {
                            x = 20.0f;
                            y += h;
                            EditorGUILayout.EndHorizontal();
                        }
                        if (x == 20.0f)
                        {
                            EditorGUILayout.BeginHorizontal();
                        }
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button(enemies[i].EnemyImage, GUILayout.MaxWidth(w), GUILayout.MaxHeight(h), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))
                        {
                            //  0の場合は要素の削除・それ以外はタイルの描画用のID
                            SelectNum = i;
                        }
                        GUILayout.FlexibleSpace();
                        x += w;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                break;
            default:
                Debug.Log("ツールバーの値が不正");
                break;
        }
    }
}
