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
        Effect_Respawn
    };

    /// <summary>
    /// 座標指定してエフェクトを再生がおわったら破棄する
    /// </summary>
    /// <param name="PlayEffect"></param>
    /// <param name="EffectPos">Effectの生成場所</param>
    /// <param name="Magnification">Effectのスケール</param>
    /// <param name="Target">親子付けするObject</param>
    private void EffectProcess(GameObject PlayEffect, Vector2 EffectPos, float Magnification, GameObject Target)
    {
        Debug.Log("InstanceEffects" + PlayEffect.transform.name);
        PlayEffect.transform.position = EffectPos;
        PlayEffect.transform.localScale = new Vector3(Magnification, Magnification, Magnification);
        PlayEffect.transform.SetParent(Target.transform);
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        if (PlayEffect != null)
            Destroy(PlayEffect, main.duration);
    }

    //実行する時はコレ↓
    //EffectManager.Instance_Effect.PlayEffect(0, new Vector2(0, 0), 1.0f);

    /// <summary>
    /// エフェクトナンバーを指定する方
    /// </summary>
    public void PlayEffect(int EffectNum, Vector2 EffectPos, float Magnification, GameObject Target)
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
            default:
                Debug.Log("Effectmanagerのエラー");
                break;
        }
        // Debug.Log(EffectNum);
        EffectProcess(PlayEffect, EffectPos, Magnification, Target);
    }
}