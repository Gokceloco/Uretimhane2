using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Player _player;
    private Rigidbody _rb;

    void Update()
    {
        if (_player.isAppleCollected)
        {
            MoveToPlayer();
        }
    }
    public void StartEnemy(Player player)
    {
        _rb = GetComponent<Rigidbody>();
        _player = player;
    }
    private void MoveToPlayer()
    {
        var dir = _player.transform.position - transform.position;
        _rb.linearVelocity = dir.normalized * speed;
        transform.LookAt(transform.position + dir);
    }    
}
