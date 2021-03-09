using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy controller.
/// Classe responsável por manipular o inimigo
/// </summary>
public class EnemyRightController : MonoBehaviour {

	/* Atributos da Classe */
	private Rigidbody2D enemyRigidbody2D;

	public int enemySpeed;

	// Use this for initialization
	void Start() 
	{
		this.enemyRigidbody2D = GetComponent<Rigidbody2D>();

		this.enemyRigidbody2D.velocity = new Vector2(this.enemySpeed, 0);
	}

	/// <summary>
	/// Raises the became invisible event.
	/// Função nativa do MonoBehavior, destrói um objeto quando o mesmo fica fora de cena
	/// </summary>
	private void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
