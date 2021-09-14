using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private Rigidbody2D physics = null;

    [SerializeField]
    private LineRenderer direction = null;// 発射方向

    private const float MaxMagnitude = 3f;// 最大付与力量

    private Vector2 currentForce = Vector2.zero;// 発射方向の力

    private Camera mainCamera = null;// メインカメラ

    private Transform mainCameraTransform = null;// メインカメラ座標

    private Vector2 dragStart = Vector2.zero;


    void Start()
    {
        this.physics = this.GetComponent<Rigidbody2D>();
        this.mainCamera = Camera.main;
        this.mainCameraTransform = this.mainCamera.transform;
    }


    /// <summary>
    /// マウス座標をワールド座標に変換して取得
    /// </summary>
    /// <returns></returns>
    private Vector2 GetMousePosition()
    {
        // マウスから取得できないZ座標を補完する
        var position = Input.mousePosition;
        //position.z = this.mainCameraTransform.position.z;
        position = this.mainCamera.ScreenToWorldPoint(position);
        //position.z = 0;

        return position;
    }

    /// <summary>
    /// ドラック開始イベントハンドラ
    /// </summary>
    public void OnMouseDown()
    {
        this.dragStart = this.GetMousePosition();

        this.direction.enabled = true;
        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, new Vector2(this.physics.position.x, this.physics.position.y * -1));

    }

    /// <summary>
    /// ドラッグ中イベントハンドラ
    /// </summary>
    public void OnMouseDrag()
    {
        var position = this.GetMousePosition();
        this.currentForce = position - this.dragStart;
        if (this.currentForce.magnitude > MaxMagnitude * MaxMagnitude)
        {
            this.currentForce *= MaxMagnitude / this.currentForce.magnitude;
        }

        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, new Vector2((this.physics.position.x + this.currentForce.x), this.physics.position.y + this.currentForce.y) * -1);
    }

    /// <summary>
    /// ドラッグ終了イベントハンドラ
    /// </summary>
    public void OnMouseUp()
    {
        this.direction.enabled = false;
        this.Flip(this.currentForce * -6f); //引っ張った方の逆にぶっ飛ばす
    }

    /// <summary>
    /// ボールをはじく
    /// </summary>
    /// <param name="force"></param>
    public void Flip(Vector2 force)
    {
        // 瞬間的に力を加えてはじく
        this.physics.AddForce(force, ForceMode2D.Impulse);
    }
}
