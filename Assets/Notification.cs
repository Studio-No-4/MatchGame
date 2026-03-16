using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public TMP_Text Text;
    public Image Background;
    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// Call for a fire-and-forget notification.  If you want to notify and wait for the notification to complete, try NotifyRoutine() instead
    /// </summary>
    /// <param name="text"></param>
    public void Notify(string text="")
    {
        StartCoroutine(NotifyRoutine(text));
    }

    /// <summary>
    /// Plays the notification animation until it is completed.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator NotifyRoutine(string text="")
    {
        UpdateVisuals(text);
        Animator.Play("Notify");
        //transform.GetChild(0).gameObject.SetActive(true);
        /*while (Animator.GetCurrentAnimatorStateInfo(0).IsName("Notify"))
        {
            
        }*/
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length * Animator.GetCurrentAnimatorStateInfo(0).speed);
        //transform.GetChild(0).gameObject.SetActive(false);
    }

    public void UpdateVisuals(string text="")
    {
        Text.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
