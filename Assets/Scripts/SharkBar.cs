using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Shark shark;

    private void Start()
    {
        slider.maxValue = shark.maxBiteValue;
    }

    private void Update()
    {
        slider.value = shark.biteValue;
    }
}
