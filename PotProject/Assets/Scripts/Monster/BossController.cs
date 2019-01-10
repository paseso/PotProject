using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BossStatus
{
    public int HP;
    public int ATTACK;
    public int INT;
    public int magicTime;
}

public class BossController : MonoBehaviour {
    [SerializeField]
    private BossStatus status;
    private float mTime;
    private bool isMagicAttack = false;

    public bool IsMagicAttack
    {
        set { isMagicAttack = value; }
    }

    private Vector2 playerPos;

    public Vector2 setPlayerPos
    {
        set { playerPos = value; }
    }
	
	// Update is called once per frame
	void Update () {
        if (isMagicAttack)
        {
            mTime += Time.deltaTime;
            if(mTime > status.magicTime)
            {
                // 魔法飛ばす処理
                GetComponentInChildren<MagicShoot>().Shoot(playerPos);
                mTime = 0;
            }
        }
	}

}
