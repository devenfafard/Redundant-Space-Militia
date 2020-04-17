using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    //private EnemyAnimator enemy_animator;
    private NavMeshAgent nav_agent;
    //private EnemyController enemy_controller;

    public float health = 100f;
    public bool is_player;
    private bool is_dead;

    // Start is called before the first frame update
    void Awake()
    {
        /*
        if(Enemy){
            enemy_animator = GetComponent<EnemyAnimator>();
            enemy_controller = GetComponent<EnemyController>();
            nav_agent = GetComponent<NavMeshAgent>();

        //get enemy audio
        }

        if(is_player){

        }
        */
    }

    public void ApplyDamage(float damage)
    {

        // if we died don't execute the rest of the code
        if (is_dead)
            return;

        health -= damage;

        if (is_player)
        {
            // show the stats(display the health UI value)
            //player_Stats.Display_HealthStats(health);
        }
        /*
        if (Enemy)
        {
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
        }
        */

        if (health <= 0f)
        {

            //PlayerDied();

            is_dead = true;
        }

    } // apply damage
}
