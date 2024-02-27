using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchArtifact : MonoBehaviour
{
    public GameObject GameOverseer;
    public GameObject ArtifactsPrefab;
    public GameObject Player;
    //public GameObject LightseekerArtifact;
    //public GameObject OutsiderArtifact;
    //public GameObject Class3Artifact;
    //public GameObject PickedPos;
    public Animator SwitchAnimator;
    public bool isSpinning = false;
    public bool inPlace = true;
    public int artifactToHave = 1;
    public float switchCD;
    public bool canCountSwitchCD = true;
    public bool canSwitch = true;

    //[HideInInspector]
    //[HideInInspector]
    //[HideInInspector]

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameOverseer = this.gameObject;
        SwitchAnimator = ArtifactsPrefab.GetComponent<Animator>();
        //PickedPos.transform.position = LightseekerArtifact.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) && !isSpinning && canSwitch) {
            isSpinning = true;
            inPlace = false;
        }
        if(isSpinning) {
            Spin();
        }
    }

    //Lightseeker = 1
    //Outsider = 2
    //Class3 = 3

    void Spin() {
        if(canSwitch) {
            switch(artifactToHave) {
                case 1:
                    SwitchAnimator.SetBool("Switch1", true);
                    Player.GetComponent<TheOutsiderDeityBehaviour>().enabled = true;
                    GameOverseer.GetComponent<GameOverseer>().currentClass = 2;
                    Player.GetComponent<TheLightseekerDeityBehaviour>().enabled = false;
                    Player.GetComponent<TheLightseekerDeityBehaviour>().Auto1.SetActive(false);
                    Player.GetComponent<TheLightseekerDeityBehaviour>().Auto2.SetActive(false);
                    Player.GetComponent<TheLightseekerDeityBehaviour>().Auto3.SetActive(false);
                    canSwitch = false;
                    artifactToHave = 2;
                    break;
                case 2:
                    SwitchAnimator.SetBool("Switch2", true);
                    Player.GetComponent<Class3DeityBehaviour>().enabled = true;
                    GameOverseer.GetComponent<GameOverseer>().currentClass = 3;
                    Player.GetComponent<TheOutsiderDeityBehaviour>().enabled = false;
                    Player.GetComponent<TheOutsiderDeityBehaviour>().Auto1.SetActive(false);
                    Player.GetComponent<TheOutsiderDeityBehaviour>().Auto2.SetActive(false);
                    Player.GetComponent<TheOutsiderDeityBehaviour>().Auto3.SetActive(false);
                    canSwitch = false;
                    artifactToHave = 3;
                    break;
                case 3:
                    SwitchAnimator.SetBool("Switch3", true);
                    Player.GetComponent<TheLightseekerDeityBehaviour>().enabled = true;
                    GameOverseer.GetComponent<GameOverseer>().currentClass = 1;
                    Player.GetComponent<Class3DeityBehaviour>().enabled = false;
                    canSwitch = false;
                    artifactToHave = 1;
                    break;
            }
        }

        canCountSwitchCD = true;
        switchCD -= Time.deltaTime;

        if(switchCD < -5f && canCountSwitchCD) {
            switch(artifactToHave) {
                case 1:
                    SwitchAnimator.SetBool("Switch1", false);
                    SwitchAnimator.SetBool("Switch3", false);
                    canCountSwitchCD = false;
                    switchCD = 0f;
                    canSwitch = true;
                    isSpinning = false;
                    break;
                case 2:
                    SwitchAnimator.SetBool("Switch1", false);
                    SwitchAnimator.SetBool("Switch2", false);
                    canCountSwitchCD = false;
                    switchCD = 0f;
                    canSwitch = true;
                    isSpinning = false;
                    break;
                case 3:
                    SwitchAnimator.SetBool("Switch2", false);
                    SwitchAnimator.SetBool("Switch3", false);
                    canCountSwitchCD = false;
                    switchCD = 0f;
                    canSwitch = true;
                    isSpinning = false;
                    break;
            }
        }
    }
    void SwitchArtifactToHave() {
        //if(artifactToHave < 3) {
        //    artifactToHave++;
        //} else if(artifactToHave >= 3) {
        //    artifactToHave = 1;
        //}
    }
}
