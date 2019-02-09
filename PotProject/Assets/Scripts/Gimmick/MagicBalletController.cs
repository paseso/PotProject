using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBalletController : MonoBehaviour {
    private float time;
    private Vector2 pos;
    private BossStatus bStatus;
    private MonsterStatus mStatus;
    private float dirRadius = 1;
    private float shootSpeed = 5;

    private bool InRadiusCheck(Vector2 pos)
    {
        //Debug.Log("transform = " + transform.position);
        //Debug.Log("pos.x(" + pos.x + ")とtransform.position.x - dirRadius(" + (transform.position.x - dirRadius) + ")");
        //Debug.Log("pos.x(" + pos.x + ")とtransform.position.x + dirRadius(" + (transform.position.x + dirRadius) + ")");
        //Debug.Log("pos.y(" + pos.y + ")とtransform.position.y - dirRadius(" + (transform.position.y - dirRadius) + ")");
        //Debug.Log("pos.y(" + pos.y + ")とtransform.position.y - dirRadius(" + (transform.position.y + dirRadius) + ")");
        if (pos.x < transform.position.x - dirRadius || pos.x > transform.position.x + dirRadius)
        {
            Debug.Log("xが範囲外");
            return false;
        }

        if (pos.y < transform.position.y - dirRadius || pos.y > transform.position.y + dirRadius)
        {
            Debug.Log("yが範囲外");
            return false;
        }
        Debug.Log("範囲内");
        return true;
        
    }

    public Vector2 Pos
    {
        get { return pos; }
        set
        {
            pos = value;
            pos = new Vector2(Pos.x - transform.position.x, Pos.y - transform.position.y);
        }
    }

    public bool IsMove { get; set; }

    void Start() {
        if (GetComponent<BossController>()) {
            bStatus = GetComponent<BossController>().Status;
        }else if(GetComponent<MonsterController>()) {
            mStatus = GetComponent<MonsterController>().Status;
        }
        Debug.Log("AfterPos = " + pos);

    }
    // Update is called once per frame
    void Update()
    {
        if (!IsMove) { return; }
        GetComponent<Rigidbody2D>().velocity = pos;
        time += Time.deltaTime;
        if (time > 5)
        {
            Destroy(gameObject);
        }
    }

    //public void ShootDirCheck()
    //{
    //    Debug.Log("BeforePos = " + pos);
    //    if (InRadiusCheck(pos)) { return; }

    //    if (pos.x < transform.position.x - dirRadius)
    //    {
    //        Debug.Log("左");
    //        pos.x = transform.position.x - shootSpeed;
    //    }
    //    else if(pos.x > transform.position.x + dirRadius)
    //    {
    //        Debug.Log("右");
    //        pos.x = transform.position.x + shootSpeed;
    //    }
    //    if (pos.y < transform.position.y - dirRadius)
    //    {
    //        Debug.Log("下");
    //        pos.y = transform.position.y - shootSpeed;
    //    }
    //    else if (pos.y > transform.position.y + dirRadius)
    //    {
    //        Debug.Log("上");
    //        pos.y = transform.position.y + shootSpeed;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<MoveController>())
        {
            PlayerController pcon = GameObject.Find("Controller").GetComponent<PlayerController>();
            pcon.HPDown(transform.GetComponent<MonsterController>().Status.GetAttack);
            Destroy(gameObject);
        }
        else if (col.GetComponent<BoxCollider2D>() && !col.GetComponent<BoxCollider2D>().isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
