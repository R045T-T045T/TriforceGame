using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
                PlayerController playerController = collision.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.ApplySpeed();
                    Debug.Log("Detective detected");
                }
        }
    }
}
