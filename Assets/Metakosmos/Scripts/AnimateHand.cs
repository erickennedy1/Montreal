using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue <float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripperValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripperValue);

    }
}


