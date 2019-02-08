using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        CLAY_N,
        CLAY_F,
        CLAY_D,
        CLAY_T,
        CLAY_W,
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
    private MoveController mController;
    private GameObject resPoint;

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
        {MonsterStatus.MonsterType.HAMSTAR,"Eye" },
        {MonsterStatus.MonsterType.SHADOW,"" },
        {MonsterStatus.MonsterType.CLOUD,"Cloud" },
        {MonsterStatus.MonsterType.TURTLE,"Crystal" },
        {MonsterStatus.MonsterType.FAIRY,"Feather" },
        {MonsterStatus.MonsterType.HARB,"Weed" },
        {MonsterStatus.MonsterType.CLAY_N,"Clay_N" },
        {MonsterStatus.MonsterType.CLAY_F,"Clay_F" },
        {MonsterStatus.MonsterType.CLAY_D,"Clay_D" },
        {MonsterStatus.MonsterType.CLAY_T,"Clay_T" },
        {MonsterStatus.MonsterType.CLAY_W,"Clay_W" },
        {MonsterStatus.MonsterType.MAGIC,"" }
    };

    private GameObject clearPanel;

    void Start()
    {
        mController = FindObjectOfType<MoveController>();
        status = GetComponent<MonsterController>().Status;
        if (status.type != MonsterStatus.MonsterType.MAGIC)
        {
            resPoint = new GameObject(Status.type.ToString() + "Resporn");
            resPoint.transform.SetParent(transform.parent);
            resPoint.transform.position = transform.position;
            resPoint.AddComponent<MonsterResporn>();
            resPoint.GetComponent<MonsterResporn>().MType = status.type;
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if(status.type == MonsterStatus.MonsterType.HARB) {
            StartCoroutine(Drop());
            return;
        }
        EffectManager.Instance.PlayEffect((int)EffectManager.EffectName.Effect_AttackIce, transform.position, 4, gameObject, true);
        KnockBack(mController.direc);
        status.HP -= damage;
        if(status.HP <= 0)
        {
            StartCoroutine(Drop());
            return;
        }        
    }

    /// <summary>
    /// ノックバック
    /// </summary>
    /// <param name="dir"></param>
    public void KnockBack(MoveController.Direction dir) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        Vector2 knock = new Vector2(knockback.x, knockback.y);
        
        if (dir == MoveController.Direction.LEFT) {
            knock.x *= -1;
        }
        rb.AddForce(knock, ForceMode2D.Impulse);
    }

    public IEnumerator Drop()
    {
        if (ItemList[status.type] == "")
        {
            Destroy(gameObject);
            yield break;
        }

        if (GetComponent<MonsterWalk>())
        {
            Destroy(GetComponent<MonsterWalk>());
        }

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetEase(Ease.Linear);

        GameObject dropPos = new GameObject("DropPos");
        dropPos.transform.SetParent(transform.parent.transform);
        dropPos.transform.position = transform.position;
        Transform trans = transform;

        resPoint.GetComponent<MonsterResporn>().CountFlag = true;
        yield return new WaitForSeconds(1f);
        GameObject item = Instantiate(Resources.Load<GameObject>(itemFolder + ItemList[status.type]));
        item.transform.SetParent(dropPos.transform);
        item.transform.position = trans.position;
        item.AddComponent<DropItemTimer>();
        
        Destroy(gameObject);
    }
}
