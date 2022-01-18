using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// 表示する障害物
    /// /// </summary>
    [SerializeField] GameObject _target;


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

    }
}
