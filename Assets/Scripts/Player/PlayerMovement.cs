using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private float _normalScale;

    public float _speed;
    private Vector3 _movement;
    private Animator _animator;
    private Rigidbody2D _rid;


    

    void Start()
    {
        camera = Camera.main.gameObject;
        _rid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -20);

        //We get the dot product of the X axis and the movement of the player to know to which side the player is going and set the side
        float dotDirection = Vector3.Dot(Vector3.Normalize(_movement), Vector3.right);
        float currentScale = (dotDirection >= 0) ? 0.24f : -0.24f;
        transform.localScale = new Vector3(currentScale, 0.24f, 0.24f);


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _animator.SetBool("isRunning", false);
        }

        //Change of the animation if there is movement
        if (_movement.magnitude > 0)
        {
            _animator.SetBool("isWalking", true);

            if (Input.GetKeyDown(KeyCode.LeftShift)){
                _animator.SetBool("isRunning", true);
            }
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _movement *= 1.5f;
        }

        _rid.velocity = _movement;
    }
}
