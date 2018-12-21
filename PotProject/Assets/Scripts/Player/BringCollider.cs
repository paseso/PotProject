using UnityEngine;

public class BringCollider : MonoBehaviour {

    [HideInInspector]
    public bool _Brotherhit = false;
    [HideInInspector]
    public bool _Tubohit = false;
    [HideInInspector]
    public bool _bring = false;
    private MoveController move_controll;
    private GameObject Joints;

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
        if (move_controll.OnLeft) // || direction == Direction.RIGHT
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            //Joints.transform.rotation = Quaternion.Euler(0, 0, 0);
            direction = Direction.LEFT;
        }
        if (move_controll.OnRight) // || direction == Direction.LEFT
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = Direction.RIGHT;
        }
    }

    /// <summary>
    /// 持てる範囲で□ボタンを押した時の処理
    /// </summary>
    private void SquereButton(Transform pos)
    {
        if (_Brotherhit && !_bring)
        {
            pos.position = new Vector2(gameObject.transform.transform.position.x, gameObject.transform.transform.position.y + 5f);
            _bring = true;
        }
        else
        {
            pos.transform.parent = null;
            if(move_controll.OnRight)
                pos.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y);
            else
                pos.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y);

            _bring = false;
            _Brotherhit = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Item")
        {
            if (!_bring)
            {
                move_controll.target = col.gameObject;
                _Brotherhit = true;
            }
        }

        //壺に当たってたらアイテムを入れれる合図を出す（UI表示）
        if (col.gameObject.tag == "Tubo")
        {
            _Tubohit = true;
            col.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Tubo")
        {
            _Tubohit = false;
            col.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
