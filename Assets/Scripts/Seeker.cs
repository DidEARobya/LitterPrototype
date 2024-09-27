using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Seeker : MonoBehaviour
{
    [SerializeField]
    public Transform seekTarget;

    [SerializeField]
    public NavMeshAgent agent;

    bool test;
    // Start is called before the first frame update
    void Start()
    {
        test = false;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.hasPath != true)
        {
            agent.destination = GetRandomPoint();
            
            test = true;
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 direction = Random.insideUnitSphere * 50;

        direction += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(direction, out hit, 50, 1);

        return hit.position;
    }
}
