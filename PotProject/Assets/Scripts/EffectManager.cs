using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    [SerializeField]
    private GameObject Effect_PickUp;
    [SerializeField]
    private GameObject Effect_AttackIce;
    [SerializeField]
    private GameObject Effect_Damage;
    [SerializeField]
    private GameObject Effect_Heal;
    [SerializeField]
    private GameObject Effect_Respawn;
    [SerializeField]
    private GameObject Effect_RespawnPosition;
    [SerializeField]
    private GameObject Effect_HeartBurst;
    [SerializeField]
    private GameObject Effect_Fire;
    [SerializeField]
    private GameObject Effect_KeyDoor;
    [SerializeField]
    private GameObject Effect_Water;
    [SerializeField]
    private GameObject Effect_Grow;
    [SerializeField]
    private GameObject Effect_GetItem;
    [SerializeField]
    private GameObject Effect_CreateItemSuccece;
    [SerializeField]
    private GameObject Effect_Alchemy;
    [SerializeField]
    private GameObject Effect_Thunder;
    [SerializeField]
    private GameObject Effect_StarExplosive;
    [SerializeField]
    private GameObject Effect_Vajura;
    [SerializeField]
    private GameObject Effect_SwordAttack_0;
    [SerializeField]
    private GameObject Effect_SwordAttack_1;
    [SerializeField]
    private GameObject Effect_ThunderHit;
    [SerializeField]
    private GameObject Effect_RespornPlayer;
    [SerializeField]
    private GameObject Effect_RespornEnemy;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public enum EffectName
    {
        Effect_PickUp = 0,
        Effect_AttackIce,
        Effect_Damage,
        Effect_Heal,
        Effect_Respawn,
        Effect_RespawnPosition,
        Effect_HeartBurst,
        Effect_Fire,
        Effect_KeyDoor,
        Effect_Water,
        Effect_Grow,
        Effect_GetItem,
        Effect_CreateItemSuccece,
        Effect_Alchemy,
        Effect_Thunder,
        Effect_StarExplosive,
        Effect_Vajura,
        Effect_SwordAttack_0,
        Effect_SwordAttack_1,
        Effect_ThunderHit,
        Effect_RespornPlayer,
        Effect_RespornEnemy,
    };

    /// <summary>
    /// 座標指定してエフェクトを再生がおわったら破棄する
    /// </summary>
    /// <param name="PlayEffect"></param>
    /// <param name="EffectPos">Effectの生成場所</param>
    /// <param name="Magnification">Effectのスケール</param>
    /// <param name="Target">親子付けするObject</param>
    private void EffectProcess(GameObject PlayEffect, Vector2 EffectPos, float Magnification, GameObject Target, bool isDestry)
    {
        PlayEffect.transform.position = EffectPos;
        PlayEffect.transform.SetParent(Target.transform);
        PlayEffect.transform.localScale = new Vector3(Magnification, Magnification, Magnification);
        
        //  再生終わったら破棄するか
        if (isDestry)
        {
            ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
            var main = particlesystem.main;
            if (PlayEffect != null)
                Destroy(PlayEffect, main.duration);
        }
    }

    //実行する時はコレ↓
    //EffectManager.Instance_Effect.PlayEffect(0, new Vector2(0, 0), 1.0f, true);

    /// <summary>
    /// エフェクトナンバーを指定する方
    /// </summary>
    public GameObject PlayEffect(int EffectNum, Vector2 EffectPos, float Magnification, GameObject Target, bool isDestry)
    {
        GameObject PlayEffect = null;
        switch (EffectNum)
        {
            case 0:
                PlayEffect = Instantiate(Effect_PickUp) as GameObject;
                break;
            case 1:
                PlayEffect = Instantiate(Effect_AttackIce) as GameObject;
                break;
            case 2:
                PlayEffect = Instantiate(Effect_Damage) as GameObject;
                break;
            case 3:
                PlayEffect = Instantiate(Effect_Heal) as GameObject;
                break;
            case 4:
                PlayEffect = Instantiate(Effect_Respawn) as GameObject;
                break;
            case 5:
                PlayEffect = Instantiate(Effect_RespawnPosition) as GameObject;
                break;
            case 6:
                PlayEffect = Instantiate(Effect_HeartBurst) as GameObject;
                break;
            case 7:
                PlayEffect = Instantiate(Effect_Fire) as GameObject;
                break;
            case 8:
                PlayEffect = Instantiate(Effect_KeyDoor) as GameObject;
                break;
            case 9:
                PlayEffect = Instantiate(Effect_Water) as GameObject;
                break;
            case 10:
                PlayEffect = Instantiate(Effect_Grow) as GameObject;
                break;
            case 11:
                PlayEffect = Instantiate(Effect_GetItem) as GameObject;
                break;
            case 12:
                PlayEffect = Instantiate(Effect_CreateItemSuccece) as GameObject;
                break;
            case 13:
                PlayEffect = Instantiate(Effect_Alchemy) as GameObject;
                break;
            case 14:
                PlayEffect = Instantiate(Effect_Thunder) as GameObject;
                break;
            case 15:
                PlayEffect = Instantiate(Effect_StarExplosive) as GameObject;
                break;
            case 16:
                PlayEffect = Instantiate(Effect_Vajura) as GameObject;
                break;
            case 17:
                PlayEffect = Instantiate(Effect_SwordAttack_0) as GameObject;
                break;
            case 18:
                PlayEffect = Instantiate(Effect_SwordAttack_0) as GameObject;
                break;
            case 19:
                PlayEffect = Instantiate(Effect_ThunderHit) as GameObject;
                break;
            case 20:
                PlayEffect = Instantiate(Effect_RespornPlayer) as GameObject;
                break;
            case 21:
                PlayEffect = Instantiate(Effect_RespornEnemy) as GameObject;
                break;
            default:
                Debug.Log("Effectmanagerのエラー");
                break;
        }
        EffectProcess(PlayEffect, EffectPos, Magnification, Target, isDestry);
        return PlayEffect;
    }

}