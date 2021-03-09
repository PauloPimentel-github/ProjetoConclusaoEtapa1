using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security;

/// <summary>
/// Player controller.
/// Classe responsável por manipular o player
/// </summary>
public class PlayerController : MonoBehaviour {

	private const string SCENE_TITLE = "title";

	/* ATRIBUTOS DA CLASSE */
	private GameController gameController;

	//controla a fisíca do player
	private Rigidbody2D playerRigidbody2D;

	//controla o sprite renderer do player
	//private SpriteRenderer playerSpriteRenderer;

	//controla a animação do player
	private Animator playerAnimator;

	//controla os movimentos do player x, y
	public float speed;
	private int speedX;
	private float speedY;
	private float horizontal;

	//controla a força do pulo do personagem
	public float jumForce;

	//controla se o player está no chão
	private bool isGrounded;
	public Transform groundCheck;

	//controla para qual lado o player está virado
	private bool isLeft;
	private bool isMouseHouse;

	//controla se o player foi destruido
	public bool isPlayerDestroy;

	//controla a posição y do player
	public float playerPositionY;

	public LayerMask whatIsGround;

	// Use this for initialization
	void Start() 
	{
		//faz a instância do atributo gameController
		this.gameController = FindObjectOfType(typeof(GameController)) as GameController;

		//faz a instância dos atributos (componentes - rigidbody2D, animator, spriteRenderer)
		this.playerRigidbody2D = GetComponent<Rigidbody2D>();

		this.playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		this.speedY = this.playerRigidbody2D.velocity.y;
		//ao pressionar a seta esquerda ou direita adiciona o valor na variável horizontal
		this.horizontal = Input.GetAxisRaw("Horizontal");
		//andar
		this.walk(this.horizontal);

		//ao pressionar a tecla de pulo
		if (Input.GetButtonDown("Jump") && this.isGrounded == true)
		{
			this.jump(this.jumForce);
		}
	}

	void LateUpdate() 
	{
		this.playerPositionY = this.transform.position.y;

		//atualiza o animator passando o valor da variável
		this.playerAnimator.SetInteger("speedX", this.speedX);
		this.playerAnimator.SetFloat("speedY", this.speedY);
		this.playerAnimator.SetBool("grounded", this.isGrounded);
	}

	void FixedUpdate() 
	{
		this.isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, 0.02f, this.whatIsGround);
	}
		
	/***** PRIVATE METHODS *****/

	/// <summary>
	/// Método responsável por fazer o player andar
	/// </summary>
	/// <param name="horizontal">Horizontal.</param>
	private void walk(float horizontal)
	{
		this.speedX = (horizontal != 0 ? 1 : 0);

		if (this.isLeft == true && horizontal > 0)
		{
			this.flip();
		}

		if (this.isLeft == false && horizontal < 0)
		{
			this.flip();
		}

		this.playerRigidbody2D.velocity = new Vector2(horizontal * this.speed, this.speedY);
	}

	/// <summary>
	/// Método responsável por aplicar o pulo no player
	/// </summary>
	/// <param name="jumpForce">Jump force.</param>
	private void jump(float jumpForce)
	{
		this.playerRigidbody2D.AddForce(new Vector2(0, jumpForce));
		this.gameController.audioSource.PlayOneShot(this.gameController.fxJumpClip);
	}

	/// <summary>
	/// Método responsável por virar o player na posição x, esquerda ou direita
	/// Verifica para qual lado o player está virado e altera o scale em x
	/// </summary>
	private void flip()
	{
		this.isLeft = !this.isLeft;
		float scaleX = this.transform.localScale.x;
		scaleX *= -1;
		this.transform.localScale = new Vector3(scaleX, this.transform.localScale.y, this.transform.localScale.z);
	}

	/// <summary>
	/// Método responsável por manipular as colisões do player
	/// </summary>
	/// <param name="collision">Collision.</param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.gameObject.tag)
		{
		case "coletavel":
			this.gameController.audioSource.PlayOneShot(this.gameController.fxScoreClip);
			this.gameController.toScore();
			Destroy(collision.gameObject);
			break;

		case "enemyLeft":
			this.gameController.audioSource.PlayOneShot(this.gameController.fxAtackClip);
			Instantiate(this.gameController.enemyLeftAtackPrefab, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
			Destroy(collision.gameObject);
			Destroy(this.gameObject);
			this.isPlayerDestroy = true;
			break;

		case "enemyRight":
			this.gameController.audioSource.PlayOneShot(this.gameController.fxAtackClip);
			Instantiate(this.gameController.enemyRightAtackPrefab, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
			Destroy(collision.gameObject);
			Destroy(this.gameObject);
			this.isPlayerDestroy = true;
			break;

		case "mouseHouse":
			this.gameController.goToScene(SCENE_TITLE);
			break;
		}
	}


}
