using UnityEngine;

public class StartFightTrigger : MonoBehaviour
{

    public GameManager GameManager;

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            GameManager.Player.GetComponent<Player>().PlayerState = PlayerState.Fight;
    }
}
