
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

                    Mana mana = Instantiate(manaPrefabs[randomIndex], position + Vector2.up * Random.Range(100,1000)/10f, Quaternion.identity);
                    mana.targetPos = position;
                    mana.SetIndices(x, y);
                    manaBoard[x, y] = new(true, mana);
                }
            }
        }
        StartCoroutine(PopCycle());
    }

    public IEnumerator PopCycle()
    {
        int poppedMana = PopMatches();
        while (poppedMana != 0)
        {
            print("Before " + poppedMana.ToString());
            yield return StartCoroutine(SettleMana());
            poppedMana = PopMatches();
            print("After " + poppedMana.ToString());
        }
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
        return poppedMana;
        //StartCoroutine(SettleMana());
    }

    public List<Node> GetMatchingMana()
    {
        List<Node> matchingMana = new();
        // Horizontal Matches
        print(manaBoard.GetLength(0).ToString() + manaBoard.GetLength(1).ToString());
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
                    print("LOOPING");
                    // Find the next mana above the empty spot
                    for (int z = y; z < manaBoard.GetLength(1) - 1; z++)
                    {
                        //if (manaBoard[x, z].mana != null)
                        //{
                            // Move all mana above the empty spot downwards

                            manaBoard[x, z].mana = manaBoard[x, z + 1].mana;
                            manaBoard[x, z].mana.targetPos = new Vector2(x-spacingX, z - spacingY);
                            manaBoard[x, z].mana.SetIndices(x, z);
                            manaBoard[x, z+1].mana = null;
                        //}
                    }
                    int randomIndex = Random.Range(0, manaPrefabs.Length);

                    Vector2 position = new (x, manaBoard.GetLength(1)-1);
                    Mana mana = Instantiate(manaPrefabs[randomIndex], new Vector2(x - spacingX, manaBoard.GetLength(1) - spacingY), Quaternion.identity);
                    mana.targetPos = position - new Vector2(spacingX, spacingY);
                    mana.SetIndices(x, (int)position.y);
                    manaBoard[x, (int)position.y] = new(true, mana);
                    //yield return new WaitForSeconds(0.5f);
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield break;
    }

    public void Swap(Vector2Int first, Vector2Int second)
    {
        try
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
        catch
        {
            print(first.ToString() + " and " + second.ToString());
        }
    }

}

