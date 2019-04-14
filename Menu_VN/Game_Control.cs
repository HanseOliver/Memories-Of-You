using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Game_Control : MonoBehaviour {
	//Para garantir a integridade da aplicação
	public static Game_Control Instance;

	//estados da maquina de estados
	public enum GameState{MainMenu, StoryTelling, Pause, Load, Save, Ops};

	//variavel q tomara valor de um dos estados
	public  GameState currentState;

	//arquivos de texto que contém o texto do jogo
	public TextAsset TxtHistoria;
	public TextAsset TxtNome;
	public TextAsset TxtData;
	public TextAsset TxtMusica;
	public TextAsset TxtBg;
	public TextAsset TxtImagens;

	//caixas de texto UI

	public Text Texto;
	public Text Nomes;
	public Text Datas;
	
	//todas as linha dos arquivos de texto;
	public string[] TxtLinesHistoria;
	public string[] TxtLinesNome;
	public string[] TxtLinesData;
	public string[] TxtLinesMusica;
	public string[] TxtLinesBg;
	public string[] TxtLinesImagens;

	//index das arrays
	public int IndexHistoria;
	public int IndexNome;
	public int IndexData;
	public int IndexMusica;
	public int IndexBg;
	public int IndexImagens;

	public string currentLineVN;
	public string currentLineNome;
	public string currentLineData;

	float vol;

	//outros GameObjects que precisam ser referenciados para serem manipulados
	public Button NovoJogo;
	public GameObject mainmenu;//para ativar o canvas do menu principal
	public GameObject load2;
	public GameObject ops;
	public GameObject story;
	public GameObject inGameMenu;

	//botoes de transcição
	public GameObject Load2MainMenu;
	public GameObject Load2StoryTelling;
	public GameObject Ops2MainMenu;
	public GameObject Ops2StoryTelling;


	//controlador de audio
	public AudioSource SoundTrack;
	
	public GameObject bg;
	public GameObject personagem;



	//botões de save
	public GameObject saveBots;
	public GameObject loadBots;

	//referenciando botões do menu
	public GameObject SaveLAbel1;
	public GameObject SaveLAbel2;
	public GameObject SaveLAbel3;
	public GameObject SaveLAbel4;
	public GameObject SaveLAbel5;
	public GameObject SaveLAbel6;
	public GameObject SaveLAbel7;
	public GameObject SaveLAbel8;
	public GameObject SaveLAbel9;

	//referenciando botões do menu
	public GameObject LoadLAbel1;
	public GameObject LoadLAbel2;
	public GameObject LoadLAbel3;
	public GameObject LoadLAbel4;
	public GameObject LoadLAbel5;
	public GameObject LoadLAbel6;
	public GameObject LoadLAbel7;
	public GameObject LoadLAbel8;
	public GameObject LoadLAbel9;
	
	//referenciando camera
	public Camera main;
	
	//instando objeto, caso ele n tenha sido instanciado
	void Awake ()
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(this.gameObject);

			Instance = this;
		}

		else
		{
			Destroy(this.gameObject);
		}

	}
		
	//Setando configurações iniciais
	public void Start () {
		//referenciando interface ingame
		inGameMenu = GameObject.Find ("MainMenu/INgame");
		//referenciado objeto referente as imagens de fundo
		bg = GameObject.Find ("MainMenu/Estoria/BG");
		//referenciado objeto referente as imagens dos personagens
		personagem = GameObject.Find ("MainMenu/Estoria/Image/Personagem");
		//recuperando os arquivos de progresso q foram salvos pelo jogador
		LoadOnStart ();
		//setando linhas da história
		DefLinhas ();
		story = GameObject.Find ("MainMenu/Estoria");
		//setando volume
		vol = 0.7f;
		//setando interface
		load2.SetActive (false);
		ops.SetActive (false);
		story.SetActive(false);
		mainmenu.SetActive (true);
		inGameMenu.SetActive (false);
		SoundTrack.volume = vol;
		//setando estado na maquina de estados
		SetCurrentState(GameState.MainMenu);
		//iniciando controladores de texto
		IndexHistoria =0;
		IndexNome = 0;
		//IndexMusica = 0;
		IndexData = 0;
		currentLineNome = TxtLinesNome[IndexNome];
		currentLineVN = TxtLinesHistoria [IndexHistoria];
		currentLineData = TxtLinesData [IndexData];
		Datas.text = currentLineData;
		Texto.text = currentLineVN;
		Nomes.text = currentLineNome;
		SoundTrack = GameObject.Find ("MusicManager").GetComponent<AudioSource>();

	}

	// Update is called once per frame
	void Update () {
		//Maquina de Estados
		switch(currentState){
			
		//caso 1
			case GameState.MainMenu:{
				
			}break;

		//caso 2
		//Neste estado temos a históira do jogo sendo contada
		case GameState.StoryTelling:
			{
				if (SceneManager.GetActiveScene ().name == "Menu") {
					//inGameMenu = GameObject.Find ("MainMenu/INgame");
					//GameObject.Find("MainMenu").GetComponent<Canvas> ().worldCamera = main;
					//mostra as imagens de fundo
					DisplayBg ();
					//mostra as imsgens dos personagens
					DisplayImages ();
					//define as informações q aparecem na tela
					currentLineNome = TxtLinesNome [IndexNome];
					currentLineVN = TxtLinesHistoria [IndexHistoria];
					currentLineData = TxtLinesData [IndexData];
					//toca a musica
					PLayMusic ();
					//mostra as informações q aparecem na tela
					Datas.text = currentLineData;
					Texto.text = currentLineVN;
					Nomes.text = currentLineNome;

					//proxima linha
					if (Input.GetKeyDown (KeyCode.Z)) {
						IndexHistoria += 1;
						IndexNome += 1;
						IndexMusica += 1;
						IndexData += 1;
						IndexBg++;
						IndexImagens++;

						PLayMusic ();
					}
					
					//pausa o jogo e abre o menu
					if (Input.GetKeyDown (KeyCode.Escape)) {
						load2.SetActive (true);
						SetCurrentState (GameState.Pause);
					}

					SetandoCena ();


				}}
			break;
		
		//Estado de Pause
		case GameState.Pause:{
				
				if (Input.GetKeyDown (KeyCode.Escape)) {
					
					SetCurrentState (GameState.StoryTelling);
				}
			}break;
		
		case GameState.Load:{
				story.SetActive (true);
				SoundTrack.enabled = true;
				SetCurrentState (GameState.StoryTelling);

			}break;

		case GameState.Save:{
				

				/*mainmenu = GameObject.Find ("MainMenu/Dest");
				inGameMenu = GameObject.Find ("MainMenu/INgame");
				Ops2StoryTelling = GameObject.Find ("MainMenu/OPs/Voltar_InGame");
				Ops2MainMenu = GameObject.Find ("MainMenu/OPs/Voltar_MEnu");
				ops = GameObject.Find ("MainMenu/OPs");
				Load2MainMenu = GameObject.Find ("MainMenu/Load/Voltar");
				Load2StoryTelling = GameObject.Find ("MainMenu/Load/Voltar_InGame");
				bg = GameObject.Find ("Estoria/BG");
				personagem = GameObject.Find ("Estoria/Image/Personagem");
*/
				//SoundTrack = GameObject.Find ("MusicManager").GetComponent<AudioSource>();
				SetCurrentState (GameState.Load);
			}break;

		case GameState.Ops:{
				ops.SetActive (true);
			}break;
		}
	}
		
	//setando eventos
	//--------------------------------
	//--------------------------------

	public void SetandoCena(){
		if (IndexHistoria== 685) {
			SetCurrentState (GameState.Save);
			SceneManager.LoadScene (1);
			IndexData =686 ;
			IndexBg= 686;
			IndexHistoria = 686;
			IndexNome = 686;
			IndexImagens = 686;
			IndexMusica = 686;
			this.enabled = false;
			SoundTrack.enabled = false;
			story.SetActive (false);
		}
		if (IndexHistoria== 759) {
			SetCurrentState (GameState.Save);
			SceneManager.LoadScene (3);
			IndexData = 760;
			IndexBg= 760;
			IndexHistoria = 760;
			IndexNome = 760;
			IndexMusica = 760;
			IndexImagens = 760;
			this.enabled = false;
			SoundTrack.enabled = false;
			story.SetActive (false);
		}
		if (IndexHistoria== 762) {
			SetCurrentState (GameState.Save);
			SceneManager.LoadScene (5);
			IndexData = 763;
			IndexBg= 763;
			IndexHistoria = 763;
			IndexNome = 763;
			IndexMusica = 763;
			IndexImagens = 763;
			this.enabled = false;
			SoundTrack.enabled = false;
			story.SetActive (false);
		}

		if (IndexHistoria== 766) {
			InGameMenuToMainMenu ();

		}
	}
	//seta o estado da maquina de estados
	public void SetCurrentState(GameState state){
		currentState = state;
		//lastStateChange = Time.time;
	}

	//Metodos de transição de Canvas
