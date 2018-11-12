using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow {

    //  参考サイト   https://qiita.com/shirasaya0201/items/ee32f35ad3caac428368 https://www.google.co.jp/search?q=unity+%E3%82%A8%E3%83%87%E3%82%A3%E3%82%BF%E3%83%BC&oq=unity%E3%80%80%E3%82%A8%E3%83%87%E3%82%A3%E3%82%BF%E3%83%BC&aqs=chrome..69i57.7637j0j1&sourceid=chrome&ie=UTF-8


    // 画像ディレクトリ
    private Object imgDirectory;
    // 仮画像のタイル画像データ
    private Texture2D tileSprite;
    // 選択した画像パス
    private string selectedImagePath;

    /// <summary>
    /// アセットパス
    /// </summary>
    private string ASSET_PATH = "Assets/Resources/MapData/";  //  ScriptableObjectSample.asset
    private string FileName = "aaa.asset";
    private int gridNum = 10;
    private Color gridColor = Color.white;
    private Rect[,] gridRect = new Rect[10,10];
    private ScriptableObjectSample _sample;
    private Rect rect;
    private float GRIDSIZE;
    

    [MenuItem("Editor/MapEditor")]
    private static void Create()
    {
        //  生成
        MapEditor window = GetWindow<MapEditor>("MapEditor");
        //  最小サイズ設定
        window.minSize = new Vector2(320, 360);
        window.maxSize = new Vector2(320, 500);
    }

    private void OnGUI()
    {
        //  インスタンス生成
        if (_sample == null)
            _sample = ScriptableObject.CreateInstance<ScriptableObjectSample>();

        //  グリッド以外のラベル表示
        //  マップデータを書き出し
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("書き込み"))
            Export();
        //_sample.SampleIntValue = EditorGUILayout.IntField("サンプル", _sample.SampleIntValue);
        GUILayout.EndHorizontal();


        //  マスの画像を取得する
        //  いまはこのクラスでやってるが後で別のクラスでやる予定
        GUILayout.BeginHorizontal();
        GUILayout.Label("Image Directory : ", GUILayout.Width(110));
        imgDirectory = EditorGUILayout.ObjectField(imgDirectory, typeof(UnityEngine.Object), true);
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("タイル画像");
        tileSprite = (Texture2D)EditorGUILayout.ObjectField(tileSprite, typeof(Texture2D), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("グリッドの色", GUILayout.Width(150));
        gridColor = EditorGUILayout.ColorField(gridColor);
        EditorGUILayout.EndHorizontal();

        rect = EditorGUILayout.BeginHorizontal();
        EditorGUILayout.EndHorizontal();

        gridRect = CreateGrid();

        for (int y = 0; y < gridNum; y++)
        {
            for (int x = 0; x < gridNum; x++)
            {
                DrawGrig(gridRect[y, x]);
            }
        }

        // クリックされた位置を探して、その場所に画像ナンバーを入れる
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
            //  trueのままだったら配列に格納
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
                        _sample.MapData[yy, xx] = 1;
                        Repaint();
                        break;
                    }
                }
            }            
        }

        // 画像を描画する
        for (int yy = 0; yy < gridNum; yy++)
        {
            for (int xx = 0; xx < gridNum; xx++)
            {
                if (_sample.MapData[yy, xx] != 0)
                {
                    GUI.DrawTexture(gridRect[yy, xx], tileSprite);
                }
            }
        }

        GUILayout.Space(280);

        //  グリッドをリセットをする
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("リセット"))
        {
            for (int xx = 0; xx < gridNum; xx++)
            {
                for (int yy = 0; yy < gridNum; yy++)
                {
                    _sample.MapData[yy, xx] = 0;
                }
            }
        }
        GUILayout.EndHorizontal();

        //  タイルの種類を分ける
        _sample.SampleIntValue = GUILayout.Toolbar(_sample.SampleIntValue, new string[] { "地面", "ギミック", "エネミー" });

        DrawImageParts();
    }



    private void Export()
    {
        string FILENAME = ASSET_PATH + FileName;
        //  新規の場合は作成
        if (!AssetDatabase.Contains(_sample as UnityEngine.Object))
        {
            AssetDatabase.CreateAsset(_sample, FILENAME);
        }
        //  インスペクターから設定できないようにする
        _sample.hideFlags = HideFlags.NotEditable;
        //  更新通知
        EditorUtility.SetDirty(_sample);
        //  保存
        AssetDatabase.SaveAssets();
        //  エディタを最新の状態にする
        AssetDatabase.Refresh();
    }

    //  グリッドのサイズを設定
    //  for分で回してバグの原因だった
    private Rect[,] CreateGrid()
    {
        rect = EditorGUILayout.GetControlRect();
        float edgeX = 20f;
        float tmpX = edgeX;
        float maxGridSize = rect.xMax - edgeX * 2;
        GRIDSIZE = maxGridSize;
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
        return resultRects;
    }

    //  Rect通りにグリッドを表示
    private void DrawGrig(Rect _rect)
    {
        //Debug.Log("Draw");
        // grid
        Handles.color = gridColor;

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

    private void DrawImageParts()
    {
        if (imgDirectory != null)
        {
            float x = 0.0f;

            float y = 00.0f;
            float w = 50.0f;
            float h = 50.0f;
            float maxW = 300.0f;

            string path = AssetDatabase.GetAssetPath(imgDirectory);
            string[] names = Directory.GetFiles(path, "*.png");
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            foreach (string d in names)
            {
                if (x > maxW)
                {
                    x = 0.0f;
                    y += h;
                    EditorGUILayout.EndHorizontal();
                }
                if (x == 0.0f)
                {
                    EditorGUILayout.BeginHorizontal();
                }
                //GUILayout.FlexibleSpace();
                Texture2D tex = (Texture2D)AssetDatabase.LoadAssetAtPath(d, typeof(Texture2D));
                if (GUILayout.Button(tex, GUILayout.MaxWidth(w), GUILayout.MaxHeight(h), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))
                {
                    selectedImagePath = d;
                }
                GUILayout.FlexibleSpace();
                x += w;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

}
