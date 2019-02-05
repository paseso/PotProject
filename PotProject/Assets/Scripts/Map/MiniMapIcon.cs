using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ミニマップアイコン点滅
/// </summary>
public class MiniMapIcon : MonoBehaviour {
    [SerializeField]
    private Sprite[] Images; 
    private float time = 0.5f;
	// Use this for initialization
	void Start () {
        StartCoroutine(FlashingIcon());
	}
	
	IEnumerator FlashingIcon()
    {
        while (true)
        {
            GetComponent<Image>().sprite = Images[1];
            yield return new WaitForSeconds(time);
            GetComponent<Image>().sprite = Images[0];
            yield return new WaitForSeconds(time);
        }
    }
}
