using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    
    public enum direction
    {
        up,
        down,
        left,
        right
    }

    private List<GameObject[]> Maps = new List<GameObject[]>();

    [SerializeField]
    private Transform firstPos;

    [SerializeField]
    private GameObject mapPrefab;

    private float sizeX;
    private float sizeY;

    // Use this for initialization
    void Start () {
        sizeX = mapPrefab.GetComponent<SpriteRenderer>().size.x;
        sizeY = mapPrefab.GetComponent<SpriteRenderer>().size.y;

        CreateStage();
        Debug.Log(Maps.Count);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Debug.Log("上");
            StageMove(0, direction.up);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)){
            Debug.Log("下");
            StageMove(0, direction.down);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            Debug.Log("右");
            StageMove(0, direction.right);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            Debug.Log("左");
            StageMove(0, direction.left);
        }

    }

    public void CreateStage()
    {
        int count = 0;
        GameObject[] maps = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                count++;
                
                GameObject map = Instantiate(mapPrefab);
                map.transform.position = new Vector2(firstPos.position.x + (sizeX * j / 2), firstPos.position.y + (sizeY * -i / 2));
                maps[j] = map;
                Maps.Add(maps);

                if(count % 2 == 0)
                map.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    public void StageMove(int num,direction dir)
    {
        
        switch (dir)
        {
            case direction.up: // 上
                for (int i = 0; i < 3; i++)
                {
                    Vector2 pos = Maps[i][num].transform.position;
                    pos = new Vector2(pos.x,pos.y + sizeY);
                    Maps[i][num].transform.position = pos;
                }
                break;
            case direction.down: // 下
                for (int i = 0; i < 3; i++)
                {
                    Vector2 pos = Maps[i][num].transform.position;
                    pos = new Vector2(pos.x, (pos.y + sizeY) * -1);
                    Maps[i][num].transform.position = pos;
                }
                break;
            case direction.right:// 右
                for (int i = 0; i < 3; i++)
                {
                    Vector2 pos = Maps[num][i].transform.position;
                    pos = new Vector2(pos.x + sizeX, pos.y);
                    Maps[num][i].transform.position = pos;
                }
                break;
            case direction.left: // 左
                for (int i = 0; i < 3; i++)
                {
                    Vector2 pos = Maps[num][i].transform.position;
                    pos = new Vector2((pos.x + sizeX) * -1, pos.y);
                    Maps[num][i].transform.position = pos;
                }
                break;
            default:
                break;
        }
    }
}
