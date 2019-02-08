using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeGrow : MonoBehaviour {
    private float growTime = 1f;
	
    public void Grow()
    {
        StartCoroutine(IsGrow());
    }

	public IEnumerator IsGrow()
    {
        SoundManager.Instance.PlaySe((int)SoundManager.SENAME.SE_GROWTREE);
        Vector2 defaultScale = transform.localScale;
        transform.DOScaleY(6f, growTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(growTime + 10);
        transform.DOScaleY(defaultScale.y, 10f).SetEase(Ease.Linear);
        yield return null;
    }
}
