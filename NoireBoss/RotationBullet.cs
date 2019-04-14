using UnityEngine;
using System.Collections;

public class RotationBullet : MonoBehaviour {
	//classe controla um dos padrões de ataque do feche da fase
	
	
	public PlayerHealth php;
	public GameObject sphereOne;
	// Use this for initialization
	void Start () {
		//referencia classe q controle hp do player
		php = GameObject.Find("Soren1").GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		//rotaciona o objeto a q esta acoplado, no caso seria uma bala
		transform.RotateAround(sphereOne.transform.position, new Vector3(0, 0, 1), 100*Time.deltaTime);
	}
	
	//setando interações com outros colliders
	void OnTriggerEnter2D (Collider2D other)
	{
		Physics2D.IgnoreLayerCollision (12, 14);

		Physics2D.IgnoreLayerCollision (14, 12);
		if (other.gameObject.name == "Soren1") {
			Destroy (gameObject);
			php.TakeDamage(1);

		}
	}
}
