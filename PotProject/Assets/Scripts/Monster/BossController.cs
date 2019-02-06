using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public struct BossStatus
{
    public int HP;
    public int ATTACK;
    public int INT;
    public int magicTime;

    // 属性
    public enum BossAttribute {
        NORMAL,
        FIRE,
        ICE,
        THUNDER,
        DARK,
    }
    public BossAttribute attribute;
}

public class BossController : MonoBehaviour {
    [SerializeField]
    private BossStatus status;
    private float mTime;
    private bool isMagicAttack = false;
    private GameObject clearPanel;

    public bool IsMagicAttack
    {
        set { isMagicAttack = value; }
    }

    private Vector2 playerPos;

    public Vector2 setPlayerPos
    {
        set { playerPos = value; }
    }

    int count = 3;
    Vector2 size;
    float moveTime = 4f;
    private CameraController cController;

    void Awake() {
        clearPanel = FindObjectOfType<GameClear>().gameObject;
    }

    void Start()
    {
        cController = FindObjectOfType<CameraController>();
        size = GetComponent<SpriteRenderer>().bounds.size;
    }

    // Update is called once per frame
    void Update () {
        // 魔法攻撃
        if (isMagicAttack)
        {
            mTime += Time.deltaTime;
            if(mTime > status.magicTime)
            {
                // 魔法飛ばす処理
                playerPos = FindObjectOfType<MoveController>().gameObject.transform.position;
                GetComponentInChildren<MagicShoot>().Shoot(playerPos);
                mTime = 0;
            }
        }
	}

    /// <summary>
    /// 吹っ飛び
    /// </summary>
    /// <param name="player"></param>
    public void Flying(GameObject player)
    {
        cController.target = gameObject;
        var sr = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;

        Vector2 pos = Camera.main.transform.position;

        sr.sortingOrder = 1000;

        transform.DOLocalMoveY(transform.localPosition.y - 0.5f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_DEMONFLYING);
            Sequence s = DOTween.Sequence();
            s.Append(transform.DORotate(new Vector3(0, 0, 365 * 4), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
            s.SetLoops(100);
            transform.DOScale(size * 1.2f, count).SetEase(Ease.InSine);
            transform.DOMove(pos, count).SetEase(Ease.Linear).OnComplete(() =>
            {
                s.Complete();
                cController.target = player;
                transform.DOMoveY(transform.localPosition.y - (size.y * 5), moveTime).SetEase(Ease.Linear).SetDelay(1f).OnComplete(() =>
                {
                    clearPanel.SetActive(true);
                });
            });

        });
    }

}
