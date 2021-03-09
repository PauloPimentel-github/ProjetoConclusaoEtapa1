using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Game over controller.
/// Classe Responsável por manipular a animação do inimigo na tela de game over
/// </summary>
public class GameOverController : MonoBehaviour {

	private const int PLAYER_SPEED = 2;

	private Rigidbody2D enemyRigidbody2D;

	// Use this for initialization
	void Start() 
	{
		this.enemyRigidbody2D = GetComponent<Rigidbody2D>();

		this.enemyRigidbody2D.velocity = new Vector2(PLAYER_SPEED, 0);
	}

		
	/// <summary>
	/// Raises the became invisible event.
	/// </summary>
	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
