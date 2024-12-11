using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Player Health Settings")]
    public float currentPlayerHealth = 100.0f;
    [SerializeField] private float maxPlayerHealth = 100.0f;

    [Header("UI and Effects")]
    [SerializeField] private Image redSplatterImage = null; // Image to represent damage on the screen
    [SerializeField] private Image hurtImage = null;        // Image to flash when taking damage
    [SerializeField] private float hurtTimer = 0.1f;        // Time for hurt flash effect
    [SerializeField] private AudioClip hurtAudio = null;    // Audio clip for hurt sound

    private AudioSource healthAudioSource;                  // Audio source for playing hurt sounds

    private void Awake()
    {
        // Initialize the AudioSource component
        healthAudioSource = GetComponent<AudioSource>();

        if (healthAudioSource == null && hurtAudio != null)
        {
            Debug.LogWarning("AudioSource component missing on player but hurt audio is assigned.");
        }

        // Update health UI at the start of the game
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (redSplatterImage != null)
        {
            // Adjust the alpha based on the current health
            Color splatterAlpha = redSplatterImage.color;
            splatterAlpha.a = 1 - (currentPlayerHealth / maxPlayerHealth);
            redSplatterImage.color = splatterAlpha;

            Debug.Log("Updated splatter alpha to: " + splatterAlpha.a);
        }
        else
        {
            Debug.LogWarning("Red Splatter Image not assigned in the Inspector.");
        }
    }

    private IEnumerator HurtFlash()
    {
        if (hurtImage != null)
        {
            hurtImage.enabled = true;
        }

        if (healthAudioSource != null && hurtAudio != null)
        {
            healthAudioSource.PlayOneShot(hurtAudio);
        }

        yield return new WaitForSeconds(hurtTimer);

        if (hurtImage != null)
        {
            hurtImage.enabled = false;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (currentPlayerHealth > 0)
        {
            currentPlayerHealth -= damageAmount;
            currentPlayerHealth = Mathf.Max(currentPlayerHealth, 0); // Ensure health doesn’t go below zero

            // Trigger hurt effects
            StartCoroutine(HurtFlash());
            UpdateHealth();

            Debug.Log("Player took " + damageAmount + " damage. Current Health: " + currentPlayerHealth);
        }
        else
        {
            Debug.Log("Player health is already at zero.");
        }
    }
}
