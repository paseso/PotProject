using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtest : MonoBehaviour {

    [SerializeField]
    GameObject player;

    public void setParent(GameObject parent)
    {
        GameObject broOld = Instantiate(player);
        broOld.transform.SetParent(parent.transform);
    }
}
