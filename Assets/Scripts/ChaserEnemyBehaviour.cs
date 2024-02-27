using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public int curPatrol = 1;
    [HideInInspector] public bool reachedPatrol = false;


    [Header("Behaviour Dependencies")]
    public GameObject[] Patrols;
    public int MaxPatrols = 2;
    [HideInInspector] public bool chasingPlayer = false;

    [Header("Player Hit")]
    public float maxShieldTime = .4f;
    private float shieldTime;
    private bool canCountShieldTime = false;

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
        EnemyBehaviour();
        if(canCountShieldTime) {
            ShieldTimer();
        }
    }

    void EnemyBehaviour() {
        //Patrol
        if(!reachedPatrol && !chasingPlayer) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Patrols[curPatrol - 1].transform.position.x, transform.position.y), this.gameObject.GetComponent<EnemyStats>().speed * Time.deltaTime);
        }
        if(reachedPatrol && !chasingPlayer) {
            if(curPatrol >= MaxPatrols) {
                curPatrol = 1;
            } else {
                curPatrol++;
            }
            reachedPatrol = false;
        }

        //See enemy, chase enemy
        if(chasingPlayer) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, transform.position.y), this.gameObject.GetComponent<EnemyStats>().speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            if(!canCountShieldTime) {
                print("Damaged");
                Player.GetComponent<PlayerStats>().health -= this.gameObject.GetComponent<EnemyStats>().damage;
                shieldTime = maxShieldTime;
                canCountShieldTime = true;
            }
            Player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 14f, ForceMode2D.Impulse);
        }
    }

    void ShieldTimer() {
        shieldTime -= Time.deltaTime;
        if(shieldTime <= 0f) {
            canCountShieldTime = false;
        }
    }
}
