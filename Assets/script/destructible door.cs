using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int requiredPoints = 750; // Points needed to destroy this object
    public float interactionRange = 3f; // How close the player needs to be to interact

    private Transform player; // Reference to the player's Transform

    void Start()
    {
        // Find the player in the scene (assuming they have the "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject is tagged 'Player'.");
        }
    }

    void Update()
    {
        // Check if the player is close enough and presses the "E" key
        if (player != null && IsPlayerInRange() && Input.GetKeyDown(KeyCode.E))
        {
            TryToDestroy();
        }
    }

    private bool IsPlayerInRange()
    {
        // Check the distance between the player and this object
        return Vector3.Distance(player.position, transform.position) <= interactionRange;
    }

    private void TryToDestroy()
    {
        // Check if the player has enough points
        if (PlayerStats.points >= requiredPoints)
        {
            // Deduct points and destroy the object
            PlayerStats.points -= requiredPoints;
            Debug.Log($"Object destroyed! {requiredPoints} points deducted. Remaining points: {PlayerStats.points}");
            Destroy(gameObject);
        }
        else
        {
            // Notify the player they need more points
            Debug.Log($"Not enough points! You need {requiredPoints - PlayerStats.points} more points.");
        }
    }
}
