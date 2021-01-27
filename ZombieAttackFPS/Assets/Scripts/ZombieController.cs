using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    Animator animator;
    float speed;
    public float radius = 30f;
    bool tookHit = false;
    float distance;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(MoveRandom(1f));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 characterSpaceDir = transform.InverseTransformVector(agent.velocity);
        animator.SetFloat("forward", characterSpaceDir.z, 0.5f, Time.deltaTime);
        gameObject.GetComponentInChildren<Canvas>().enabled = false;

        distance = Vector3.Distance(player.position, transform.position);
        Vector3 targetDir = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(targetDir, transform.forward);

        var stateNfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateNfo.IsName("Z_FallingBack"))
        {
            agent.enabled = false;
            transform.rotation = Quaternion.identity;
            StartCoroutine(WaitDeath(5f));
        }
        if (agent.enabled && ((distance <= radius && angleToPlayer >= -60 && angleToPlayer <= 90) || tookHit))
        {
            agent.speed = 4f;
            agent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) < 5f)
                animator.SetTrigger("attack");
        }
        if (stateNfo.IsName("Z_TakeHit"))
        {
            agent.speed = 0f;
            tookHit = true;
        }
    }

    IEnumerator WaitDeath(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject, 1f);
    }

    IEnumerator MoveRandom(float t)
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance > radius && tookHit == false)
        {
            agent.speed = UnityEngine.Random.Range(1f, 2.5f);
            agent.SetDestination(new Vector3(transform.position.x + UnityEngine.Random.Range(-20f, 20f), 0f, transform.position.z + UnityEngine.Random.Range(-20f, 20f)));
        }
        yield return new WaitForSeconds(t);
        float newt = UnityEngine.Random.Range(10f, 15f);
        yield return StartCoroutine(MoveRandom(newt));
    }
}
