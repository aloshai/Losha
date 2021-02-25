using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    [Header("Item Viewer")]
    public Camera ItemViewerCamera;
    public float ItemViewDistance = 2f;
    public LayerMask ItemLayerMask;

    public Transform ItemPickView;

    private bool itemLastActive;

    void Update()
    {
        Vector3 lookAtPosition = ItemViewerCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, ItemViewerCamera.nearClipPlane));

        if (Physics.Raycast(new Ray(lookAtPosition, ItemViewerCamera.transform.forward), out var hitInfo, ItemViewDistance, ItemLayerMask))
        {
            var item = hitInfo.transform.GetComponent<Item>();
            ItemPickView.gameObject.SetActive(true);
            itemLastActive = true;
        }
        else if (itemLastActive)
        {
            itemLastActive = false;
            ItemPickView.gameObject.SetActive(false);
        }
    }

}
