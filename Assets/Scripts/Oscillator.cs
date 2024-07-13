using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (period < Mathf.Epsilon) return;

        double tau = Math.PI * 2;
        float cycles = Time.time / period;
        float sin = Mathf.Sin((float)(cycles * tau));
        movementFactor = (sin + 1) / 2; //convert to range 0 to 1

        Vector3 offset = movementFactor * movementVector;
        transform.localPosition = startingPosition + offset;
    }
}
