using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HbAttack : MonoBehaviour
{
    private GameObject Player;
    private GameObject GameOverseer;

    private int damage;
    private int healing;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameOverseer = GameObject.FindGameObjectWithTag("Overseer");
        if(GameOverseer.GetComponent<GameOverseer>().chosenClass == GameOverseer.GetComponent<GameOverseer>().classes[2]) {
            damage = Player.GetComponent<TheLightseekerDeityBehaviour>().autoDamage;
            healing = Player.GetComponent<TheLightseekerDeityBehaviour>().autoHeal;
        }
        if(GameOverseer.GetComponent<GameOverseer>().chosenClass == GameOverseer.GetComponent<GameOverseer>().classes[1]) {
            damage = Player.GetComponent<TheOutsiderDeityBehaviour>().autoDamage;
            healing = Player.GetComponent<TheOutsiderDeityBehaviour>().autoHeal;
        }
        if(GameOverseer.GetComponent<GameOverseer>().chosenClass == GameOverseer.GetComponent<GameOverseer>().classes[0]) {
            damage = Player.GetComponent<TheLightseekerDeityBehaviour>().autoDamage;
            healing = Player.GetComponent<TheLightseekerDeityBehaviour>().autoHeal;
        }
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
