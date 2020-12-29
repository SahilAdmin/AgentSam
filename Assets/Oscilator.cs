using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{    
    [SerializeField] Vector3 movementVector;

    [Range(0,1)][SerializeField] float movementFactor;

    [SerializeField] float period;   
    

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
            return;
        
        float cycles = Time.time / period;
        float tau = Mathf.PI * 2;
        float rawSin = Mathf.Sin(cycles * tau);

        movementFactor = (rawSin / 2) + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
        
    }
}
