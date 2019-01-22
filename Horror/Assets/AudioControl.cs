using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour {

    public Slider Volume;
    public AudioSource biosphere;
    public Toggle toggle;
	
	// Update is called once per frame
	void Update () {
        biosphere.volume = Volume.value;

	}
}
