using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	public float speed =5f;
	void Update () {
		transform.Rotate(Vector3.back *speed * Time.deltaTime);
	}


}
