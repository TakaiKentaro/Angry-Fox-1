using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody rb;
    private const float MaxMagnitude = 2f;
    private Vector2 force;
    private Camera mainCamera;
    private Vector3 dragPos = Vector2.zero;

    public void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    public void OnMouseDown()
    {
        var position = Input.mousePosition;
        position.z = mainCamera.transform.position.z;
        position = mainCamera.ScreenToWorldPoint(position);
        position.y = 0;
        dragPos = position;
    }

    public void OnMouseDrag()
    {
        var position = Input.mousePosition;
        position.z = mainCamera.transform.position.z;
        position = mainCamera.ScreenToWorldPoint(position);
        position.y = 0;
        force = position - dragPos;
    }

    public void OnMouseUp()
    {
        rb.AddForce(force * 5f, ForceMode.Impulse);
    }
}
