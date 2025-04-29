using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;

    public float speed;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player.isAppleCollected)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        var dir = player.transform.position - transform.position;
        _rb.linearVelocity = dir.normalized * speed;
        transform.LookAt(transform.position + dir);
    }
}
