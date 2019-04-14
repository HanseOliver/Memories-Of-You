using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InicioEllen : MonoBehaviour {

	public TextAsset inicio;
	public string[] linhas;
	public int Index;
	public Text caixa;
	public GameObject isto;
	public GameObject Opcao;
	public Game_Control control;
	
	//Classe controla o texto q aparece na tela durante o jogo
	
	// Use this for initialization
	void Start () {
		//chama metodo q separa as linhas
		defLinhas();
		//referencia a um script que controla toda a parte de Visual Novel, incluinod o meni
		control = GameObject.Find ("GameControl").GetComponent<Game_Control> ();
		//apresenta o texto na caixa de texto
		caixa.text = linhas[Index];	
	}
	
	// Update is called once per frame
	void Update () {
		//define o texte q sera apresentado
		caixa.text = linhas[Index];
		//define nova linha a ser aprensentada
		if(Input.GetKeyDown(KeyCode.Z)){
			Index++;
		}
		
		//setando para aparecer botões de interação
		if (Index == 12) {
			
			isto.SetActive(false);
			Opcao.SetActive (true);
			this.enabled = false;
		}

		//final da cena
		if (Index == 86) {

			SceneManager.LoadScene (0);
			control.enabled = true;
		}
	}


	void defLinhas(){
		if (inicio != null) {
			//separa o .txt pelas quebras de linha
			linhas = (inicio.text.Split ('\n'));
		}
	}
}
