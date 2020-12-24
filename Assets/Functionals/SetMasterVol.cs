using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //needed to do this behavior
public class SetMasterVol : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVol (float slidervalue)
    {
      // mixer.Setfloat ("MasterVol", slidervalue); // This isn't logarethmic
     mixer.SetFloat ("MasterVol", Mathf.Log10 (slidervalue) * 20 ); //This is log form
    } 

}
