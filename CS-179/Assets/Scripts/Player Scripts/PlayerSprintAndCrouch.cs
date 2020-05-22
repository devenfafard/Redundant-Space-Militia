using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprint_speed = 5f;
    public float move_speed = 5f;
    public float crouch_speed = 2f;

    private Transform look_root;
    private float stand_height = 1.6f;
    private float crouch_height = 1f;
    private bool is_crouching;

    private PlayerFootSteps player_footstep;
    private float sprint_volume = 1f;
    private float crouch_volume = 0.1f;
    private float walk_volume_min = 0.2f;
    private float walk_volume_max = 0.6f;
    private float walk_step_distance = 0.4f;
    private float sprint_step_distance = 0.25f;
    private float crouch_step_distance = 0.5f; //larger value because you step less often
    private PlayerStats player_stats;

    private float sprint_value = 100f;
    public float sprint_threshold = 5f;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        look_root = transform.GetChild(0); //Gets look root
        player_footstep = GetComponentInChildren<PlayerFootSteps>();
        player_stats = GetComponent<PlayerStats>();

    }

    // Start is called before the first frame update
    void Start()
    {
        player_footstep.volume_min = walk_volume_min;
        player_footstep.volume_max = walk_volume_max;
        player_footstep.step_distance = walk_step_distance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if(sprint_value > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !is_crouching)
            {
                playerMovement.speed = sprint_speed;
                player_footstep.step_distance = sprint_step_distance;
                player_footstep.volume_min = sprint_volume;
                player_footstep.volume_max = sprint_volume;
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !is_crouching)
        {
            playerMovement.speed = move_speed;
            player_footstep.volume_min = walk_volume_min;
            player_footstep.volume_max = walk_volume_max;
            player_footstep.step_distance = walk_step_distance;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !is_crouching)
        {
            sprint_value -= sprint_threshold * Time.deltaTime;

            if(sprint_value <= 0f)
            {
                sprint_value = 0f;
                playerMovement.speed = move_speed;
                player_footstep.volume_min = walk_volume_min;
                player_footstep.volume_max = walk_volume_max;
                player_footstep.step_distance = walk_step_distance;
            }

            player_stats.DisplayStaminaStats(sprint_value);
        }
        else
        {
            if(sprint_value != 100f)
            {
                sprint_value += (sprint_threshold / 2) * Time.deltaTime;
                player_stats.DisplayStaminaStats(sprint_value);
                if(sprint_value > 100f)
                {
                    sprint_value = 100f;
                }
            }
        }

    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C)) //if we press C
        {
            if (is_crouching) //stand up
            {
                look_root.localPosition = new Vector3(0f, stand_height, 0f);
                playerMovement.speed = move_speed;

                player_footstep.step_distance = walk_step_distance;
                player_footstep.volume_min = walk_volume_min;
                player_footstep.volume_max = walk_volume_max;

                is_crouching = false;
            }
            else //crouch
            {
                look_root.localPosition = new Vector3(0f, crouch_height, 0f);
                playerMovement.speed = crouch_speed;
                player_footstep.volume_min = crouch_volume;
                player_footstep.volume_max = crouch_volume;
                player_footstep.step_distance = crouch_step_distance;

                is_crouching = true;
            }

        }
    }
}
