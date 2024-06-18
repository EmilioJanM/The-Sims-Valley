using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _speed;
    private Rigidbody2D _rid;
    Vector3 _movement;
    Animator _animator;

    void Start()
    {
        _rid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {

        //We get the dot product of the X axis and the movement of the player to know to which side the player is going and set the side
        float dotDirection = Vector3.Dot(Vector3.Normalize(_movement), Vector3.right);
        float currentScale = (dotDirection >= 0) ? 0.46f : -0.46f;
        transform.localScale = new Vector3(currentScale, 0.46f, 0.46f);


        //Change of the animation if there is movement
        if(_movement.magnitude > 0)
        {
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        //Recieve input for the movement
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        _movement = new Vector3(inputX * _speed, inputY * _speed);
        _movement *= Time.fixedDeltaTime;

        _rid.velocity = _movement;
    }
}
