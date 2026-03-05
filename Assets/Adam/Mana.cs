using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    public ManaType manaType;

    public int xIndex;
    public int yIndex;

    public bool isMatched;

    public Vector3 TargetPos;

    public bool isMoving;

    private bool hovered = false;
    public bool dragging = false;

    public float SwapSpeed = 10f;

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

    private void OnMouseEnter()
    {
        hovered = true;
    }

    void OnDrawGizmos()
    {
        Handles.Label(transform.position, xIndex.ToString() + ":" + yIndex.ToString());
    }

    public void SetType(int type)
    {
        manaType = (ManaType)type;
        // Sets the image according to the given type
        sprite.sprite = GlobalData.Instance.ElementIcons[type];
    }

    private void OnMouseExit()
    {
        if (dragging && Input.GetMouseButton(0))
        {
            print("Moved " + gameObject.name);
            Camera mainCamera = Camera.main;
            Vector3 direction = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position);
            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                if (direction.y > 0)
                {
                    // Up
                    print("Moving Up");
                    Board.Instance.Swap(new Vector2Int(xIndex, yIndex), new Vector2Int(xIndex, yIndex + 1));
                }
                else
                {
                    // Down
                    print("Moving Down");
                    Board.Instance.Swap(new Vector2Int(xIndex, yIndex), new Vector2Int(xIndex, yIndex - 1));
                }
            }
            else
            {
                if (direction.x > 0)
                {
                    // Right
                    print("Moving Right");
                    Board.Instance.Swap(new Vector2Int(xIndex, yIndex), new Vector2Int(xIndex + 1, yIndex));
                }
                else
                {
                    // Left
                    print("Moving Left");
                    Board.Instance.Swap(new Vector2Int(xIndex, yIndex), new Vector2Int(xIndex - 1, yIndex));
                }
            }
            // Detect if match was made, otherwise undo move
            // if (match was made)
            //{
            Board.Instance.PopMatches();
            // Lock the grid so no more matches can be made, unless it was a match-4
            GameManager.Instance.GridLocked = true;
            //}
        }
        hovered = false;
    }

    private void Update()
    {
        if (hovered && !GameManager.Instance.GridLocked)
        {
            transform.localScale = Vector3.one * 0.1f;
            if (Input.GetMouseButtonDown(0))
            {
                dragging = true;
            }
        }
        else
        {
            transform.localScale = Vector3.one * 0.09f;
            dragging = false;
        }
        transform.Translate(SwapSpeed * Time.deltaTime * (TargetPos - transform.position));
    }
}

public enum ManaType
{
    Red,
    Green, 
    Blue,
    White,
    Black,
    Skull
}
