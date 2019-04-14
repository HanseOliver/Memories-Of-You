using UnityEngine;
using System.Collections;

public class DontDestroyCamera : MonoBehaviour {
	public static DontDestroyCamera InstanceLoad;
	public GameObject LoadCanvas;
	// Use this for initialization
	void Awake ()
	{
		if (InstanceLoad == null)
		{
			DontDestroyOnLoad(this.gameObject);

			InstanceLoad = this;
		}

		else
		{
			Destroy(this.gameObject);
		}
	}
	// Use this for initialization
}
