using System.Security.Cryptography;
using UnityEngine;

public class SpawnBurger : MonoBehaviour
{
    public Transform teleportDestination; // Locația unde trebuie să ajungă Burger_Bun_Brioche_Bottom
    public Transform burgerTeleportDestination;
    public GameObject burgerPreset; // Prefab-ul pentru BurgerPreset_1
    public GameObject[] ingredients; // Lista ingredientelor
    public Transform[] ingredientInitLocations; // Locațiile inițiale ale ingredientelor
    private Vector3 newColliderSize = new Vector3(0.2f, 1.5f, 0.35f);
    private float scaleMultiplier = 2.333f;

    void Update()
    {
        // Verifică dacă Burger_Bun_Brioche_Top ajunge la destinație
        if (Vector3.Distance(transform.position, teleportDestination.position) < 0.1f)
        {
            SpawnBurgerAndResetIngredients();
        }
    }

    void SpawnBurgerAndResetIngredients()
    {
        // Resetează poziția ingredientelor la locațiile lor inițiale
        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null && ingredientInitLocations[i] != null)
            {
                ResizeCollider(ingredients[i]);
                ScaleObject(ingredients[i]);
                ingredients[i].transform.position = ingredientInitLocations[i].position;
                ingredients[i].transform.rotation = ingredientInitLocations[i].rotation;
                UnityEngine.Debug.Log(ingredients[i].transform.position);
                UnityEngine.Debug.Log($"{ingredients[i].name} reset to its initial position.");
            }
        }

        if (burgerPreset != null && burgerTeleportDestination != null)
        {
            // Spawnează burgerul la locația specificată
            Instantiate(burgerPreset, burgerTeleportDestination.position, burgerTeleportDestination.rotation);
            UnityEngine.Debug.Log("BurgerPreset_1 spawned at the destination.");
        }
        else
        {
            UnityEngine.Debug.LogWarning("BurgerPreset or teleportDestination is not set!");
        }
    }
   
    void ResizeCollider(GameObject ingredient)
    {
        Collider collider = ingredient.GetComponent<Collider>();
        if (collider != null)
        {
            if (collider is BoxCollider boxCollider)
            {
                boxCollider.size = newColliderSize;
                UnityEngine.Debug.Log($"{ingredient.name} BoxCollider set to: {newColliderSize}");
            }
            else
            {
                UnityEngine.Debug.LogWarning($"{ingredient.name} does not have a BoxCollider, resizing skipped.");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning($"{ingredient.name} does not have a Collider.");
        }
    }

    void ScaleObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            if (currentScale.x != 35f && currentScale.z != 35f)
            {
                gameObject.transform.localScale = new Vector3(
                    currentScale.x / scaleMultiplier,
                    currentScale.y,
                    currentScale.z / scaleMultiplier
                );
                UnityEngine.Debug.Log($"{gameObject.name} scaled up.");
            }
        }
    }
}
