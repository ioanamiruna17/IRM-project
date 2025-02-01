using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject ingredientPrefab; // Prefab of the ingredient
    private Transform spawnPoint;       // Position to spawn the new ingredient

    void Start()
    {
        // Save the spawn position
        spawnPoint = transform;
        SpawnIngredient(); // Spawn the initial ingredient
    }

    public void OnIngredientGrabbed()
    {
        // Spawn a new ingredient at the same position
        Instantiate(ingredientPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnIngredient()
    {
        // Instantiate the first ingredient and assign the grab callback
        GameObject newIngredient = Instantiate(ingredientPrefab, spawnPoint.position, spawnPoint.rotation);
        newIngredient.GetComponent<XRGrabInteractable>().selectExited.AddListener((args) => OnIngredientGrabbed());
    }
}
