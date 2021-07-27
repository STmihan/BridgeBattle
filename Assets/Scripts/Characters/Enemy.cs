using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public int maxHp;
    public float attackSpeed;
    public float spawnSpeed;
    
    
    [HideInInspector]public int _hp;
    
    [Space]
    [SerializeField] private Material hitEffectMaterial;

    private Spawner _spawner;
    private Transform _fightPossition;
    
    private Material _origMaterial;
    private MeshRenderer _meshRenderer;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private Player _player;

    private EnemyState _enemyState;

    public void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _origMaterial = _meshRenderer.material;
        _hp = maxHp;
        _enemyState = EnemyState.Spawn;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _fightPossition = GameObject.FindWithTag("FightPosition").transform;
        _spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
    }

    private void FixedUpdate()
    {
        if (transform.position.z < _fightPossition.position.z)
        {
            _rigidbody.MovePosition(transform.position + Vector3.forward * (spawnSpeed * Time.fixedDeltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Attack());
        _player = other.GetComponent<StartFightTrigger>().Player.GetComponent<Player>();
    }

    public IEnumerator TakeDamage(int dmg)
    {
        StartCoroutine(HitEffect());
        _hp -= dmg;
        if (_hp <= 0)
        {
            Destroy(gameObject);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ScoreUp();
            yield return new WaitForSeconds(1);
            _spawner.Spawn();
        } 
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
    
    private IEnumerator HitEffect()
    {
        _meshRenderer.material = hitEffectMaterial;
        yield return new WaitForSeconds(.1f);
        _meshRenderer.material = _origMaterial;
    }
}
