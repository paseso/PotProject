using UnityEngine;

public class BringCollider : MonoBehaviour {

    private MoveController move_ctr;
    private PlayerController player_ctr;
    //範囲内に入ったアイテムオブジェクト
    private GameObject target;
    private bool _setTarget = false;
    private AnimController anim_ctr;

    private MoveController.Direction direction;

    // Use this for initialization
    void Start ()
    {
        _setTarget = false;
        move_ctr = gameObject.transform.parent.GetComponentInChildren<MoveController>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        anim_ctr = gameObject.transform.parent.GetComponent<AnimController>();
        direction = move_ctr.direc;
    }
	
	// Update is called once per frame
	void Update () {
        MoveCollider();
	}

    /// <summary>
    /// 持つ範囲コライダーを左右に合わせて移動
    /// </summary>
    private void MoveCollider()
    {
        if (direction == move_ctr.direc)
            return;
        if (move_ctr.direc == MoveController.Direction.LEFT)
        {
            gameObject.transform.localPosition = new Vector2(0, gameObject.transform.localPosition.y);
            direction = MoveController.Direction.LEFT;
        }else
        {
            gameObject.transform.localPosition = new Vector2(2.6f, gameObject.transform.localPosition.y);
            direction = MoveController.Direction.RIGHT;
        }
    }

    /// <summary>
    /// 持てる範囲で□ボタンを押した時の処理
    /// </summary>
    public void SquereButton()
    {
        if (!_setTarget)
            return;
        if (player_ctr.getItemList().Count >= 3)
        {
            //UIアニメーション
            Debug.Log("マックス");
            return;
        }
        //アイテムを拾う
        //プレイヤーの操作制限
        player_ctr.EventFlag = true;
        target.GetComponent<Animator>().enabled = false;
        player_ctr.setItemList(target.GetComponent<ItemManager>().getItemStatus());
        GameObject vec = target;
        anim_ctr.setItemtaget = vec;
        gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if(move_ctr.direc == MoveController.Direction.LEFT)
        {
            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFT_GETITEM);
        }
        else
        {
            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHT_GETITEM);
        }
        target = null;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //アイテムの子供にあるUIを表示
        if (col.gameObject.tag == "Item")
        {
            target = col.gameObject;
            col.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            _setTarget = true;
            //アイテム拾うアニメーション中は×UIを非表示
            if(anim_ctr.animstate.animtype == AnimController.AnimState.AnimType.LEFT_GETITEM || 
                anim_ctr.animstate.animtype == AnimController.AnimState.AnimType.RIGHT_GETITEM)
            {
                col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                col.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                return;
            }
            //アイテムボックスが最大数だった時
            if (player_ctr.getItemList().Count >= 3)
            {
                //バツUIを出す処理
                if (!col.gameObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf)
                    col.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                //バツUIを消す処理
                if (col.gameObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf)
                    col.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //アイテムの子供にあるUIを非表示
        if (col.gameObject.tag == "Item")
        {
            target = null;
            col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            _setTarget = false;
        }
    }
}
