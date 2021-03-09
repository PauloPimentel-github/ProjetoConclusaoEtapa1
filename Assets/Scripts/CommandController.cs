using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Command controller.
/// Classe responsável por manipular comandos de cena do jogo
/// </summary>
public class CommandController : MonoBehaviour {

	/// <summary>
	/// Gos to.
	/// Método responsável por altear de cena
	/// </summary>
	/// <param name="scene">Scene.</param>
	public void goTo(String scene) {
		SceneManager.LoadScene(scene);
	}
}
