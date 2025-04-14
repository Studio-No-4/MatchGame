using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public ManaType manaType;

    public int xIndex;
    public int yIndex;

    public bool isMatched;

    private Vector2 currentPos;
    private Vector2 targetPos;

    public bool isMoving;

    public Mana(int _x, int _y)
    {
        xIndex = _x;
        yIndex = _y;
    }

    public void SetIndices(int _x, int _y)
    {
        xIndex = _x;
        yIndex = _y;
    }
}

public enum ManaType
{
    Red,
    Green, 
    Blue,
    white,
    black,
    Skull
}
