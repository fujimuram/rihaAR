using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DemoSettings : MonoBehaviour
{
    // MEMO: インスペクタ上の値（.yamlに保存される）が優先されることに注意！
    // MEMO: 2019.x以下では自動実装のpropはバッキングフィールドの名前がインスペクタに表示される

    #region Primary Config Params
    [field: Header("Primary Config Params")]

    /// <summary>
    /// 障害物の速度
    /// </summary>
    [field: SerializeField]
    public float Speed { get; set; } = 30f;

    /// <summary>
    /// 総歩行距離
    /// </summary>
    [SerializeField]
    private int _distance = 4;
    public int Distance 
    { 
        get => _distance;
        set
        {
            _distance = value;
            this.Distdata = 4.0f + 16.0f * value;
            Debug.Log("Distance have changed!");
            Debug.Log("Dist data: " + this.Distdata);
        }
    }
    /// <summary>
    /// 実際の歩行距離（？）
    /// </summary>
    [SerializeField, NonEditable]
    private float _distdata;
    public float Distdata 
    {
        get => _distdata;
        private set => _distdata = value;
    }

    /// <summary>
    /// 出現させるターゲットの色
    /// </summary>
    [field: SerializeField]
    public Serialize.ColorTable Colors { get; set; }
        

    #endregion

    #region Orbit Config Params
    [field: Header("Orbit Config Params")]

    /// <summary>
    /// 軌道（障害物が飛んでくる軌跡）角度
    /// </summary>
    /// <value>Range: -45 to 45</value>
    [field: SerializeField, Range(-45, 45)]
    public int OrbitDegree { get; set; } = 15;

    /// <summary>
    /// 軌道角度をランダムにするか
    /// </summary>
    [field: SerializeField]
    public bool IsRandom { get; set; } = false;
    /// <summary>
    /// 最小の軌道角度
    /// ランダム生成がtrueのときのみ参照
    /// </summary>
    [field: SerializeField]
    public int MinOrbitDegree { get; set; } = -45;
    /// <summary>
    /// 最大の軌道角度
    /// ランダム生成がtrueのときのみ参照
    /// </summary>
    [field: SerializeField]
    public int MaxOrbitDegree { get; set; } = 45;

    #endregion

    #region Save Config Params
    [field: Header("Save Config Params")]

    /// <summary>
    /// 前回の記録データに上書きするか
    /// </summary>
    [field: SerializeField]
    public bool IsOverwrite { get; set; } = true;

    #endregion

    /// <summary>
    /// シーン制御（適当実装）
    /// </summary>
    [SerializeField] 
    SceneController _scene;
    public SceneController SceneController => _scene;


    /// <summary>
    /// Setterを強制使用させる
    /// インスペクタから変更されたとき, UnityのSetterが機能しないため
    /// </summary>
    [CustomEditor(typeof(DemoSettings))]
    public class CustomInspector : Editor
    {
        /// <summary>
        /// 衝突データ保存ファイル名
        /// </summary>
        string _filename = @"collision_datas.xml";

        public override void OnInspectorGUI () 
        {
            base.OnInspectorGUI();
        
            //!< ボタンを押したタイミングでSetterを反映させる
            if (GUILayout.Button("Use setters/getters"))
            {
                if(target.GetType() == typeof(DemoSettings))
                {
                    DemoSettings getterSetter = (DemoSettings)target;
                    getterSetter.Distance = getterSetter.Distance;
                }
            }

            if (GUILayout.Button("Save to file"))
            {
                //!< 衝突データを保存する
                if ((target as DemoSettings).SceneController.CollisionDatas.Write(_filename))
                    Debug.Log("Data saved!");
            }

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // MEMO: 初期化時にセッター機能しないためあらためて初期化
        this.Distance = this.Distance;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
