using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using SimpleFileBrowser;

public class ObjectPlacer : MonoBehaviour
{
    public Transform placementIndicator;
    public GameObject selectionUI;

    private List<GameObject> Objects = new List<GameObject>();
    private GameObject curSelected;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        selectionUI.SetActive(false);
    }

    public void PlaceObjects (GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, placementIndicator.position, Quaternion.identity);
        Objects.Add(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
