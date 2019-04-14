using UnityEngine;
using System.Collections;

public class DontDestroyMain : MonoBehaviour {

	public static DontDestroyMain InstanceLoad;
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
}
