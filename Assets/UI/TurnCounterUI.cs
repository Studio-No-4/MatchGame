using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text TurnCounterText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnTurnChange += UpdateVisuals;
        UpdateVisuals(GameManager.Turn);
    }

    public void UpdateVisuals(int turn)
    {
        TurnCounterText.text = turn.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
