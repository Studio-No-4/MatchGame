using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    bool PlayerReady = false;

    public Transform ManaCollection;
    [SerializeField] private Notification MegaNotification;


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
        print("Started Player Turn");
        MegaNotification.Notify("Player Turn");
        while (!PlayerReady)
        {
            yield return new WaitForEndOfFrame();
        }
        print("Player Turn Complete");
    }

    IEnumerator EnemyTurn()
    {
        print("Started Enemy Turn");
        MegaNotification.Notify("Enemy Turn");
        yield return new WaitForSeconds(1f);
        print("Enemy Turn Complete");
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
