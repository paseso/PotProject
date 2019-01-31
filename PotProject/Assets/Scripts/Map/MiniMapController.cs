using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour {
    private MapInfo mInfo;
    private GameObject player;
    private GameObject controller;
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
        status = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>().Status;
        for (int i = 0; i < 3; i++) {
            List<Image> tempList = new List<Image>();
            for(int j = 0; j < 3; j++) {
                tempList.Add(transform.GetChild(count).GetComponent<Image>());
                count++;
            }
            miniMaps.Add(tempList);
        }
        if (GameObject.Find("Controller")) {
            controller = GameObject.Find("Controller");
            cManager = controller.GetComponent<CameraManager>();
            sController = controller.gameObject.GetComponent<StageController>();
            player_ctr = controller.GetComponent<PlayerController>();
        }
        player = GameObject.FindGameObjectWithTag("Player");
	}

    /// <summary>
    /// 現在のマップに切り替え
    /// </summary>
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
        StartCoroutine(MiniMapCoroutine());
    }

    public IEnumerator MiniMapCoroutine() {
        PlayerStatus tempStatus = new PlayerStatus();
        if (status.event_state != PlayerStatus.EventState.MINIMAP) {
            player_ctr.AllCommandActive = false;
            isMiniMap = true;
            tempStatus = status;
            status.event_state = PlayerStatus.EventState.MINIMAP;
            cManager.SwitchingCameraSub(sController.GetMaps[1][1].transform.localPosition, 70);
            yield return new WaitForSeconds(1.2f);
            player_ctr.IsCommandActive = false;
            player_ctr.AllCommandActive = true;

        } else {
            player_ctr.AllCommandActive = false;
            cManager.SwitchingCameraMain();
            status.event_state = tempStatus.event_state;
            isMiniMap = false;
            status.event_state = PlayerStatus.EventState.NORMAL;
            yield return new WaitForSeconds(1.2f);
            player_ctr.IsCommandActive = true;
            player_ctr.AllCommandActive = true;

        }
    }
}
