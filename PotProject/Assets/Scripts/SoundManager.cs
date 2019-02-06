using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    #region Singleton

    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SoundManager)FindObjectOfType(typeof(SoundManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(SoundManager) + "is nothing");
                }
            }
            return instance;
        }
    }

    #endregion Singleton


    public enum BGMNAME {
        BGM_TITLE,
        BGM_MAIN,
        BGM_MAINFAST
    };
    public enum SENAME
    {
        SE_SLIDESTAGE = 0,
        SE_MATCHSTAGE,
        SE_GROWTREE,
        SE_CHOICE,
        SE_SWORD,
        SE_SWORDHEAVY,
        SE_SWORDSLIM,
        SE_WATER,
        SE_SELECT,
        SE_FOOTSTEPS,
        SE_DEMONKINGCLASH,
        SE_DEMONFLYING,
        SE_THUNDER,
        SE_FALL,
        SE_ALCHEMYMISS,
        SE_ALCHEMYSUCCESS,
        SE_STONEDOOR,
    };

    [SerializeField]
    AudioClip[] BGM_LIST = new AudioClip[3];

    [SerializeField]
    AudioClip[] SE_LIST = new AudioClip[16];

    private AudioSource audioSource;

    //[SerializeField]
    //AudioMixer bgmMixer;

    void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBgm(2);
    }
    public void PlayBgm(int BGM_num)
    {
        audioSource.clip = BGM_LIST[BGM_num];
        audioSource.Play();
    }

    public void PlaySe(int SEnum)
    {
        audioSource.PlayOneShot(SE_LIST[SEnum]);
    }
    public void PlaySe(int SEnum, float vol = 1.0f)
    {
        audioSource.PlayOneShot(SE_LIST[SEnum], vol);
    }

    //public void FadeOutBGM()
    //{
    //    StartCoroutine(FadeOutBgm(1));
    //}

    //public void FadeInBGM()
    //{
    //    StartCoroutine(FadeOutBgm(1));
    //}


    //IEnumerator FadeOutBgm(float interval)
    //{
    //    //だんだん小さく
    //    float time = 0;
    //    while (time <= interval)
    //    {
    //        bgmMixer.SetFloat("BGM", Mathf.Lerp(0, -40, time / interval));
    //        time += Time.unscaledDeltaTime;
    //        yield return 0;
    //    }
    //    yield break;
    //}
    //IEnumerator FadeInBgm(float interval)
    //{
    //    //だんだん小さく
    //    float time = 0;
    //    while (time <= interval)
    //    {
    //        bgmMixer.SetFloat("BGM", Mathf.Lerp(-40, 0, time / interval));
    //        time += Time.unscaledDeltaTime;
    //        yield return 0;
    //    }
    //    yield break;
    //}
}
