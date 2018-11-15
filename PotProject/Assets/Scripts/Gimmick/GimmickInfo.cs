﻿using System.Collections;
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
        TREE,
        M_WATER,
    }

    public GimmickType type;
}