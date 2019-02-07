using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChange : MonoBehaviour {
    private MiniMapController mMapController;
    private GameObject beforeMap;
    private StageController sController;
    private PotController pot_ctr;

    private PotStatus.PotType pot_type;

    void Start()
    {
        mMapController = GameObject.Find("Canvas/MiniMap").GetComponent<MiniMapController>();
        sController = GameObject.Find("Controller").GetComponent<StageController>();
        mMapController.NowMap();
        pot_ctr = FindObjectOfType<PotController>();
        Debug.Log(transform.root.GetComponent<MapInfo>().attribute);
        AttributePot(transform.root.GetComponent<MapInfo>().attribute);
    }

    /// <summary>
    /// マップの属性によってツボの属性を変える処理
    /// </summary>
    private void AttributePot(MapInfo.Attribute attri)
    {
        switch (attri)
        {
            case MapInfo.Attribute.NORMAL:
                pot_type = PotStatus.PotType.Normal;
                break;
            case MapInfo.Attribute.ICE:
                pot_type = PotStatus.PotType.Ice;
                break;
            case MapInfo.Attribute.THUNDER:
                pot_type = PotStatus.PotType.Thunder;
                break;
            default:
                break;
        }
        pot_ctr.ChangePotType(pot_type);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name != "Leg") { return; }
        sController.ChangeFlag = 1;
    }

    public void OnTriggerStay2D(Collider2D col) {
        if (col.name != "Leg") { return; }
        if (sController.ChangeFlag == 2)
        {
            col.transform.parent.transform.parent.transform.SetParent(transform.root.gameObject.transform);
            mMapController.NowMap();
            AttributePot(transform.root.GetComponent<MapInfo>().attribute);
            sController.ChangeFlag = 0;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.name != "Leg") { return; }
        sController.ChangeFlag = 2;
    }

}
