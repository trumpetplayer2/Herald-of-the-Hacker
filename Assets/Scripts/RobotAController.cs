using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotAController : EnemyBase
{
    float nextCheck = 0f;
    public float timeBetweenTargetChecks = 10;
    Transform target;
    NavMeshAgent agent;
    public float distanceCheck = 1f;
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        if (GameManager.instance.enemyTargets != null)
        {
            foreach (GameObject tempTarget in GameManager.instance.enemyTargets)
            {
                if (target == null)
                {
                    target = tempTarget.transform;
                }
                else
                if (Vector3.Distance(transform.position, tempTarget.transform.position) < Vector3.Distance(transform.position, target.position))
                {
                    target = tempTarget.transform;
                }
            }
        }
        else
        {
            nextCheck = timeBetweenTargetChecks - 1f;
        }
        agent.SetDestination(target.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead) {
            //Stop moving!
            agent.isStopped = true;
            return; 
        }
        nextCheck += Time.deltaTime;
        if(timeBetweenTargetChecks <= nextCheck)
        {
            //Update target
            //For now target player, if time later, add a priority list and add boxes on buttons if nearby\
            foreach (GameObject tempTarget in GameManager.instance.enemyTargets)
            {
                if (target == null)
                {
                    target = tempTarget.transform;
                }
                else
                if (Vector3.Distance(transform.position, tempTarget.transform.position) < Vector3.Distance(transform.position, target.position))
                {
                    target = tempTarget.transform;
                }
                
            }
            //Reset check
            nextCheck = 0;
        }
        agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.isDead) { return; }
        Debug.Log(other.name);
        if(other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.hurt(damage);
            Die();
        }
    }
}
