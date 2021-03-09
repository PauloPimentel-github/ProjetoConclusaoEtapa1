using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Game controller.
/// Classe Responsável por Gerenciar mecanicas da tela
/// </summary>
public class GameController : MonoBehaviour {

	/* CONSTANTES DA CLASSE */
	private const string SCENE_GAME_OVER = "gameOver";

	private const int POSITION_Z = -10;

	private const int SECONDS = 1;

	private const string TIME = "Tempo: ";

	/* ATRIBUTOS DA CLASSE */
	public PlayerController playerController;

	private EnemyLeftController enemyLeftController;

	private EnemyRightController enemyRightController;

	[Header("Config. Player")]
	public Transform leftLimit;
	public Transform rightLimit;
	public Transform positionHouse;
	public GameObject mouseHousePrefab;

	[Header("Config. Enemy")]
	public GameObject enemyLeftPrefab;
	public GameObject enemyRightPrefab;

	public GameObject enemyLeftAtackPrefab;
	public GameObject enemyRightAtackPrefab;

	public Transform enemySpawnLeft;
	public Transform enemySpawnRight;

	public float enemyTempSpawn;

	[Header("Config. Camera")]
	public Camera camera;
	public Transform leftCameraLimit;
	public Transform rightCameraLimit;
	public float speedCamera;

	[Header("Config. Sound")]
	public AudioSource audioSource;
	public AudioClip fxJumpClip;
	public AudioClip fxScoreClip;
	public AudioClip fxAtackClip;
	public AudioClip fxAlertaClip;


	[Header("Config. Score & Time")]
	public int score;
	public Text textScore;

	public int minutes;
	public Text textTime;

	// Use this for initialization
	void Start()
	{
		QualitySettings.vSyncCount = 1;

		this.playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;

		this.textTime.text = TIME + this.minutes.ToString() + "s";

		StartCoroutine("spawnEnemy");

		StartCoroutine("time");
	}
	
	// Update is called once per frame
	void Update() 
	{
		this.playerMovementLimits();

		if (this.playerController.isPlayerDestroy)
		{
			StartCoroutine("gameOver");
		}
	}

	// Update is called after update
	void LateUpdate() 
	{
		this.cameraPositionController();
	}

	/// <summary>
	/// Método responsável pela pontuação
	/// </summary>
	/// <param name="value">Value.</param>
	public void toScore()
	{
		this.score++;
		this.textScore.text = this.score.ToString();

		if (this.score == 10)
		{
			Instantiate(this.mouseHousePrefab);
			this.mouseHousePrefab.transform.position = new Vector3(this.positionHouse.position.x, this.positionHouse.position.y, 0);
		}
	}

	/// <summary>
	/// Tos the time.
	/// Método responsável por calcular o tempo de jogo
	/// </summary>
	public void toTime()
	{
		this.minutes--;

		this.textTime.text = TIME + this.minutes.ToString() + "s";

		if (this.minutes == 0) {
			this.goToScene(SCENE_GAME_OVER);
		}
	}

	/// <summary>
	/// Gos to scene.
	/// Método responsável por mudar de cena
	/// </summary>
	public void goToScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	/***** PRIVATE METHODS *****/

	/// <summary>
	/// Método responsável por controlar limites do player na fase
	/// </summary>
	private void playerMovementLimits()
	{
		if (!this.playerController.isPlayerDestroy) 
		{
			float positionX = this.playerController.transform.position.x;

			if (positionX > this.rightLimit.position.x)
			{
				this.playerController.transform.position = new Vector3(this.rightLimit.position.x, this.playerController.transform.position.y, 0);
			}
			else if (positionX < this.leftLimit.position.x)
			{
				this.playerController.transform.position = new Vector3(this.leftLimit.position.x, this.playerController.transform.position.y, 0);
			}
		}
	}

	/// <summary>
	/// Método responsável por manipular a posição da camera
	/// </summary>
	private void cameraPositionController()
	{
		if (!this.playerController.isPlayerDestroy) 
		{

			if (this.camera.transform.position.x > this.leftCameraLimit.position.x && this.camera.transform.position.x < this.rightCameraLimit.position.x)
			{
				Vector3 targetPositionCamera = new Vector3(this.playerController.transform.position.x, this.camera.transform.position.y, POSITION_Z);
				//recebe a posição atual, posição de destino e uma velocidade em Vector3.lerp
				this.camera.transform.position = Vector3.Lerp(this.camera.transform.position, targetPositionCamera, this.speedCamera * Time.deltaTime);
			}
		}
	}

	/// <summary>
	/// Spawns the enemy.
	/// Coroutine para manipular o spawn do inimigo na tela
	/// </summary>
	/// <returns>The enemy.</returns>
	IEnumerator spawnEnemy() 
	{
		yield return new WaitForSeconds(this.enemyTempSpawn);

		//emite um som de alerta do inimigo
		this.audioSource.PlayOneShot(this.fxAlertaClip);

		//range com sorteio de 0 a 100
		int random = Random.Range(0, 100);

		//calculo com base em 50% para instanciar o inimigo vindo pela esquerda ou direita
		if (random < 50) 
		{
			//faz a instancia de um objeto temporário
			Instantiate(this.enemyLeftPrefab);
			//intanciar na esquerda
			//define a posição que o objeto temporário sera instanciado na tela
			this.enemyLeftPrefab.transform.position = new Vector3(this.enemySpawnLeft.position.x, this.enemySpawnLeft.position.y, 0);
		}
		else 
		{
			Instantiate(this.enemyRightPrefab);
			//instanciar na direita
			//define a posição que o objeto temporário sera instanciado na tela
			this.enemyRightPrefab.transform.position = new Vector3(this.enemySpawnRight.position.x, this.enemySpawnRight.position.y, 0);
		}

		StartCoroutine("spawnEnemy");
	}

	/// <summary>
	/// Time this instance.
	/// Coroutine para manipular o tempo de jogo
	/// </summary>
	IEnumerator time()
	{
		yield return new WaitForSeconds(SECONDS);

		this.toTime();

		StartCoroutine("time");
	}

	/// <summary>
	/// Games the over.
	/// Coroutine para carregar a tela de game over após 5 segundos
	/// </summary>
	/// <returns>The over.</returns>
	IEnumerator gameOver()
	{
		yield return new WaitForSeconds(3);

		this.goToScene(SCENE_GAME_OVER);
	}
}
