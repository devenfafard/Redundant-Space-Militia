using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimation enemy_animator;
    private NavMeshAgent nav_agent;
    private EnemyMeleeBehavior enemy_controller;

    public float health = 100f;
    public bool is_player;
    public bool is_alien;
    private bool is_dead;

    // Start is called before the first frame update
    void Awake()
    {
        
        if(is_player || is_alien){
            enemy_animator = GetComponent<EnemyAnimation>();
            enemy_controller = GetComponent<EnemyMeleeBehavior>();
            nav_agent = GetComponent<NavMeshAgent>();

        //get enemy audio
        }

        if(is_player){

        }
       
    }

    public void ApplyDamage(float damage)
    {

        if (is_dead)
            return;

        health = health - damage;

        if (is_player)
        {
            // show the stats(display the health UI value)
            //player_Stats.Display_HealthStats(health);
        }

        
        if (is_alien)
        {
            if (enemy_controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_controller.chase_distance = 45f;
            }
        }
        
        

        if (health <= 0f)
        {

            PlayerDied();

            is_dead = true;
        }

    } // apply damage

    void PlayerDied()
    {
        if (is_alien)
        {
            nav_agent.velocity = Vector3.zero;
            nav_agent.isStopped = true;
            enemy_controller.enabled = false;

            enemy_animator.Dead();

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("LEVEL01");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

}

