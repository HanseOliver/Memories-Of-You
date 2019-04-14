using UnityEngine;
using System.Collections;

public class AUTODESTRUCTION : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		//destroy um objeto
		gameObject.SetActive (false);
		Destroy (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
