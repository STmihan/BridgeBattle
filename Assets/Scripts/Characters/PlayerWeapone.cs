using UnityEngine;

public class PlayerWeapone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Player.GetComponent<Player>();
        var enemy = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Enemy.GetComponent<Enemy>();
        if (other.CompareTag("EnemyTrigger") && player.PlayerState == PlayerState.Fight)
        {
            enemy.TakeDamage(player.damage);
        }
    }
}