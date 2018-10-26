using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    
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
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void CreateStage()
    {
        GameObject[] maps = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i + j >= 4) return;
                
                GameObject map = Instantiate(mapPrefab);
                map.transform.position = new Vector2(firstPos.position.x + (sizeX * j / 2), firstPos.position.y + (sizeY * -i / 2));
                maps[j] = map;
            }
        }
    }

    public void StageMove()
    {

    }
}
