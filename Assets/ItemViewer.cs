using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    public Camera Camera;
    public float maxDistance = 2f;
    public LayerMask layerMask;

    public Transform ItemPickView;

    private bool lastActive;

    void Update()
    {
        Vector3 lookAtPosition = Camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.nearClipPlane));

        if (Physics.Raycast(new Ray(lookAtPosition, Camera.transform.forward), out var hitInfo, maxDistance, layerMask))
        {
            var item = hitInfo.transform.GetComponent<Item>();
            Debug.Log(item.name + " " + item.id);

            ItemPickView.gameObject.SetActive(true);
            lastActive = true;
        }
        else if (lastActive)
        {
            lastActive = false;
            ItemPickView.gameObject.SetActive(false);
        }
    }
}
