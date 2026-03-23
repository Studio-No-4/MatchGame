using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "GlobalData", order = 1)]
public class GlobalData : ScriptableObject
{
    public static GlobalData Instance;

    public List<Sprite> ElementIcons = new();

    private void OnValidate()
    {
        Instance = this;
    }
}
