using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] SensorB _sensorB;

    Vector3 _init_pos; 
    Transform _transform;
    float _vx;
    float _vz;
    float _del_z = 3f; // 物体を削除する距離


    void Awake()
    {
        _transform = this.transform;
        _init_pos = _transform.localPosition;  
    }

    void OnEnable()
    {
        // 位置を決定
        // センサーBに当たったときに有効化される
        _vx = -_sensorB.VX;
        _vz = -_sensorB.VZ;
        _init_pos.x = _sensorB.X - (0.2f * _vx);
        _init_pos.z = _sensorB.Z;
        _transform.localPosition = _init_pos;
    }

    // MEMO: 多分削除する <- prefabから生成するため
    // void OnEnable()
    // {
    //     _vx=-_sensorB.VX;
    //     _vz=-_sensorB.VZ;
    //     _init_pos.x=_sensorB.X-(0.2f*_vx);
    //     _init_pos.z=_sensorB.Z;
    //     _transform.localPosition = _init_pos;
    // }

    
    void Update()
    {
        _transform.Translate(Time.deltaTime * _vx, 0f, Time.deltaTime * _vz);

        // 十分にターゲットが通り過ぎてしまったら
        // if ((_vx > 0 && _transform.localPosition.x >= 2) ||
        //     (_vx <= 0 && _transform.localPosition.x <= -2))
        if (_transform.localPosition.z < _sensorB.transform.localPosition.z - _del_z)
        {
            // オブジェクトを無効化
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// オブジェクトの色をセットする
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color) =>
        this.gameObject.GetComponent<Renderer>().material.color = color;
}
