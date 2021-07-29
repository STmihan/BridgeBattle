using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Fields

    public int maxHp;
    public float attackSpeed;
    public int damage;

    [Space]
    [SerializeField] private Material hitEffectMaterial;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject main;
    
    [Space]
    public Animator PlayerAnimator;
    public Animator ShieldAnimator;
    
    [Space]
    public Image HpBarFillPlayer;

    public int Hp;
    public PlayerState PlayerState { get; set; }
    
    private bool isBlocking;
    #endregion
    
    #region Unity methods
    private void Awake()
    {
        PlayerState = PlayerState.Idle;
        Hp = maxHp;
    }

    private void Update()
    {
        PlayerAnimator.SetFloat("AttackSpeed", attackSpeed);
        if (Hp <= 0)
        {
            Destroy(gameObject);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().isFight = false;
        }
        HpBarFillPlayer.fillAmount = (float)Hp / (float)maxHp;
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
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().isFight = true;
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
        shield.SetActive(true);
    }
    

    public void BlockUp()
    {
        isBlocking = false;
        shield.SetActive(false);
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
            StartCoroutine(HitEffect(shield));
        }
        else
        {
            Hp -= dmg;
            StartCoroutine(HitEffect(main));
        }
    }
    #endregion
}
