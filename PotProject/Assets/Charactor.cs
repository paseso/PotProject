using UnityEngine;
using System;

[Serializable]
public class Charactor
{
    [SerializeField]
    Texture icon;

    [SerializeField]
    string name;

    [SerializeField]
    int hp;

    [SerializeField]
    int power;
}