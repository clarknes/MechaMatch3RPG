using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoChangeFont : MonoBehaviour {

    public Font newDefaultFont;
    GUIStyle newFontStyle;

	// Use this for initialization
	void Start () {
        newFontStyle.font = newDefaultFont;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
