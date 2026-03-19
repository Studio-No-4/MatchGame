using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image Bar;
    public Health Health;
    [SerializeField] private float Speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, Health.HP / (float)Health.MaxHP, Time.deltaTime * Speed);
    }
}
