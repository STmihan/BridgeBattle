using UnityEngine;

public class EnemyWeapone : MonoBehaviour
{
    private GameManager GameManager;
    private void OnTriggerEnter(Collider other)
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (other.CompareTag("PlayerTrigger"))
        {
            Debug.Log("Enemy attacked");
            GameManager.Player.TakeDamage(GameManager.Enemy.damage);
        }
    }
}