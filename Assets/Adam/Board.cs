using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public int width = 8;
    public int height = 8;

    public float spacingX;
    public float spacingY;

    public Mana[] manaPrefabs;

    public Node[,] manaBoard;
    public GameObject manaBoardGO;

    public ArrayLayout arrayLayout;


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
                if (arrayLayout.rows[y].row[x])
                {
                    manaBoard[x, y] = new(false, null);
                }
                else
                {
                    int randomIndex = Random.Range(0, manaPrefabs.Length);

                    Mana mana = Instantiate(manaPrefabs[randomIndex], position, Quaternion.identity);
                    mana.targetPos = position;
                    mana.SetIndices(x, y);
                    manaBoard[x, y] = new(true, mana);
                }
            }
        }
        if (checkBoard())
        {
            Debug.Log("We have Matches let's re-create the board");
            //InitializeBoard();
        } else
        {
            Debug.Log("There are no matches, time to start game.");
        }
    }

    public bool checkBoard()
    {
        Debug.Log("Checking Board");
        bool hasMatched = false;

        List<Mana> manaToRemove = new();

        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (manaBoard[x, y].isUsable)
                {
                    Mana mana = manaBoard[x, y].mana.GetComponent<Mana>();
                    if (!mana.isMatched)
                    {
                        MatchResult matchedMana = IsConnected(mana);
                        if (matchedMana.connectedMana.Count >= 3)
                        {
                            manaToRemove.AddRange(matchedMana.connectedMana);
                            foreach (Mana mp in matchedMana.connectedMana)
                                mp.isMatched = true;
                            hasMatched = true;
                        }
                    }
                }
            }
        }
        return hasMatched;
    }

    MatchResult IsConnected(Mana mana)
    {
        List<Mana> connectedMana = new();

        connectedMana.Add(mana);

        CheckDirection(mana, new Vector2Int(1, 0), connectedMana);

        CheckDirection(mana, new Vector2Int(-1, 0), connectedMana);

        if (connectedMana.Count == 3)
        {
            Debug.Log("I have a normal horizontal match, the color of my match is:" + connectedMana[0].manaType);

            return new MatchResult
            {
                connectedMana = connectedMana,
                direction = MatchDirection.Horizontal
            };
        } //More than 3
        else if (connectedMana.Count > 3)
        {
            Debug.Log("I have a Long Horizontal match, the color of my match is:" + connectedMana[0].manaType);

            return new MatchResult
            {
                connectedMana = connectedMana,
                direction = MatchDirection.LongHorizontal
            };
        } //more than 4
        /*else if (connectedMana.Count > 4)
        {
            Debug.Log("I have a Super Horizontal match, the color of my match is:" + connectedMana[0].manaType);

            return new MatchResult
            {
                connectedMana = connectedMana,
                direction = MatchDirection.SuperHorizontal
            };
        }*/

        connectedMana.Clear();

        connectedMana.Add(mana);

        CheckDirection(mana, new Vector2Int(0, 1), connectedMana);

        CheckDirection(mana, new Vector2Int(0, -1), connectedMana);

        if (connectedMana.Count == 3)
        {
            Debug.Log("I have a normal Vertical match, the color of my match is:" + connectedMana[0].manaType);

            return new MatchResult
            {
                connectedMana = connectedMana,
                direction = MatchDirection.Vertical
            };
        } // more than 3
        else if (connectedMana.Count > 3)
        {
            Debug.Log("I have a Long Vertical match, the color of my match is:" + connectedMana[0].manaType);

            return new MatchResult
            {
                connectedMana = connectedMana,
                direction = MatchDirection.LongVertical
            };
        } // more than 4
        /*else if (connectedMana.Count > 4)
        {
            Debug.Log("I have a Super Vertical match, the color of my match is:" + connectedMana[0].manaType);

            return new MatchResult
            {
                connectedMana = connectedMana,
                direction = MatchDirection.SuperVertical
            };
        }*/
        else
        {
            return new MatchResult { connectedMana = connectedMana, direction = MatchDirection.None };
        }
    }

    void CheckDirection(Mana mana, Vector2Int direction, List<Mana> connectedMana)
    {
        ManaType manatype = mana.manaType;
        int x = mana.xIndex + direction.x;
        int y = mana.yIndex + direction.y;

        while (x >= 0 && x < width && y >= 0 && y < height)
        {
            if (manaBoard[x,y].isUsable)
            {
                Mana neighbourMana = manaBoard[x, y].mana.GetComponent<Mana>();

                if(!neighbourMana.isMatched && neighbourMana.manaType == manatype)
                {
                    connectedMana.Add((Mana)neighbourMana);
                    x += direction.x;
                    y += direction.y;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    public void Swap(Vector2Int first, Vector2Int second)
    {
        // Set positions to target positions to prevent bugs
        (manaBoard[second.x, second.y].mana.transform.position, manaBoard[first.x, first.y].mana.transform.position) = (manaBoard[second.x, second.y].mana.targetPos, manaBoard[first.x, first.y].mana.targetPos);
        // Set target positions to the other's position
        (manaBoard[second.x, second.y].mana.targetPos, manaBoard[first.x, first.y].mana.targetPos) = (manaBoard[first.x, first.y].mana.transform.position, manaBoard[second.x, second.y].mana.transform.position);
        // Swap Mana Indexes
        (manaBoard[first.x, first.y].mana.xIndex, manaBoard[first.x, first.y].mana.yIndex, manaBoard[second.x, second.y].mana.xIndex, manaBoard[second.x, second.y].mana.yIndex) = (manaBoard[second.x, second.y].mana.xIndex, manaBoard[second.x, second.y].mana.yIndex, manaBoard[first.x, first.y].mana.xIndex, manaBoard[first.x, first.y].mana.yIndex);
        // Swap Mana Objects
        (manaBoard[second.x, second.y].mana, manaBoard[first.x, first.y].mana) = (manaBoard[first.x, first.y].mana, manaBoard[second.x, second.y].mana);
    }

}


public class MatchResult
{
    public List<Mana> connectedMana;
    public MatchDirection direction;
}

public enum MatchDirection
{
    Vertical,
    Horizontal,
    LongVertical,
    LongHorizontal,
    SuperVertical,
    SuperHorizontal,
    None
}