using UnityEngine;

public class PlayerWeapone : MonoBehaviour
{
    private GameManager GameManager;
    private void OnTriggerEnter(Collider other)
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (other.CompareTag("EnemyTrigger") && GameManager.Player.PlayerState == PlayerState.Fight)
        {
            GameManager.Enemy.TakeDamage(GameManager.Player.damage);
        }
    }
}