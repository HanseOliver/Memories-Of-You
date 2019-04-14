using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Ellen_Botoes : MonoBehaviour {

	public GameObject inicio;
	public GameObject obj1;
	public GameObject obj2;
	public GameObject obj3;
	public GameObject obj4;
	public GameObject obj5;
	public GameObject obj6;
	public GameObject obj7;

	public bool ob3;
	public bool ob4;
	public bool ob5;
	public bool ob6;
	public bool final;

	void Update(){
		//caso todos os eventos tenho sido presenciados, o player pode proseguir
		if(ob3 == true && ob4 == true && ob5 == true && ob6 == true){
			final = true;
		}
	}
	
	//setando transições
	public void UmparaDois(){
		obj1.SetActive (false);
		obj2.SetActive (true);
	}
	
	//setando transições
	public void DoisparaTres (){
		obj2.SetActive (false);
		obj3.SetActive (true);
		//verificando q o player passou por certo evento
		ob3 = true;
	}
	
	//setando transições
	public void TresparaInicio(){
		if (final == true) {
			obj3.SetActive (false);
			obj7.SetActive (true);
		} else {
			obj3.SetActive (false);
			obj1.SetActive (true);
		}
	}


	//setando transições
	public void DoisparaQuatro(){
		obj2.SetActive (false);
		obj4.SetActive (true);
		//verificando q o player passou por certo evento
		ob4 = true;
	}
	//setando transições
	public void QuatroparaInicio(){
		if (final == true) {
			obj4.SetActive (false);
			obj7.SetActive (true);
		} else {
			obj4.SetActive (false);
			obj1.SetActive (true);
		}
	}


	//setando transições
	public void DoisparaCinco(){
		obj2.SetActive (false);
		obj5.SetActive (true);
		//verificando q o player passou por certo evento
		ob5 = true;
	}
	
	//setando transições
	public void CincoparaInicio(){
		if (final == true) {
			obj5.SetActive (false);
			obj7.SetActive (true);
		} else {
			obj5.SetActive (false);
			obj1.SetActive (true);
		}

	}


	//setando transições
	public void DoisparaSeis(){
		obj2.SetActive (false);
		obj6.SetActive (true);
		//verificando q o player passou por certo evento
		ob6 = true;
	}
	
	//setando transições
	public void SeisparaInicio(){
		if (final == true) {
			obj6.SetActive (false);
			obj7.SetActive (true);
		} else {
			obj6.SetActive (false);
			obj1.SetActive (true);
		}

	}




}
