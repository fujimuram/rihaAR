using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorB : MonoBehaviour
{
    /// <summary>
    /// 表示する障害物
    /// </summary>
    [SerializeField] GameObject _target;

    /// <summary>
    /// 前方センサー
    /// </summary>
    [SerializeField] SensorA _sensorA;

    /// <summary>
    /// 設定項目
    /// </summary>
    [SerializeField] DemoSettings _settings;

    Vector3 _init_pos;
    Vector3 _next_pos;

    float _vdata = 1.5f;
    public float VX { get; private set; }
    public float VZ { get; private set; }
    public float X { get; private set; }
    public float Z { get; private set; }
    public int Discrim { get; private set; }
    public float Distance { get; private set; }

    // 重み（？多分）
    float _mag = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        _init_pos = this.transform.localPosition;
        _next_pos = _init_pos;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(_sensorA.IsWalking)
            {
                _sensorA.StopPlayer();
                
                // 軌道角度
                var angle = 0f;
                if (_settings.IsRandom)
                {
                    angle = Random.Range(
                        _settings.MinOrbitDegree,_settings.MaxOrbitDegree);
                }
                else
                {
                    angle = _settings.OrbitDegree;
                }

                // 色
                // NOTE: 煩雑なのでいつか書き換え
                Color? color = null;
                var table = _settings.Colors.GetTable();

                while (color == null)
                {
                    var rand = Random.Range(0, table.Count);
                    int i = 0;

                    foreach (var pair in table)
                    {
                        if (i++ == rand && pair.Value == true)
                            color = pair.Key;
                    }
                }

                // 距離
                // MEMO: センサーBから1~2mの位置にランダムで
                this.Distance = Random.Range(1.0f,2.0f);
                
                Debug.Log(angle + " , " + color + " , " + this.Distance);


                // ターゲットの位置と速度を決定
                this.VX = _vdata * Mathf.Sin(angle * Mathf.Deg2Rad) * _mag;
                this.X = this.Distance * this.VX * _sensorA.Duration;
                this.VZ = _vdata * Mathf.Cos(angle * Mathf.Deg2Rad) * _mag;
                this.Z = this.transform.localPosition.z + this.Distance +
                            this.Distance * this.VZ * _sensorA.Duration;

                // センサーを動かす
                _next_pos.z += this.Distance + 1;
                this.transform.localPosition = _next_pos;
                _sensorA.Move(this.Distance);
        
                // ターゲットを有効化
                _target.SetActive(true);
                _target.GetComponent<Target>().SetColor(color.Value);

                // //count
                // if(angle<=0)
                // {discrim=color*4;}
                // else
                // {discrim=color*4+2;}
                // fw.Count(discrim);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
