using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private readonly float duration = 0.7f;
    private float shakeDuration = 0;
    private float shakeMagnitude = 0.1f;
    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
    }

    public void Shake()
    {
        shakeDuration = duration;
    }

    private void Update()
    {
        if(shakeDuration > 0)
        {
            transform.position = initialPos + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0;
            transform.position = initialPos;
        }
    }
}
