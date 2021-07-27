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

    [Space] [SerializeField] private Material hitEffectMaterial;
    [HideInInspector] public int Hp;
    [HideInInspector] public Enemy Enemy;
    [HideInInspector] public PlayerState PlayerState;

    private Material _origMaterial;
    private MeshRenderer _meshRenderer;
    #endregion
    
    #region Unity methods
    private void Start()
    {
        Hp = maxHp;
        PlayerState = PlayerState.Idle;
        _meshRenderer = GetComponent<MeshRenderer>();
        _origMaterial = _meshRenderer.material;
    }

    private void Update()
    {
        SwitchState(PlayerState);
        if(Hp <= 0 ) Destroy(gameObject);
    }
    #endregion
    
    private void SwitchState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                Attack(false);
                // Block();
                    break;
            case PlayerState.Fight:
                Attack(true);
                // Block();
                    break;
            case PlayerState.Death:
                // OnDeath();
                break;
        }
    }

    private IEnumerator HitEffect()
    {
        _meshRenderer.material = hitEffectMaterial;
        yield return new WaitForSeconds(.1f);
        _meshRenderer.material = _origMaterial;
    }
    
    #region Input methods
    float nextFireTime = 0f;
    private void Attack(bool isFight)
    {
        if (Time.time > nextFireTime)
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
    #endregion

    #region Public methods
    public void TakeDamage(int dmg)
    {
        Hp -= dmg;
        StartCoroutine(HitEffect());
    }
    #endregion
}
