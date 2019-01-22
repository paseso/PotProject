﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// モンスターステータス
/// </summary>
[System.Serializable]
public struct MonsterStatus
{
    [SerializeField]
    private int hp, atk;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public int GetAttack
    {
        get { return atk; }
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
        MAGIC,
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
/// モンスターコントローラー
/// </summary>
public class MonsterController : MonoBehaviour
{
    private string itemFolder = "Prefabs/Items/Drop/";

    [SerializeField]
    private MonsterStatus status = new MonsterStatus();

    public MonsterStatus Status
    {
        get { return status; }
        set { status = value; }
    }

    //private float knockback = 6;
    private Vector2 knockback = new Vector2(1, 6);

    private Dictionary<MonsterStatus.MonsterType, string> ItemList = new Dictionary<MonsterStatus.MonsterType, string>
    {
        {MonsterStatus.MonsterType.WATER,"" },
        {MonsterStatus.MonsterType.LION,"Cane" },
        {MonsterStatus.MonsterType.BAT,"Mic" },
        {MonsterStatus.MonsterType.LAMP,"Lamp" },
        {MonsterStatus.MonsterType.ROBOT,"Pothook" },
        {MonsterStatus.MonsterType.SLIME,"" },
        {MonsterStatus.MonsterType.SNAKE,"Exuvia" },
        {MonsterStatus.MonsterType.LUKEWARM,"" },
        {MonsterStatus.MonsterType.WOOD,"Roots" },
        {MonsterStatus.MonsterType.HAMSTAR,"" },
        {MonsterStatus.MonsterType.SHADOW,"" },
        {MonsterStatus.MonsterType.CLOUD,"Cloud" },
        {MonsterStatus.MonsterType.TURTLE,"Crystal" },
        {MonsterStatus.MonsterType.FAIRY,"" },
        {MonsterStatus.MonsterType.HARB,"Weed" },
    };

    private GameObject clearPanel;

    void Start()
    {
        status = GetComponent<MonsterController>().Status;
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        status.HP -= damage;
        if(status.HP <= 0)
        {
            DropItem(status);
            return;
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(knockback.x, rb.velocity.y + knockback.y), ForceMode2D.Impulse);
    }

    public void DropItem(MonsterStatus status)
    {
        if(ItemList[status.type] == "") {
            Destroy(gameObject);
            return;
        }

        GameObject item = Instantiate(Resources.Load<GameObject>(itemFolder + ItemList[status.type]));
        item.AddComponent<DropItemMove>();
        item.transform.SetParent(transform.parent.transform);
        item.transform.localPosition = transform.localPosition;
        Destroy(gameObject);
    }
}
