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
        HARB,
        FATHER,
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
    private string itemFolder = "Prefabs/Items/";

    [SerializeField]
    private MonsterStatus status;

    public Dictionary<MonsterStatus.MonsterType, string> ItemList = new Dictionary<MonsterStatus.MonsterType, string>
    {
        {MonsterStatus.MonsterType.WATER,"" },
        {MonsterStatus.MonsterType.LION,"" },
        {MonsterStatus.MonsterType.BAT,"" },
        {MonsterStatus.MonsterType.LAMP,"" },
        {MonsterStatus.MonsterType.ROBOT,"" },
        {MonsterStatus.MonsterType.SLIME,"" },
        {MonsterStatus.MonsterType.SNAKE,"RopePrefab" },
        {MonsterStatus.MonsterType.LUKEWARM,"" },
        {MonsterStatus.MonsterType.WOOD,"HarbPrefab" },
        {MonsterStatus.MonsterType.HAMSTAR,"" },
        {MonsterStatus.MonsterType.SHADOW,"" },
        {MonsterStatus.MonsterType.CLOUD,"" },
        {MonsterStatus.MonsterType.TURTLE,"" },
        {MonsterStatus.MonsterType.FAIRY,"" },
        {MonsterStatus.MonsterType.HARB,"HarbPrefab" },
    };

    private GameObject clearPanel;

    public MonsterStatus GetMStatus
    {
        get { return status; }
    }



    public void OnDestroy()
    {
        DropItem(GetMStatus);
    }

    public void DropItem(MonsterStatus status)
    {
        GameObject item = Instantiate(Resources.Load<GameObject>(itemFolder + ItemList[status.type]));
        item.AddComponent<PopUp>();
        item.transform.SetParent(transform.parent.transform);
        
        item.transform.localPosition = transform.localPosition;
    }
}
