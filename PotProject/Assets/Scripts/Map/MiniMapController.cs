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
    private Sprite[] mSprite = new Sprite[9];
    private PlayerStatus status;
    private PlayerController player_ctr;
    private CameraManager cManager;
    private StageController sController;

    private bool isMiniMap;
    public bool getIsMiniMap
    {
        get { return isMiniMap; }
    }

    public Sprite[] MSprite
    {
        get { return mSprite; }
        set { mSprite = value; }
    }

    // Use this for initialization
    void Start () {
        int count = 0;
        status = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>().Status;
        for (int i = 0; i < 3; i++) {
            List<Image> tempList = new List<Image>();
            for(int j = 0; j < 3; j++) {
                tempList.Add(transform.GetChild(count).GetComponent<Image>());
                transform.GetChild(count).GetComponent<Image>().sprite = mSprite[count];
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

    public void miniMapSride(int num,StageController.Direction dir)
    {
        Sprite temp = new Sprite();
        switch (dir)
        {
            case StageController.Direction.DOWN:
                temp = miniMaps[2][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite;
                for(int i = 2; i >= 0; i--)
                {
                    if(i == 0)
                    {
                        miniMaps[i][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite = temp;
                        continue;
                    }
                    miniMaps[i][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite = miniMaps[i - 1][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite;
                }
                
                break;
            case StageController.Direction.UP:
                temp = miniMaps[0][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite;
                for (int i = 0; i < 3; i++)
                {
                    if (i == 2)
                    {
                        miniMaps[0][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite = temp;
                        continue;
                    }
                    miniMaps[2 - i][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite = miniMaps[2 - 1 - i][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite;
                }
                break;
            case StageController.Direction.LEFT:
                temp = miniMaps[mInfo.MapNumY][0].GetComponent<SpriteRenderer>().sprite;
                for (int i = 0; i < 3; i++)
                {
                    if (i == 2)
                    {
                        miniMaps[0][mInfo.MapNumX].GetComponent<SpriteRenderer>().sprite = temp;
                        continue;
                    }
                    miniMaps[mInfo.MapNumX][2 - i].GetComponent<SpriteRenderer>().sprite = miniMaps[mInfo.MapNumX][2 - 1 - i].GetComponent<SpriteRenderer>().sprite;
                }
                break;
            case StageController.Direction.RIGHT:
                break;
            default:
                break;
        }
    }
}
