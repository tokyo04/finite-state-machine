using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase,
        Attack
    }

    public State currentState;

    public Transform player;
    public float speed = 2f;
    public float chaseDistance = 5f;
    public float attackDistance = 1.5f;

    private Vector2 patrolDirection = Vector2.right;

    void Start()
    {
        currentState = State.Patrol;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    void Patrol()
    {
        transform.Translate(patrolDirection * speed * Time.deltaTime);

        // bolak-balik
        if (transform.position.x > 3f) patrolDirection = Vector2.left;
        if (transform.position.x < -3f) patrolDirection = Vector2.right;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseDistance)
        {
            currentState = State.Chase;
        }
    }

    void Chase()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        if (distance < attackDistance)
        {
            currentState = State.Attack;
        }
        else if (distance > chaseDistance)
        {
            currentState = State.Patrol;
        }
    }

    void Attack()
    {
        Debug.Log("Enemy Attack!");

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackDistance)
        {
            currentState = State.Chase;
        }
    }
}