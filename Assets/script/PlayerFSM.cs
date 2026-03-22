using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack
    }

    public State currentState;

    public float speed = 5f;
    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = State.Idle;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;

            case State.Move:
                Move();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    void FixedUpdate()
    {
        if (currentState == State.Move)
        {
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Idle()
    {
        if (moveInput != 0)
        {
            currentState = State.Move;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = State.Attack;
        }
    }

    void Move()
    {
        if (moveInput == 0)
        {
            currentState = State.Idle;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = State.Attack;
        }
    }

    void Attack()
    {
        Invoke(nameof(BackToIdle), 0.5f);
    }

    void BackToIdle()
    {
        currentState = State.Idle;
    }
}