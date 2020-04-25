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
    private GameObject gate;
    

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

    void Start()
    {
        gate = GameObject.FindGameObjectWithTag("Canyon Gate");
    }

    void Start()
    {
        gate = GameObject.FindGameObjectWithTag("Canyon Gate");
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
<<<<<<< Updated upstream
=======

    void PlayerDied()
    {
        if (is_alien)
        {
            nav_agent.velocity = Vector3.zero;
            nav_agent.isStopped = true;
            enemy_controller.enabled = false;

            enemy_animator.Dead();
            KillCounter();

            //startCoroutine
            //EnemyManager to spawn more enemies

        }

        if (is_player)
        {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyMeleeBehavior>().enabled = false;
            }


            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().getCurrentSelectedWeapon().gameObject.SetActive(false);

        }

        if (tag == Tags.PLAYER_TAG)
        {

            Invoke("RestartGame", 3f);

        }
        else
        {

            Invoke("TurnOffGameObject", 3f);

        }

    } // player died

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    private void KillCounter()
    {
        gate.GetComponent<OpenGate>().UpdateKills();
    }
    
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
}
