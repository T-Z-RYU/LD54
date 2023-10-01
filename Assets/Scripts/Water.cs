using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterLowerSpeed;

    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.isAbsorbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.isAbsorbing = false;
        }
    }

    private void Update()
    {
        if (player.isAbsorbing)
        {
            transform.position -= new Vector3(0, waterLowerSpeed * Time.deltaTime, 0);
        }
    }
}
