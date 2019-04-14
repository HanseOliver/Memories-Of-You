using UnityEngine;
using System.Collections;

public class MOVEMENT : MonoBehaviour {
	
	public int speedH = 0;
	public int speedV = 350;

	void Start(){
		GetComponent <Rigidbody2D> ().velocity = new Vector2(speedH, speedV);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		
		Physics2D.IgnoreLayerCollision (12, 14);
		Physics2D.IgnoreLayerCollision (14, 12);
		
		if (other.gameObject.name == "FUNDO") {
			Destroy (gameObject);
		}
	}
}
