using UnityEngine;

public class patrol : MonoBehaviour
{
    // A string to identify the player object (e.g. "Player")
    [Tooltip("The Tag of the GameObject that should trigger the disappearance.")]
    public string playerTag = "Player";

    // 1. PUBLIC variable to hold the audio clip, assigned in the Inspector
    public AudioClip collectSound;

    // This function is called when another object enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check if the object that touched us has the correct tag
        if (other.CompareTag(playerTag))
        {
            // 2. Play the sound effect at the object's position.
            // This creates a temporary AudioSource and lets the sound play out.
            if (collectSound != null)
            {
                // PlayClipAtPoint(clip, position, volume)
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // 3. The player has touched the collectible. Destroy this GameObject.
            // Now you can safely destroy the collectible immediately!
            Debug.Log("Player touched the object. Destroying " + gameObject.name);
            Destroy(gameObject);

            // OPTIONAL: Add functionality here, like increasing score or playing a sound.
        }
    }
}