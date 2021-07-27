using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum FightState
    {
        Idle,
        Fight,
        Death
    }

    #region Fields
    public int maxHp;
    public int attackSpeed;
    public int damage;

    [Space] [SerializeField] private Material hitEffectMaterial;
    [HideInInspector] public int Hp;
    [HideInInspector] public Enemy Enemy;
    [HideInInspector] public FightState _fightState;

    private Material _origMaterial;
    private MeshRenderer _meshRenderer;
    #endregion
    
    #region Unity methods
    private void Start()
    {
        Hp = maxHp;
        _fightState = FightState.Idle;
        _meshRenderer = GetComponent<MeshRenderer>();
        _origMaterial = _meshRenderer.material;
    }

    private void Update()
    {
        SwitchState(_fightState);
        if(Hp <= 0 ) Destroy(gameObject);
    }
    #endregion
    
    private void SwitchState(FightState state)
    {
        switch (state)
        {
            case FightState.Idle:
                Attack(false);
                // Block();
                    break;
            case FightState.Fight:
                Attack(true);
                // Block();
                    break;
            case FightState.Death:
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
    private void Attack(bool isFight)
    {
        if (isFight)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //anim hit
                Enemy.TakeDamage(damage);
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
    #endregion

    #region Public methods
    public void TakeDamage(int dmg)
    {
        Hp -= dmg;
        StartCoroutine(HitEffect());
    }
    #endregion
}
