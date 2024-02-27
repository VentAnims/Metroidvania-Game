using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public int health = 25;
    public int damage = 3;
    public int speed = 6;

    private Rigidbody2D rb;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            this.gameObject.transform.parent.GetComponent<KillEnemyScript>().Killed();
        }
    }

}
