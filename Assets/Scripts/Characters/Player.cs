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
    
    [Space]
    public Animator PlayerAnimator;
    public Animator ShieldAnimator;

    public int Hp { get; private set; }
    public PlayerState PlayerState { get; set; }
    
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
        PlayerAnimator.SetFloat("AttackSpeed", attackSpeed);
        if (Hp <= 0 ) Destroy(gameObject);
    }
    #endregion
    

    
    #region Input methods
    float nextFireTime = 0f;
    public void Attack()
    {
        if (Time.time > nextFireTime && !isBlocking)
        {
            if (PlayerState == PlayerState.Fight)
            {
                PlayerAnimator.SetTrigger("AttackTrigger");
                    nextFireTime = Time.time + 1f/attackSpeed;
            }
            else
            {
                PlayerAnimator.SetTrigger("AttackTrigger");
                nextFireTime = Time.time + 1f/attackSpeed;
            }
        }
    }

    public void BlockDown()
    {
        isBlocking = true;
        Shield.SetActive(true);
    }
    

    public void BlockUp()
    {
            isBlocking = false;
            Shield.SetActive(false);
    }
    #endregion

    
    
    #region Hit Effect
    private IEnumerator HitEffect(GameObject target)
    {
        var _meshRenderer = target.GetComponent<MeshRenderer>();
        var _origMaterial = _meshRenderer.material;
        _meshRenderer.material = hitEffectMaterial;
        yield return new WaitForSeconds(.1f);
        _meshRenderer.material = _origMaterial;
    }

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
