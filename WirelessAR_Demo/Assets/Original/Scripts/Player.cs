using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{   
    /// <summary>
    /// 衝突時のエフェクト用画像
    /// </summary>
    [SerializeField]
    private Image _img;

    /// <summary>
    /// 自動で前に進ませるか（デバグ用）
    /// </summary>
    [field: SerializeField]
    public bool IsForceForward { get; set; } = false;

    /// <summary>
    /// 移動速度
    /// </summary>
    [field: SerializeField]
    public float Speed { get; set; } = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        // 透明にしておく
        _img.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        // 徐々に透明へ
        if (_img.color != Color.clear)
        {
            _img.color = Color.Lerp(_img.color, Color.clear, Time.deltaTime);
        }

        if (this.IsForceForward)
        {
            this.transform.Translate(0f, 0f, this.Speed);
        }

        // 左右移動（デバグ用）
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-this.Speed * 2, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(this.Speed * 2, 0f, 0f);
        }
    }

    /// <summary>
    /// 衝突時
    /// </summary>
    /// <param name="other">衝突相手のオブジェクト</param>
    void OnTriggerEnter(Collider other)
    {
        // 何かしらの障害物とヒットしたとき
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Hit");
            this.Damage();
        }
    }

    /// <summary>
    /// ダメージモーション
    /// </summary>
    public virtual void Damage()
    {
        // 画面を赤塗りにする
        _img.color = new Color(0.5f, 0f, 0f, 0.5f);
    }
}
