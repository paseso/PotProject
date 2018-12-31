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
public class MonsterController : MonoBehaviour
{
    [SerializeField]
    private MonsterStatus status;

    private GameObject clearPanel;

    public MonsterStatus GetMStatus
    {
        get { return status; }
    }

    void Start()
    {
        if(status.type == MonsterStatus.MonsterType.HAMSTAR)
        {
            clearPanel = GameObject.FindGameObjectWithTag("ClearPanel");
            clearPanel.SetActive(false);
        }
    }

    public void OnDestroy()
    {
        DropItem(GetMStatus);
    }

    public void DropItem(MonsterStatus status)
    {
        GameObject item = new GameObject();
        switch (status.type)
        {
            case MonsterStatus.MonsterType.WOOD:
                item = Instantiate(Resources.Load<GameObject>("Prefabs/HarbPrefab"));
                item.transform.SetParent(transform.parent.transform);
                break;
            case MonsterStatus.MonsterType.SNAKE:
                item = Instantiate(Resources.Load<GameObject>("Prefabs/Rope"));
                item.transform.SetParent(transform.parent.transform);
                break;
        }
        item.transform.localPosition = transform.localPosition;
    }
}
