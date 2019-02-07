using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShoot : MonoBehaviour {
    private bool shootFlag = false;
    public Vector2 playerPos { get; set; }

    public bool ShootFlag {
        get { return shootFlag; }
        set {shootFlag = value;}
    }
    private float shootTime;

    void Update() {
        if (shootFlag) {
            shootTime += Time.deltaTime;
            if(shootTime > 3) {
                Shoot(playerPos);
                shootTime = 0;
            }
        }
    }

    public void Shoot(Vector2 pos)
    {
        GameObject magic = new GameObject();
        
        if (transform.parent.GetComponent<BossController>()) {
            magic = Instantiate(Resources.Load<GameObject>("Prefabs/FireMagic"));
            EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Fire, magic.transform.position, 2, magic, false);
        }
        else if (transform.parent.GetComponent<MonsterController>()) {
            magic = Instantiate(Resources.Load<GameObject>("Prefabs/ThunderMagic"));
            EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Fire, magic.transform.position, 2, magic, false);
        }

        magic.transform.SetParent(transform.parent.transform);
        magic.transform.localPosition = transform.localPosition;
        magic.GetComponent<MagicBalletController>().Pos = pos;
    }
}
