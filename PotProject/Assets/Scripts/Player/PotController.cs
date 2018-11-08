using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour {

    [SerializeField]
    private PlayerManager manager;
    [SerializeField]
    private BringCollider bring_col;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ototo")
        {
            if (bring_col._Tubohit)
            {
                Destroy(col.gameObject);
                //col.gameObject.SetActive(false);
                manager.ItemAlchemy(ItemStatus.ITEM.SLIME);
            }
        }

    }
}
