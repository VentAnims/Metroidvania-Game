using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolChecker : MonoBehaviour
{
    [SerializeField] private int patrolNumber = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy" && other.gameObject.GetComponent<ChaserEnemyBehaviour>().curPatrol == patrolNumber) {
            other.gameObject.GetComponent<ChaserEnemyBehaviour>().reachedPatrol = true;
        }
    }
}
