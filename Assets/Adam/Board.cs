
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public int width = 8;
    public int height = 8;

    public float spacingX;
    public float spacingY;

    public Mana ManaPrefab;

    public Node[,] manaBoard;
    public GameObject manaBoardGO;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializeBoard();   
    }

    void InitializeBoard()
    {
        manaBoard = new Node[width, height];

        spacingX = (float)((width - 1) / 2) ;
        spacingY = (float)((height - 1) / 2) + 1;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 position = new(x - spacingX, y - spacingY);
                int randomIndex = Random.Range(0, GlobalData.Instance.ElementIcons.Count);

                Mana mana = Instantiate(ManaPrefab, position + Vector2.up * Random.Range(100,1000)/10f, Quaternion.identity);
                mana.SetType(randomIndex);
                mana.TargetPos = position;
                mana.SetIndices(x, y);
                manaBoard[x, y] = new(true, mana);
            }
        }
        PopMatches();
    }

    public int PopMatches()
    {
        int poppedMana = 0;
        List<Node> matchingMana = GetMatchingMana();
        if (matchingMana.Count > 0)
        {
            foreach (Node node in matchingMana)
            {
                if (node.mana)
                    node.PopMana();
                poppedMana++;
                print(poppedMana);
            }
        }
        if (poppedMana > 0) StartCoroutine(SettleMana());
        return poppedMana;
    }

    public bool IsMoveValid(Vector2Int start, Vector2Int end)
    {
        /*
        Node[,] boardState = (Node[,])manaBoard.Clone();

        try
        {
            // Set positions to target positions to prevent bugs
            (boardState[second.x, second.y].mana.transform.position, boardState[first.x, first.y].mana.transform.position) = (boardState[second.x, second.y].mana.TargetPos, boardState[first.x, first.y].mana.TargetPos);
            // Set target positions to the other's position
            (boardState[second.x, second.y].mana.TargetPos, boardState[first.x, first.y].mana.TargetPos) = (boardState[first.x, first.y].mana.transform.position, boardState[second.x, second.y].mana.transform.position);
            // Swap Mana Indexes
            (boardState[first.x, first.y].mana.xIndex, boardState[first.x, first.y].mana.yIndex, boardState[second.x, second.y].mana.xIndex, boardState[second.x, second.y].mana.yIndex) = (boardState[second.x, second.y].mana.xIndex, boardState[second.x, second.y].mana.yIndex, boardState[first.x, first.y].mana.xIndex, boardState[first.x, first.y].mana.yIndex);
            // Swap Mana Objects
            (boardState[second.x, second.y].mana, boardState[first.x, first.y].mana) = (boardState[first.x, first.y].mana, boardState[second.x, second.y].mana);
        }
        catch
        {
            print(first.ToString() + " and " + second.ToString());
            return false;
        }
        // Horizontal Matches
        for (int x = 0; x < boardState.GetLength(0) - 2; x++)
        {
            for (int y = 0; y < boardState.GetLength(1); y++)
            {
                if (boardState[x, y].mana && boardState[x + 1, y].mana && boardState[x + 2, y].mana)
                    if (boardState[x, y].mana.manaType == boardState[x + 1, y].mana.manaType && boardState[x + 1, y].mana.manaType == boardState[x + 2, y].mana.manaType)
                    {
                        return true;
                    }
            }
        }
        // Vertical Matches
        for (int x = 0; x < boardState.GetLength(0); x++)
        {
            for (int y = 0; y < boardState.GetLength(1) - 2; y++)
            {
                if (boardState[x, y].mana && boardState[x, y + 1].mana && boardState[x, y + 2].mana)
                    if (boardState[x, y].mana.manaType == boardState[x, y + 1].mana.manaType && boardState[x, y + 1].mana.manaType == boardState[x, y + 2].mana.manaType)
                    {
                        return true;
                    }
            }
        }*/

        // If End is in bounds
        if (end.x >= 8 || end.x < 0 || end.y >= 8 || end.y < 0) return false;

        // Save mana type of Start to reduce code
        ManaType type = manaBoard[start.x, start.y].mana.manaType;

        // If start and end match, invalid
        if (manaBoard[end.x, end.y].mana.manaType == type) return false;

        // Calculate direction from start to end
        Vector2Int direction = end - start;

        // If end-adjacent coords are out of bounds
        if (end.x + 1 >= 8 || end.x - 1 < 0 || end.y + 1 >= 8 || end.y - 1 < 0) return false;

        print(start.ToString() + " => " + end.ToString());

        try
        {
            if (manaBoard[end.x + direction.y, end.y + direction.x].mana.manaType == type)
            {
                if (end.x + direction.y * 2 < 8 && end.y + direction.x * 2 < 8)
                    if (manaBoard[end.x + direction.y * 2, end.y + direction.x * 2].mana.manaType == type) return true;
                if (end.x - direction.y >= 0 && end.y - direction.x >= 0)
                    if (manaBoard[end.x - direction.y, end.y - direction.x].mana.manaType == type) return true;
            }
            if (manaBoard[end.x - direction.y, end.y - direction.x].mana.manaType == type)
            {
                if (end.x - direction.y * 2 >= 0 && end.y - direction.x * 2 >= 0)
                    if (manaBoard[end.x - direction.y * 2, end.y - direction.x * 2].mana.manaType == type) return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public List<Vector4> GetAllValidMoves()
    {
        Vector2Int[] directions = { new(0, 1), new(0, -1), new(1, 0), new(-1, 0) };
        List<Vector4> ValidMoves = new();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                foreach (Vector2Int direction in directions)
                {
                    if (IsMoveValid(new Vector2Int(i, j), new Vector2Int(i, j) + direction))
                    {
                        ValidMoves.Add(new Vector4(i, j, i + direction.x, j + direction.y));
                    }
                }
            }
        }
        return ValidMoves;
    }

    public List<Node> GetMatchingMana()
    {
        List<Node> matchingMana = new();
        // Horizontal Matches
        for (int x = 0; x < manaBoard.GetLength(0)-2; x++)
        {
            for (int y = 0; y < manaBoard.GetLength(1); y++)
            {
                if (manaBoard[x,y].mana && manaBoard[x+1, y].mana && manaBoard[x + 2, y].mana)
                if (manaBoard[x,y].mana.manaType == manaBoard[x+1,y].mana.manaType && manaBoard[x + 1, y].mana.manaType == manaBoard[x+2, y].mana.manaType)
                {
                    matchingMana.Add(manaBoard[x, y]);
                    matchingMana.Add(manaBoard[x + 1, y]);
                    matchingMana.Add(manaBoard[x + 2, y]);
                }
            }
        }
        // Vertical Matches
        for (int x = 0; x < manaBoard.GetLength(0); x++)
        {
            for (int y = 0; y < manaBoard.GetLength(1) - 2; y++)
            {
                if (manaBoard[x, y].mana && manaBoard[x, y + 1].mana && manaBoard[x, y + 2].mana)
                    if (manaBoard[x, y].mana.manaType == manaBoard[x, y + 1].mana.manaType && manaBoard[x, y + 1].mana.manaType == manaBoard[x, y + 2].mana.manaType)
                    {
                        matchingMana.Add(manaBoard[x, y]);
                        matchingMana.Add(manaBoard[x, y + 1]);
                        matchingMana.Add(manaBoard[x, y + 2]);
                    }
            }
        }
        return matchingMana;
    }

    IEnumerator SettleMana()
    {
        print("Settling Mana");
        // Iterate from left to right, bottom to top
        for (int y = 0; y < manaBoard.GetLength(1); y++)
        {
            for (int x = 0; x < manaBoard.GetLength(0); x++)
            {
                Debug.DrawLine(new Vector2(x-spacingX, y-spacingY), new Vector2(x - spacingX, y - spacingY - 1), Color.yellow, 1f);
                // Check if the current node is empty
                while (manaBoard[x,y].mana == null)
                {
                    //print("LOOPING");
                    // Find the next mana above the empty spot
                    for (int z = y; z < manaBoard.GetLength(1) - 1; z++)
                    {
                        if (manaBoard[x, z+1].mana != null)
                        {
                            manaBoard[x, z].mana = manaBoard[x, z + 1].mana;
                            manaBoard[x, z].mana.TargetPos = new Vector2(x - spacingX, z - spacingY);
                            manaBoard[x, z].mana.SetIndices(x, z);
                            manaBoard[x, z + 1].mana = null;
                        }
                    }
                    int randomIndex = Random.Range(0, GlobalData.Instance.ElementIcons.Count);

                    Vector2 position = new (x, manaBoard.GetLength(1)-1);
                    Mana mana = Instantiate(ManaPrefab, new Vector2(x - spacingX, manaBoard.GetLength(1) - spacingY), Quaternion.identity);
                    ManaPrefab.SetType(randomIndex);
                    mana.TargetPos = position - new Vector2(spacingX, spacingY);
                    mana.SetIndices(x, (int)position.y);
                    manaBoard[x, (int)position.y] = new(true, mana);
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return new WaitForSeconds(0.01f);
        PopMatches();
        yield break;
    }

    public void Swap(Vector2Int first, Vector2Int second)
    {
        try
        {
            // Set positions to target positions to prevent bugs
            (manaBoard[second.x, second.y].mana.transform.position, manaBoard[first.x, first.y].mana.transform.position) = (manaBoard[second.x, second.y].mana.TargetPos, manaBoard[first.x, first.y].mana.TargetPos);
            // Set target positions to the other's position
            (manaBoard[second.x, second.y].mana.TargetPos, manaBoard[first.x, first.y].mana.TargetPos) = (manaBoard[first.x, first.y].mana.transform.position, manaBoard[second.x, second.y].mana.transform.position);
            // Swap Mana Indexes
            (manaBoard[first.x, first.y].mana.xIndex, manaBoard[first.x, first.y].mana.yIndex, manaBoard[second.x, second.y].mana.xIndex, manaBoard[second.x, second.y].mana.yIndex) = (manaBoard[second.x, second.y].mana.xIndex, manaBoard[second.x, second.y].mana.yIndex, manaBoard[first.x, first.y].mana.xIndex, manaBoard[first.x, first.y].mana.yIndex);
            // Swap Mana Objects
            (manaBoard[second.x, second.y].mana, manaBoard[first.x, first.y].mana) = (manaBoard[first.x, first.y].mana, manaBoard[second.x, second.y].mana);
        }
        catch
        {
            print(first.ToString() + " and " + second.ToString());
        }
    }

}