//=================================================================================================================================
	public void MainMenuToStoryReset(){
		load2.SetActive (false);
		mainmenu.SetActive (false);
		ops.SetActive (false);
		story.SetActive (true);		
		IndexData = 0;
		IndexBg= 0;
		IndexHistoria = 0;
		IndexNome = 0;
		IndexImagens = 0;
		IndexMusica = 0;
		SetCurrentState (GameState.StoryTelling);

	}


	public void MainMenuToLoad(){//TranscicoesMenu
		Load2MainMenu.SetActive (true);
		Load2StoryTelling.SetActive (false);
		load2.SetActive (true);
		mainmenu.SetActive (false);
		SetCurrentState(GameState.MainMenu);
		saveBots.SetActive (false);
		loadBots.SetActive (true);
	}

	public void LoadToMainMenu(){//TranscicoesMenu
		load2.SetActive (false);	
		Load2MainMenu.SetActive (false);
		Load2StoryTelling.SetActive (true);
		mainmenu.SetActive (true);
	}

	public void LoadToStoryTelling(){//TranscicoesStory
		SetCurrentState(GameState.StoryTelling);
		Load2MainMenu.SetActive (false);
		Load2StoryTelling.SetActive (true);
		story.SetActive (true);
		load2.SetActive (false);
	}

	public void LoadToMenuInGame(){//TranscicoesStory
		SetCurrentState(GameState.MainMenu);
		Load2MainMenu.SetActive (false);
		Load2StoryTelling.SetActive (false);
		inGameMenu.SetActive (true);
		load2.SetActive (false);
	}
	
	public void StoryTellingToInGameMenu(){//TranscicoesStory
		
		SetCurrentState(GameState.MainMenu);
		story.SetActive (false);
		inGameMenu.SetActive (true);
		SoundTrack.clip=GameObject.Find("MainMenu/MusicManager").GetComponent<MusicManager2>().music [1];

		if(SoundTrack.isPlaying==false){
			SoundTrack.Play ();
		}
	}

	public void InGameMenuToLoad(){//TranscicoesStory

		SetCurrentState(GameState.MainMenu);
		Load2MainMenu.SetActive (false);
		Load2StoryTelling.SetActive (true);
		load2.SetActive (true);
		loadBots.SetActive (true);
		saveBots.SetActive (false);
		inGameMenu.SetActive (false);

	}

	public void InGameMenuToSave(){//TranscicoesStory

		SetCurrentState(GameState.MainMenu);
		Load2MainMenu.SetActive (false);
		Load2StoryTelling.SetActive (true);
		load2.SetActive (true);
		loadBots.SetActive (false);
		saveBots.SetActive (true);
		inGameMenu.SetActive (false);
	}

	public void InGameMenuToOps(){//TranscicoesStory

		SetCurrentState(GameState.MainMenu);
		ops.SetActive (true);
		Ops2MainMenu.SetActive (false);
		Ops2StoryTelling.SetActive (true);
		inGameMenu.SetActive (false);
	}

	public void InGameMenuToStory(){//TranscicoesStory

		SetCurrentState(GameState.StoryTelling);
		story.SetActive (true);
		inGameMenu.SetActive (false);
	}

	public void InGameMenuToMainMenu(){//TranscicoesStory

		SetCurrentState(GameState.MainMenu);
		mainmenu.SetActive (true);
		inGameMenu.SetActive (false);
		story.SetActive (false);
		SoundTrack.clip=GameObject.Find("MainMenu/MusicManager").GetComponent<MusicManager2>().music [0];

		if(SoundTrack.isPlaying==false){
			SoundTrack.Play ();
		}

	}

	public void MenuToOPs(){
			mainmenu.SetActive (false);
			load2.SetActive (false);
			Ops2MainMenu.SetActive (true);
			Ops2StoryTelling.SetActive (false);
			ops.SetActive (true);
	}

	public void OpsToMenu(){
			ops.SetActive (false);
			load2.SetActive (false);
			Ops2MainMenu.SetActive (true);
			Ops2StoryTelling.SetActive (false);
			mainmenu.SetActive (true);
		SaveOps ();
	}
	public void OpsToMenuInGame(){
		ops.SetActive (false);
		load2.SetActive (false);
		Ops2MainMenu.SetActive (false);
		Ops2StoryTelling.SetActive (false);
		inGameMenu.SetActive (true);
		SaveOps ();
	}
	public void VlwFlw(){
		Application.Quit ();
	}
