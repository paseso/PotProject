using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerStatus
{
    //兄のHP
    private int hp, atk;

    private const int maxHP = 6;

    public int GetMaxHP{
        get { return maxHP; }
    }

    // バリアの制限時間
    [SerializeField]
    private const float barrierTime = 10;

    public float GetBarrierTime
    {
        get { return barrierTime; }
    }

    public int PlayerHP
    {
        get { return hp; }
        set { hp = value; }
    }

    public int PlayerAttack
    {
        get { return atk; }
        set { atk = value; }
    }

    //剣のタイプ
    public enum SWORDTYPE
    {
        NORMAL,
        FIRE,
        FROZEN,
        DARK,
        SPEAR,
        AXE,
        KEY,
        TORCH,
    }

    // 兄がどの状態か
    public enum GimmickState
    {
        NORMAL,
        ONLADDER,
        ONTREE,
    }

    //今のイベント状態
    public enum EventState
    {
        NORMAL = 0,
        CAMERA,
        ALCHEMYUI,
        MINIMAP,
    }

    //アイテム
    public SWORDTYPE swordtype;

    // 状態
    public GimmickState gimmick_state;

    public EventState event_state;

    //持ち物に入ってるアイテム
    [HideInInspector]
    public List<ItemStatus.Type> ItemList;
}

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>{
    [SerializeField]
    private PlayerStatus status;
    private PlayerController player_ctr;

    public PlayerStatus Status {
        get;set;
    }

    public PlayerStatus.SWORDTYPE GetSwordType
    {
        get { return status.swordtype; }
    }

    public PlayerStatus.SWORDTYPE SetSwordType
    {
        set
        {
            status.swordtype = value;
            player_ctr.setSwordList(GetSwordType);
        }
    }

    void Awake()
    {
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
        player_ctr.setStartSwordList();
        InitStatus();
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void InitStatus()
    {
        status.PlayerHP = status.GetMaxHP;
        status.PlayerAttack = 1;
        SetSwordType = PlayerStatus.SWORDTYPE.NORMAL;
        status.event_state = PlayerStatus.EventState.NORMAL;
        status.gimmick_state = PlayerStatus.GimmickState.NORMAL;
        status.ItemList = new List<ItemStatus.Type>();
    }

    private void Start()
    {
        player_ctr = GameObject.Find("Controller").GetComponent<PlayerController>();
    }
}

