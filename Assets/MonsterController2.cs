using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController2 : MonoBehaviour {

    public List<Transform> patrolPosList;
    public Transform player;
    public int attackDistance;
    public int traceDistance;
    private NavMeshAgent nav;
    private Animator anim;
    private bool isDie = false;
    private int MonsterState = 0;
    private int patrolIndex = 0;
    private const int STATE_IDLE = 1;
    private const int STATE_TRACE = 2;
    private const int STATE_ATTACK = 3;
    private const int STATE_PATROL = 4;

    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();

        StartCoroutine("EnemyAction");
        StartCoroutine("EnemyAI");
    }

    IEnumerator EnemyAction()
    {
        while (!isDie)
        {
            float dist = Vector3.Distance(transform.position, player.position);
          
            if (dist < attackDistance)//当玩家进入敌人攻击范围时
            {
                MonsterState = STATE_ATTACK;
            }
            else if (dist < traceDistance)//当玩家进入敌人追踪范围时
            {
                MonsterState = STATE_TRACE;
            }
            else//当玩家不在敌人追踪范围时
            {
                MonsterState = STATE_PATROL;
            }
            yield return null;//等待下一帧再继续执行
        }
    }

    private void CheckPatrolDistance()
    {
        if (nav.remainingDistance < 0.5f)//如果达到寻路目标点，让怪物待机3秒钟
        {
            MonsterState = STATE_IDLE;//状态设为待机
            anim.SetBool("attack", false);//将动画攻击状态置为非攻击状态
            anim.SetFloat("speed",0);//将动画攻击状态置为待机状态
            Invoke("PatroNext",3); //3秒后执行IdleWait函数      
        }
    }
    //去往下一个寻路点
    private void PatroNext()
    {
        MonsterState = STATE_PATROL;//状态设为寻路
        CancelInvoke("PatroNext");//清理定时函数
        patrolIndex = patrolIndex % patrolPosList.Count;
        if (patrolPosList[patrolIndex])
        {
            nav.isStopped = false;
            nav.SetDestination(patrolPosList[patrolIndex].position);  //设置下一目标点      
            anim.SetFloat("speed", 2);//修改动画状态为行走

        }
        patrolIndex++;
    }

    IEnumerator EnemyAI()
    {
        while (!isDie)
        {
            switch (MonsterState)
            {
                case STATE_IDLE:
                    nav.isStopped = true;//告诉NavMeshAgent停止寻路
                    anim.SetBool("attack", false);
                    anim.SetFloat("speed", 0);
                    break;
                case STATE_PATROL:
                    CheckPatrolDistance();
                    break;
                case STATE_TRACE:
                    CancelInvoke("PatroNext");
                    nav.isStopped = false;//告诉NavMeshAgent开始寻路
                    nav.SetDestination(player.position);
                    anim.SetBool("attack", false);
                    anim.SetFloat("speed", 2);
                    break;
                case STATE_ATTACK:
                    CancelInvoke("PatroNext");
                    anim.SetBool("attack", true);
                    anim.SetFloat("speed", 2);
                    break;

            }
            yield return null;//等待下一帧再继续执行

        }
    }
}
