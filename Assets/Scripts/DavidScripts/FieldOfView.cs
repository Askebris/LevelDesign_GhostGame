using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform point3;
    [SerializeField] private Transform point4;
    

    private Transform current;
    //"How to Add Field of View for you Enemies [Unity Tutorial]" by Comp-3 Interactive. Continue watching from timestamp 13:20.

    public GameObject seenPlayer;
    //Radius and angle in which our view can be seen
    public float radius;
    [Range(0, 360)]
    public float angle;

    //Referance to player/object you're trying to search for
    public GameObject[] playerRef;

    //infrastructure for dealing with the targets and obstructions (such as walls). Doing this by using layers: layersmasks
    public LayerMask targetMask;
    public LayerMask obstructionsMask;

    //boolian for if I can see the player
    public bool canSeePlayer;
    [SerializeField]private Transform target;
    [SerializeField] private float MoveSpeed = 20;
    private float MaxDist = 10;
    private float MinDist = 5;

    
   
    private void Start()
    {
        playerRef = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(FOVRoutine());

        agent.SetDestination(point1.position);
        current = point1;
    }

    private void Update()
    {
        if (canSeePlayer == true)
        {
            transform.LookAt(target);

            if (Vector3.Distance(transform.position, target.position) >= MinDist)
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, target.position) <= MaxDist)
                {
                    //Here Call any function U want Like Shoot at here or something
                }

            }
        }

        else if (canSeePlayer == false) 
        {
            Patroll();
        }
    }

    //Looking for player. For better preformance, we're doing it five times every second instead of once every frame. 
    //Player won't notice the diffrence, but it makes a differece for the preformance.
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    //Check for the player with raycasting and sperecasting
    private void FieldOfViewCheck()
    {
        //Collider array
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //checking for the angle. Devided by two, because of angle is one to the left and one to the right. Half it to get a correct angle.
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                RaycastHit hit;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, targetMask) && !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionsMask))
                {
                    if (hit.transform.tag == "Player")
                    {
                        seenPlayer = hit.transform.gameObject;
                        canSeePlayer = true;
                      
                    }
                    else
                    {
                        canSeePlayer = false;
                    }

                }
            }
            else
            {
                canSeePlayer = false;
            }

        }

        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(target.transform.position);
    }

    private void Patroll()
    {
        if (agent.remainingDistance <= 0.5f)
        {
            if (current == point1 && !canSeePlayer)
            {
                current = point2;
            }
            else if (current == point2 && !canSeePlayer)
            {
                current = point3;
            }

            else if (current == point3 && !canSeePlayer)
            {
                current = point4;
            }
           
            else
            {
                current = point1;
            }

            agent.SetDestination(current.position);
        }
    }
}
