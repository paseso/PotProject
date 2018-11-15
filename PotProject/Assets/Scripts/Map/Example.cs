using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField]
    Charactor[] charactors;
}

[System.Serializable]
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