using UnityEngine;
using System.Collections;

public class ColissionTest : MonoBehaviour {
	public Vector2 movement_vector2;
	public Transform spawner;
	public GameObject bulletPrefab;
	
	// Use this for initialization
	void Start () {
	
	}
	
	//Classe criada para cenas de batalha contra chefes
	//Ira instanciar o Gameobject de uma bala e ira adicionar uma força a ela
	void Update () {
		//criando variavel de vetor
		movement_vector2 = new Vector2 (700,0);
		//verificando se o botão de atirar foi pressionado
		if (Input.GetButtonDown("Fire1")) {
			
			//Instanciando a bala
			GameObject bulletInstance;
			bulletInstance = Instantiate(bulletPrefab, spawner.transform.position, spawner.rotation) as GameObject;
			
			//Referenciando um componente q posibilita manipular as propriedades fisicas do GameObject
			Rigidbody2D b = bulletInstance.GetComponent<Rigidbody2D>();
			//Adicionando uma força a ele
			b.AddForce (movement_vector2, ForceMode2D.Impulse);
			//MovePosition(rbodyB.position + (movement_vector2 * 125) * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		//Caso a bala saia da tela ela sera destruida, desocupando memoria
		if(coll.gameObject.name == "FUNDO"){
			Destroy(this);
		}
	}
}
