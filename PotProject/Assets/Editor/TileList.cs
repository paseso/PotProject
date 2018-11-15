using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class TileList : EditorWindow {

    [SerializeField]
    Tile[] tiles;

    ReorderableList reorderableList;

}

[Serializable]
public class Tile
{
    [SerializeField]
    Texture2D icon;

    [SerializeField]
    int x;

    [SerializeField]
    int y;

}
