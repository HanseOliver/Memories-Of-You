using System.Collections;
using UnityEngine;
public class Bullet : MonoBehaviour{ 
	public AliceHealth aliceHP;
	public SorenBossMOve m;
	public int speed = 250;
	public GameObject sc;
	public GameObject shot1;
	public GameObject shot2;
	public GameObject shot3;

	void Start(){
		
		//Referenciando o componente da vida do chefe da fase
		aliceHP = GameObject.Find ("Alice").GetComponent<AliceHealth> ();
		
		//Referenciando a classe q seta o movimento do player
		m = GameObject.Find ("Soren").GetComponent <SorenBossMOve>();
		
		//Diminui o tamanho da sprite do player
		sc.transform.localScale = new Vector2 (25, 25);
		
		//Verifica padrão de movimento do chefe
		if (m.alice.curPOINTS == 1) {
		
			//define direção a qual a bala sera lançada
			GetComponent <Rigidbody2D> ().velocity = new Vector2(speed, 0);
		}
		
		//Verifica padrão de movimento do chefe
		if (m.alice.curPOINTS == 2) {
			//muda a direção da sprite da bala ao colocar um numero negativo no vetor de escala
			sc.transform.localScale = new Vector2 (-25, 25);
			
			//define direção a qual a bala sera lançada
			GetComponent <Rigidbody2D> ().velocity = new Vector2(-speed, 0);
		}
		

	}

	//Controla as interações entre os colliders
	void OnCollisionEnter2D (Collision2D coll){
		//se os tiros pegam no cenario eles são destruidos,
		if (coll.gameObject.name == "FUNDO" | coll.gameObject.name == "Plataforma" | coll.gameObject.name == "Plataforma (1)" | coll.gameObject.name == "Plataforma (2)" | coll.gameObject.name == "Plataforma (3)" ) {
			Destroy (gameObject);
		}
		//se atingem o chefe eles dão dano
		if (coll.gameObject.name == "Alice") {
			Destroy (gameObject);
			aliceHP.TakeDamage (2);
		}
		//se atingem tiros do chefe eles se auto destroem junto com o tiro do chefe (a segunda parte eh setada na clase do tiro do chefe)
		if (coll.gameObject.name == "ShotAlice2(Clone)" | coll.gameObject.name == "ShotAlice3(Clone)") {
			Destroy (gameObject);
		}
	}
	}
	


