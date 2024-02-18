using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverseer : MonoBehaviour
{
    public int currentCheckpoint = 0;
    public GameObject[] classes;
    public GameObject chosenClass;

    public int currentClass = 0;

    public GameObject[] Checkpoints;
    public GameObject chooseClassPanelGameObject;
    public GameObject classGfxs;
    public GameObject currentClassCheckPanel;

    void Awake()
    {
        for (int i = 0; i < 7; i++) {
            classes[i] = currentClassCheckPanel.transform.GetChild(i).gameObject;
            classGfxs.transform.position = classes[i].transform.position;
        }
    }

    void Start() {
        ChooseClassMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChooseClassMenu() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        chooseClassPanelGameObject.SetActive(true);
    }

    public void ScrollClassMenuRight() {
        if(currentClass != 6) {
            currentClass += 1;
            classGfxs.transform.position = classes[currentClass].transform.position;
        } else {
            currentClass = 0;
            classGfxs.transform.position = classes[0].transform.position;
        }
    }

    public void ScrollClassMenuLeft() {
        if(currentClass != 0) {
            currentClass -= 1;
            classGfxs.transform.position = classes[currentClass].transform.position;
        } else {
            currentClass = 6;
            classGfxs.transform.position = classes[6].transform.position;
        }
    }

    public void Select() {
        chosenClass = classes[currentClass];
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        chooseClassPanelGameObject.SetActive(false);
        Destroy(chooseClassPanelGameObject);
    }
}