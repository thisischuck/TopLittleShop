using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
    }

    void SwitchToEquip()
    {
        camera.gameObject.transform.position = equipPosition;
        camera.orthographicSize = equipProjection;
    }

    void ResetCamera()
    {
        camera.gameObject.transform.position = startPosition;
        camera.orthographicSize = startProjection;
    }
}
