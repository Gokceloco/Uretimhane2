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

    private Vector3 _direction;
    private Vector3 _initialMousePos;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //MovePlayerWithKeys();
        MovePlayerWithMouse();
        if (doesCameraFollow)
        {
            MoveCamera();
        }
    }

    private void MovePlayerWithMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initialMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _direction = Input.mousePosition - _initialMousePos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _direction = Vector3.zero;
        }

        var adjustedDir = new Vector3(_direction.x, 0, _direction.y);

        _rb.linearVelocity = adjustedDir.normalized * speed;
        transform.LookAt(transform.position + adjustedDir);
    }

    void MovePlayerWithKeys()
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

    public void RestartPlayer()
    {
        _rb.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        isAppleCollected = false;
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Dead,
}