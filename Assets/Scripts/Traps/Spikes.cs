using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Transform player;
    public int attackDamage = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Took Damage! - 1 Health");
            other.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}
