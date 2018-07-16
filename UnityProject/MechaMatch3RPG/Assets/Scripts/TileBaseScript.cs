using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBaseScript : MonoBehaviour {

    [SerializeField]
    float TileMoveSpeed = 5.0f;
    TileContainerScripts currentStorageTile;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        ResetPosition();
	}

    void ResetPosition()
    {
        currentStorageTile = transform.parent.GetComponent<TileContainerScripts>();
        transform.position = Vector3.Lerp(transform.position, currentStorageTile.transform.position, Time.deltaTime);
    }

    private void OnMouseDown()
    {
        currentStorageTile.tileContained = null;
        Destroy(gameObject);
    }
}
