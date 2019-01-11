using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChange : MonoBehaviour {
    private MiniMapController mMapController;

    void Start()
    {
        mMapController = FindObjectOfType<MiniMapController>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        col.transform.parent.transform.parent.transform.SetParent(transform.root.gameObject.transform);
        mMapController.NowMap();
    }

}
