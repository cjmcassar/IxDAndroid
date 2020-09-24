using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    public Transform placementIndicator;
    public GameObject selectionUI;

    private List<GameObject> furniture = new List<GameObject>();
    private GameObject curSelected;
    private Camera cam;

    void Start ()
    {
        cam = Camera.main;
        selectionUI.SetActive(false);
    }

    void Update ()
    {
        // first frame we touch the screen
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            // create a ray from where we're touching on the screen
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            // shoot the raycast
            if(Physics.Raycast(ray, out hit))
            {
                // did we hit something?
                if(hit.collider.gameObject != null && furniture.Contains(hit.collider.gameObject))
                {
                    // select the touching object
                    if(curSelected != null && hit.collider.gameObject != curSelected)
                        Select(hit.collider.gameObject);
                    else if(curSelected == null)
                        Select(hit.collider.gameObject);
                }
            }
            else
                Deselect();
        }

        if(curSelected != null && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
            MoveSelected();
    }

    void MoveSelected ()
    {
        Vector3 curPos = cam.ScreenToViewportPoint(Input.touches[0].position);
        Vector3 lastPos = cam.ScreenToViewportPoint(Input.touches[0].position - Input.touches[0].deltaPosition);

        Vector3 touchDir = curPos - lastPos;

        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        curSelected.transform.position += (camRight * touchDir.x + camForward * touchDir.y);
    }

    // called when we select a object piece
    void Select (GameObject selected)
    {
        if(curSelected != null)
            ToggleSelectionVisual(curSelected, false);

        curSelected = selected;
        ToggleSelectionVisual(curSelected, true);
        selectionUI.SetActive(true);
    }

    // called when we deselect a object piece
    void Deselect ()
    {
        if(curSelected != null)
            ToggleSelectionVisual(curSelected, false);

        curSelected = null;
        selectionUI.SetActive(false);
    }

    // called when we select/deselect a object piece
    void ToggleSelectionVisual (GameObject obj, bool toggle)
    {
        obj.transform.Find("Selected").gameObject.SetActive(toggle);
    }

    // called when we press the object button - creates a new object
    public void PlaceFurniture (GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, placementIndicator.position, Quaternion.identity);
        furniture.Add(obj);
        Select(obj);
    }

    public void ScaleSelected (float rate)
    {
        curSelected.transform.localScale += Vector3.one * rate;
    }

    public void RotateSelected (float rate)
    {
        curSelected.transform.eulerAngles += Vector3.up * rate;
    }

    public void SetColor (Image buttonImage)
    {
        MeshRenderer[] meshRenderers = curSelected.GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer mr in meshRenderers)
        {
            if(mr.gameObject.name == "Selected")
                continue;

            mr.material.color = buttonImage.color;
        }
    }

    public void DeleteSelected ()
    {
        furniture.Remove(curSelected);
        Destroy(curSelected);
        Deselect();
    }
}