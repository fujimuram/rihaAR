using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// 表示する障害物
    /// </summary>
    [SerializeField] GameObject _target;

    /// <summary>
    /// カメラ（= プレイヤー）
    /// </summary>
    [SerializeField] GameObject _camera;

    /// <summary>
    /// 強制的にカメラを前に動かすか
    /// </summary>
    [SerializeField] bool _isForceForward;

    /// <summary>
    /// カメラの移動速度
    /// </summary>
    [SerializeField] float _speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        // 初期値は非表示
        // Enable, Disableで制御
        _target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isForceForward)
        {
            _camera.transform.Translate(0f, 0f, _speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _camera.transform.Translate(-_speed * 2, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _camera.transform.Translate(_speed * 2, 0f, 0f);
        }
    }
}
