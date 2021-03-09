using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Title controller.
/// Classe Responsável por manipular animação do Player e Enemy na tela de título
/// </summary>
public class TitleController : MonoBehaviour {

	private const int PLAYER_SPEED = 2;

	private Rigidbody2D playerRigidbody2D;

	private Rigidbody2D enemyRigidbody2D;

	// Use this for initialization
	void Start() 
	{
		this.playerRigidbody2D = GetComponent<Rigidbody2D>();
		this.enemyRigidbody2D = GetComponent<Rigidbody2D>();

		this.playerRigidbody2D.velocity = new Vector2(PLAYER_SPEED, 0);

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
