using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モンスターステータス
/// </summary>
[System.Serializable]
public struct MonsterStatus
{
    [SerializeField]
    private int HP, ATK;

    public int SetHP
    {
        set { value = HP; }
    }

    public int GetAttack
    {
        get { return ATK; }
    }
    
    public bool barrier;

    // どのモンスターか
    public enum MonsterType
    {
        WATER,
        LION,
        BAT,
        LAMP,
        ROBOT,
        SLIME,
        SNAKE,
        LUKEWARM,
        WOOD,
        HAMSTAR,
        SHADOW,
        CLOUD,
        TURTLE,
        FAIRY,
    }
    public MonsterType type;

    // 属性
    public enum MonsterAttribute
    {
        NORMAL,
        FIRE,
        ICE,
        THUNDER,
        DARK,
    }
    public MonsterAttribute attribute;

}

/// <summary>
/// モンスター情報
/// </summary>
public class MonsterInfo : MonoBehaviour {
    [SerializeField]
    private MonsterStatus status;

    public MonsterStatus GetMStatus
    {
        get { return status; }
    }
}
