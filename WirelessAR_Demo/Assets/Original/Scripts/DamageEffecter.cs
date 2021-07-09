using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffecter : MonoBehaviour
{
    // 画面を赤にするためのイメージ
    public Image img;

    
 
    public void Start()
    {
        // 透明にして見えなくしておきます。
        img.color = Color.clear;
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			this.img.color = new Color(0.5f, 0f, 0f, 0.5f);
		}
		else
		{
            // 徐々に透明へ
			this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
		}
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        this.Damage();
    }

    public virtual void Damage()
    {
        // 画面を赤塗りにする
        this.img.color = new Color(0.5f, 0f, 0f, 0.5f);
    }
}
