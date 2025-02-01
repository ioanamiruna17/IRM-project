using UnityEngine;

public class TeleportLettuce : MonoBehaviour
{
    public Transform teleportDestination; // Locația unde vrei să teleportezi obiectul
    public Vector3 newColliderSize = new Vector3(0.1f, 0.1f, 0.1f); // Dimensiunea nouă a Colliderului (exemplu pentru BoxCollider)

    void Update()
    {
        // Verifică dacă utilizatorul apasă tasta L
        if (Input.GetKeyDown(KeyCode.L))
        {
            RaycastHit hit;

            // Verifică dacă XR Ray Interactor intersectează obiectul
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    UnityEngine.Debug.Log("Teleportare lettuce");
                    Teleport();
                }
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
            UnityEngine.Debug.Log($"Lettuce teleportat la locația data: {transform.position}");

            // Reatașează părintele (dacă este necesar)
            if (parent != null)
            {
                transform.SetParent(parent);
            }

            // Micșorează Colliderul obiectului
            ResizeCollider();

            // Reactivare fizică după teleportare
            if (rb != null)
            {
                rb.isKinematic = false; // Permite fizicii să intervină
                rb.useGravity = true; // Reactivare gravitație
            }
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
}
