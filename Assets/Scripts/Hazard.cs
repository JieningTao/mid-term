using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Hazard : MonoBehaviour
{

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player killed!");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerMoveScript player = collision.GetComponent<PlayerMoveScript>();
            player.killed();
            audioSource.Play();
        }
        else
            Debug.Log("something else entered hazard");
    }


}
