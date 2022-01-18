using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// 表示する障害物
    /// /// </summary>
    [SerializeField] GameObject _target;

    /// <summary>
    /// 衝突回数データ群
    /// </summary>
    private CollisionData _datas;
    public CollisionData CollisionDatas => _datas;


    // Start is called before the first frame update
    void Start()
    {
        // 初期値は非表示
        // Enable, Disableで制御
        _target.SetActive(false);

        // TODO: 衝突データの復元など...
    }

    // Update is called once per frame
    void Update()
    {

    }
}
