using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public Animator leftHandAnimator;   // Animator for the left hand
    public Animator rightHandAnimator;  // Animator for the right hand
    public XRDirectInteractor leftHandInteractor;  // Interactor for left hand
    public XRDirectInteractor rightHandInteractor; // Interactor for right hand

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Trigger animations
            if (leftHandAnimator != null)
                leftHandAnimator.SetTrigger("Trigger");

            if (rightHandAnimator != null)
                rightHandAnimator.SetTrigger("Trigger");

            // Attempt to grab with both hands
            AttemptGrab(leftHandInteractor);
            AttemptGrab(rightHandInteractor);
        }
    }

    private void AttemptGrab(XRDirectInteractor interactor)
    {
        if (interactor != null)
        {
            // Get the valid targets
            List<IXRInteractable> validTargets = new List<IXRInteractable>();
            interactor.GetValidTargets(validTargets);

            foreach (var target in validTargets)
            {
                if (target is IXRSelectInteractable selectInteractable)
                {
                    // Perform the grab
                    interactor.interactionManager.SelectEnter(interactor, selectInteractable);
                    break; // Grab only the first valid target
                }
            }
        }
    }
}
