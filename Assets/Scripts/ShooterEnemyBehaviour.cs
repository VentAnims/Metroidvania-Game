using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public bool shootingPlayer = false;
    private GameObject Player;
    public GameObject toxicPrefab;
    public float shotSpeed = 10f;
    public float maxShotCd = 1;
    private float shotCd = 1;
    private bool canCountShotCd = false;
    private bool canShoot = true;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        shotCd = maxShotCd;
    }

    // Update is called once per frame
    void Update()
    {
        if(shootingPlayer) {
            LookAtPlayer();
        }

        if(shootingPlayer && canShoot) {
            ShooterBehaviour();
        }

        if(!canShoot || canCountShotCd) {
            ShootTimer();
        }
    }


    void LookAtPlayer() {
        //Look towards player
        Vector3 direction = Player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;

        if(Player.GetComponent<PlayerMovement>().IsFacingRight) {
            float finalAngle = angle + -80f;
            rb.rotation = finalAngle;
        } else {
            float finalAngle = angle + -100f;
            rb.rotation = finalAngle;
        }

    }

    void ShooterBehaviour() {
        //Shoot
        var shot = Instantiate(toxicPrefab, transform.position, transform.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = transform.up * shotSpeed;
        canShoot = false;
        canCountShotCd = true;
    }

    void ShootTimer() {
        if(shotCd >= 0) {
            shotCd -= Time.deltaTime;
        } else if(shotCd < 0) {
            canCountShotCd = false;
            canShoot = true;
            shotCd = maxShotCd;
        }

    }
}
