using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool doesCameraFollow;
    public Transform mainCamera;

    public float speed;
    public float jumpPower;

    public float rotationSpeed;

    public bool isAppleCollected;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
        if (doesCameraFollow)
        {
            MoveCamera();
        }
    }

    void MovePlayer()
    {
        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }

        var yVelocity = _rb.linearVelocity;

        yVelocity.x = 0;
        yVelocity.z = 0;

        _rb.linearVelocity = direction.normalized * speed + yVelocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpPower, _rb.linearVelocity.z);
        }
    }

    void MoveCamera()
    {
        mainCamera.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Apple"))
        {
            isAppleCollected = true;
            collision.gameObject.SetActive(false);
        }
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Dead,
}