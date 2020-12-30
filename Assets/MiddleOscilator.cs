using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MiddleOscilator : MonoBehaviour
{    
    [SerializeField] Vector3 movementVector2;

    [Range(0,1)][SerializeField] float movementFactor2;

    [SerializeField] float period2;   
    

    Vector3 startingPos2;

    // Start is called before the first frame update
    void Start()
    {
        startingPos2 = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period2 <= Mathf.Epsilon)
            return;
        
        float cycles = Time.time / period2;
        float tau = Mathf.PI * 2;
        float rawSin = Mathf.Sin(cycles * tau);

        movementFactor2 = rawSin;

        Vector3 offset = movementVector2 * movementFactor2;
        transform.position = startingPos2 + offset;
        
    }
}
