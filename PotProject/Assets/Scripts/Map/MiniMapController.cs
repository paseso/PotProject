using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour {
    private MapInfo mInfo;
    private GameObject player;
    private int mapNum;
    private int numX, numY;
    private List<List<Image>> miniMaps = new List<List<Image>>();

    void Awake() {

    }

	// Use this for initialization
	void Start () {
        int count = 0;
        for (int i = 0; i < 3; i++) {
            List<Image> tempList = new List<Image>();
            for(int j = 0; j < 3; j++) {
                tempList.Add(transform.GetChild(count).GetComponent<Image>());
                count++;
            }
            miniMaps.Add(tempList);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        NowMap();
	}

    public void NowMap() {
        miniMaps[numY][numX].color = Color.white;
        mInfo = player.transform.root.gameObject.GetComponent<MapInfo>();
        numX = mInfo.MapNumX;
        numY = mInfo.MapNumY;
        miniMaps[mInfo.MapNumY][mInfo.MapNumX].color = Color.yellow;
    }
}
