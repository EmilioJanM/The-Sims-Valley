using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAround : MonoBehaviour
{
    [SerializeField] private float _normalScale;

    public float _speed;
    private Vector3 _movement;
    private Animator _animator;
    private Rigidbody2D _rid;
    private Vector2 nextPosition;
    private Vector2 newDirection;

    void Start()
    {
        _rid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        nextPosition = new Vector2(Random.Range(-24.0f, 24.0f), Random.Range(-12.0f, 12.0f));
        StartCoroutine(SetNewDirection());
        _animator.SetBool("isWalking", true);

    }

    void Update()
    {

        //We get the dot product of the X axis and the movement of the player to know to which side the player is going and set the side
        float dotDirection = Vector3.Dot(Vector3.Normalize(nextPosition), Vector3.right);
        float currentScale = (dotDirection >= 0) ? 0.74f : -0.74f;
        transform.localScale = new Vector3(currentScale, 0.74f, 0.24f);
    }

    private void FixedUpdate()
    {
        newDirection = Vector2.MoveTowards(transform.position, nextPosition, Time.fixedDeltaTime * _speed);
        _rid.MovePosition(newDirection);

        if (Vector2.Distance(transform.position, nextPosition) < 0.2f)
        {
            _animator.SetBool("isWalking", false);
        }
    }

    /// <summary>
    /// Sets a new direction for the Astronaut to go to
    /// </summary>
    /// <returns></returns>
    IEnumerator SetNewDirection()
    {
        _animator.SetBool("isWalking", true);

        yield return new WaitForSeconds(6);
        nextPosition = new Vector2(Random.Range(-24.0f, 24.0f) + 8, Random.Range(-12.0f, 12.0f) + 8);

        StartCoroutine(SetNewDirection());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.SetBool("isWalking", false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _animator.SetBool("isWalking", true);
    }
}
