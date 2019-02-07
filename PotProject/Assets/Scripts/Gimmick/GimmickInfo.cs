using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ギミック情報
/// </summary>
public class GimmickInfo : MonoBehaviour {
    
    public enum GimmickType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ROCK,
        LADDER,
        LADDERBLOCK,
        LADDERTOP,
        GROWTREE,
        BAKETREE,
        MAPCHANGE,
        KEYDOOR,
        WATER,
        FIREFIELD,
        MAGIC,
        SPRING,
        THUNDERFIELD,
    }

    public GimmickType type;
}
