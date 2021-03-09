using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Onway.
/// Classe responsável por manipular a superficie na plataforma (OnWay)
/// </summary>
public class OnWay : MonoBehaviour {

	/* ATRIBUTOS DA CLASSE */

	private PlayerController playerController;

	public Transform surface;
	public BoxCollider2D surfaceBoxCollider2D;

	// Use this for initialization
	void Start() 
	{
		//faz a instância do atributo gameController
		this.playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (!this.playerController.isPlayerDestroy) 
		{
			//ativa o colisor da plataforma apenas quando a posição y do player for maior que a posição y da superficie da plataforma
			this.surfaceBoxCollider2D.enabled = ((this.surface.position.y + this.playerController.transform.localScale.y)  < this.playerController.playerPositionY ? true : false);
		}
	}
}
