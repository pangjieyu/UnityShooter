using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {
    public Transform player;
    public int attackDistance;
    public int traceDistance;
    private NavMeshAgent nav;
    private Animator anim;
    private bool isDie = false;
    private int MonsterState = 0;
    private const int STATE_IDLE = 1;
    private const int STATE_TRACE = 2;
    private const int STATE_ATTACK = 3;
	// Use this for initialization
	void Start () {
        nav = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();

        StartCoroutine("EnemyAction");
        StartCoroutine("EnemyAI");
	}
	
    IEnumerator EnemyAction()
    {
        while(!isDie)
        {
            float dist = Vector3.Distance(transform.position, player.position);
            Debug.Log(dist);
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
                MonsterState = STATE_IDLE;
            }
        yield return null;//等待下一帧再继续执行
        }
    }

    IEnumerator EnemyAI()
    {
        while(!isDie)
        {
            switch (MonsterState)
            {
                case STATE_IDLE:
                    nav.isStopped = true ;//告诉NavMeshAgent停止寻路
                    anim.SetBool("attack", false);                
                    anim.SetFloat("speed", 0);                  
                    break;
                case STATE_TRACE:
                    nav.isStopped = false;//告诉NavMeshAgent开始寻路
                    nav.SetDestination(player.position);
                    anim.SetBool("attack", false);
                    anim.SetFloat("speed", 2);
                    break;
                case STATE_ATTACK:
                    anim.SetBool("attack", true);
                    anim.SetFloat("speed", 2);
                    break;

            }
        yield return null;//等待下一帧再继续执行

        }
    }
}
