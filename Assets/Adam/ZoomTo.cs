using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTo : MonoBehaviour
{
    public Transform Target;
    public float Rate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Rate * Time.deltaTime * (Target.position - transform.position));
        if (Vector3.Distance(transform.position, Target.position) <= Rate * Time.deltaTime)
        {
            Destroy(this);
        }
    }
}
