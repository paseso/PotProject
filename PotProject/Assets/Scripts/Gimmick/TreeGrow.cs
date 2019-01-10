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
        Vector2 defaultScale = transform.localScale;
        transform.DOScaleY(3f, growTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(growTime + 3);
        transform.DOScaleY(defaultScale.y, growTime).SetEase(Ease.Linear);
        yield return null;
    }
}
