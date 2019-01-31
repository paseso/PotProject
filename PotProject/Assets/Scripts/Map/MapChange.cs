using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChange : MonoBehaviour {
    private MiniMapController mMapController;
    private GameObject beforeMap;

    void Start()
    {
        mMapController = GameObject.Find("Canvas/MiniMap").GetComponent<MiniMapController>();
    }

    //public void OnTriggerEnter2D(Collider2D col)
    //{  
    //    if(col.name != "Leg") { return; }
    //    col.transform.parent.transform.parent.transform.SetParent(transform.root.gameObject.transform);
    //    mMapController.NowMap();
    //}

    public void OnTriggerStay2D(Collider2D col) {
        if (col.name != "Leg") { return; }
        col.transform.parent.transform.parent.transform.SetParent(transform.root.gameObject.transform);
        mMapController.NowMap();
    }

}
