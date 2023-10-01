using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    public int addingValue;

    [SerializeField] private Player player;
    [SerializeField] private Slider slider;
    [SerializeField] private GameController gameController;

    private void Update()
    {
        if (player.isAbsorbing)
        {
            slider.value += addingValue * Time.deltaTime;
            if (slider.value >= slider.maxValue)
            {
                gameController.Win();
            }
        }
    }
}
