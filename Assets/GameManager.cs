using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event System.Action<int> OnTurnChange;
    private static int _turn = 1;
    public static int Turn
    {
        get => _turn;
        set
        {
            _turn = value;
            OnTurnChange?.Invoke(_turn);
        }
    }

    bool PlayerReady = false;
    public bool GridLocked = false;

    public GlobalData GlobalData;

    [SerializeField] private Notification MegaNotification;


    public Character Player;
    public Character Enemy;

    private void Awake()
    {
        Instance = this;
        if (GlobalData && !GlobalData.Instance) GlobalData.Instance = GlobalData;
    }

    public static Character CurrentCharacter()
    {
        if (Turn % 2 == 1) return Instance.Player;
        else return Instance.Enemy;
    }
    public static Character OpposingCharacter()
    {
        if (Turn % 2 == 0) return Instance.Player;
        else return Instance.Enemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnRoutine());
    }

    IEnumerator TurnRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(PlayerTurn());
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(EnemyTurn());
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator PlayerTurn()
    {
        PlayerReady = false;
        GridLocked = false;
        print("Started Player Turn");
        MegaNotification.Notify("Player Turn");
        while (!PlayerReady)
        {
            yield return new WaitForEndOfFrame();
        }
        print("Player Turn Complete");
        Turn++;
    }

    IEnumerator EnemyTurn()
    {
        print("Started Enemy Turn");
        MegaNotification.Notify("Enemy Turn");
        yield return new WaitForSeconds(.7f);
        List<Vector4> validMoves = Board.Instance.GetAllValidMoves();
        /*foreach (Vector4 move in validMoves)
        {
            print("VALID MOVE: " + move.ToString());
        }*/
        Vector4 selectedMove = validMoves[Random.Range(0, validMoves.Count)];
        Vector2Int start = new((int)selectedMove.x, (int)selectedMove.y);//new(Random.Range(0, 8), Random.Range(0, 8));
        Vector2Int end = new((int)selectedMove.z, (int)selectedMove.w);/*start;
        if (Random.Range(0, 100) < 50)
        {
            if (Random.Range(0, 100) < 50)
            {
                end += Vector2Int.up;
            }
            else
            {
                end += Vector2Int.down;
            }
        }
        else
        {
            if (Random.Range(0, 100) < 50)
            {
                end += Vector2Int.right;
            }
            else
            {
                end += Vector2Int.left;
            }
        }*/
        Board.Instance.Swap(start, end);
        Board.Instance.PopMatches();
        yield return new WaitForSeconds(.3f);
        print("Enemy Turn Complete");
        Turn++;
    }

    public void ReadyPlayer()
    {
        PlayerReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerReady = true;
        }
    }
}