using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム効果スクリプト
/// </summary>
public class ItemController : MonoBehaviour {
    private PlayerController playerController = FindObjectOfType<PlayerController>();

    /// <summary>
    /// 回復ポーション
    /// </summary>
    void HPPortion() {
        playerController.HPUp(1);
    }

    /// <summary>
    /// 攻撃力UPポーション
    /// </summary>
    void ATKPortion() {
        playerController.ATKChange(1);
    }
}
