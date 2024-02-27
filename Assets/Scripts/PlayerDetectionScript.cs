using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionScript : MonoBehaviour
{
    [SerializeField]private GameObject EnemyAttachedTo;
    [SerializeField]private bool enemyIsShooter = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = EnemyAttachedTo.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            if(enemyIsShooter) {
                EnemyAttachedTo.GetComponent<ShooterEnemyBehaviour>().shootingPlayer = true;
            } else {
                EnemyAttachedTo.GetComponent<ChaserEnemyBehaviour>().chasingPlayer = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            if(enemyIsShooter) {
                EnemyAttachedTo.GetComponent<ShooterEnemyBehaviour>().shootingPlayer = false;
            } else {
                EnemyAttachedTo.GetComponent<ChaserEnemyBehaviour>().chasingPlayer = false;
            }
        }
    }
}
