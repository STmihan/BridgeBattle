using UnityEngine;

public class StartFightTrigger : MonoBehaviour
{

    public GameManager GameManager;
    
    private void OnTriggerEnter(Collider other)
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (other.CompareTag("Enemy"))
            GameManager.Player.PlayerState = PlayerState.Fight;
    }
}
