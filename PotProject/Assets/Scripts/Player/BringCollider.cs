using UnityEngine;

public class BringCollider : MonoBehaviour {

    private MoveController move_ctr;
    private PotController pot_ctr;
    private PlayerController player_ctr;
    //範囲内に入ったアイテムオブジェクト
    private GameObject target;
    private bool _setTarget = false;
    private AnimController anim_ctr;

    public bool getSetTarget
    {
        get { return _setTarget; }
    }

    private enum Direction
    {
        RIGHT = 0,
        LEFT
    }
    private Direction direction;

    // Use this for initialization
    void Start ()
    {
        _setTarget = false;
        move_ctr = gameObject.transform.parent.GetComponentInChildren<MoveController>();
        pot_ctr = GameObject.FindObjectOfType<PotController>();
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        anim_ctr = gameObject.transform.parent.GetComponent<AnimController>();
        direction = Direction.LEFT;
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
        if (move_ctr.OnLeft)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            direction = Direction.LEFT;
        }
        if (move_ctr.OnRight)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = Direction.RIGHT;
        }
    }

    /// <summary>
    /// 持てる範囲で□ボタンを押した時の処理
    /// </summary>
    public void SquereButton()
    {
        if (!_setTarget)
            return;
        Debug.Log("きてる");
        //アイテムを拾う
        //pot_ctr.AddItem(target.GetComponent<CreateItemManager>().getStatus());
        player_ctr.setItemList(target.GetComponent<ItemStatus.Type>());
        Vector2 vec = new Vector2(target.transform.position.x, target.transform.position.y);
        anim_ctr.setItemtaget = vec;
        if(move_ctr.direc == MoveController.Direction.LEFT)
        {
            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.LEFT_GETITEM);
        }
        else
        {
            anim_ctr.ChangeAnimatorState(AnimController.AnimState.AnimType.RIGHT_GETITEM);
        }
        Destroy(target.gameObject);
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
