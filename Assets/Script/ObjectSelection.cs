using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectSelection : MonoBehaviour
{
    [HideInInspector]
    public int index;
    [SerializeField]
    private ARPlacementInteractable PlacementInteractable;
    [SerializeField]
    private List<GameObject> ListOfObjects;
    [SerializeField]
    private GameObject SelectionMenu;

    private GameObject selectedobj;

    private void Awake()
    {
        PlacementInteractable = GameObject.Find("AR Placement Interactable").GetComponent<ARPlacementInteractable>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {            
            if (EventSystem.current.currentSelectedGameObject != null&& EventSystem.current.currentSelectedGameObject.layer == 5)
            {
                PlacementInteractable.enabled = false;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlacementInteractable.enabled = true;
            }
        }
    }

    public void Select()
    {
        PlacementInteractable.placementPrefab = ListOfObjects[index];
    }

    public void ShowSelectionMenu()
    {
        SelectionMenu.SetActive(true);
    }

    public void HideSelectionMenu()
    {
        SelectionMenu.SetActive(false);
    }

    public void Rest()
    {
        PlacementInteractable.placementPrefab = null;
    }

    public void ActivateOutline()
    {
        if (selectedobj)
        {
            selectedobj.GetComponent<Outline>().enabled = true;
        }
    }

    public void turnoffOutline()
    {
        if (selectedobj)
        {
            selectedobj.GetComponent<Outline>().enabled = false;
        }
    }

    public void getselectedobj()
    {
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.layer == 3)
            {
                selectedobj = EventSystem.current.currentSelectedGameObject;
            }
        }
    }
}
