using UnityEngine;

public class SnapToSurface : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Snap to the surface if it collides with the Plate or another Ingredient
        if (collision.gameObject.CompareTag("Plate") || collision.gameObject.CompareTag("Ingredient"))
        {
            // Attach this ingredient to the collided object
            transform.position = collision.contacts[0].point;
            transform.SetParent(collision.transform); // Make it a child
            GetComponent<Rigidbody>().isKinematic = true; // Stop physics
        }
    }
}
