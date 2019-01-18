using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム効果スクリプト
/// </summary>
public class ItemController : MonoBehaviour {
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Controller").GetComponent<PlayerController>();
    }

    /// <summary>
    /// 回復ポーション
    /// </summary>
    public void HPPortion() {
        playerController.HPUp(1);
    }

    /// <summary>
    /// 攻撃力UPポーション
    /// </summary>
    public void ATKPortion() {
        playerController.ATKChange(1);
    }
}
