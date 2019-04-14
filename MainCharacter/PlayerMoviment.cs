using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerMoviment : MonoBehaviour {

	public AudioClip[] bgs = null;
	public AudioSource source;


	public bool hasKey;
	public bool AlbumChecked;
	public bool QuadroChecked;
	public bool RelogioChecked;
	public bool ColchaoChecked;
	public bool RetratoChecked;


	public bool quartoPais;
	public bool quartoNoire;
	public bool poraoEvent;
	public bool escritorioEvent;
	public bool dogEvent;


	public bool quarto1;
	public bool quarto2;
	public bool quarto3;
	public bool portaE;
	public bool tesouro;


	//Controle de Estado Dentro da Dungeon
	public enum GameStateAlice{Jogavel, StoryTelling, Animacao, Pause, Null, Boss, AlbumPRE, QuadroPRE, RelogioPRE, 
		ColchaoPRE, RetratoPRE, DialogoPRE, QuartoPais, QuartoNoire, Porao, Escritorio,
		Quarto1, Quarto2, Quarto3, PortaE};
	public GameStateAlice currentStateAlice;



	//Coisas Gerais
	public Vector2 movement_vector;
	public Rigidbody2D rbody;

	//Alice
	public Camera mainCamera;
	public Camera houseCamera;
	public Camera kitchenCamera;
	public Camera qDestrancadoCamera;
	public Camera qTrancadoCamera;

	//Noire
	public Camera foraCamera;
	public Camera entradaCamera;
	public Camera jantarCamera;
	public Camera quartosCamera;
	public Camera corredorCamera;
	public Camera poraoCamera;
	public Camera nCorredorCamera;
	public Camera escritorioCamera;
	public Camera nQuartoCamera;


	//Ellen
	public Camera segundoCorredor;
	public Camera quartoCorredor;
	public Camera quartoEvento;
	public Camera fstQuarto;
	public Camera thrQuarto;

	public Animator anim;
	public Animator anim2;

	public GameObject player;

	public GameObject mapaExterior;
	public GameObject mapaInterior;
	public GameObject NoireSprite;
	public GameObject PaiSprite;
	public GameObject LapideSprite;


	//Messagem via Sprite
	public int chaves;
	public int Quadro;
	public int Relogio;
	public int Album;
	public int Colchao;
	public int Retrato;
	public int PREdialogo;
	public int PREdialogoNOMES;

	public int eventos;
	public int qPais;
	public int qPaisNOMES;
	public int qNoire;
	public int qNoireNOMES;
	public int pEvent;
	public int pEventNOMES;
	public int escEvent;
	public int escEventNOMES;

	public int qOne;
	public int qTwo;
	public int qThree;

	public TextAsset dialogo;
	public Animator sorenWalk;
	public SpriteRenderer fadeRender;
	public Sprite fimANim;

	//public GameObject SavaPage;
	//public GameObject OpsPage;

	public Image texto;
	public Text desc;
	public Text descN;
	public GameObject cozinha;

	public string[] linhasDialogo; 

	void Start () {
		chaves = 0;eventos = 0;
		setandoLinhas ();
		source.clip = bgs [1];
		source.Play();
		if(LapideSprite !=null){
			LapideSprite.SetActive (false);}
		Album = 1;Quadro = 12;Relogio = 21;Colchao = 29;Retrato = 46;PREdialogo = 61;PREdialogoNOMES = 85;

		qPais = 112;qPaisNOMES = 120;qNoire = 129;qNoireNOMES = 135;pEvent=141;pEventNOMES=152;escEvent = 208;escEventNOMES = 221;

		qOne = 176; qTwo = 185; qThree = 195;

		if(texto !=null){texto.enabled = false;}
		if (desc != null) {desc.enabled = false;}
		if (descN != null) {descN.enabled = false;}
		if (houseCamera != null) {mapaInterior.SetActive (false);houseCamera.enabled = false;}
		if (qDestrancadoCamera != null) {qDestrancadoCamera.enabled = false;}
		if (qTrancadoCamera != null) {qTrancadoCamera.enabled = false;}
		if (kitchenCamera != null) {kitchenCamera.enabled = false;}
		if(entradaCamera != null){entradaCamera.enabled = false;}
	

		if (jantarCamera != null) {jantarCamera.enabled = false;}
		if (quartosCamera != null) {quartosCamera.enabled = false;}
		if (corredorCamera != null) {corredorCamera.enabled = false;}
		if (poraoCamera != null) {poraoCamera.enabled = false;}
		if (nQuartoCamera != null) {nQuartoCamera.enabled = false;}
		if (nCorredorCamera != null) {nCorredorCamera.enabled = false;}
		if (escritorioCamera != null) {escritorioCamera.enabled = false;}



		if (segundoCorredor != null) {segundoCorredor.enabled = false;}
		if (quartoCorredor != null) {segundoCorredor.enabled = false;}
		if (quartoEvento != null) {quartoEvento.enabled = false;}
		if (fstQuarto != null) {fstQuarto.enabled = false;}
		if (thrQuarto != null) {thrQuarto.enabled = false;}

		//player.gameObject.transform.position = new Vector2 (136.6f, 72.9f);

		rbody =this.GetComponent<Rigidbody2D> ();
		//anim = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (chaves == 4) {
			hasKey = true;
		}
		if (quarto1 == true && quarto2 == true && quarto3 == true && tesouro ==true) { portaE = true; }


		if(dogEvent){
			LapideSprite.SetActive (true);
		}
		switch (currentStateAlice) {

		//caso 1. Movimentação do personagem
		case GameStateAlice.Jogavel:
			{
				//velocidade do player
				sorenWalk.speed = 1;
				//config de animação
				anim2.enabled =true;
				//setando controles
				movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
				
				//CONFIGS DE ANIMAÇÃO
				if (movement_vector != Vector2.zero) {
					anim.SetBool ("isWalking", true); 
					anim.SetFloat ("input_x", movement_vector.x);
					anim.SetFloat ("input_y", movement_vector.y);

				} else {
					anim.SetBool ("isWalking", false);
				}
				
				//movendo o personagem
				rbody.MovePosition (rbody.position + (movement_vector * 125) * Time.deltaTime);

				//abrindo menu
				if (Input.GetKeyDown (KeyCode.Escape)) {
					SetCurrentStateAlice (GameStateAlice.Pause);
				}

				//if (Input.GetKeyDown (KeyCode.Q)) {
					
				//	anim2.SetBool ("animate", true);
				//	SetCurrentStateAlice (GameStateAlice.Animacao);



					//}
				}
				
			break;
		//pausa o jogo
		case GameStateAlice.Null:
			{if (Input.GetKeyDown (KeyCode.Z)) {
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;

					SetCurrentStateAlice (GameStateAlice.Jogavel);
				}}
			break;
			
			//o resto dos casos seta interações entre colliders q 
			//mostram caixas de texto com informações importantes
			//o modelo eh basicamente o msm para todos os caso
		case GameStateAlice.AlbumPRE:
			{
				//texto sendo mostrado
				desc.text = linhasDialogo[Album];
				//nome do personagem sendo mostrado
				descN.text = "Soren";
				//proxima linha
				if(Input.GetKeyDown (KeyCode.Z)) {
					Album += 1;
				}
				//setando evento
				if (Album == 9) {
					SetCurrentStateAlice (GameStateAlice.Jogavel);
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					AlbumChecked = true;
					//evento importante
					chaves += 1;
				}
			}
			break;

		case GameStateAlice.QuadroPRE:
			{
				desc.text = linhasDialogo[Quadro];
				descN.text = "Soren";
				if(Input.GetKeyDown (KeyCode.Z)) {
					Quadro += 1;
				}
				if (Quadro == 19) {
					SetCurrentStateAlice (GameStateAlice.Jogavel);
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					QuadroChecked = true;
					chaves += 1;
				}
			}
			break;

		case GameStateAlice.RelogioPRE:
			{
				desc.text = linhasDialogo[Relogio];
				descN.text = "Soren";
				if(Input.GetKeyDown (KeyCode.Z)) {
					Relogio += 1;
				}
				if (Relogio == 27) {
					SetCurrentStateAlice (GameStateAlice.Jogavel);
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					RelogioChecked = true;
					chaves += 1;
				}
			}
			break;

		case GameStateAlice.ColchaoPRE:
			{
				desc.text = linhasDialogo[Colchao];
				descN.text = "Soren";
				if(Input.GetKeyDown (KeyCode.Z)) {
					Colchao += 1;
				}
				if (Colchao == 40) {
					SetCurrentStateAlice (GameStateAlice.Jogavel);
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					ColchaoChecked = true;
					chaves += 1;
				}
			}
			break;

		case GameStateAlice.RetratoPRE:
			{
				desc.text = linhasDialogo[Retrato];
				descN.text = "Soren";
				if(Input.GetKeyDown (KeyCode.Z)) {
					Retrato += 1;
				}
				if (Retrato == 52) {
					SetCurrentStateAlice (GameStateAlice.Jogavel);
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					RetratoChecked = true;
				}
			}
			break;

		case GameStateAlice.DialogoPRE:
			{
				desc.text = linhasDialogo[PREdialogo];
				descN.text = linhasDialogo[PREdialogoNOMES];
				if(Input.GetKeyDown (KeyCode.Z)) {
					PREdialogo += 1;
					PREdialogoNOMES += 1;
				}
				if (PREdialogo == 83) {
					SetCurrentStateAlice (GameStateAlice.Null);
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					SceneManager.LoadScene (2);

				}
			}
			break;

		case GameStateAlice.QuartoPais:
			{
				desc.text = linhasDialogo[qPais];
				descN.text = linhasDialogo[qPaisNOMES];
				if(Input.GetKeyDown (KeyCode.Z)) {
					qPais ++;
					qPaisNOMES++;
				}
				if (qPais == 118) {
					SetCurrentStateAlice (GameStateAlice.Jogavel);
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					quartoPais = true;
					eventos++;
				}
			}
			break;

		case GameStateAlice.QuartoNoire:
			{
				desc.text = linhasDialogo[qNoire];
				descN.text = linhasDialogo[qNoireNOMES];
				if(Input.GetKeyDown (KeyCode.Z)) {
					qNoire ++;
					qNoireNOMES++;
				}
				if (qNoire == 133) {
					SetCurrentStateAlice (GameStateAlice.Jogavel);
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					quartoNoire = true;
					eventos++;

				}
			}
			break;

		case GameStateAlice.Porao:
			{
				desc.text = linhasDialogo [pEvent];
				descN.text = linhasDialogo [pEventNOMES];
				if (Input.GetKeyDown (KeyCode.Z)) {
					pEvent++;
					pEventNOMES++;
				}
				if (pEvent == 146) {
					PaiSprite.SetActive (false);
					NoireSprite.SetActive (false);
				}
				if (pEvent == 151) {
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					poraoEvent = true;
					SetCurrentStateAlice (GameStateAlice.Jogavel);
					eventos++;
					dogEvent = true;

				}
			}
			break;

		case GameStateAlice.Escritorio:
			{
				desc.text = linhasDialogo [escEvent];
				descN.text = linhasDialogo [escEventNOMES];
				if (Input.GetKeyDown (KeyCode.Z)) {
					escEvent++;
					escEventNOMES++;
				}
				if (escEvent ==219) {
					SceneManager.LoadScene (4);
					//SetCurrentStateAlice (GameStateAlice.Jogavel);
				}	
			}
			break;

		case GameStateAlice.Quarto1:
			{
				descN.text = "Soren";
				desc.text = linhasDialogo [qOne];
				if (Input.GetKeyDown (KeyCode.Z)) {
					qOne++;

				}
				if (qOne==182) {
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					quarto1 = true;
					SetCurrentStateAlice (GameStateAlice.Jogavel);
				}
			}
			break;

		case GameStateAlice.Quarto2:
			{
				descN.text = "Soren";
				desc.text = linhasDialogo [qTwo];
				if (Input.GetKeyDown (KeyCode.Z)) {
					qTwo++;

				}
				if (qTwo == 192) {
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					quarto2 = true;
					SetCurrentStateAlice (GameStateAlice.Jogavel);
				}
			}
			break;

		case GameStateAlice.Quarto3:
			{
				descN.text = "Soren";
				desc.text = linhasDialogo [qThree];
				if (Input.GetKeyDown (KeyCode.Z)) {
					qThree++;

				}
				if (qThree == 203) {
					texto.enabled =false;
					desc.enabled = false;
					descN.enabled = false;
					quarto3 = true;
					SetCurrentStateAlice (GameStateAlice.Jogavel);
				}
			}
			break;

		case GameStateAlice.Pause:
			{
				if (Input.GetKeyDown (KeyCode.Escape)) {
					SetCurrentStateAlice (GameStateAlice.Jogavel);
				}
			}
			break;

		case GameStateAlice.Animacao:
			{
				if (this.anim2.GetCurrentAnimatorStateInfo (0).IsName ("Transicao")) {
					if (fadeRender.sprite == fimANim) {
						anim2.SetBool ("animate", false);
						SetCurrentStateAlice (GameStateAlice.Jogavel);
					}
				}
			}
			break;
		}
	}

	//muda de estado
	public void SetCurrentStateAlice(GameStateAlice stateAlice){
		currentStateAlice = stateAlice;
	}

	void setandoLinhas(){
		if (dialogo != null) { 
			linhasDialogo = (dialogo.text.Split ('\n'));
		}

	}






	//setando interações entre os colliders
	//isso seta todas as interações q o personagens vai ter na dungeons
	//o modelo eh o msm para todas as interações
	//em certas interações a mudança na posição do jogar, o teletransporta a outra parte do mapa
	void OnCollisionEnter2D (Collision2D coll)
	{
		//velocidade do player
		sorenWalk.speed = 0;
		
		switch (coll.gameObject.name) {
		//caso 1
		case "Rua_Pt1_End":
			{
				//pausando o jogo
				SetCurrentStateAlice (GameStateAlice.Null);
				//desativando animações
				anim2.enabled =false;
				//ativando interface de Visual Novel
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				//setando texto
				desc.text = "Acho que essa não é a direção correta";
				descN.text = "Soren";
			}
			break;

		case "Lixo":
			{
				SetCurrentStateAlice (GameStateAlice.Null);
				anim2.enabled =false;
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				desc.text = "Está meio cheio.\nOu meio vazio?\nDepende do ponto de vista.\nEstranhamente, a maioria das coisas que estão no lixo são remédios e papeis sujos.\nNão tem praticamente nada relacionado comida.";
				descN.text = "Soren";
			}
			break;

		case "Tv":
			{
				SetCurrentStateAlice (GameStateAlice.Null);
				anim2.enabled =false;
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				desc.text = "Está desligada, e o botão está afundado...\nSe eu considerar essa televisão 'quebrada', vou estar sendo gentil.";
				descN.text = "Soren";
			}
			break;

		case "Pia":
			{
				SetCurrentStateAlice (GameStateAlice.Null);
				anim2.enabled =false;
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				desc.text = "É uma pia limpa. Não tem o que ver aqui.";
				descN.text = "Soren";
			}
			break;

		case "Geladeira":
			{
				SetCurrentStateAlice (GameStateAlice.Null);
				anim2.enabled =false;
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				desc.text = "Está totalmente vazia, exceto por uma garrafa d'água.\nTem um cheiro de coisa velha e mal usada.";
				descN.text = "Soren";
			}
			break;

		case "Fogao":
			{
				SetCurrentStateAlice (GameStateAlice.Null);
				anim2.enabled =false;
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				desc.text = "Limpo e sem nada em cima, além da poeira.\nNada dentro do forno, também.";
				descN.text = "Soren";
			}
			break;

		case "Mesa":
			{
				SetCurrentStateAlice (GameStateAlice.Null);
				anim2.enabled =false;
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				desc.text = "Está bem empoeirado.\nCompletamente desprovido de uso. Não tem nada em cima.";
				descN.text = "Soren";
			}
			break;

		case "Quadro"://chave
			{
				if (QuadroChecked == false) {
					SetCurrentStateAlice (GameStateAlice.QuadroPRE);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
				} else if (QuadroChecked == true) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Este Quadro me intriga";
					descN.text = "Soren";
				}
			}
			break;

		case "Album"://chave
			{
				if (AlbumChecked == false) {
					SetCurrentStateAlice (GameStateAlice.AlbumPRE);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
				} else if (AlbumChecked == true) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Não há nada para ver por aqui, por mais que essa foto seja intrigante...\nQuem é essa mulher?";
					descN.text = "Soren";
				}
			}
			break;

		case "Relogio"://chave
			{
				if (RelogioChecked == false) {
					SetCurrentStateAlice (GameStateAlice.RelogioPRE);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
				} else if (AlbumChecked == true) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Não há nada para ver por aqui, por mais que essa foto seja intrigante...Quem é essa mulher?";
					descN.text = "Soren";
				}
			}
			break;

		case "Colchao"://chave
			{
				if (ColchaoChecked == false) {
					SetCurrentStateAlice (GameStateAlice.ColchaoPRE);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
				} else if (ColchaoChecked == true) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Não tem mais nada para ver nesse colchão";
					descN.text = "Soren";
				}
			}
			break;
		
		case "Retrato":
			{
				if (RetratoChecked == false) {
					SetCurrentStateAlice (GameStateAlice.RetratoPRE);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
				} else if (RetratoChecked == true) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Por que esta mulher esta em todo lugar?";
					descN.text = "Soren";
				}
			}
			break;

			//Transições

		case "Casa_Porta":
			{
				mapaInterior.SetActive (true);
				mapaExterior.SetActive (false);
				player.transform.position = new Vector2 (3663.7f, -325.6f);
				mainCamera.enabled = false;
				houseCamera.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
				//source.Play = bgs [2];
			}
			break;

		case "CasaDentro_Porta":
			{
				mapaExterior.SetActive (true);
				mapaInterior.SetActive (false);
				player.transform.position = new Vector2 (1843.3f, -221.1f);
				houseCamera.enabled = false;
				mainCamera.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 2;
			}
			break;

		case "Cozinha_Transicao":
			{
				player.transform.position = new Vector2 (player.transform.position.x -604.9f, -260.6f);
				houseCamera.enabled = true;
				kitchenCamera.enabled = false;
				//SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				//ordemCamada.sortingOrder = 2;
			}
			break;

		case "Casa_Cozinha":
			{
				player.transform.position = new Vector2 (player.transform.position.x+604.9f, -221.8f);
				houseCamera.enabled = false;
				kitchenCamera.enabled = true;
			}
			break;

		case "Quarto_Trancado_Indo":
			{
				if (hasKey == true) {
					player.transform.position = new Vector2 (3842.6f, 705f);
					houseCamera.enabled = false;
					kitchenCamera.enabled = false;
					qDestrancadoCamera.enabled = false;
					qTrancadoCamera.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					texto.enabled = true;
					SetCurrentStateAlice (GameStateAlice.DialogoPRE);
					source.clip = bgs [0];
					source.Play();
				} else {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;

					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Consigo escutar choros baixos. Parece ser uma garota ou uma criança.\n É melhor eu tentar abrir essa porta.\n Ela tem três trancas.\n Há dicas escritas em arranhões nas portas. Que bizonho...\n Um novo caminho se abre quando o tempo para, uma nova luz se acende, e lembranças são eternizadas.";
					descN.text = "Soren";
				}
			}
			break;

		case "Quarto_Destrancado_Indo":
			{
				player.transform.position = new Vector2 (5609.4f, 647.1f);
				houseCamera.enabled = false;
				kitchenCamera.enabled = false;
				qTrancadoCamera.enabled = false;
				qDestrancadoCamera.enabled = true;
			}
			break;

		case "Quarto_Trancado_Saindo":
			{
				player.transform.position = new Vector2 (3475f, -167.3f);
				kitchenCamera.enabled = false;
				qTrancadoCamera.enabled = false;
				qDestrancadoCamera.enabled = false;
				houseCamera.enabled = true;}
			break;

		case "Quarto_Destrancado_Saindo":
			{
				player.transform.position = new Vector2 (3682.6f, -167.3f);
				kitchenCamera.enabled = false;
				qTrancadoCamera.enabled = false;
				qDestrancadoCamera.enabled = false;
				houseCamera.enabled = true;
			}
			break;


			//Noire
		case "ForaParaDentro":
			{
				player.transform.position = new Vector2 (2202f, -801f);
				foraCamera.enabled = false;
				entradaCamera.enabled = true;
			}
			break;

		case "DentroParaFora":
			{
				player.transform.position = new Vector2 (740.2f, -928.9f);
				entradaCamera.enabled= false;
				foraCamera.enabled = true;
			}
			break;

		case "EntradaParaJantar":
			{
				player.transform.position = new Vector2 (3582f, -650f);
				entradaCamera.enabled= false;
				jantarCamera.enabled = true;
			}
			break;
		
		case "JantarParaEntrada":
			{
				player.transform.position = new Vector2 (2021f, -650f);
				jantarCamera.enabled = false;
				entradaCamera.enabled= true;
			}
			break;

		case "EntradaParaQuartos":
			{
				player.transform.position = new Vector2 (4576.5f, -655.5f);
				entradaCamera.enabled= false;
				quartosCamera.enabled = true;
			}
			break;

		case "Corredor(N)ParaEntrada":
			{
				player.transform.position = new Vector2 (2360.1f, -647.5f);
				nCorredorCamera.enabled= false;
				entradaCamera.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 2;
			}
			break;

		case "EntradaParaCorredor(N)":
			{
				player.transform.position = new Vector2 (8197.1f, -766.9f);
				nCorredorCamera.enabled= true;
				entradaCamera.enabled = false;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "QuartosParaEntrada":
			{
				player.transform.position = new Vector2 (2191.1f, -399f);
				quartosCamera.enabled = false;
				entradaCamera.enabled= true;
			}
			break;

		case "EntradaParaCorredor":
			{
				player.transform.position = new Vector2 (5851.3f, -906.9f);
				entradaCamera.enabled= false;
				corredorCamera.enabled = true;
			}
			break;

		case "CorredorParaEntrada":
			{
				player.transform.position = new Vector2 (2189.3f, -574.7f);
				entradaCamera.enabled= true;
				corredorCamera.enabled = false;
			}
			break;

		case "CorredorParaPorao":
			{
				if (!poraoEvent) {
					player.transform.position = new Vector2 (7292.3f, -956f);
					poraoCamera.enabled = true;
					corredorCamera.enabled = false;
					SetCurrentStateAlice (GameStateAlice.Porao);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					source.clip = bgs [2];
					source.Play();
				} else {
					player.transform.position = new Vector2 (7292.3f, -956f);
					poraoCamera.enabled = true;
					corredorCamera.enabled = false;


				}}
			break;

		case "PoraoParaCorredor":
			{
				player.transform.position = new Vector2 (5851.2f, -626.6f);
				poraoCamera.enabled= false;
				corredorCamera.enabled = true;
			}
			break;

		case "Quarto(N)ParaCorredor(N)":
			{
				player.transform.position = new Vector2 (8807.8f, -771.7f);
				nCorredorCamera.enabled= true;
				nQuartoCamera.enabled = false;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "Corredor(N)ParaQuarto(N)":
			{
				if(!quartoNoire){
					SetCurrentStateAlice (GameStateAlice.QuartoNoire);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
				}
				else{
					player.transform.position = new Vector2 (10726.8f, -753.9f);
					nCorredorCamera.enabled= false;
					nQuartoCamera.enabled = true;
					SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
					ordemCamada.sortingOrder = 3;
				}
			}
			break;

		case "QuartosParaEscritorio":
			{
				if(eventos == 3){
				player.transform.position = new Vector2 (9856.8f,-743f);
				quartosCamera.enabled= false;
				escritorioCamera.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					source.clip = bgs [0];
					source.Play();
				SetCurrentStateAlice (GameStateAlice.Escritorio);
				}else{
				SetCurrentStateAlice (GameStateAlice.Null);
				anim2.enabled =false;
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				desc.text = "Ouço um choramingo \nTenho o precentimento que eu não deveria entrar ai agora";
				descN.text = "Soren";
				}
				}
			break;

		case "EscritorioParaQuartos":
			{
				player.transform.position = new Vector2 (4855.8f, -628.8f);
				quartosCamera.enabled= true;
				escritorioCamera.enabled = false;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "QuartoPais(N)":
			{
				if (quartoPais == false) {
					SetCurrentStateAlice (GameStateAlice.QuartoPais);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
				} else if (quartoPais == true) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Não ha mais ninguem falando";
					descN.text = "Soren";
				}
			}
			break;

		case "Prato":
			{
				SetCurrentStateAlice (GameStateAlice.Null);
				anim2.enabled =false;
				texto.enabled = true;
				desc.enabled = true;
				descN.enabled = true;
				desc.text = "Tres pessoas moram na casa, mas só tem dois pratos na mesa";
				descN.text = "Soren";
			}
			break;

		case "Casinha":
			{
				if (!dogEvent) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text = "Uma casinha de cachorro.\n Não sabia que ela tinha um";
					descN.text = "Soren";
				} else {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text = "Ué, o cachorre se foi assim, do nada.";
					descN.text = "Soren";

				}}
				break;
				case "Lapide":
				{
						SetCurrentStateAlice (GameStateAlice.Null);
						anim2.enabled = false;
						texto.enabled = true;
						desc.enabled = true;
						descN.enabled = true;
						desc.text = "Uma lapide, ha algo escrito nela. \n'Aqui jaz Weiss, o unico ser que se importava comigo.\nE com ele vai também minhas ultimas gotas de humanidade'";
						descN.text = "Soren";

			}break;

		case "IndoSegundoCorr":
			{
				player.transform.position = new Vector2 (-467.5f, -325.2f);
				mainCamera.enabled= false;
				segundoCorredor.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "IndoQuarto1":
			{

				if (!quarto1) {
					player.transform.position = new Vector2 (-1225.8f, 346f);
					mainCamera.enabled= false;
					fstQuarto.enabled = true;
					SetCurrentStateAlice (GameStateAlice.Quarto1);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					descN.text = "Soren";
				} else {
					player.transform.position = new Vector2 (-1225.8f, 346f);
					mainCamera.enabled= false;
					fstQuarto.enabled = true;


				}
			}
			break;

		case "Quarto1ParaFstCorr":
			{
				player.transform.position = new Vector2 (-1155.7f, -275.4f);
				mainCamera.enabled= true;
				fstQuarto.enabled = false;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "SndCorrParaFrtCorr":
			{
				player.transform.position = new Vector2 (-1086.2f, -275.4f);
				mainCamera.enabled= true;
				segundoCorredor.enabled = false;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "SndCorrParaTrdCorr":
			{
				player.transform.position = new Vector2 (785.7f, -179.5f);
				mainCamera.enabled= true;
				segundoCorredor.enabled = false;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "SndCorrParaQuarto2":
			{
				

				if (!quarto2) {
					player.transform.position = new Vector2 (-473.8f, 349.2f);
					quartoEvento.enabled = true;
					segundoCorredor.enabled = false;
					SetCurrentStateAlice (GameStateAlice.Quarto2);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					descN.text = "Soren";
				} else {
					player.transform.position = new Vector2 (-473.8f, 349.2f);
					quartoEvento.enabled = true;
					segundoCorredor.enabled = false;


				}
			}
			break;
		case "Quarto2ParaCorrSnd":
			{//AQUI
				player.transform.position = new Vector2 (114.2f, -292f);
				quartoEvento.enabled= false;
				segundoCorredor.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "3CorrPara2Corr":
			{
				player.transform.position = new Vector2 (116f, -338.3f);
				mainCamera.enabled= false;
				segundoCorredor.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "3CorrParaQuarto3":
			{
				
				if (!quarto3) {
					player.transform.position = new Vector2 (-2385.8f, 347.2f);
					thrQuarto.enabled= true;
					mainCamera.enabled = false;
					SetCurrentStateAlice (GameStateAlice.Quarto3);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					descN.text = "Soren";
				} else {
					player.transform.position = new Vector2 (-2385.8f, 347.2f);
					thrQuarto.enabled= true;
					mainCamera.enabled = false;


				}
			}
			break;

		case "3QuartoPara3Corr":
			{
				player.transform.position = new Vector2 (815.2f, -506.1f);
				thrQuarto.enabled= false;
				mainCamera.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "3CorrPara4Corr":
			{
				player.transform.position = new Vector2 (116.1f,-712.5f);
				mainCamera.enabled= false;
				quartoCorredor.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;
			}
			break;

		case "4CorrPara3Corr":
			{
				player.transform.position = new Vector2 (750f, -506.1f);
				quartoCorredor.enabled= false;
				mainCamera.enabled = true;
				SpriteRenderer ordemCamada = player.GetComponent<SpriteRenderer>();
				ordemCamada.sortingOrder = 3;







			}
			break;

		case "4CorrParaBoss":
			{
				if (portaE == true) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					SceneManager.LoadScene (6);
				} else {
					
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="A porta esta trancada";
					descN.text = "Soren";
				}
			}
			break;

		case "Tesouro":
			{
				if (tesouro == true) {
					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Não ha mais nada aqui";
					descN.text = "Soren";
				} else {

					SetCurrentStateAlice (GameStateAlice.Null);
					anim2.enabled = false;
					texto.enabled = true;
					desc.enabled = true;
					descN.enabled = true;
					desc.text="Nossa, uma chave dentro do bau";
					descN.text = "Soren";
					tesouro = true;
				}
			}
			break;


			}
			
	 	}

	}
