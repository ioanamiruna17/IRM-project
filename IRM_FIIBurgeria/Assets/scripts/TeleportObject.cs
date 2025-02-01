using UnityEngine;
using System.Collections;

public class TeleportObject : MonoBehaviour

{
    public Transform teleportDestination; // Locația unde vrei să teleportezi obiectul
    public Vector3 newColliderSize = new Vector3(0.1f, 0.1f, 0.1f); // Dimensiunea nouă a Colliderului (exemplu pentru BoxCollider)
    public Transform trayBlueTransform; // Transform-ul pentru "Tray_Blue (1)"
    public Transform burgerPresetInitLocation; // Transform-ul pentru "BurgerPresetInitLocation"
    public static bool teleported = false;
    private float scaleMultiplier = 2.333f;
    private float teleportDelay = 4f;
    public AudioSource audioSource; // Adăugat pentru redarea sunetului

    void Update()
    {
        // Verifică dacă utilizatorul apasă tasta G
        if (Input.GetKeyDown(KeyCode.G))
        {

            RaycastHit hit;

            // Verifică dacă XR Ray Interactor intersectează obiectul
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (audioSource != null)
                    {
                        audioSource.Play();
                    }
                    UnityEngine.Debug.Log("Teleportare lmao");
                    ScaleObject();
                    Teleport();
                }
            }

            // Verifică dacă obiectul a ajuns la Tray_Blue (1)
            if (trayBlueTransform != null && Vector3.Distance(transform.position, trayBlueTransform.position) < 0.1f)
            {
                StartCoroutine(TeleportAfterDelay());
            }
        }
    }

    void Teleport()
    {
        if (teleportDestination != null)
        {
            UnityEngine.Debug.Log($"Destinația de teleportare setată: {teleportDestination.position}");

            // Prevenire probleme de fizică și coliziuni
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Dezactivează efectele fizicii temporar
                rb.useGravity = false; // Dezactivează gravitația temporar
            }

            // Eliminare părinți care pot influența poziția
            Transform parent = transform.parent;
            transform.SetParent(null); // Elimină părintele pentru a seta poziția globală

            // Mută obiectul la locația destinației
            transform.position = teleportDestination.position;

            // Așezare finală pe suprafață
            UnityEngine.Debug.Log($"Teleportat la locația data: {transform.position}");

            // Reatașează părintele (dacă este necesar)
            if (parent != null)
            {
                transform.SetParent(parent);
            }

            // Micșorează Colliderul obiectului
            ResizeCollider();
        }
        else
        {
            UnityEngine.Debug.LogWarning("Teleport destination is not set!");
        }
    }

    void ResizeCollider()
    {
        // Obține Collider-ul obiectului
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            if (collider is BoxCollider boxCollider)
            {
                boxCollider.size = newColliderSize; // Modifică dimensiunea pentru BoxCollider
                UnityEngine.Debug.Log($"BoxCollider micșorat la dimensiunea: {newColliderSize}");
            }
            else if (collider is SphereCollider sphereCollider)
            {
                sphereCollider.radius = Mathf.Min(newColliderSize.x, newColliderSize.y, newColliderSize.z) / 2f;
                UnityEngine.Debug.Log($"SphereCollider micșorat la raza: {sphereCollider.radius}");
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                capsuleCollider.radius = Mathf.Min(newColliderSize.x, newColliderSize.z) / 2f;
                capsuleCollider.height = newColliderSize.y;
                UnityEngine.Debug.Log($"CapsuleCollider micșorat la dimensiunile: Rază - {capsuleCollider.radius}, Înălțime - {capsuleCollider.height}");
            }
            else
            {
                UnityEngine.Debug.LogWarning("Collider-ul nu poate fi micșorat. Nu este un BoxCollider, SphereCollider sau CapsuleCollider.");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Obiectul nu are un Collider atașat.");
        }
    }

    void ScaleObject()
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

    IEnumerator TeleportAfterDelay()
    {
        yield return new WaitForSeconds(teleportDelay);
        if (burgerPresetInitLocation != null)
        {
            transform.position = burgerPresetInitLocation.position;
            UnityEngine.Debug.Log($"{gameObject.name} teleported to BurgerPresetInitLocation after {teleportDelay} seconds.");
            teleported = true;
        }
        else
        {
            UnityEngine.Debug.LogWarning("BurgerPresetInitLocation is not set!");
            teleported = false;
        }
    }
}