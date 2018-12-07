using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モンスターステータス
/// </summary>
[System.Serializable]
struct MonsterStatus
{
    [SerializeField]
    private int HP, ATK;

    [SerializeField]
    private bool barrier;

    // どのモンスターか
    public enum MonsterType
    {
        WATER,
        SNAKE,
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

}
