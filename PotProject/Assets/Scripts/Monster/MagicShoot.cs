using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShoot : MonoBehaviour {

    public void Shoot(Vector2 pos)
    {
        GameObject magic = Instantiate(Resources.Load<GameObject>("Prefabs/MagicPrefab"));
        magic.transform.SetParent(transform.parent.transform);
        magic.transform.localPosition = transform.localPosition;
        magic.GetComponent<Rigidbody2D>().velocity = pos;
    }
}
