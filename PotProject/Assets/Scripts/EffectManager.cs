using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{

    #region Singleton

    private static EffectManager instance_Effect;

    public static EffectManager Instance_Effect
    {
        get
        {
            if (instance_Effect == null)
            {
                //Objectを検索
                instance_Effect = (EffectManager)FindObjectOfType(typeof(EffectManager));

                if (instance_Effect == null)
                {
                    //アタッチされているGameObjectが無いのでエラー
                    Debug.LogError(typeof(EffectManager) + "is nothing");
                }
            }
            return instance_Effect;
        }
    }

    #endregion Singleton

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

    public void Awake()
    {
        //Destroyしない
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
        Effect_Grow
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
        Debug.Log("InstanceEffects" + PlayEffect.transform.name);
        PlayEffect.transform.position = EffectPos;
        PlayEffect.transform.localScale = new Vector3(Magnification, Magnification, Magnification);
        PlayEffect.transform.SetParent(Target.transform);
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
            default:
                Debug.Log("Effectmanagerのエラー");
                break;
        }
        EffectProcess(PlayEffect, EffectPos, Magnification, Target, isDestry);
        return PlayEffect;
    }

}