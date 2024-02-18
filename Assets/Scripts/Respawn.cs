using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private GameObject LastCheckpoint;
    private GameObject GameOverseer;

    void Start() {
        GameOverseer = GameObject.FindGameObjectWithTag("Overseer");
        LastCheckpoint = GameOverseer.GetComponent<GameOverseer>().Checkpoints[GameOverseer.GetComponent<GameOverseer>().currentCheckpoint];
    }


    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            other.transform.position = LastCheckpoint.transform.position;
        }
    }
}