//================================================================================================================================
	//Separando linhas
	void DefLinhas ()
	{
		if (TxtHistoria != null) { 
			TxtLinesHistoria = (TxtHistoria.text.Split ('\n'));
		}

		if (TxtNome != null) { 
			TxtLinesNome = (TxtNome.text.Split ('\n'));
		}

		if (TxtData != null) { 
			TxtLinesData = (TxtData.text.Split ('\n'));
		}

		if (TxtMusica != null) { 
			TxtLinesMusica = (TxtMusica.text.Split ('\n'));
		}

		if (TxtBg != null) { 
			TxtLinesBg = (TxtBg.text.Split ('\n'));
		}

		if (TxtImagens != null) { 
			TxtLinesImagens = (TxtImagens.text.Split ('\n'));
		}
	}

//Manipulação dos arquivos do jogador
//=============================================================================================================================
//A manipulação eh feita via formatador binario
//q transforma os dados selecionados em uma stream de bytes q  n podem ser abertos pelo sistema operacional 
	
	public void Save(int id){
		
			
			
		switch (id) {
		//Botão de Save1
		//O modelo eh o msm para todos os botões
		case 1:
			{
				//criando formatador
				BinaryFormatter bf = new BinaryFormatter ();
				//setando arquivos
				FileStream file1 = File.Create (Application.persistentDataPath + "/playerDATA10.dat");
				Info data1 = new Info ();

				//armazenando informações no arquivo
				//data1.currentState = currentState;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				data1.IndexMusica = IndexMusica;

				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	//GameObject.Find ("TempoDeJogoSave1").GetComponent<Text>().text = currentLineVN;
				//data1.IndexMusica = IndexMusica;
				SaveLAbel1.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel1.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel1.GetComponent<Text>().text;
				SaveLAbel1.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel1.GetComponent<Text> ().text = data1.SaveLabel;

				//salvando informações
				bf.Serialize (file1,data1);
				file1.Close ();
			}
			break;

		case 2:
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file2 = File.Create (Application.persistentDataPath + "/playerDATA20.dat");
				Info data1 = new Info ();

				data1.IndexMusica = IndexMusica;
				//data1.currentState = currentState;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	
				//data1.IndexMusica = IndexMusica;

				SaveLAbel2.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel2.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel2.GetComponent<Text>().text;
				SaveLAbel2.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel2.GetComponent<Text> ().text = data1.SaveLabel;
				bf.Serialize (file2,data1);
				file2.Close ();



			}
			break;

		case 3:
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file3 = File.Create (Application.persistentDataPath + "/playerDATA30.dat");
				Info data1 = new Info ();
				data1.IndexMusica = IndexMusica;
				//data1.currentState = currentState;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	
				//data1.IndexMusica = IndexMusica;
				SaveLAbel3.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel3.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel3.GetComponent<Text>().text;
				SaveLAbel3.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel3.GetComponent<Text> ().text = data1.SaveLabel;
				bf.Serialize (file3,data1);
				file3.Close ();}
			break;

		case 4:
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file4 = File.Create (Application.persistentDataPath + "/playerDATA40.dat");
				Info data1 = new Info ();
				data1.IndexMusica = IndexMusica;
				//data1.currentState = currentState;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	
				//data1.IndexMusica = IndexMusica;

				SaveLAbel4.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel4.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel4.GetComponent<Text>().text;
				SaveLAbel4.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel4.GetComponent<Text> ().text = data1.SaveLabel;
				bf.Serialize (file4,data1);
				file4.Close ();}
			break;

		case 5:
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file5 = File.Create (Application.persistentDataPath + "/playerDATA50.dat");
				Info data1 = new Info ();
				data1.IndexMusica = IndexMusica;
				//data1.currentState = currentState;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	
				//data1.IndexMusica = IndexMusica;
				SaveLAbel5.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel5.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel5.GetComponent<Text>().text;
				SaveLAbel5.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel5.GetComponent<Text> ().text = data1.SaveLabel;
				bf.Serialize (file5,data1);
				file5.Close ();}
			break;

		case 6:
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file6 = File.Create (Application.persistentDataPath + "/playerDATA60.dat");
				Info data1 = new Info ();
				data1.IndexMusica = IndexMusica;
				//data1.currentState = currentState;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	
				//data1.IndexMusica = IndexMusica;
				SaveLAbel6.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel6.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel6.GetComponent<Text>().text;
				SaveLAbel6.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel6.GetComponent<Text> ().text = data1.SaveLabel;
				bf.Serialize (file6,data1);
				file6.Close ();}
			break;

		case 7:
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file7 = File.Create (Application.persistentDataPath + "/playerDATA70.dat");
				Info data1 = new Info ();
				data1.IndexMusica = IndexMusica;
				//data1.currentState = currentState;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	
				//data1.IndexMusica = IndexMusica;
				SaveLAbel7.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel7.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel7.GetComponent<Text>().text;
				SaveLAbel7.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel7.GetComponent<Text> ().text = data1.SaveLabel;
				bf.Serialize (file7,data1);
				file7.Close ();}
			break;

		case 8:
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file8 = File.Create (Application.persistentDataPath + "/playerDATA80.dat");
				Info data1 = new Info ();
				data1.IndexMusica = IndexMusica;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	
				//data1.IndexMusica = IndexMusica;
				SaveLAbel8.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel8.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel8.GetComponent<Text>().text;
				SaveLAbel8.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel8.GetComponent<Text> ().text = data1.SaveLabel;
				bf.Serialize (file8,data1);
				file8.Close ();}
			break;

		case 9:
			{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file9 = File.Create (Application.persistentDataPath + "/playerDATA90.dat");
				Info data1 = new Info ();
				data1.IndexMusica = IndexMusica;
				//data1.currentState = currentState;
				data1.IndexNome = IndexNome;
				data1.IndexHistoria = IndexHistoria;
				data1.IndexData = IndexData;
				data1.IndexBg = IndexBg;
				data1.IndexImagens = IndexImagens;
				//data1.IndexBg = IndexBg;
				//data1.IndexImagens = IndexImagens;
				//data1.vol = vol;	
				//data1.IndexMusica = IndexMusica;
				SaveLAbel9.GetComponent<Text>().text = currentLineData;
				data1.SaveLabel= SaveLAbel9.GetComponent<Text>().text;
				data1.LoadLabel = SaveLAbel9.GetComponent<Text>().text;
				SaveLAbel9.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel9.GetComponent<Text> ().text = data1.SaveLabel;
				bf.Serialize (file9,data1);
				file9.Close ();}
			break;

		}


			

	}
	//Carregamento de progresso via formatador binario
	//o modelo eh o msm para todos os botões
	public void Load(int id){
			


			switch (id) {

		case 1:
			{
				//verifica se o arquivo existe
				if (File.Exists (Application.persistentDataPath + "/playerDATA10.dat")) {
					//caso exista ele vai deserializar
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file1 = File.Open (Application.persistentDataPath + "/playerDATA10.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file1);
					
					//e vai settar os valores serializados como os valores atuais
					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;

					//e fecha o arquivo
					file1.Close ();
					LoadToStoryTelling ();
				}}
				break;

		case 2:
			{
				if (File.Exists (Application.persistentDataPath + "/playerDATA20.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file2 = File.Open (Application.persistentDataPath + "/playerDATA20.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file2);
					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;
					file2.Close ();
					LoadToStoryTelling ();

				}}
				break;

		case 3:
			{
				if (File.Exists (Application.persistentDataPath + "/playerDATA30.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file3 = File.Open (Application.persistentDataPath + "/playerDATA30.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file3);

					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;
					file3.Close ();
					LoadToStoryTelling ();

				}}
				break;
			case 4:
				{
				if (File.Exists (Application.persistentDataPath + "/playerDATA40.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file4 = File.Open (Application.persistentDataPath + "/playerDATA40.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file4);

					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;
					file4.Close ();
					LoadToStoryTelling ();
				}
				}
				break;

		case 5:
			{
				if (File.Exists (Application.persistentDataPath + "/playerDATA50.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file5 = File.Open (Application.persistentDataPath + "/playerDATA50.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file5);
					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;
					file5.Close ();
					LoadToStoryTelling ();

				}}
				break;
		case 6:
			{
				if (File.Exists (Application.persistentDataPath + "/playerDATA60.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file6 = File.Open (Application.persistentDataPath + "/playerDATA60.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file6);
					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;
					file6.Close ();
					LoadToStoryTelling ();

				}}
				break;

			case 7:
				{
				if (File.Exists (Application.persistentDataPath + "/playerDATA70.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file7 = File.Open (Application.persistentDataPath + "/playerDATA70.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file7);

					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;
					file7.Close ();
					LoadToStoryTelling ();
				}
				}
				break;

		case 8:
			{
				if (File.Exists (Application.persistentDataPath + "/playerDATA80.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file8 = File.Open (Application.persistentDataPath + "/playerDATA80.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file8);

					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;
					file8.Close ();
					LoadToStoryTelling ();

				}}
				break;

		case 9:
			{
				if (File.Exists (Application.persistentDataPath + "/playerDATA90.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file9 = File.Open (Application.persistentDataPath + "/playerDATA90.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file9);

					//currentState = data1.currentState;
					IndexNome = data1.IndexNome;
					IndexHistoria = data1.IndexHistoria;
					IndexData = data1.IndexData;
					IndexBg= data1.IndexBg;
					IndexImagens= data1.IndexImagens;
					//IndexBg = data1.IndexBg;
					//IndexImagens = data1.IndexImagens;
					//vol = data1.vol;
					IndexMusica = data1.IndexMusica;
					file9.Close ();
					LoadToStoryTelling ();

				}}
				break;
			}
			

			


	}
	
	public void LoadOnStart(){
	
		//verifica se o arquivo de save existe
		if (File.Exists (Application.persistentDataPath + "/playerDATA10.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file1 = File.Open (Application.persistentDataPath + "/playerDATA10.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file1);
			//caso exista ele seta as labels do botão de save/load correspondente
			//msm modelo para todos os botões
			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel1.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel1.GetComponent<Text> ().text = data1.SaveLabel;
			}
					file1.Close ();
		}	
				
			

				if (File.Exists (Application.persistentDataPath + "/playerDATA20.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file2 = File.Open (Application.persistentDataPath + "/playerDATA20.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file2);
			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel2.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel2.GetComponent<Text> ().text = data1.LoadLabel;

			}
			file2.Close ();
				}

				if (File.Exists (Application.persistentDataPath + "/playerDATA30.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file3 = File.Open (Application.persistentDataPath + "/playerDATA30.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file3);
			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel3.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel3.GetComponent<Text> ().text = data1.LoadLabel;

			}file3.Close ();
				}

				if (File.Exists (Application.persistentDataPath + "/playerDATA40.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file4 = File.Open (Application.persistentDataPath + "/playerDATA40.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file4);

			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel4.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel4.GetComponent<Text> ().text = data1.LoadLabel;
			
			}	file4.Close ();
				}

				if (File.Exists (Application.persistentDataPath + "/playerDATA50.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file5 = File.Open (Application.persistentDataPath + "/playerDATA50.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file5);
			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel5.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel5.GetComponent<Text> ().text = data1.LoadLabel;

			}
			file5.Close ();
				}
				if (File.Exists (Application.persistentDataPath + "/playerDATA60.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file6 = File.Open (Application.persistentDataPath + "/playerDATA60.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file6);
			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel6.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel6.GetComponent<Text> ().text = data1.LoadLabel;

			}file6.Close ();
				}

		if (File.Exists (Application.persistentDataPath + "/playerDATA70.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file7 = File.Open (Application.persistentDataPath + "/playerDATA70.dat", FileMode.Open);
			Info data1 = (Info)bf.Deserialize (file7);
			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel7.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel7.GetComponent<Text> ().text = data1.LoadLabel;
			
			}	file7.Close ();
		}

				if (File.Exists (Application.persistentDataPath + "/playerDATA80.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file8 = File.Open (Application.persistentDataPath + "/playerDATA80.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file8);
			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel8.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel8.GetComponent<Text> ().text = data1.LoadLabel;

					
			}file8.Close ();
				}

				if (File.Exists (Application.persistentDataPath + "/playerDATA90.dat")) {
					BinaryFormatter bf = new BinaryFormatter ();
					FileStream file9 = File.Open (Application.persistentDataPath + "/playerDATA90.dat", FileMode.Open);
					Info data1 = (Info)bf.Deserialize (file9);
			if (data1.SaveLabel != null && data1.SaveLabel != null) {
				SaveLAbel9.GetComponent<Text> ().text = data1.SaveLabel;
				LoadLAbel9.GetComponent<Text> ().text = data1.LoadLabel;

					
			}file9.Close ();
				}
		}
		
	public void SaveOps(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file99 = File.Create (Application.persistentDataPath + "/volumeInfo.dat");
		PlayerOps ops1 = new PlayerOps ();
		ops1.volume = vol; 
		bf.Serialize (file99,ops1);
		file99.Close ();


	







	}
