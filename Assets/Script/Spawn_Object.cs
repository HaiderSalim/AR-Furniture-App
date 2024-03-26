using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
class Spawn_Object : MonoBehaviour
{
    [SerializeField]
    private GameObject PlaceableObject;
    [SerializeField]
    private GameObject Rotation_slider;
    [SerializeField]
    private GameObject doneButton;
    [SerializeField]
    private GameObject cancelButton;

    private ARPlaneManager planeManager;
    private ARRaycastManager raycastManager;
    private GameObject CurrentObject = null;
    private List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        //EnhancedTouch.Touch.onFingerMove += MoveObject;
        EnhancedTouch.Touch.onFingerDown += Fingerdown; 
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        //EnhancedTouch.Touch.onFingerMove -= MoveObject;
        EnhancedTouch.Touch.onFingerDown -= Fingerdown;
    }

    private void Fingerdown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0 || CurrentObject != null)
            return;

        if (raycastManager.Raycast(finger.currentTouch.screenPosition, aRRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in aRRaycastHits)
            {
                CurrentObject = Instantiate(PlaceableObject, hit.pose.position, hit.pose.rotation);
                ActivateUI();
            }
        }
    }

    public void RotateCurrentObject()
    {
        CurrentObject.transform.localRotation = Quaternion.Euler(0, -Rotation_slider.GetComponent<Slider>().value, 0);
    }

    public void ActivateUI()
    {
        Rotation_slider.SetActive(true);
        doneButton.SetActive(true);
        cancelButton.SetActive(true);
        Rotation_slider.GetComponent<Slider>().value = 0;
    }

    public void DonePlacement()
    {
        if (CurrentObject)
        {
            CurrentObject = null;
            Rotation_slider.SetActive(false);
            doneButton.SetActive(false);
            cancelButton.SetActive(false);
        }
    }

    public void CancelPlacement()
    {
        if (CurrentObject)
        {
            Destroy(CurrentObject);
            CurrentObject = null;
            Rotation_slider.SetActive(false);
            doneButton.SetActive(false);
            cancelButton.SetActive(false);
        }
    }
};
