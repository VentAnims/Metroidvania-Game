using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HbAttack : MonoBehaviour
{
    private GameObject Player;
    private GameObject GameOverseer;

    public int damage = 0;
    public int healing = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameOverseer = GameObject.FindGameObjectWithTag("Overseer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {
            other.GetComponent<EnemyStats>().health -= damage;
            Player.GetComponent<PlayerStats>().health += healing;
            this.gameObject.SetActive(false);
        }
    }
}
