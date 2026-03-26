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

    public Overlay Webbed = new();
    public Overlay Burning = new();
    public Overlay Bomb = new();

    [System.Serializable]
    public class Overlay
    {
        private bool _status = false;
        public bool Status
        {
            get => _status;
            set
            {
                _status = value;
                overlay.SetActive(value);
            }
        }
        [SerializeField] private GameObject overlay;

        public static bool operator ==(Overlay obj, bool value) => obj.Status == value;
        public static bool operator !=(Overlay obj, bool value) => obj.Status != value;
        public override bool Equals(object obj)
        {
            if (obj is Overlay other)
            {
                return this == other;
            }
            return false;
        }
        public override int GetHashCode() => Status.GetHashCode();
        public static implicit operator bool(Overlay instance)
        {
            return instance.Status;
        }
    }

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
            direction.Normalize();
            Vector2Int start = new(xIndex, yIndex);
            Vector2Int fixedDirection = new();
            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                if (direction.y > 0)
                {
                    // Up
                    print("Moving Up");
                    fixedDirection = new Vector2Int(0, 1);
                }
                else
                {
                    // Down
                    print("Moving Down");
                    fixedDirection = new Vector2Int(0, -1);
                }
            }
            else
            {
                if (direction.x > 0)
                {
                    // Right
                    print("Moving Right");
                    fixedDirection = new Vector2Int(1, 0);
                }
                else
                {
                    // Left
                    print("Moving Left");
                    fixedDirection = new Vector2Int(-1, 0);
                }
            }

            if (Board.Instance.IsMoveValid(start, start + fixedDirection) || Board.Instance.IsMoveValid(start + fixedDirection, start))
            {
                StartCoroutine(Board.Instance.Swap(start, start + fixedDirection));
                GameManager.Instance.GridLocked = true;
                // Detect if match was made, otherwise undo move
                // if (match was made)
                //{
                Board.Instance.Invoke(nameof(Board.Instance.PopMatches), 0.5f);
                // Lock the grid so no more matches can be made, unless it was a match-4
                //}
            }
        }
        hovered = false;
    }

    public void ClearStates()
    {
        Burning.Status = false;
        Webbed.Status = false;
    }

    private void Update()
    {
        if (PauseMenu.Paused)
        {
            hovered = false;
            dragging = false;
            return;
        }
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
    Black,
    White,
    Skull
}
