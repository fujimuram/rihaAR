using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comer : MonoBehaviour
{
    Vector3 reset; 
    Transform mTransform;
    [SerializeField] SensorB sensorB;
    float vx;
    float vz;


    void Awake()
    {
        mTransform = this.transform;
        reset=mTransform.localPosition;  
    }

    void OnEnable()
    {
        vx=-sensorB.VX;
        vz=-sensorB.VZ;
        reset.x=sensorB.X-(0.2f*vx);
        reset.z=sensorB.Z;
        mTransform.localPosition = reset;
    }

    
    void Update()
    {
        mTransform.Translate(Time.deltaTime*vx,0f,Time.deltaTime*vz);

        if ((vx>0&&mTransform.localPosition.x >= 2)||(vx<=0&&mTransform.localPosition.x <= -2))
        {
            this.gameObject.SetActive(false);
        }
    }

}
