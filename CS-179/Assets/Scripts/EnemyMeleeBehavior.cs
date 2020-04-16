using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EnemyState{ PATROL, CHASE, ATTACK}

public class EnemyMeleeBehavior : MonoBehaviour
{

    private EnemyState enemyState;

    public float walk_speed = 1f;
    public float run_speed = 1f;

    public float chase_distance = 2f;
    private float current_chase_dist = 0;

    public float attack_distance = 1;
    public float chase_after_attack_dist = 1f;

    public float patrol_radius_min = 1f;
    public float patrol_radius_max = 10f;

    private float patrol_timer;
    private float attack_timer;

    public float wait_before_attack = 2f;

    private Transform target;

    private void Awake()
    {
        //enemy_Anim = GetComponent<EnemyNavigator>();
        //navigation = GetComponent<NavMMeshAgent>();

        //target = GameObject.FindwithTag(Tag.PLAYER_TAG).Transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        enemy_State = EnemyState.Patrol;

        patrol_timer = patrol_For_This_Time;

        attack_timer = wait_before_attack;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == )
        {

        }

    }
}
