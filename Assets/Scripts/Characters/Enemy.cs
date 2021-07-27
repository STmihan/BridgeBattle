using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public int maxHp;
    public float attackSpeed;
    private int _hp;

    private Player _player;

    public void Start()
    {
        _hp = maxHp;
    }

    private void Update()
    {
        if (_hp <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Attack());
        _player = other.GetComponent<StartFightTrigger>().Player.GetComponent<Player>();
    }

    public void TakeDamage(int dmg)
    {
        //hit anim
        _hp -= dmg;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackSpeed);
        while (_player.Hp > 0)
        {
            _player.TakeDamage(damage);
            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
