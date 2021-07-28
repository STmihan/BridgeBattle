using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHp;
    public float attackSpeed;
    public int damage;
    public float spawnSpeed;
    
    public int Hp { get; set; }
    
    [Space]
    public MeshRenderer _meshRenderer;
    public Material hitEffectMaterial;
    private Material _origMaterial;

    [Space]
    public Animator _animator;
    
    [Space]
    public Rigidbody _rigidbody;
    
    public EnemyState _enemyState { get; set; }
    private GameManager GameManager;

    
    public void Start()
    {
        _origMaterial = _meshRenderer.material;
        Hp = maxHp;
        _enemyState = EnemyState.Spawn;
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        if (transform.position.z < GameManager.StartFightTrigger.gameObject.transform.position.z)
        {
            _rigidbody.MovePosition(transform.position + Vector3.forward * (spawnSpeed * Time.fixedDeltaTime));
        }
        else
        {
            _enemyState = EnemyState.Fight;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Attack());
    }
    
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackSpeed);
        while (GameManager.Player.GetComponent<Player>().Hp > 0)
        {
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    #region Hit effect

    private IEnumerator HitEffect()
    {
        _meshRenderer.material = hitEffectMaterial;
        yield return new WaitForSeconds(.1f);
        _meshRenderer.material = _origMaterial;
    }

    public IEnumerator TakeDamage(int dmg)
    {
        StartCoroutine(HitEffect());
        Hp -= dmg;
        if (Hp <= 0)
        {
            Destroy(gameObject);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ScoreUp();
            yield return new WaitForSeconds(1);
            GameManager.Spawner.GetComponent<Spawner>().Spawn(0);
        } 
    }
    #endregion

}
