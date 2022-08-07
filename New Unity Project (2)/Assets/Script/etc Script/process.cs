/*using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class process : MonoBehaviour
{
    //public PostProcessProfile ppProfile;
    public VolumeProfile vol;
    public LensDistortion lensDistortion;
    private float f_X;
    private float f_Y;


    void Start()
    {
        
        vol = gameObject.GetComponent<Volume>().profile;
        //vol = ppProfile.GetComponent<Volume>(); //vol.profile
        vol.TryGet<LensDistortion>(out lensDistortion);

    }

        // Update is called once per frame
        void Update()
    {
        f_X = ParticleSpawner.Get_MousePos().x;
        f_Y = ParticleSpawner.Get_MousePos().y;
        //lensDistortion.Center_X = f_X;
        //lensDistortion.Center_Y = f_Y;
        //lensDistortion.intensity = new ClampedFloatParameter(60, -100f, 100f, false);
    }
}*/
