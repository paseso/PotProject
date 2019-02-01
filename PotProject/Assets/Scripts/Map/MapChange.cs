using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChange : MonoBehaviour {
    private MiniMapController mMapController;
    private GameObject beforeMap;
    private StageController sController;

    void Start()
    {
        mMapController = GameObject.Find("Canvas/MiniMap").GetComponent<MiniMapController>();
        sController = GameObject.Find("Controller").GetComponent<StageController>();
        mMapController.NowMap();
        Debug.Log(transform.root.GetComponent<MapInfo>().attribute);
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
            sController.ChangeFlag = 0;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.name != "Leg") { return; }
        sController.ChangeFlag = 2;
    }

}
