﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    //todo remove from inspector
    [SerializeField][Range(0,1)]
    float movementFactor; //0 for not moved, 1 for fully moved. 

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f; //about 6.28
        float RawSinWave = Mathf.Sin(cycles * tau);

        print(RawSinWave);
        movementFactor = RawSinWave / 2f * 0.5f; 
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset; 
    }
}
