using UnityEngine;

public class BringCollider : MonoBehaviour {

    [HideInInspector]
    public bool _Brotherhit = false;
    [HideInInspector]
    public bool _Tubohit = false;
    [HideInInspector]
    public bool _bring = false;
    private MoveController move_controll;
    private PotController pot_ctr;

    private enum Direction
    {
        RIGHT = 0,
        LEFT
    }
    private Direction direction;

    // Use this for initialization
    void Start ()
    {
        _Brotherhit = false;
        _Tubohit = false;
        _bring = false;
        move_controll = gameObject.transform.parent.GetComponentInChildren<MoveController>();
        pot_ctr = GameObject.FindObjectOfType<PotController>();
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
        if (move_controll.OnLeft)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            direction = Direction.LEFT;
        }
        if (move_controll.OnRight)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = Direction.RIGHT;
        }
    }

    /// <summary>
    /// 持てる範囲で□ボタンを押した時の処理
    /// </summary>
    public void SquereButton(Transform pos)
    {
        //アイテムを拾う
        pot_ctr.AddItem(pos.GetComponent<ItemManager>().getItemStatus());
        Destroy(pos.gameObject);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //アイテムの子供にあるUIを表示
        if (col.gameObject.tag == "Item")
        {
            col.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //アイテムの子供にあるUIを非表示
        if (col.gameObject.tag == "Item")
        {
            col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
