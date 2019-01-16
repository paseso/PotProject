using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DropItemStatus {
    public enum Type {
        none,       // 空(から)
        cane,       // ヴァジュラ先端
        mic,        // コウモリ
        lamp,       // ランプ
        pothook,    // カギ
        clay,       // 粘土
        exuvia,     // 縄
        collar,     // トカゲ
        roots,      // 根っこ
        ore,        // 王冠
        smokeball,  // 煙玉
        crystals,   // クリスタル
        cloud,      // 雲
        scales,     // 鱗粉
        weed,

    }

    public Type type;

}

public class DropItemManager : MonoBehaviour {

    [SerializeField]
    private DropItemStatus status;
    
    public DropItemStatus getStatus {
        get { return status; }
    }
}
