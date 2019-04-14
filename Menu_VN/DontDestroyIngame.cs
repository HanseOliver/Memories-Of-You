using UnityEngine;
using System.Collections;

public class DontDestroyIngame : MonoBehaviour {

	public static DontDestroyIngame InstanceLoad;
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

}
