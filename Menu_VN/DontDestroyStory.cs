using UnityEngine;
using System.Collections;

public class DontDestroyStory: MonoBehaviour {
	public static DontDestroyStory InstanceLoad;
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
