using UnityEngine;

public class LegCollider : MonoBehaviour {

    private MoveController move_ctr;
    //足の部分にfloorがあったってるかどうか
    [HideInInspector]
    public bool _legFloor = false;

	// Use this for initialization
	void Start () {
        move_ctr = transform.parent.GetComponentInChildren<MoveController>();
        _legFloor = false;
	}
    
    private void OnTriggerStay2D(Collider2D col)
    {
        move_ctr._isJump = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        move_ctr._isJump = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "floor")
        {
            _legFloor = true;
        }
    }
}