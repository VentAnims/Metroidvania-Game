using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLightseekerDeityBehaviour : MonoBehaviour
{
    [Header("Prefabs to Load")]
    public GameObject Player;
    public GameObject HitboxesPrefab;
    public GameObject GameOverseer;
    public List<GameObject> hitboxesLight;
    
    public GameObject Auto1;
    public GameObject Auto2;
    public GameObject Auto3;

    [Header("Cooldowns and dependencies")]
    private int attackNumber = 1;
    private float lastAttacktime = 0f;
    private bool canCountLastAttackTime = true;
    private bool attackFinished = true;
    private float attackCooldown = .4f;

    private bool canDash = true;
    //private float dashCooldown = 1f;

    [Header("Stats")]
    public int autoDamage = 7;
    public int ultDamage = 27;
    public int autoHeal = 0;
    public int ultHeal = 0;
    public int movementHeal = 2;

    void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameOverseer = GameObject.FindGameObjectWithTag("Overseer");
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
        if(Input.GetMouseButtonDown(0) && lastAttacktime < 0 && attackFinished) {
            PlayerAttack();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
            LightDash();
        }
    }

    void PlayerAttack() {
        lastAttacktime = -0.1f;
        attackFinished = false;
        if(attackNumber == 1) {
            Auto1.gameObject.SetActive(true);   
        }
        if(attackNumber == 2) {
            Auto2.gameObject.SetActive(true);
        }
        if(attackNumber > 2) {
            Auto3.gameObject.SetActive(true);
            attackNumber = 0;
        }
        attackNumber++;
        canCountLastAttackTime = false;
    }

    void AttackTimer() {
        if(canCountLastAttackTime) {
            lastAttacktime -= Time.deltaTime;
        }
        if(!attackFinished) {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown <= 0) {
                Auto1.gameObject.SetActive(false);
                Auto2.gameObject.SetActive(false);
                Auto3.gameObject.SetActive(false);
                canCountLastAttackTime = true;
                attackFinished = true;
                attackCooldown = 1f;
            }
        }
    }

    void LightDash() {
        //Put code here, before the canDash
        canDash = false;
        //Dash and apply heal :D
        //Make it start a dashCooldown and at the end enable canDash
    }
}
