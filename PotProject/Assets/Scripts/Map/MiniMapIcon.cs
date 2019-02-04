using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ミニマップアイコン点滅
/// </summary>
public class MiniMapIcon : MonoBehaviour {
    private float time = 0.5f;
	// Use this for initialization
	void Start () {
        StartCoroutine(FlashingIcon());
	}
	
	IEnumerator FlashingIcon()
    {
        while (true)
        {
            GetComponent<Image>().DOColor(Color.red,time).SetEase(Ease.Linear);
            //GetComponent<Image>().DOFade(0, time).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time);
            GetComponent<Image>().DOColor(Color.white, time).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time);
        }
    }
}
