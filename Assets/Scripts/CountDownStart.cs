using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownStart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameController gameController;

    // Update is called once per frame
    void Update()
    {
        countText.text = Mathf.Round(gameController.countDownStartTime).ToString();
        if (gameController.isGameStart)
        {
            gameObject.SetActive(false);
        }
    }
}
