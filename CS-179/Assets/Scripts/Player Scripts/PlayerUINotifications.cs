using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUINotifications : MonoBehaviour
{
    private GameObject first_gate;
    private GameObject second_gate;
    public Canvas game_intro;
    //private SpaceStation third_checkpoint;

    private bool intro_done = false;
    private bool first_checkpoint_complete = false;
    private bool first_checkpoint_done = false;
    private bool second_checkpoint_complete = false;
    private bool second_checkpoint_done = false;
    private bool third_checkpoint_complete = false;
    private bool third_checkpoint_done = false;

    [SerializeField]
    private float timer = 7f;
    private float time = 0f;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        first_gate = GameObject.FindGameObjectWithTag("First Gate");
        second_gate = GameObject.FindGameObjectWithTag("Canyon Gate");
        

    }

    // Update is called once per frame
    void Update()
    {
        DisplayGameIntro();
        CheckFirstGate();
        CheckSecondGate();
    }

    private void CheckFirstGate()
    {
        first_checkpoint_complete = first_gate.GetComponent<FirstGate>().GetFirstCheckpoint();
        if (first_checkpoint_complete && !first_checkpoint_done)
        {
            DisplayFirstCheckpoint();
        }

        first_checkpoint_done = true;
    }

    private void CheckSecondGate()
    {

    }

    private void DisplayGameIntro()
    {
        if (intro_done == false)
        {

            if (time < timer)
            {
                game_intro.gameObject.SetActive(true);
                time += Time.deltaTime;
            }
            else
            {
                game_intro.gameObject.SetActive(false);
                time = 0;
                intro_done = true;
            }
            
        }


    }

    private void DisplayFirstCheckpoint()
    {
        if (time < timer)
        {
            //game_intro.gameObject.SetActive(true);
            time += Time.deltaTime;
        }
        else
        {
            //game_intro.gameObject.SetActive(false);
            time = 0;
            first_checkpoint_done = true;
        }
    }

}
