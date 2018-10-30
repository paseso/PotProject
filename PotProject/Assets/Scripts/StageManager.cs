using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    
    /// <summary>
    /// スライドの方向
    /// </summary>
    public enum Direction
    {
        up,
        down,
        left,
        right
    }

    private List<List<GameObject>> Maps = new List<List<GameObject>>();

    [SerializeField]
    private Transform firstPos;

    [SerializeField]
    private GameObject mapPrefab;

    [SerializeField]
    private int stageLength;

    private float sizeX;
    private float sizeY;

    void Start () {
        // Spriteサイズ取得
        sizeX = mapPrefab.GetComponent<SpriteRenderer>().size.x;
        sizeY = mapPrefab.GetComponent<SpriteRenderer>().size.y;

        CreateStage();
        Debug.Log(Maps.Count);
	}
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Debug.Log("上");
            SrideStage(0, Direction.up);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)){
            Debug.Log("下");
            SrideStage(0, Direction.down);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            Debug.Log("右");
            SrideStage(1, Direction.right);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            Debug.Log("左");
            SrideStage(1, Direction.left);
        }

    }

    // ステージ生成(仮)
    public void CreateStage()
    {
        int count = 0;
        for(int i = 0; i < stageLength; i++)
        {
            List<GameObject> mapClones = new List<GameObject>();
            for (int j = 0; j < stageLength; j++)
            {
                count++;
                GameObject map = Instantiate(mapPrefab);
                map.name = ("Clone" + count);

                map.transform.position = new Vector2(firstPos.position.x + (sizeX * j / 2), firstPos.position.y + (sizeY * -i / 2));
                mapClones.Add(map);

                if(count % 2 == 0)
                map.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            Maps.Add(mapClones);
        }
    }

    /// <summary>
    /// ステージスライド処理
    /// </summary>
    /// <param name="num"></param>
    /// <param name="dir"></param>
    public void SrideStage(int num,Direction dir)
    {
        GameObject temp;
        Vector2 tempPos = new Vector2();
        switch (dir)
        {
            case Direction.up: // 上
                for (int i = 0; i < stageLength; i++)
                {
                    if (i == 0){
                        // 折り返し座標を保持
                        tempPos = Maps[stageLength - 1][num].transform.localPosition;
                        Maps[i][num].transform.localPosition = tempPos;
                        continue;
                    }

                    // スライド
                    Vector2 pos = Maps[i][num].transform.localPosition;
                    pos = new Vector2(pos.x,pos.y + sizeY / 2);
                    Maps[i][num].transform.localPosition = pos;
                }

                // スライド終了時の配列内入れ替え
                temp = Maps[0][num];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[i][num] = Maps[i + 1][num];
                }
                Maps[stageLength - 1][num] = temp;
                break;

            case Direction.down: // 下

                for (int i = 0; i < stageLength; i++)
                {
                    // 折り返し座標を保持
                    if (i == 0) tempPos = Maps[i][num].transform.localPosition;
                    else if (i == stageLength - 1)
                    {
                        Maps[i][num].transform.localPosition = tempPos;
                        continue;
                    };

                    // スライド
                    Vector2 pos = Maps[i][num].transform.localPosition;
                    pos = new Vector2(pos.x, (pos.y + (sizeY * -1) / 2));
                    Maps[i][num].transform.localPosition = pos;
                }

                // スライド終了時の配列内入れ替え
                temp = Maps[stageLength - 1][num];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[stageLength -1 -i][num] = Maps[stageLength -2 -i][num];
                }
                Maps[0][num] = temp;
                break;

            case Direction.right:// 右
                for (int i = 0; i < stageLength; i++)
                {
                    // 折り返し座標を保持
                    if (i == 0) tempPos = Maps[num][i].transform.localPosition;
                    else if(i == stageLength - 1)
                    {
                        Maps[num][i].transform.localPosition = tempPos;
                        continue;
                    }

                    // スライド
                    Vector2 pos = Maps[num][i].transform.localPosition;
                    pos = new Vector2((pos.x + sizeX / 2), pos.y);
                    Maps[num][i].transform.localPosition = pos;
                }

                // スライド終了時の配列内入れ替え
                temp = Maps[num][stageLength - 1];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[num][stageLength - 1 - i] = Maps[num][stageLength - 2 - i];
                }
                Maps[num][0] = temp;
                break;

            case Direction.left: // 左
                for (int i = 0; i < stageLength; i++)
                {
                    if (i == 0)
                    {
                        // 折り返し座標を保持
                        tempPos = Maps[num][stageLength - 1].transform.localPosition;
                        Maps[num][0].transform.localPosition = tempPos;
                        continue;
                    }

                    // スライド
                    Vector2 pos = Maps[num][i].transform.localPosition;
                    pos = new Vector2((pos.x + (sizeX * -1) / 2), pos.y);
                    Maps[num][i].transform.localPosition = pos;
                }

                // スライド終了時の配列内入れ替え
                temp = Maps[num][0];
                for (int i = 0; i < stageLength - 1; i++)
                {
                    Maps[num][i] = Maps[num][i + 1];
                }
                Maps[num][stageLength - 1] = temp;
                break;
            default:
                break;
        }
    }
}
