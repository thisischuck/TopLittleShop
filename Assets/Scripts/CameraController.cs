using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public EquipmentSystem equipmentSystem;
    public float startProjection, equipProjection;
    public Vector3 startPosition, equipPosition;
    private Camera camera;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        camera = GetComponent<Camera>();
        equipmentSystem.EquipOpen += SwitchToEquip;
    }

    void SwitchToEquip()
    {
        camera.gameObject.transform.localPosition = equipPosition;
        camera.orthographicSize = equipProjection;
    }

    public void ResetCamera()
    {
        camera.gameObject.transform.localPosition = startPosition;
        camera.orthographicSize = startProjection;
    }
}
