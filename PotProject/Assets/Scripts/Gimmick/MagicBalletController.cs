using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBalletController : MonoBehaviour {
    private float time;
    private Vector2 pos;
    private BossStatus bStatus;
    private MonsterStatus mStatus;

    public Vector2 Pos
    {
        get { return pos; }
        set
        {
            pos = value;
            pos = new Vector2(Pos.x - transform.position.x, Pos.y - transform.position.y);
        }
    }

    void Start() {
        if (GetComponent<BossController>()) {
            bStatus = GetComponent<BossController>().Status;
        }else if(GetComponent<MonsterController>()) {
            mStatus = GetComponent<MonsterController>().Status;
        }
        
        //GetComponent<MonsterStatus>().attribute = MonsterStatus.MonsterAttribute.THUNDER;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Pos;
        time += Time.deltaTime;
        if (time > 5)
        {
            Destroy(gameObject);
        }
    }

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
