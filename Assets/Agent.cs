using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [HideInInspector]
    public bool isChasing = false;
    [HideInInspector]
    public GameObject player;

    public bool canWait;
    public float totalWaitTime = 3f;
    [Range(0f, 1f)]
    public float swicthProbability;
    public GameObject[] patrolPositions;
    public string[] endPositions;
    public bool isMelee;

    public bool isRange;
    NavMeshAgent agent;
    int currPatrolIndex;
    bool isTravelling;
    bool isWaiting;
    float waitTimer;
    bool patrolForward = true;
    [SerializeField]
    public float Distance;
    float dodgeTimer = 0f;
    bool isAttacking = false;
    bool attackDelayed = true;
    public float attackTimer = .5f;


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
            StartCoroutine(Attack(attackTimer));
        }
        //need detection script to change this variable.
        if (isChasing)
        {
            // waitTimer += Time.deltaTime;
            // if (waitTimer >= 1f)

           //{
                
                if (isMelee)
                {
                    dodgeTimer += Time.deltaTime;
                    waitTimer += Time.deltaTime;

                    if (waitTimer >= 1f)
                    {
                        agent.speed = 3.5f;
                        Distance = UnityEngine.Vector3.Distance(agent.transform.position, player.transform.position);

                        if (Distance > 2)
                        {

                            agent.SetDestination(player.transform.position);
                            isAttacking = false;
                        }
                        else
                        {

                            agent.SetDestination(agent.transform.position);
                            isAttacking = true;
                            waitTimer = 0f;

                        }

                    }

                }else if (isRange){

                    waitTimer += Time.deltaTime;

                    if (waitTimer >= 1f)
                    {

                        Distance = UnityEngine.Vector3.Distance(agent.transform.position, player.transform.position);

                        if (Distance > 6)
                        {

                            agent.SetDestination(player.transform.position);

                        }
                        else
                        {

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
                    isWaiting = true;
                    waitTimer = 0f;
                }
                else
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
            }

            if (isWaiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= totalWaitTime)
                {
                    isWaiting = false;
                    ChangePatrolPoint();
                    SetDestination();
                }
            }
        }
    }

    void SetDestination()
    {
        if (patrolPositions != null)
        {
            UnityEngine.Vector3 targetPos = patrolPositions[currPatrolIndex].transform.position;
            //create some random range to choose instead of exact point.
            float x = targetPos.x + Random.Range(-1f, 1f);
            float z = targetPos.z + Random.Range(-1f, 1f);
            targetPos.Set(x, targetPos.y, z);
            agent.SetDestination(targetPos);
            isTravelling = true;
        }
        else
        {
            //Choose random point in space
            
        }
    }

    IEnumerator Attack(float damage)
    {
        attackDelayed = true;
        //deal damage to player
        yield return new WaitForSeconds(attackTimer);
        attackDelayed = false;
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
