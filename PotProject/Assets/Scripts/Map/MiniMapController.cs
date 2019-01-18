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
    private PlayerStatus status;
    private PlayerController player_ctr;
    private CameraManager cManager;
    private StageController sController;

    private bool isMiniMap;
    public bool getIsMiniMap
    {
        get { return isMiniMap; }
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
        cManager = GameObject.Find("Controller").gameObject.GetComponent<CameraManager>();
        sController = GameObject.Find("Controller").gameObject.GetComponent<StageController>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

    public void NowMap() {
        miniMaps[numY][numX].color = Color.white;
        mInfo = player.transform.root.gameObject.GetComponent<MapInfo>();
        numX = mInfo.MapNumX;
        numY = mInfo.MapNumY;
        miniMaps[mInfo.MapNumY][mInfo.MapNumX].color = Color.yellow;
    }

    /// <summary>
    /// ミニマップを開く処理
    /// </summary>
    public void ActiveMiniMap()
    {
        PlayerStatus tempStatus = new PlayerStatus();
        if (status.event_state != PlayerStatus.EventState.MINIMAP)
        {
            player_ctr.IsCommandActive = false;
            isMiniMap = true;
            tempStatus = status;
            status.event_state = PlayerStatus.EventState.MINIMAP;
            cManager.SwitchingCameraSub(sController.GetMaps[1][1].transform.localPosition, 70);
        }
        else
        {
            cManager.SwitchingCameraMain();
            status.event_state = tempStatus.event_state;
            isMiniMap = false;
            player_ctr.IsCommandActive = true;
            status.event_state = PlayerStatus.EventState.NORMAL;
        }
    }
}
