using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [HideInInspector]
    public bool isChasing = false;
    [HideInInspector]
    public GameObject player;
    public Animator animator;

    public bool canWait;
    [Range(0f, 1f)]
    public float swicthProbability;
    public GameObject[] patrolPositions;
    public bool isMelee;

    public bool isRange;
    NavMeshAgent agent;
    int currPatrolIndex;
    bool isTravelling;
    bool isWaiting = true;
    float waitTimer;
    bool patrolForward = true;
    [SerializeField]
    public float Distance;
    bool isAttacking = false;
    bool attackDelayed = false;
    [Header("Enemy Damage Stats")]
    public float attackTimer = 1.5f;
    public float attackDamage = 6f;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        if (patrolPositions != null && patrolPositions.Length >= 2)
        {
            currPatrolIndex = 0;
            SetDestination();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking && !attackDelayed)
        {
            StartCoroutine(Attack(attackDamage));
        }
        //need detection script to change this variable.
        if (isChasing)
        {
            // waitTimer += Time.deltaTime;
            // if (waitTimer >= 1f)

           //{
                
                if (isMelee)
                {
                    waitTimer += Time.deltaTime;

                    if (waitTimer >= 1f)
                    {
                        agent.speed = 3.5f;
                        Distance = UnityEngine.Vector3.Distance(agent.transform.position, player.transform.position);

                        if (Distance > 3)
                        {

                            agent.SetDestination(player.transform.position);
                            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Walk_Cycle_1")) animator.SetTrigger("Walk_Cycle_1");
                            isAttacking = false;
                        }
                        else
                        {
                            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fight_Idle_1")) animator.SetTrigger("Fight_Idle_1");
                            agent.SetDestination(agent.transform.position);
                            isAttacking = true;
                            waitTimer = 0f;

                        }

                    }

                }else if (isRange){

                    waitTimer += Time.deltaTime;

                    if (waitTimer >= 1f)
                    {

                        Distance = Vector3.Distance(agent.transform.position, player.transform.position);

                        if (Distance > 6)
                        {
                            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Walk_Cycle_1")) animator.SetTrigger("Walk_Cycle_1");
                            agent.SetDestination(player.transform.position);

                        }
                        else
                        {
                            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Fight_Idle_1")) animator.SetTrigger("Fight_Idle_1");
                            agent.SetDestination(agent.transform.position);

                            waitTimer = 0f;

                        }
                    }
                }
            //}
        }
        else 
        {
            if (isTravelling && agent.remainingDistance <= 1f)
            {
                isTravelling = false;

                if (canWait)
                {
                    animator.SetTrigger("Rest");
                    isWaiting = true;
                    waitTimer = 0f;
                }
                else
                {
                    //ChangePatrolPoint();
                    SetDestination();
                }
            }

            if (isWaiting)
            {
                waitTimer += Time.deltaTime;
                float totalWaitTime = Random.Range(1f, 10f);
                if (waitTimer >= totalWaitTime)
                {
                    isWaiting = false;
                    //ChangePatrolPoint();
                    SetDestination();
                }
            }
        }
    }

    void SetDestination()
    {
        if (patrolPositions.Length != 0)
        {
            print("!");
            Vector3 targetPos = patrolPositions[currPatrolIndex].transform.position;
            //create some random range to choose instead of exact point.
            float x = targetPos.x + Random.Range(-1f, 1f);
            float z = targetPos.z + Random.Range(-1f, 1f);
            targetPos.Set(x, targetPos.y, z);
            agent.SetDestination(targetPos);
            isTravelling = true;
        }
        else
        {
            //Enemies should wander when not engaged with the player
            Vector3 wanderPos = transform.position;
            float x = wanderPos.x + Random.Range(-3f, 3f);
            float z = wanderPos.z + Random.Range(-3f, 3f);
            wanderPos.Set(x, wanderPos.y, z);
            agent.SetDestination(wanderPos);
            isTravelling = true;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Walk_Cycle_2")) animator.SetTrigger("Walk_Cycle_2");
        }
    }

    IEnumerator Attack(float damage)
    {
        attackDelayed = true;
        print("attack");
        //deal damage to player
        yield return new WaitForSeconds(attackTimer);
        player.GetComponent<PlayerHealth>().DamagePlayer(damage);
        this.transform.GetComponent<AudioSource>().PlayOneShot(AudioManager.Instance.alienAttackSound);
        attackDelayed = false;
        int randomAttack = Random.Range(1, 5);
        string key = "Attack_";
        string newKey = string.Concat(key, randomAttack);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(newKey)) animator.SetTrigger(newKey);
    }

    void ChangePatrolPoint()
    {
        float switchChance = Random.Range(0f, 1f);
        //Debug.Log(switchChance+"****");
        if (switchChance <= swicthProbability)
        {
            patrolForward = !patrolForward;
        }

        //branching patrol
        // if (patrolPositions[currPatrolIndex].name.Contains('-'))
        // {
        //     currPatrolIndex -= 1 + int.Parse(patrolPositions[currPatrolIndex].name.Split('-')[1]);
        // }
        // else
        // {        
        //     Debug.Log(currPatrolIndex);    
        //     int childCount = int.Parse(patrolPositions[currPatrolIndex].name.Split(',')[1]);
        //     float chance = Random.Range(0f, 1f);
        //     //Go to branching path
        //     if (chance < .5f) currPatrolIndex += 1 + Random.Range(0, childCount);
        //     //Go to next non branching path.
        //     else
        //     {
        //         if (patrolForward)
        //         {
        //             currPatrolIndex += 1;
                    
        //             while(patrolPositions.Length != currPatrolIndex && patrolPositions[currPatrolIndex].name.Contains("-"))
        //             {
        //                 currPatrolIndex += 1;
        //             }

        //             if (currPatrolIndex == patrolPositions.Length)
        //             {
        //                 currPatrolIndex -= 1;
        //                 if (currPatrolIndex > 0 && patrolPositions[currPatrolIndex].name.Contains('-'))
        //                 {
        //                     currPatrolIndex -= 1 + int.Parse(patrolPositions[currPatrolIndex].name.Split('-')[1]);
        //                 }
        //                 patrolForward = false;
        //             }
        //         }
        //         else
        //         {
        //             currPatrolIndex -= 1;
        //             if (currPatrolIndex >= 0 && patrolPositions[currPatrolIndex].name.Contains('-'))
        //             {
        //                 currPatrolIndex -= 1 + int.Parse(patrolPositions[currPatrolIndex].name.Split('-')[1]);
        //             }
        //         }
        //     }
        // }
        // Debug.Log(currPatrolIndex);

        // // linear patrol
        if (patrolForward)
        {
            currPatrolIndex = (currPatrolIndex + 1) % patrolPositions.Length;
        }
        else
        {
            if (--currPatrolIndex < 0)
            {
                currPatrolIndex = patrolPositions.Length - 1;
            }
        }
    }
}
