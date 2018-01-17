using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	X   X   XXXXX    XXXX    XXXX    XXX     XXXX   XXXXX       XXX    XXX      XXX    X   X
//	XX XX   X       X       X       X   X   X       X          X   X   X  X    X   X    X X 
//	X X X   XXX      XXX     XXX    XXXXX   X  XX   XXX        X X X   XXXX    X   X     X  
//	X   X   X           X       X   X   X   X   X   X          X  XX   X   X   X   X    X X 
//	X   X   XXXXX   XXXX    XXXX    X   X    XXXX   XXXXX       XXX    XXXX     XXX    X   X
// ################################################################################

public class MessageQBox : MonoBehaviour {

	// PRIVATE VARIABLES:
	MessageQBoxYesAction	ActionYES;
	MessageQBoxNoAction		ActionNO;
	private	object[]		action_objects;

	// PUBLIC VARIABLES:
	public	GameObject		text_title;
	public	GameObject		text_subtitle;
	public	GameObject		button_yes;
	public	GameObject		button_no;

	public	delegate void	MessageQBoxYesAction( object[] args );
	public	delegate void	MessageQBoxNoAction( object[] args );

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	/// <summary>
	/// Funkcja inicjująca okno zapytania.
	/// </summary>
	/// <param name="texts"> Teksty, (Tytuł, zapytanie, tekst przycisku Tak, tekst przycisku Nie). </param>
	/// <param name="functionYES"> Funkcja przycisku Tak </param>
	/// <param name="functionNO"> Funkcja przycisku Nie </param>
	/// <param name="args"> Argumenty funckji. </param>

	public void Init( string[] texts, MessageQBoxYesAction functionYES, MessageQBoxNoAction functionNO, object[] args ) {
		clearBox();
		if ( texts.Length > 0 && texts[0] != null ) { text_title.GetComponent<Text>().text								=	texts[0]; }
		if ( texts.Length > 1 && texts[1] != null ) { text_subtitle.GetComponent<Text>().text							=	texts[1]; }
		if ( texts.Length > 2 && texts[2] != null ) { button_yes.transform.GetChild(0).GetComponent<Text>().text		=	texts[2]; }
		if ( texts.Length > 3 && texts[3] != null ) { button_no.transform.GetChild(0).GetComponent<Text>().text			=	texts[3]; }
		ActionYES		=	functionYES;
		ActionNO		=	functionNO;
		action_objects	=	args;
		showBox();
	}
	
	// ######################################################################
	//	X   X    XXX    X   X    XXX     XXXX   XXXXX   XXXX 
	//	XX XX   X   X   XX  X   X   X   X       X       X   X
	//	X X X   XXXXX   X X X   XXXXX   X  XX   XXX     XXXX 
	//	X   X   X   X   X  XX   X   X   X   X   X       X   X
	//	X   X   X   X   X   X   X   X    XXXX   XXXXX   X   X
	// ######################################################################
	/// <summary>
	/// Funkcja czyszcząca okno.
	/// </summary>

	private void clearBox() {
		ActionYES														=	null;
		ActionNO														=	null;
		text_title.GetComponent<Text>().text							=	"Wprowadzanie zmian";
		text_subtitle.GetComponent<Text>().text							=	"Czy chcesz aby polecenie zostało wykonane?";
		button_yes.transform.GetChild(0).GetComponent<Text>().text		=	"Tak";
		button_no.transform.GetChild(0).GetComponent<Text>().text		=	"Nie";
		button_yes.GetComponent<Button>().onClick.RemoveAllListeners();
		button_no.GetComponent<Button>().onClick.RemoveAllListeners();
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wyświetlająca okno
	/// </summary>

	private void showBox() {
		button_yes.GetComponent<Button>().onClick.AddListener( onButtonYesClick );
		button_no.GetComponent<Button>().onClick.AddListener( onButtonNoClick );
		gameObject.SetActive( true );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja ukrywająca okno
	/// </summary>

	private void hideBox() {
		gameObject.SetActive( false );
	}

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX   XXXX     XXX     XXX    XXXXX   XXXXX    XXX    X   X
	//	  X     XX  X     X     X       X   X   X   X   X   X     X       X     X   X   XX  X
	//	  X     X X X     X     XXX     XXXX    XXXXX   X         X       X     X   X   X X X
	//	  X     X  XX     X     X       X   X   X   X   X   X     X       X     X   X   X  XX
	//	XXXXX   X   X     X     XXXXX   X   X   X   X    XXX      X     XXXXX    XXX    X   X
	// ######################################################################
	/// <summary>
	/// Funkcja przycisku Tak
	/// </summary>

	private void onButtonYesClick() {
		hideBox();
		if ( ActionYES != null ) { ActionYES( action_objects ); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja przycisku Nie
	/// </summary>

	private void onButtonNoClick() {
		hideBox();
		if ( ActionNO != null ) { ActionNO( action_objects ); }
	}

	// ######################################################################

}

// ################################################################################