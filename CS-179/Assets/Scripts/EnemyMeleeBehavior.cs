using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { PATROL, FOLLOW, ATTACK }

public class EnemyMeleeBehavior : MonoBehaviour
{
    private EnemyAnimation enemy_Anima;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_speed = 0.5f;
    public float run_speed = 2f;

    public float chase_distance = 6f;
    private float current_chase_dist = 0;

    public float attack_distance = 0.5f;
    public float follow_after_attack_dist = 2f;

    public float patrol_radius_min = 2f;
    public float patrol_radius_max = 20f;
    public float patrol_time_limit = 5f;

    private float patrol_timer;
    private float attack_timer;

    public float wait_before_attack = 2f;

    private Transform target;
    public GameObject attack_point;
    private GameObject gun;

    private Transform enemy;

    [SerializeField]
    private float enemy_damage = 5f;

    [SerializeField]
    private AudioSource shoot_sound;

    [SerializeField]
    private GameObject plasma_bullet;

    [SerializeField]
    private Transform plasma_bullet_start_position;

    private Rigidbody bullet;
    public float bullet_speed = 30f;
    public float deactivate_timer = 3f;

    void Awake()
    {
        enemy_Anima = GetComponent<EnemyAnimation>();
        navAgent = GetComponent<NavMeshAgent>();
        gun = GameObject.FindWithTag("AlienGun");
        target = GameObject.FindWithTag("Player").transform;
        enemy = GetComponent<Transform>();
        bullet = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        enemy_State = EnemyState.PATROL;
        patrol_timer = patrol_time_limit;
        attack_timer = wait_before_attack;

        current_chase_dist = chase_distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemy_State == EnemyState.FOLLOW)
        {
            Follow();
        }

        if (enemy_State == EnemyState.ATTACK)
        {
            enemy.LookAt(target);
            Attack();
        }

    }

    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walk_speed;

        patrol_timer += Time.deltaTime;

        if (patrol_timer > patrol_time_limit)
        {
            NewRandomDestination();

            patrol_timer = 0f;
        }

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anima.Walk(true);
        }
        else
        {
            enemy_Anima.Walk(false);
        }

        //when player gets close to enemy go to follow
        if (Vector3.Distance(transform.position, target.position) <= chase_distance)
        {
            enemy_Anima.Walk(false);
            enemy_State = EnemyState.FOLLOW;

            //we can play audio for enemy here if you want to play something
            //when enemy finds the player
        }
    }

    void Follow()
    {
        navAgent.isStopped = false;
        navAgent.speed = run_speed;

        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anima.Run(true);
        }
        else
        {
            enemy_Anima.Run(false);
        }

        //when enemy close enough to player then go to attack state
        // if not then 
        if (Vector3.Distance(transform.position, target.position) <= attack_distance)
        {
            enemy_Anima.Walk(false);
            enemy_Anima.Run(false);
            enemy_State = EnemyState.ATTACK;

            if (chase_distance != current_chase_dist)
            {
                chase_distance = current_chase_dist;
            }

            //we can play audio for enemy here if you want to play something
            //when enemy attacks the player
        }
        else if (Vector3.Distance(transform.position, target.position) > chase_distance)
        {
            enemy_Anima.Run(false);
            enemy_State = EnemyState.PATROL;
            patrol_timer = patrol_time_limit;

            if (chase_distance != current_chase_dist)
            {
                chase_distance = current_chase_dist;
            }

        }
    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_timer += Time.deltaTime;

        //attack once for the enemy
        if (attack_timer > wait_before_attack)
        {

            enemy_Anima.Attack();

            RaycastHit hit;
            Vector3 direction = target.position - enemy.position;

            if (Physics.Raycast(plasma_bullet_start_position.position, direction, out hit))
            {
                GameObject plasma = GameObject.Instantiate(plasma_bullet, plasma_bullet_start_position.position, transform.rotation);
                plasma.GetComponent<Rigidbody>().AddForce(direction * bullet_speed);
                
                if (hit.collider.gameObject.tag == Tags.PLAYER_TAG)
                {
                    hit.transform.GetComponent<HealthScript>().ApplyDamage(enemy_damage);
                }

            }

            attack_timer = 0f;

            shoot_sound.Play();
        }

        if (Vector3.Distance(transform.position, target.position) > attack_distance + follow_after_attack_dist)
        {
            enemy_State = EnemyState.FOLLOW;
        }



    }


    void NewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_radius_min, patrol_radius_max);

        Vector3 rand_direction = Random.insideUnitSphere * rand_Radius;
        rand_direction += transform.position;

        NavMeshHit navInside;

        NavMesh.SamplePosition(rand_direction, out navInside, rand_Radius, -1);

        navAgent.SetDestination(navInside.position);
    }

    void turnOnAttackPoint()
    {
        attack_point.SetActive(true);
    }

    void turnOffAttackPoint()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }

    public EnemyState Enemy_State
    {
        get; set;
    }

    void playShootSound()
    {
        
    }


}
