﻿using System.Collections;
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

public class MainMenu : MonoBehaviour {

	// PRIVATE VARIABLES:
	private	string				str_welcome			=	"   Witaj w programie Maszyny Proste, "
													+	"wybierz tryb aby kontynuować. ";

	private	string				str_learn			=	"   Tryb nauki. Dzięki interaktywnym zabawom, "
													+	"naucz się jak działają maszyny proste w świecie fizyki. ";

	private string				str_test			=	"   Tryb testów. Dzięki tym testom, "
													+	"sprawdzisz, czego się nauczyłeś podczas kożystania z tego programu. ";

	private string				str_exit			=	"   Zamknij program i wróć do systemu operacyjnego. ";

	private string				str_sett			=	"   Otwórz okno ustaień i konfiguracji aplikacji. ";

	private string				str_information		=	"   Aplikacja: \"Maszyny Proste\", wersja 1.0, "
													+	"stworzona w środowisku \"UNITY\".   "
													+	"Autorzy: Agata Dziurka, Kamil Karpiński, Grzegorz Klauza.   "
													+	"Projekt Programowania Zespołowego, Uniwersytet Śląski, "
													+	"wydział Matematyki, Fizyki i Chemii, 2018.";

	// PUBLIC VARIABLES:
	public	GameObject			button_learn;
	public	GameObject			button_test;
	public	GameObject			button_exit;
	public	GameObject			button_settings;
	public	GameObject			rawimage_informations;
	public	GameObject			text_informations;

	public	GameObject			component_settings;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	/// <summary>
	/// Uruchamia konfigurację wszystkich komponentów
	/// </summary>

	void Start () {
		button_learn.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_learn.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_learn.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );
		button_test.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_test.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_test.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );
		button_exit.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_exit.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_exit.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );
		button_settings.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_settings.GetComponent<ButtonBehaviour>().setOnMouseClick( ButtonMouseClickBehaviour );
		button_settings.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );
		rawimage_informations.GetComponent<ButtonBehaviour>().setOnMouseOver( ImageMouseOverBehaviour );
		rawimage_informations.GetComponent<ButtonBehaviour>().setOnMouseExit( ImageMouseExitBehaviour );

		text_informations.GetComponent<Text>().text		=	str_welcome;
		text_informations.GetComponent<TextRoll>().setText( str_welcome );

		component_settings.GetComponent<SettingsBox>().Setup();
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Wykonuje polecenia wykonywane w czasie rzeczywistym.
	/// </summary>

	void Update() {
		
		/* ---------- Debug KeyCode ---------- */
		if ( Input.GetKey( KeyCode.LeftControl ) && Input.GetKey( KeyCode.LeftShift ) && Input.GetKey( KeyCode.D ) ) {
			SceneManager.LoadScene( "UI Test Scene" );
		}
		
	}

	// ######################################################################
	//	XXX     X   X   XXXXX   XXXXX    XXX    X   X    XXXX
	//	X  X    X   X     X       X     X   X   XX  X   X    
	//	XXXX    X   X     X       X     X   X   X X X    XXX 
	//	X   X   X   X     X       X     X   X   X  XX       X
	//	XXXX     XXX      X       X      XXX    X   X   XXXX
	// ######################################################################
	/// <summary>
	/// Funkcja wykonująca akcję po najechaniu na przycisk.
	/// </summary>
	/// <param name="args">Arguments.</param>

	public void ButtonMouseOverBehavior( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == button_test ) {
			text_informations.GetComponent<TextRoll>().setText( str_test );
		} else if ( current_button == button_learn ) {
			text_informations.GetComponent<TextRoll>().setText( str_learn );
		} else if ( current_button == button_exit ) {
			text_informations.GetComponent<TextRoll>().setText( str_exit );
		} else if ( current_button == button_settings ) {
			text_informations.GetComponent<TextRoll>().setText( str_sett );
		} else {
			text_informations.GetComponent<TextRoll>().setText( str_welcome );
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonująca akcję po kliknięciu na przycisk.
	/// </summary>
	/// <param name="args">Arguments.</param>

	public void ButtonMouseClickBehaviour( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == button_learn ) {
			SceneManager.LoadScene( "Menu 1" );
		} else if ( current_button == button_test ) {
			SceneManager.LoadScene( "Menu 2" );
		} else if ( current_button == button_exit ) {
			Application.Quit();
		} else if ( current_button == button_settings ) {
			component_settings.GetComponent<SettingsBox>().Init( false );
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonująca akcję po opuszczeniu przycisku przez kursor.
	/// </summary>
	/// <param name="args">Arguments.</param>

	public void ButtonMouseExitBehaviour( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }

		text_informations.GetComponent<TextRoll>().setText( str_welcome );
	}

	// ######################################################################
	//	XXXXX   X   X    XXX     XXXX   XXXXX
	//	  X     XX XX   X   X   X       X    
	//	  X     X X X   XXXXX   X  XX   XXX  
	//	  X     X   X   X   X   X   X   X    
	//	XXXXX   X   X   X   X    XXXX   XXXXX
	// ######################################################################
	/// <summary>
	/// Funkcja wykonująca akcję po najechaniu na obrazek.
	/// </summary>
	/// <param name="args">Arguments.</param>

	public void ImageMouseOverBehaviour( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<RawImage>() == null ) { return; }
		if ( (args[0] as GameObject) == rawimage_informations ) {
			text_informations.GetComponent<TextRoll>().setText( str_information );
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonująca akcję po opuszczeniu obrazka przez kursor.
	/// </summary>
	/// <param name="args">Arguments.</param>

	public void ImageMouseExitBehaviour( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<RawImage>() == null ) { return; }
		if ( (args[0] as GameObject) == rawimage_informations ) {
			text_informations.GetComponent<TextRoll>().setText( str_welcome );
		}
	}

	// ######################################################################

}

// ################################################################################