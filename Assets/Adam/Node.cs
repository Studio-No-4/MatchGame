using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isUsable;

    public GameObject mana;

    public Node(bool _isUsable, GameObject _mana)
    {
        isUsable = _isUsable;
        mana = _mana;
    }
}
