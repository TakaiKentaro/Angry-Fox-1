using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private Rigidbody2D physics = null;
    [SerializeField]
    private LineRenderer ren = null;
    private const float MaxMagnitude = 3f;
    private Vector2 m_power = Vector2.zero;
    private Camera mainCamera = null; 
    private Transform mainCameraTransform = null;
    private Vector2 s_power = Vector2.zero;
    void Start()
    {
        this.physics = this.GetComponent<Rigidbody2D>();
        this.mainCamera = Camera.main;
        this.mainCameraTransform = this.mainCamera.transform;
    }

    private Vector2 GetMousePosition()
    {
        var position = Input.mousePosition;
        position = this.mainCamera.ScreenToWorldPoint(position);

        return position;
    }
    public void OnMouseDown()
    {
        this.s_power = this.GetMousePosition();

        this.ren.enabled = true;
        this.ren.SetPosition(0, this.physics.position);
        this.ren.SetPosition(1, new Vector2(this.physics.position.x, this.physics.position.y * -1));

    }

    public void OnMouseDrag()
    {
        var position = this.GetMousePosition();
        this.m_power = position - this.s_power;
        if (this.m_power.magnitude > MaxMagnitude * MaxMagnitude)
        {
            this.m_power *= MaxMagnitude / this.m_power.magnitude;
        }

        this.ren.SetPosition(0, this.physics.position);
        this.ren.SetPosition(1, new Vector2((this.physics.position.x + this.m_power.x), this.physics.position.y + this.m_power.y) * -1);
    }

    public void OnMouseUp()
    {
        this.ren.enabled = false;
        this.Flip(this.m_power * -6f); 
    }

    public void Flip(Vector2 force)
    {
        // 瞬間的に力を加えてはじく
        this.physics.AddForce(force, ForceMode2D.Impulse);
    }
}
