using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action<int> OnTurnChange;
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

    public Transform ManaCollection;
    [SerializeField] private Notification MegaNotification;

    public Health Enemy;

    public Dictionary<ManaType, int> PlayerMana = new();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerMana[ManaType.Red] = 0;
        PlayerMana[ManaType.Green] = 0;
        PlayerMana[ManaType.Blue] = 0;
        PlayerMana[ManaType.White] = 0;
        PlayerMana[ManaType.Black] = 0;
        PlayerMana[ManaType.Skull] = 0;
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
        yield return new WaitForSeconds(1f);
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
