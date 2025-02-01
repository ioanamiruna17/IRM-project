using UnityEngine;

public class SodaDispenser : MonoBehaviour
{
    public ParticleSystem sodaEffect;       // Particle system for soda effect
    public Transform cupSpawnPoint;         // Spawn point for new cups
    public GameObject cupPrefab;            // Cup Prefab to spawn
    private bool isCupReady = false;        // Check if the cup is ready under dispenser
    private Transform liquid;               // Reference to the liquid inside the cup
    private GameObject currentCup;          // Reference to the current cup

    void Start()
    {
        // Spawn the first cup at the start
        SpawnNewCup();
    }

    void Update()
    {
        // Pour soda when player presses 'E'
        if (isCupReady && Input.GetKeyDown(KeyCode.E))
        {
            PourSoda();
        }

        // Spawn a new cup if the current one is moved
        if (currentCup != null && Vector3.Distance(currentCup.transform.position, cupSpawnPoint.position) > 1f)
        {
            SpawnNewCup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cup"))
        {
            isCupReady = true;
            liquid = other.transform.Find("Liquid"); // Get reference to the liquid
            Debug.Log("Cup ready. Press 'E' to pour soda.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cup"))
        {
            isCupReady = false;
            Debug.Log("Cup removed.");
        }
    }

    void PourSoda()
    {
        if (sodaEffect != null)
            sodaEffect.Play(); // Play soda effect

        if (liquid != null)
            StartCoroutine(FillCup());
    }

    System.Collections.IEnumerator FillCup()
    {
        float fillDuration = 3f; // Time to fill the cup
        float elapsedTime = 0f;

        Vector3 startScale = liquid.localScale;
        Vector3 endScale = new Vector3(startScale.x, 1f, startScale.z); // Full height

        while (elapsedTime < fillDuration)
        {
            liquid.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / fillDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        liquid.localScale = endScale; // Ensure full scale
        Debug.Log("Cup is full!");
    }

    void SpawnNewCup()
    {
        if (currentCup != null)
        {
            Destroy(currentCup, 2f); // Optionally destroy old cup after delay
        }

        currentCup = Instantiate(cupPrefab, cupSpawnPoint.position, Quaternion.identity);
        Debug.Log("New cup spawned.");
    }
}
