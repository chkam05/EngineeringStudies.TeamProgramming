using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ################################################################################
//	X   X    XXX    XXXXX   X   X      X   X   XXXXX   X   X   X   X
//	XX XX   X   X     X     XX  X      XX XX   X       XX  X   X   X
//	X X X   XXXXX     X     X X X      X X X   XXXX    X X X   X   X
//	X   X   X   X     X     X  XX      X   X   X       X  XX   X   X
//	X   X   X   X   XXXXX   X   X      X   X   XXXXX   X   X    XXX 
// ################################################################################

public class MenuTwo : MonoBehaviour {

	// PRIVATE VARIABLES:
	private	string				str_welcome			=	"   Witaj w trybie testów, "
													+	"wybierz rozdział aby sprawdzić swoją wiedzę w wybranej dziedzinie. ";

	private	string				str_blocks			=	"   Tutaj sprawdzisz swoja wiedzę w zakresie bloków. ";

	private string				str_level			=	"   Tutaj sprawdzisz swoją wiedzę w zakresie równi pochyłej. ";

	private string				str_crane			=	"   Tutaj sprawdzisz swoją wiedzę w zarkesie dźwigni. ";

	private string				str_other			=	"   Tutaj niestety swojej wiedzy nie sprawdzisz. "
													+	"Pamiętasz? Tajemnice Świata. ";

	private string				str_exit			=	"   Powrót do menu głównego. ";

	// PUBLIC VARIABLES:
	public	GameObject			button_block;
	public	GameObject			button_level;
	public	GameObject			button_crane;
	public	GameObject			button_other;
	public	GameObject			button_exit;
	public	GameObject			text_informations;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	void Start () {
		button_block.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_block.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_block.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );
		button_level.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_level.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_level.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );
		button_crane.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_crane.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_crane.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );
		button_other.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_other.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_other.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );
		button_exit.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_exit.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_exit.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );

		text_informations.GetComponent<Text>().text		=	str_welcome;
		text_informations.GetComponent<TextRoll>().setText( str_welcome );
	}

	// ######################################################################
	//	XXX     X   X   XXXXX   XXXXX    XXX    X   X    XXXX
	//	X  X    X   X     X       X     X   X   XX  X   X    
	//	XXXX    X   X     X       X     X   X   X X X    XXX 
	//	X   X   X   X     X       X     X   X   X  XX       X
	//	XXXX     XXX      X       X      XXX    X   X   XXXX
	// ######################################################################

	public void ButtonMouseOverBehavior( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == button_block ) {
			text_informations.GetComponent<TextRoll>().setText( str_blocks );
		} else if ( current_button == button_level ) {
			text_informations.GetComponent<TextRoll>().setText( str_level );
		} else if ( current_button == button_crane ) {
			text_informations.GetComponent<TextRoll>().setText( str_crane );
		} else if ( current_button == button_other ) {
			text_informations.GetComponent<TextRoll>().setText( str_other );
		} else if ( current_button == button_exit ) {
			text_informations.GetComponent<TextRoll>().setText( str_exit );
		} else {
			text_informations.GetComponent<TextRoll>().setText( str_welcome );
		}
	}

	// ----------------------------------------------------------------------
	public void ButtonMouseClickBehaviour( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == button_block ) {
			//
		} else if ( current_button == button_level ) {
			//
		} else if ( current_button == button_crane ) {
			//
		} else if ( current_button == button_other ) {
			//
		} else if ( current_button == button_exit ) {
			SceneManager.LoadScene( "Main Menu" );
		}
	}

	// ----------------------------------------------------------------------
	public void ButtonMouseExitBehaviour( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }

		text_informations.GetComponent<TextRoll>().setText( str_welcome );
	}

	// ######################################################################

}

// ################################################################################