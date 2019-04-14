using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using System;


public class DontDestroyOps : MonoBehaviour {
	public static DontDestroyOps InstanceOps;
	public GameObject OPs;

	void Awake ()
	{
		if (InstanceOps == null)
		{
			DontDestroyOnLoad(this.gameObject);

			InstanceOps = this;
		}

		else
		{
			Destroy(this.gameObject);
		}
	}

}
