using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHp;
    public float attackSpeed; // 1-4.5
    public int damage;
    public float spawnSpeed;

    public int Hp;
    
    [Space]
    public MeshRenderer _meshRenderer;
    public Material hitEffectMaterial;
    private Material _origMaterial;

    [Space]
    public Animator _animator;
    public Rigidbody _rigidbody;
    
    [Space]
    public Image HpBarFillEnemy;

    
    public EnemyState _enemyState { get; set; }
    private GameManager GameManager;
    private GameController GameController;
    

    #region Unity methods
    public void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        GameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        
        maxHp = GameController.maxHpEnemy;
        attackSpeed = GameController.attackSpeedEnemy;
        damage = GameController.damageEnemy;
        spawnSpeed = GameController.spawnSpeedEnemy;
        
        _origMaterial = _meshRenderer.material;
        Hp = maxHp;
        _enemyState = EnemyState.Spawn;
    }
    private void Update()
    {
        HpBarFillEnemy.fillAmount = (float)Hp / (float)maxHp;
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
        Hp -= dmg;
        if (Hp <= 0)
        {
            Destroy(gameObject);
            GameController.onEnemyDeath();
        } 
    }
    #endregion
}
