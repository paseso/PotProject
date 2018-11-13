using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickInfo : MonoBehaviour {
    
    public enum GimmickType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ROCK,
        LADDER,
        TREE,
    }

    public GimmickType type;
}
