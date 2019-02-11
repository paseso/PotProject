using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCol : MonoBehaviour {
    private GameObject player;
    //雲にのってるかどうかの判定
    private bool _landingCloud = false;

    public bool getLandingCloud
    {
        get { return _landingCloud; }
    }

    private void Start()
    {
        _landingCloud = false;
    }

    void SetColParent(GameObject player) {
        player.transform.SetParent(transform);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Leg")
        {
            player = col.transform.parent.transform.parent.gameObject;
            player.transform.SetParent(transform);
            _landingCloud = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Leg") {
            GameObject player = col.transform.parent.transform.parent.gameObject;
            player.transform.SetParent(transform.root.transform);
            _landingCloud = false;
        }
    }
}
