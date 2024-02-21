using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOutsiderDeityBehaviour : MonoBehaviour
{
    //Delete everything and replace, using TheLightSeekerDeityBehaviour script :D
    public GameObject Player;
    public GameObject HitboxesPrefab;
    public GameObject GameOverseer;
    public List<GameObject> hitboxesLight;
    
    public GameObject Auto1;
    public GameObject Auto2;
    public GameObject Auto3;

    private bool canAttack = true;
    private int attackNumber = 1;
    private float lastAttacktime = 0f;
    private bool canCountLastAttackTime = true;
    private float attackCooldown = 0.3f;

    public int autoDamage = 12;
    public int ultDamage = 42;
    public int autoHeal = 1;
    public int ultHeal = 0;
    public int movementHeal = 0;

    void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameOverseer = GameObject.FindGameObjectWithTag("Overseer");
        Instantiate(GameOverseer.GetComponent<GameOverseer>().LightseekerHitboxes, Player.transform.position, Quaternion.identity);
        HitboxesPrefab = GameObject.FindGameObjectWithTag("Hitboxes");
        HitboxesPrefab.transform.parent = Player.transform;
        Auto1 = HitboxesPrefab.transform.GetChild(0).gameObject;
        Auto2 = HitboxesPrefab.transform.GetChild(1).gameObject;
        Auto3 = HitboxesPrefab.transform.GetChild(2).gameObject;
        Auto1.SetActive(false);
        Auto2.SetActive(false);
        Auto3.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AttackTimer();
        if(Input.GetMouseButtonDown(0) && canAttack && lastAttacktime < 0) {
            PlayerAttack();
        }
    }

    void PlayerAttack() {
        canAttack = false;
        canCountLastAttackTime = false;
        lastAttacktime = 0.4f;
        if(attackNumber == 1) {
            Auto1.gameObject.SetActive(true);
            StartCoroutine(WaitForSec(attackCooldown));
            Auto1.gameObject.SetActive(false);
        }
        if(attackNumber == 2) {
            Auto2.gameObject.SetActive(true);
            StartCoroutine(WaitForSec(attackCooldown));
            Auto2.gameObject.SetActive(false);
        }
        if(attackNumber == 3) {
            Auto3.gameObject.SetActive(true);
            StartCoroutine(WaitForSec(attackCooldown));
            Auto3.gameObject.SetActive(false);
        }
        if(attackNumber == 3) {
            attackNumber = 1;
        } else {
            attackNumber++;
        }
        canAttack = true;
        canCountLastAttackTime = true;
    }

    void AttackTimer() {
        if(canCountLastAttackTime) {
            lastAttacktime -= Time.deltaTime;
        }
    }

    IEnumerator WaitForSec(float time) {
        yield return new WaitForSeconds(time);
    }
}
