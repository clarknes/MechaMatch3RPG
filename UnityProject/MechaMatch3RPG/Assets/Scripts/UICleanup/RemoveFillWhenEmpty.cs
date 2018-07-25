using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveFillWhenEmpty : MonoBehaviour {

    public Image fill;
    public Slider slider;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
        if (slider != null)
        {
            fill = slider.fillRect.gameObject.GetComponent<Image>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(fill != null)
        {
            if (slider != null)
            {
                if(slider.value <= 0)
                {
                    fill.enabled = false;
                }
                else
                {
                    fill.enabled = true;
                }
            }
        }
	}
}
