using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	X   X   XXXXX    XXXX    XXXX    XXX     XXXX   XXXXX      XXXXX   XXX      XXX    X   X
//	XX XX   X       X       X       X   X   X       X            X     X  X    X   X    X X 
//	X X X   XXX      XXX     XXX    XXXXX   X  XX   XXX          X     XXXX    X   X     X  
//	X   X   X           X       X   X   X   X   X   X            X     X   X   X   X    X X 
//	X   X   XXXXX   XXXX    XXXX    X   X    XXXX   XXXXX      XXXXX   XXXX     XXX    X   X
// ################################################################################

public class MessageIBox : MonoBehaviour {

	// PRIVATE VARIABLES:
	MessageIBoxOKAction		ActionOK;
	private	object[]		action_objects;

	// PUBLIC VARIABLES:
	public	GameObject		text_title;
	public	GameObject		text_subtitle;
	public	GameObject		button_ok;

	public	delegate void	MessageIBoxOKAction( object[] args );

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	/// <summary>
	/// Funkcja inicjująca okno powiadomienia.
	/// </summary>
	/// <param name="texts"> Teksty, (Tytuł, treść, tekst przycisku OK). </param>
	/// <param name="functionOK"> Funkcja wykonywana po naciśnięciu przycisku OK. </param>
	/// <param name="args"> Argumenty funkcji ok. </param>

	public void Init( string[] texts, MessageIBoxOKAction functionOK, object[] args ) {
		clearBox();
		if ( texts.Length > 0 && texts[0] != null ) { text_title.GetComponent<Text>().text								=	texts[0]; }
		if ( texts.Length > 1 && texts[1] != null ) { text_subtitle.GetComponent<Text>().text							=	texts[1]; }
		if ( texts.Length > 2 && texts[2] != null ) { button_ok.transform.GetChild(0).GetComponent<Text>().text			=	texts[2]; }
		ActionOK		=	functionOK;
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
	/// Czyszczenie okna powiadomienia.
	/// </summary>

	private void clearBox() {
		ActionOK														=	null;
		text_title.GetComponent<Text>().text							=	"Informacja";
		text_subtitle.GetComponent<Text>().text							=	"Czy zapoznałeś się z tą informacją?";
		button_ok.transform.GetChild(0).GetComponent<Text>().text		=	"OK";
		button_ok.GetComponent<Button>().onClick.RemoveAllListeners();
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Wyświetlenie okna powiadomienia
	/// </summary>

	private void showBox() {
		button_ok.GetComponent<Button>().onClick.AddListener( onButtonOKClick );
		gameObject.SetActive( true );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ukrycie okna powiadomienia.
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
	/// Funkcja wykonująca się po naciśnięciu przycisku OK.
	/// </summary>

	private void onButtonOKClick() {
		hideBox();
		if ( ActionOK != null ) { ActionOK( action_objects ); }
	}

	// ######################################################################

}

// ################################################################################