using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryBurger : MonoBehaviour
{
    public Transform teleportDestination; // Locația unde trebuie să ajungă Burger_Bun_Brioche_Bottom
    public GameObject burgerPattyRaw; // Obiectul raw al chiftelei
    public GameObject cookedPatty; // Prefab-ul pentru chifteaua gătită
    public Transform PattyInitLocation; // Locația inițială a chiftelei crude
    public Transform PanLocation; // Locația tigăii unde apare chifteaua gătită
    public float teleportDelay = 5f;
    private bool teleported = false;
    public float scaleMultiplier = 2.333f;
    private Vector3 newColliderSize = new Vector3(0.2f, 1.5f, 0.35f);

    public AudioSource audioSource; // Adăugat pentru redarea sunetului


    void Update()
    {
        if (!teleported && Vector3.Distance(transform.position, teleportDestination.position) < 0.1f)
        {
            StartCoroutine(FryPatty());
            teleported = true; // Evită reapelarea
        }
    }

    IEnumerator FryPatty()
    {
        
        // Redă sunetul la începutul teleportării
        if (audioSource != null)
        {
            audioSource.Play();
        }

        yield return new WaitForSeconds(teleportDelay);

        if (PattyInitLocation != null)
        {
            ResizeCollider(burgerPattyRaw);
            ScaleObjectDown(burgerPattyRaw);
            burgerPattyRaw.transform.position = PattyInitLocation.position;
            UnityEngine.Debug.Log($"{burgerPattyRaw.name} teleported to PattyInitLocation after {teleportDelay} seconds.");
            teleported = true;
        }
        else
        {
            UnityEngine.Debug.LogWarning("PattyInitLocation is not set!");
            teleported = false;
        }
        
        // Teleportează chifteaua gătită la locația tigăii
        if (cookedPatty != null)
        {
            teleported = false;
            ScaleObjectUp(cookedPatty);
            cookedPatty.transform.position = PanLocation.position;
            UnityEngine.Debug.Log($"{cookedPatty.name} teleported to PanLocation.");
        }
    }

    void ScaleObjectDown(GameObject gameObject)
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

    void ScaleObjectUp(GameObject gameObject)
    {
        if (gameObject != null)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            if (currentScale.x <= 15f && currentScale.z <= 15f)
            {
                gameObject.transform.localScale = new Vector3(
                    currentScale.x * scaleMultiplier,
                    currentScale.y,
                    currentScale.z * scaleMultiplier
                );
                UnityEngine.Debug.Log($"{gameObject.name} scaled up.");
            }
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
}
