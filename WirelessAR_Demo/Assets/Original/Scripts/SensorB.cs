using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorB : MonoBehaviour
{
    /// <summary>
    /// 表示する障害物
    /// </summary>
    [SerializeField] List<GameObject> _targets;

    /// <summary>
    /// 前方センサー
    /// </summary>
    [SerializeField] SensorA _sensorA;

    /// <summary>
    /// 設定項目
    /// </summary>
    [SerializeField] DemoSettings _settings;

    /// <summary>
    /// シーン制御（適当実装）
    /// </summary>
    [SerializeField] SceneController _scene;

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

    // 評価実験用
    int _loop = 0;
    Dictionary<int, Color> color_tbl = new Dictionary<int, Color>()
    {
        {0, Color.red},
        {1, Color.blue},
    };
    List<(int id, int color_id, int angle)> _objs1 = new List<(int, int, int)>()
    {
        // id, 色, 角度, 
        (0, 0, -30),
        (1, 0, 30),
        (1, 1, -20),
        (0, 1, 20),
        (0, 0, 30),
        (0, 1, 0),
        (1, 0, -10),
        (1, 1, 10),  
    };
    List<(int id, int color_id, int angle)> _objs2 = new List<(int, int, int)>()
    {
        // id, 色, 角度, 
        (0, 0, -30),
        (0, 1, 30),
        (0, 1, -20),
    };
    // 使用オブジェクト群
    List<(int id, int color_id, int angle)> _objs;


    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        _init_pos = this.transform.localPosition;
        _next_pos = _init_pos;

        // ターゲット数を設定
        _scene.SetTargetNum(_targets.Count * _settings.Colors.GetTable().Count);

        // 使用オブジェクト群
        _objs = _objs1;
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
                // 評価実験用
                angle = _objs[_loop].angle;

                // 色
                // NOTE: 煩雑なのでいつか書き換え
                Color? color = null;
                var table = _settings.Colors.GetTable();
                int color_id = 0;

                while (color == null)
                {
                    color_id = Random.Range(0, table.Count);
                    int i = 0;

                    foreach (var pair in table)
                    {
                        if (i++ == color_id && pair.Value == true)
                            color = pair.Key;
                    }
                }
                // 評価実験用
                color = color_tbl[_objs[_loop].color_id];

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
                var sel_target = _targets[_objs[_loop].id];
                sel_target.SetActive(true);

                // ターゲットの設定
                // var target = sel_target.GetComponent<Target>();
                // target.SetColor(color.Value);
                // target.Id = color_id;
                // target.Direction = angle <= 0 ? Direction.Left : Direction.Right;

                // ターゲットの設定（評価実験用）
                var target = sel_target.GetComponent<Target>();
                target.SetColor(color.Value);
                target.Id = _objs[_loop].id * _targets.Count + _objs[_loop].color_id;
                target.Direction = angle <= 0 ? Direction.Left : Direction.Right;

                // 出現情報を格納
                _scene.CollisionDatas.CountAppear(target.Id, target.Direction);

                if (++_loop >= _objs.Count)
                    _loop = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
