using UnityEngine;
using System.Collections;

public class DontDestroyLoad : MonoBehaviour {
	public static DontDestroyLoad InstanceLoad;
	public GameObject LoadCanvas;

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
