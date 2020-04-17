using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTags
{
    public const string WALK_PARAM = "Walk";
    public const string RUN_PARAM = "Run";
    public const string ATTACK_PARAM = "Attack";
    public const string DEAD_PARAM = "Dead";
    public const string SHOOT_PARAM = "Shoot";

}


public class EnemyAnimation : MonoBehaviour
{

    private Animator Anima;
    // Start is called before the first frame update
    void Awake()
    {
        Anima = GetComponent<Animator>();

    }

    // Update is called once per frame
    public void Walk(bool walk)
    {
        Anima.SetBool(AnimationTags.WALK_PARAM, walk);
    }

    public void Run(bool run)
    {
        Anima.SetBool(AnimationTags.WALK_PARAM, run);
    }

    public void Attack()
    {
        Anima.SetTrigger(AnimationTags.ATTACK_PARAM);
    }
}
