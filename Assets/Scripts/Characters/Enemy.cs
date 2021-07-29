using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHp;
    public float attackSpeed; // 1-4.5
    public int damage;
    public float spawnSpeed;

    public int HpEnemy;
    
    [Space]
    public MeshRenderer _meshRenderer;
    public Material hitEffectMaterial;
    public Material _origMaterial;

    [Space]
    public Animator _animator;
    public Rigidbody _rigidbody;
    
    [Space]
    public Image HpBarFillEnemy;

    
    public EnemyState _enemyState { get; set; }
    private GameManager GameManager;


    #region Unity methods
    public void Start()
    {
        maxHp = GameManager.maxHpNextEnemy;
        attackSpeed = GameManager.attackSpeedNextEnemy;
        damage = GameManager.damageNextEnemy;
        _origMaterial = _meshRenderer.materials[0];
        HpEnemy = GameManager.maxHpNextEnemy;
        _enemyState = EnemyState.Spawn;
    }
    private void Update()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        HpBarFillEnemy.fillAmount = (float)HpEnemy / (float)maxHp;
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
        _animator.SetFloat("AttackSpeed", attackSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("FightPosition"))
            StartCoroutine(Attack());
    }
    #endregion

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f/attackSpeed);
        _animator.SetTrigger("AttackTrigger");
    }

    #region Hit effect

    private IEnumerator HitEffect()
    {
        _meshRenderer.material = hitEffectMaterial;
        yield return new WaitForSeconds(.1f);
        _meshRenderer.material = _origMaterial;
    }

    public void TakeDamage(int dmg)
    {
        StartCoroutine(HitEffect());
        HpEnemy -= dmg;
        if (HpEnemy <= 0)
        {
            GameManager.onEnemyDeath();
            Destroy(this.gameObject);
        } 
    }
    #endregion
}
