using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;

    private void private void Awake()
    {

    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.position; //Move player to checkpoint position
    }
}
