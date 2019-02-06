using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenDoor : MonoBehaviour {
    int count;
    public void Open() {
        StartCoroutine(OpenColutine());
    }

    public void OpenKey() {
        StartCoroutine(OpenColutine_Key());
    }

    IEnumerator OpenColutine() {
        
        GameObject child = transform.GetChild(0).gameObject;
        child.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
            child = child.transform.GetChild(0).gameObject;
            child.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                child = child.transform.GetChild(0).gameObject;
                child.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                    child = child.transform.GetChild(0).gameObject;
                    child.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                        Destroy(gameObject);
                    });
                });
            });
        });
        while (true) {
            transform.localPosition = new Vector2(transform.localPosition.x + 0.05f, transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
            transform.localPosition = new Vector2(transform.localPosition.x - 0.05f, transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator OpenColutine_Key() {

        GameObject child = transform.GetChild(0).gameObject;
        child.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
            child = child.transform.GetChild(0).gameObject;
            child.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                child = child.transform.GetChild(0).gameObject;
                child.transform.DOMoveY(transform.position.y, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                    Destroy(gameObject);
                });
            });
        });
        while (true) {
            transform.localPosition = new Vector2(transform.localPosition.x + 0.05f, transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
            transform.localPosition = new Vector2(transform.localPosition.x - 0.05f, transform.localPosition.y);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
