using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private GameObject CharaObject;
    [SerializeField]
    private GameObject logoObject;
    private FadeManager fade_m;
    [SerializeField]
    private float loopTime = 3;
    [SerializeField]
    private Vector2 startPos = new Vector2(0, 135), endPos = new Vector2(0, 100);
    private PlayerManager pInfo;

    // Use this for initialization
    void Start()
    {
        fade_m = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        if (GameObject.Find("PlayerStatus"))
        {
            pInfo = GameObject.Find("PlayerStatus").GetComponent<PlayerManager>();
            pInfo.InitStatus();
        }

        LogoAnimatino();
        LoopCharaAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Circle") || Input.GetKeyDown(KeyCode.E))
        {
            TapTitle();
        }
    }

    /// <summary>
    ///　ボタンを押してシーン移動
    /// </summary>
    private void TapTitle()
    {
        fade_m.LoadScene(1, 0.3f);
    }

    private void LoopCharaAnimation()
    {
        Animator anim = CharaObject.GetComponent<Animator>();
        anim.SetBool("isRightJump", false);
        anim.SetBool("isLeftJump", false);
        anim.SetBool("isLeftWalk", true);
        anim.SetBool("isRightWalk", false);
        anim.SetBool("isLeftIdle", false);
        anim.SetBool("isRightIdle", false);
        anim.SetBool("isSordAttackLeft", false);
        anim.SetBool("isSordAttackRight", false);
        anim.SetBool("isLadderUp", false);
        anim.SetBool("isLeftGetItem", false);
        anim.SetBool("isRightGetItem", false);
    }

    /// <summary>
    /// タイトルロゴを上下させる
    /// </summary>
    private void LogoAnimatino()
    {
        StartCoroutine(logoAnimation());
    }

    IEnumerator logoAnimation()
    {
        float time = 0;
        bool isDown = false;
        while (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (time > loopTime)
            {
                time = 0;
                isDown = !isDown;
            }
                
            if (time < loopTime && !isDown)
            {
                logoObject.GetComponent<RectTransform>().localPosition = Vector3.Slerp(startPos, endPos, time / loopTime);
                time += Time.deltaTime;
                yield return null;
            }
            if (time < loopTime && isDown)
            {
                logoObject.GetComponent<RectTransform>().localPosition = Vector3.Slerp(endPos, startPos, time / loopTime);
                time += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
        yield break;
    }
}
