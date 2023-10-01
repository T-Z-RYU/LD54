using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameController gameController;

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time\n" + Mathf.Round(gameController.levelTime).ToString();
    }
}
