using UnityEngine;

public class EnemyWeapone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Player.GetComponent<Player>();
        var enemy = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Enemy.GetComponent<Enemy>();
        if (other.CompareTag("PlayerTrigger"))
        {
            Debug.Log("Enemy attacked");
            player.TakeDamage(enemy.damage);
        }
    }
}