using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CreateItemStatus {
    public enum Type {
        rope,       // はしご
        key,        // 鍵
        HPPortion,  // 回復ポーション
        barrier,    // バリア
        vajura,     // バジュラ
        transceiver,// 拡声器
        flyCloud,   // 飛べる雲
        tornago,    // 竜巻
        ATKPortion, // 攻撃UPポーション
        lasso,      // 投げ縄
        smokescreen,// 煙玉
        rainCloud,  // 雨雲
        peek        // 項目数
    }

    public Type type;
}

public class CreateItemManager :MonoBehaviour {

    [SerializeField]
    private CreateItemStatus status;

    public CreateItemStatus getStatus {
        get { return status; }
    }
}
