using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicBulletScript : MonoBehaviour
{
    public int damage = 9;
    private GameObject Player;
    private float lifetime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            other.gameObject.GetComponent<PlayerStats>().health -= damage;
            Destroy(this.gameObject);
        }
    }
}
