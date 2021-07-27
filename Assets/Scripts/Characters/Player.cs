using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Fields
    public int maxHp;
    public int attackSpeed;
    public int damage;

    [Space]
    [SerializeField] private Material hitEffectMaterial;
    [SerializeField] private GameObject Shield;
    
    [HideInInspector] public int Hp;
    [HideInInspector] public Enemy Enemy;
    [HideInInspector] public PlayerState PlayerState;
    
    private bool isBlocking;
    #endregion
    
    #region Unity methods
    private void Start()
    {
        Hp = maxHp;
        PlayerState = PlayerState.Idle;
    }

    private void Update()
    {
        SwitchState(PlayerState);
        if (Hp <= 0 ) Destroy(gameObject);
    }
    #endregion
    
    private void SwitchState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                Attack(false);
                Block();
                    break;
            case PlayerState.Fight:
                Attack(true);
                Block();
                    break;
            case PlayerState.Death:
                // OnDeath();
                break;
        }
    }

    private IEnumerator HitEffect(GameObject target)
    {
        var _meshRenderer = target.GetComponent<MeshRenderer>();
        var _origMaterial = _meshRenderer.material;
        _meshRenderer.material = hitEffectMaterial;
        yield return new WaitForSeconds(.1f);
        _meshRenderer.material = _origMaterial;
    }
    
    #region Input methods
    float nextFireTime = 0f;
    private void Attack(bool isFight)
    {
        if (Time.time > nextFireTime && !isBlocking)
        {
            if (isFight)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //anim hit
                    StartCoroutine(Enemy.TakeDamage(damage));
                    nextFireTime = Time.time + 1f/attackSpeed;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //anim hit
                }
            }
        }
    }

    private void Block()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isBlocking = true;
            Shield.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            isBlocking = false;
            Shield.SetActive(false);
        }
    }
    #endregion

    #region Public methods
    public void TakeDamage(int dmg)
    {
        if (isBlocking)
        {
            StartCoroutine(HitEffect(Shield));
        }
        else
        {
            Hp -= dmg;
            StartCoroutine(HitEffect(gameObject));
        }
    }
    #endregion
}
