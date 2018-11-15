using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGimmick : MonoBehaviour {

    [SerializeField]
    private int ladderNum;
    private bool[] ladderFlag;
    private MapInfo info;

    void Awake()
    {
        info = transform.root.GetComponent<MapInfo>();
    }

    public bool[] LadderFlag
    {
        get { return ladderFlag; }
        private set { value = ladderFlag; }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        LadderFlag[ladderNum] = true;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        LadderFlag[ladderNum] = false;
    }
}
