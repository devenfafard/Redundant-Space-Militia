using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBase1 : MonoBehaviour
{
    private GameObject powerCell;
    private bool powerCell_Found = false;
    public bool powerCell1_complete = false;

    public Canvas Second_powercell;
    public bool display_done = false;
    [SerializeField]
    private float timer = 7f;
    private float time = 0f;
    private bool dropped = false;


    // Start is called before the first frame update
    void Start()
    {
        powerCell = GameObject.FindGameObjectWithTag("PowerCell1");
    }

    // Update is called once per frame
    void Update()
    {
        check_PowerCell();

        if (powerCell_Found && !display_done && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            dropped = true;
        }
    }

    private void OnTriggerEnter(Collider PowerStation)
    {
        if (PowerStation.gameObject == powerCell)
        {
            powerCell_Found = true;

        }
    }

    private void check_PowerCell()
    {
        if (powerCell_Found && dropped)
        {
            powerCell1_complete = true;

            if (display_done == false)
            {
                if (time < timer)
                {
                    Second_powercell.gameObject.SetActive(true);
                    time += Time.deltaTime;
                }
                else
                {
                    Second_powercell.gameObject.SetActive(false);
                    time = 0;
                    display_done = true;

                }
            }
        }
    }

    public bool checkPowerCell1Status()
    {
        return powerCell1_complete;
    }
}
