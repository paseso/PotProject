using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    [SerializeField]
    private List<SpriteRenderer> Maps = new List<SpriteRenderer>();

    [SerializeField]
    private GameObject firstPos;

    [SerializeField]
    private Canvas canvas;

    Vector3 mouse;
    Vector3 pos;

    private float sizeX;
    private float sizeY;

    public void StageGridCreate()
    {

    }

    // Use this for initialization
    void Start () {
        Vector3 spriteSize = firstPos.GetComponent<SpriteRenderer>().size / 2;
        for (int i = 0; i < Maps.Count; i++)
        {
            pos = new Vector3(firstPos.transform.position.x + spriteSize.x * i,
                              firstPos.transform.position.y + spriteSize.y * i * -1,0);

            Maps[i].transform.position = pos;
            Debug.Log(Camera.main.ScreenToWorldPoint(firstPos.transform.position));
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
