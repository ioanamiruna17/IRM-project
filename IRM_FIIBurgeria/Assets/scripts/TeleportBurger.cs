using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportBurger : MonoBehaviour
{
    public GameObject burger; // Referința la BurgerPreset_1
    public Transform teleportLocation; // Referința la locația BurgerTeleportLocation

    private XRRayInteractor rayInteractor;

    void Start()
    {
        // Obține XRRayInteractor din controller-ul utilizat
        rayInteractor = GetComponent<XRRayInteractor>();
        if (rayInteractor == null)
        {
            UnityEngine.Debug.Log("XRRayInteractor not found on this GameObject.");
        }
    }

    void Update()
    {
        // Verifică dacă utilizatorul apasă G sau butonul de pe controller
        if (Input.GetKeyDown(KeyCode.G) || IsControllerButtonPressed())
        {
            HandleRayInteraction();
        }
    }

    private void HandleRayInteraction()
    {
        if (rayInteractor == null) return;

        RaycastHit hit;
        // Verifică dacă ray-ul lovește ceva
        if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Verifică dacă obiectul lovit este burger-ul
            if (hitObject == burger)
            {
                UnityEngine.Debug.Log("Burger detected by ray interactor.");
                TeleportBurgerToLocation();
            }
        }
    }

    private void TeleportBurgerToLocation()
    {
        if (burger != null && teleportLocation != null)
        {
            // Teleportează burger-ul la locația specificată
            burger.transform.position = teleportLocation.position;
            UnityEngine.Debug.Log("Burger teleported to the location.");
        }
        else
        {
            UnityEngine.Debug.Log("Burger or teleport location is not set.");
        }
    }

    private bool IsControllerButtonPressed()
    {
        // Exemplu pentru butonul de pe Meta Quest 2 (Primary Button)
        return Input.GetButtonDown("XRInput.PrimaryButton");
    }
}
