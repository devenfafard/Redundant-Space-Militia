using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenCoup : MonoBehaviour
{
    private GameObject chicken_1;
    private GameObject chicken_2;
    private GameObject chicken_3;
    private GameObject chicken_4;
    private GameObject chicken_5;

    private bool chicken_1_Found = false;
    private bool chicken_2_Found = false;
    private bool chicken_3_Found = false;
    private bool chicken_4_Found = false;
    private bool chicken_5_Found = false;


    private bool checkpoint_complete = false; //is the puzzle solved? 

    public string levelName; public GameObject playerController;//test line

    // Start is called before the first frame update
    void Start()
    {
        chicken_1 = GameObject.FindGameObjectWithTag("Chicken01");
        chicken_2 = GameObject.FindGameObjectWithTag("Chicken02");
        chicken_3 = GameObject.FindGameObjectWithTag("Chicken03");
        chicken_4 = GameObject.FindGameObjectWithTag("Chicken04");
        chicken_5 = GameObject.FindGameObjectWithTag("Chicken05");
    }

    // Update is called once per frame
    void Update()
    {
        check_Chickens();
    }

    private void OnTriggerEnter(Collider chicken)
    {
        if (chicken.gameObject == chicken_1)
        {
            chicken_1_Found = true;
        }
        if (chicken.gameObject == chicken_2)
        {
            chicken_2_Found = true;
        }
        if (chicken.gameObject == chicken_3)
        {
            chicken_3_Found = true;
        }
        if (chicken.gameObject == chicken_4)
        {
            chicken_4_Found = true;
        }
        if (chicken.gameObject == chicken_5)
        {
            chicken_5_Found = true;
        }
    }

    private void check_Chickens()
    {
        if (chicken_1_Found && chicken_2_Found && chicken_3_Found && chicken_4_Found && chicken_5_Found)
        {
            checkpoint_complete = true; //the puzzle is solved

            /*Vector3 rot = new Vector3(0f, -93.215f, 0f);
            Vector3 loc = new Vector3(-19, 1, 10);
            playerController.transform.eulerAngles = rot;
            playerController.transform.position = loc;*/

            SceneManager.LoadScene(levelName);
        }
    }

    public bool get_Checkpoint_1_Status()
    {
        return checkpoint_complete;
    }
}
