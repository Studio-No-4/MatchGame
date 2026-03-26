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

    // Persistent list of player spells
    public static List<SpellData> PlayerSpells = new();
    [Tooltip("If true, ignores the static PlayerSpells variable.  Use when PlayerSpells cannot be initialized")]
    public bool IgnoreStaticSpells = false;

    public EnemyData EnemyData;
    public List<EnemyData> EnemyTypes = new();

    private void Awake()
    {
        Instance = this;
        // Probably shouldn't be in Awake, but prevents race-condition
        if (!IgnoreStaticSpells) Player.Spells = PlayerSpells;
        if (GlobalData && !GlobalData.Instance) GlobalData.Instance = GlobalData;

        // Randomize enemy and set relevant values
        EnemyData = EnemyTypes[Random.Range(0, EnemyTypes.Count)];
        Enemy.Health.MaxHP = EnemyData.Health;
        Enemy.Health.HP = EnemyData.Health;
        Enemy.Spells = EnemyData.StartingSpells;
        Enemy.CharacterArt.sprite = EnemyData.Image;
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

        foreach (RelicData relic in Game.Relics)
        {
            relic.OnCombatStart.Invoke();
        }
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
        Player.StartTurn();
        foreach (RelicData relic in Game.Relics)
        {
            relic.OnTurnStart.Invoke();
        }
        PlayerReady = false;
        GridLocked = false;
        print("Started Player Turn");
        MegaNotification.Notify("Player Turn");
        while (!PlayerReady || !Board.Instance.Stable)
        {
            List<Vector4> validMoves = Board.Instance.GetAllValidMoves();
            // If there are no valid moves, reset the board
            while (validMoves.Count == 0)
            {
                Board.Instance.ResetBoard();
                while (!Board.Instance.Stable) yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
        Player.EndTurn();
        print("Player Turn Complete");
        Turn++;
    }

    IEnumerator EnemyTurn()
    {
        Enemy.StartTurn();
        print("Started Enemy Turn");
        MegaNotification.Notify("Enemy Turn");
        yield return new WaitForSeconds(1.5f);
        List<Vector4> validMoves = Board.Instance.GetAllValidMoves();
        /*foreach (Vector4 move in validMoves)
        {
            print("VALID MOVE: " + move.ToString());
        }*/
        // If there are no valid moves, reset the board
        while (validMoves.Count == 0)
        {
            //print("reset");
            Board.Instance.ResetBoard();
            while (!Board.Instance.Stable) yield return new WaitForEndOfFrame();
        }
        Vector4 selectedMove = validMoves[Random.Range(0, validMoves.Count)];
        Vector2Int start = new((int)selectedMove.x, (int)selectedMove.y);
        Vector2Int end = new((int)selectedMove.z, (int)selectedMove.w);
        StartCoroutine(Board.Instance.Swap(start, end));
        //while (!Board.Instance.Stable) yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.5f);
        Board.Instance.PopMatches();
        while (!Board.Instance.Stable)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(.1f);
        List<SpellData> CastableSpells = new();
        foreach (SpellData spell in Enemy.Spells)
        {
            if (spell.CanCast(Enemy))
            {
                CastableSpells.Add(spell);
            }
        }
        if (CastableSpells.Count > 0) CastableSpells[Random.Range(0, CastableSpells.Count)].Cast(Enemy);
        yield return new WaitForSeconds(.1f);
        Enemy.EndTurn();
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