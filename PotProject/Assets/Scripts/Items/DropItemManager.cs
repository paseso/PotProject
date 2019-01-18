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
        clay_N,     // 粘土(無)
        clay_W,     // 粘土(水)
        clay_F,     // 粘土(火)
        clay_T,     // 粘土(雷)
        clay_D,     // 粘土(闇)
        exuvia,     // 縄
        collar,     // トカゲ
        roots,      // 根っこ
        ore,        // 王冠
        smokeball,  // 煙玉
        crystals,   // クリスタル
        cloud,      // 雲
        scales,     // 鱗粉
        weed,       // 薬草

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
