using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class UIInteractionHandler : MonoBehaviour
{
    [SerializeField] public Transform uiTarget;
    private Transform _originalParent;
    
    private void Start()
    {
        _originalParent = uiTarget.parent;
        SetupInteractable();
    }

    private void SetupInteractable()
    {
        print("Setup UI interactable");
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabStart);
        grabInteractable.selectExited.AddListener(OnGrabEnd);
    }

    private void OnGrabStart(SelectEnterEventArgs args)
    {
        print("OnGrabStart");
        uiTarget.SetParent(args.interactorObject.transform);
        uiTarget.localPosition = Vector3.zero;
    }

    private void OnGrabEnd(SelectExitEventArgs args)
    {
        print("OnGrabEnd");
        uiTarget.SetParent(_originalParent);
        uiTarget.localRotation = Quaternion.identity;
    }
}
