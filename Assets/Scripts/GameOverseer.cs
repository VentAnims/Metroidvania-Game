using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverseer : MonoBehaviour
{
    public int currentCheckpoint = 0;

    public int currentClass = 1;

    public GameObject[] Checkpoints;

    public GameObject Player;

    void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start() {
        Player.GetComponent<TheLightseekerDeityBehaviour>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
