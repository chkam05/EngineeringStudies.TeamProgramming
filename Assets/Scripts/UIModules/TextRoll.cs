using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	XXXXX   XXXXX   X   X   XXXXX
//	  X     X        X X      X  
//	  X     XXX       X       X  
//	  X     X        X X      X  
//	  X     XXXXX   X   X     X  
//
//	XXXX     XXX    X       X    
//	X   X   X   X   X       X    
//	XXXX    X   X   X       X    
//	X   X   X   X   X       X    
//	X   X    XXX    XXXXX   XXXXX
// ################################################################################

public class TextRoll : MonoBehaviour {

	// PRIVATE VARIABLES:
	private	string				initText		=	"";
	private	string				activeText		=	"";

	private float				timer			=	0.0f;
	private	float				timerTick		=	0.10f;
	private	float				timerSpeed		=	0.01f;

	// PUBLIC VARIABLES:
	// ...

	// ######################################################################
	/// <summary>
	/// Uruchamia konfigurację wszystkich komponentów
	/// </summary>

	void Start () {
		initText		=	gameObject.GetComponent<Text>().text;
		resetText();
	}
	
	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja timera;
	/// </summary>

	void Update () {
		timer			+=	timerSpeed;

		if ( timer >= timerTick ) {
			timer		=	0.0f;
			activeText	=	activeText.Substring( 1 );
		}

		if ( activeText.Length == 0 ) {
			resetText();
		}

		showText();
	}
	
	// ######################################################################
	/// <summary>
	/// Funkcja ustawia tekst przewijający się.
	/// </summary>
	/// <param name="text"> Tekst. </param>

	public void setText( string text ) {
		initText		=	text;
		resetText();
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja pokazująca tekst.
	/// </summary>

	private void showText() {
		gameObject.GetComponent<Text>().text	=	activeText;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja resetująca tekst.
	/// </summary>

	private void resetText() {
		activeText		=	initText;
		timer			=	0.0f;
	}

	// ######################################################################

}

// ################################################################################