//==============================================================================================================================

	//seta as musicas do jogo
	public void PLayMusic ()
	{
		//numero da musica atual
		int currentMusic = int.Parse (TxtLinesMusica [IndexMusica]);
		//referenciando controlador de musica
		GameObject MusicManager = GameObject.Find ("MusicManager");
		//referenciando fonte de audio
		AudioSource m = MusicManager.GetComponent<AudioSource>(); 
		//tocando musica
		m.clip=MusicManager.GetComponent<MusicManager2>().music [currentMusic];

		if(m.isPlaying==false){
			m.Play ();
		}
	}

	//controle de volume via menu
	public void VolUP(){
		/*vol += 0.05f;
		SoundTrack.volume = vol;
		float show = vol * 100;
		vol_control.text = show.ToString ();
		*/vol =GameObject.Find ("VolSlider").GetComponent<Slider>().value;
		SoundTrack.volume = vol;
		//vol_control.text = vol.ToString();
		}

	/*public void VolDOWN(){
		vol -= 0.05f;
		SoundTrack.volume = vol;
		float show = vol * 100;
		vol_control.text = show.ToString ();
	}*/
	
	void DisplayBg(){//ele joga as imagens no Sprite Renderer para serem exibidas na tela
		int currntSprite = int.Parse(TxtLinesBg [IndexBg]); // cconverte um caracter string em int para q seja utilizavel na array

		SpriteRenderer currSprite = bg.gameObject.GetComponent<SpriteRenderer> ();
		currSprite.sprite = bg.GetComponent<BGS>().bgss[currntSprite];

	}
	
	void DisplayImages(){//ele joga as imagens no Sprite Renderer para serem exibidas na tela
		int currentSprite = int.Parse(TxtLinesImagens [IndexImagens]); // converte um caracter string em int para q seja utilizavel na array

		if (TxtLinesNome[IndexNome] != "") {
			SpriteRenderer currSprite = personagem.gameObject.GetComponent<SpriteRenderer> ();
			currSprite.sprite = personagem.GetComponent<PERSONAGENS>().bgss[currentSprite];
		}

	}

	public void BossFight(){
		SceneManager.LoadScene (1);
	}
		
}

//as informações são carregadas nas vars desta classe e passadas para as vars da classe principal
[Serializable]
public class Info {
	public int IndexHistoria;
	public int IndexNome;
	public int IndexData;
	public int IndexBg;
	public int IndexMusica;
	public int IndexImagens;
	public string SaveLabel;
	public string LoadLabel;




	//public int[] IndexMusica;
	//public int[] IndexBg;

	//public float vol;
	//public int IndexImagens;

}

[Serializable]
public class PlayerOps {

	public float volume;
}