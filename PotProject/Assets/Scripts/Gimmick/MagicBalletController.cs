using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBalletController : MonoBehaviour {
    private float time;
    private Vector2 pos;

    public Vector2 Pos
    {
        get { return pos; }
        set
        {
            pos = value;
            pos = new Vector2(Pos.x - transform.position.x, Pos.y - transform.position.y);
        }
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
            PlayerController pcon = FindObjectOfType<PlayerController>();
            pcon.HPDown(transform.GetComponent<MonsterController>().Status.GetAttack);
            Destroy(gameObject);
        }
        else if (col.GetComponent<BoxCollider2D>() && !col.GetComponent<BoxCollider2D>().isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
