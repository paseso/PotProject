﻿using System.Collections;
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
        if (transform.parent.GetComponent<BossController>()) {
            GameObject magic_F = Instantiate(Resources.Load<GameObject>("Prefabs/FireMagic"));
            magic_F.transform.SetParent(transform.parent.transform);
            magic_F.transform.localPosition = transform.localPosition;
            magic_F.transform.SetParent(transform.root.transform);
            magic_F.GetComponent<MagicBalletController>().Pos = pos;
            //magic_F.GetComponent<MagicBalletController>().ShootDirCheck();
            EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Fire, magic_F.transform.position, 2, magic_F, false);
            magic_F.GetComponent<MagicBalletController>().IsMove = true;
        }
        else if (transform.parent.GetComponent<MonsterController>()) {
            GameObject magic_T = Instantiate(Resources.Load<GameObject>("Prefabs/ThunderMagic"));
            magic_T.transform.SetParent(transform.parent.transform);
            magic_T.transform.localPosition = transform.localPosition;
            magic_T.transform.SetParent(transform.root.transform);
            magic_T.GetComponent<MagicBalletController>().Pos = pos;
            //magic_T.GetComponent<MagicBalletController>().ShootDirCheck();
            EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_Thunder, magic_T.transform.position, 2, magic_T, false);
            magic_T.GetComponent<MagicBalletController>().IsMove = true;
        }
    }
}
