using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool PlayerReady = false;

    [SerializeField] private Notification MegaNotification;

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
