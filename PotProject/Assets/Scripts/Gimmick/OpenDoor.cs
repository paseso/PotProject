using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenDoor : MonoBehaviour {

    public void Open() {
        foreach(Transform i in transform) {
            StartCoroutine(OpenColutine(i.gameObject));
        }
    }

    IEnumerator OpenColutine(GameObject obj) {
        obj.transform.DOMoveY(transform.position.y, 2f).SetEase(Ease.Linear);
        yield return null;
    }
}
