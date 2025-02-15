﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	XXXXX    XXX     XXX    X       XXX      XXX    XXXX       XXX      XXX    X   X
//	  X     X   X   X   X   X       X  X    X   X   X   X      X  X    X   X    X X
//	  X     X   X   X   X   X       XXXX    XXXXX   XXXX       XXXX    X   X     X  
//	  X     X   X   X   X   X       X   X   X   X   X   X      X   X   X   X    X X 
//	  X      XXX     XXX    XXXXX   XXXX    X   X   X   X      XXXX     XXX    X   X
// ################################################################################

public class ToolBarBox : MonoBehaviour {

	// PRIVATE VARIABLES:
	private	ActionStart		functionStart			=	null;
	private	ActionStop		functionStop			=	null;
	private	object[]		objectsStart			=	null;
	private	object[]		objectsStop				=	null;
	private	bool			startstop_status		=	false;

	private	ActionEnd		functionEnd				=	null;
	private	object[]		objectsEnd				=	null;

	private	bool			show_animation			=	false;
	private	bool			hide_animation			=	false;
	private	bool			show_status				=	true;
	private	float			show_height				=	384.0f;
	private	float			hide_height				=	128.0f;
	private	float			show_controls			=	160.0f;
	private	float			speed_animation			=	5.0f;

	private	string			info_title				=	"Informacje";
	private string			info_about				=	"Opis akcji do wykonania.";
	private int				info_size				=	128;

	// PUBLIC VARIABLES:
	public	GameObject		container_tools;
	public	GameObject		button_openclose;
	public	GameObject		button_startstop;
	public	GameObject		button_achivments;
	public	GameObject		button_informations;
	public	GameObject		button_exit;

	public	GameObject		component_achivments;
	public	GameObject		component_messageQBox;
	public	GameObject		component_informations;

	public delegate void	ActionStart( object[] args );
	public delegate	void	ActionStop( object[] args );
	public delegate void	ActionEnd( object[] args );

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//	  X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	/// <summary>
	/// Uruchamia konfigurację wszystkich komponentów
	/// </summary>

	void Start () {
		button_openclose.GetComponent<Button>().onClick.AddListener( animationReactor );
		button_startstop.GetComponent<Button>().onClick.AddListener( startstopReactor );
		button_achivments.GetComponent<Button>().onClick.AddListener( achivmentBoxInit );
		button_informations.GetComponent<Button>().onClick.AddListener( showInformations );
		button_exit.GetComponent<Button>().onClick.AddListener( onExitClick );
		
	}
	
	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonywująca akcje w czasie rzeczywistym.
	/// </summary>

	void Update () {
		if ( show_animation ) { showAnimation(); }
		if ( hide_animation ) { hideAnimation(); }
	}

	// ######################################################################
	//	 XXX    X   X   XXXXX   X   X    XXX    XXXXX   XXXXX    XXX    X   X
	//	X   X   XX  X     X     XX XX   X   X     X       X     X   X   XX  X
	//	XXXXX   X X X     X     X X X   XXXXX     X       X     X   X   X X X
	//	X   X   X  XX     X     X   X   X   X     X       X     X   X   X  XX
	//	X   X   X   X   XXXXX   X   X   X   X     X     XXXXX    XXX    X   X
	// ######################################################################

	/// <summary>
	/// Funckja reakcji na animacje.
	/// </summary>

	private	void animationReactor() {
		if (show_status) {
			hide_animation=true;
		} else {
			show_animation=true;
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Animacja otwarcia okna narzędzi.
	/// </summary>

	private void showAnimation() {
		float	width	=	GetComponent<RectTransform>().sizeDelta.x;
		float	height	=	GetComponent<RectTransform>().sizeDelta.y;

		if ( height < show_height ) {
			float	new_height							=	height + speed_animation;
			if ( height > show_controls ) {
				container_tools.SetActive( true );
			}
			GetComponent<RectTransform>().sizeDelta		=	new Vector2( width, new_height );
		} else {
			show_animation	=	false;
			show_status		=	true;
			GetComponent<RectTransform>().sizeDelta		=	new Vector2( width, show_height );
			button_openclose.transform.GetChild(0).GetComponent<RawImage>().texture		=	Resources.Load( "Images/icon_arrowup" ) as Texture;
		}		
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Animacja zamknięcia okna narzędzi.
	/// </summary>

	private void hideAnimation() {
		float	width	=	GetComponent<RectTransform>().sizeDelta.x;
		float	height	=	GetComponent<RectTransform>().sizeDelta.y;

		if ( height > hide_height ) {
			float	new_height							=	height - speed_animation;
			GetComponent<RectTransform>().sizeDelta		=	new Vector2( width, new_height );
			if ( height < show_controls ) {
				container_tools.SetActive( false );
			}
		} else {
			hide_animation	=	false;
			show_status		=	false;
			GetComponent<RectTransform>().sizeDelta		=	new Vector2( width, hide_height );
			button_openclose.transform.GetChild(0).GetComponent<RawImage>().texture		=	Resources.Load( "Images/icon_arrowdown" ) as Texture;
		}
	}

	// ######################################################################
	//	 XXXX   XXXXX    XXX    XXXX    XXXXX
	//	X         X     X   X   X   X     X  
	//	 XXX      X     XXXXX   XXXX      X  
	//	    X     X     X   X   X   X     X  
	//	XXXX      X     X   X   X   X     X  
	//
	//	 XXXX   XXXXX    XXX    XXXX 
	//	X         X     X   X   X   X
	//	 XXX      X     X   X   XXXX 
	//	    X     X     X   X   X    
	//	XXXX      X      XXX    X    
	// ######################################################################

	/// <summary>
	/// Ustawia funkcje na wybrane akcje w oknie narzędzi.
	/// </summary>
	/// <param name="functionStart"> Funkcja start. </param>
	/// <param name="functionStop"> funkcja stop. </param>
	/// <param name="argsStart"> Argumenty funkcji start. </param>
	/// <param name="argsStop"> Argumenty funkcji stop. </param>

	public void setStartStop( ActionStart functionStart, ActionStop functionStop, object[] argsStart, object[] argsStop ) {
		this.functionStart		=	functionStart;
		this.functionStop		=	functionStop;
		this.objectsStart		=	argsStart;
		this.objectsStop		=	argsStop;
	}

	/// <summary>
	/// Ustawia funkcje na najechanie kursorem na przycisku wybranych akcje w oknie narzędzi.
	/// </summary>
	/// <param name="enterButton"> Funkcja najechania kursorem. </param>
	/// <param name="exitButton"> Funkcja opuszczenia kursorem. </param>

	public void setStartStopHover( ButtonBehaviour.CustomButtonAction enterButton, ButtonBehaviour.CustomButtonAction exitButton ) {
		if ( enterButton != null ) { button_startstop.GetComponent<ButtonBehaviour>().setOnMouseOver( enterButton ); }
		if ( exitButton != null ) { button_startstop.GetComponent<ButtonBehaviour>().setOnMouseExit( exitButton ); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja zarządzająca trybem symulacji.
	/// </summary>
	/// <param name="activeStatus"> Aktywna, nie aktywna. </param>

	public void manualStartStop( bool activeStatus ) {
		startstop_status	=	activeStatus;
		if ( activeStatus ) { ssStart(); }
		else { ssStop(); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja przełączająca tryb symulacji.
	/// </summary>

	private void startstopReactor() {
		if ( startstop_status ) { ssStop(); }
		else { ssStart(); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonywująca się podczas startu symulacji.
	/// </summary>

	private void ssStart() {
		if ( startstop_status ) { return; }
		if ( functionStart != null ) { functionStart( objectsStart ); }
		startstop_status	=	true;
		button_startstop.transform.GetChild(0).GetComponent<RawImage>().texture		=	Resources.Load( "Images/icon_stop" ) as Texture;
		button_startstop.transform.GetChild(1).GetComponent<Text>().text			=	"Stop";
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonywująca się podczas zakończenia symulacji.
	/// </summary>

	private void ssStop() {
		if ( !startstop_status ) { return; }
		if ( functionStop != null ) { functionStop( objectsStop ); }
		startstop_status	=	false;
		button_startstop.transform.GetChild(0).GetComponent<RawImage>().texture		=	Resources.Load( "Images/icon_play" ) as Texture;
		button_startstop.transform.GetChild(1).GetComponent<Text>().text			=	"Start";
	}

	// ######################################################################
	//	 XXX     XXX    X   X   XXXXX   X   X   X   X   XXXXX   X   X   XXXXX    XXXX
	//	X   X   X   X   X   X     X     X   X   XX XX   X       XX  X     X     X    
	//	XXXXX   X       XXXXX     X     X   X   X X X   XXX     X X X     X      XXX 
	//	X   X   X   X   X   X     X      X X    X   X   X       X  XX     X         X
	//	X   X    XXX    X   X   XXXXX     X     X   X   XXXXX   X   X     X     XXXX 
	// ######################################################################
	/// <summary>
	/// Inicjacja komponentu okna postępów.
	/// </summary>

	private void achivmentBoxInit() {
		component_achivments.GetComponent<AchivmentBox>().Init();
	}

	/// <summary>
	/// Ustawia funckje na najechanie kursorem na przycisku wybranych akcje w oknie narzędzi.
	/// </summary>
	/// <param name="enterButton"> Funkcja najechania kursorem. </param>
	/// <param name="exitButton"> Funkcja opuszczenia kursorem. </param>

	public void setAchivmnetsHover( ButtonBehaviour.CustomButtonAction enterButton, ButtonBehaviour.CustomButtonAction exitButton ) {
		if ( enterButton != null ) { button_achivments.GetComponent<ButtonBehaviour>().setOnMouseOver( enterButton ); }
		if ( exitButton != null ) { button_achivments.GetComponent<ButtonBehaviour>().setOnMouseExit( exitButton ); }
	}

	// ######################################################################
	//	XXXXX   X   X   XXXXX    XXX    XXXX    X   X    XXX    XXXXX   XXXXX    XXX    X   X
	//	  X     XX  X   X       X   X   X   X   XX XX   X   X     X       X     X   X   XX  X
	//	  X     X X X   XXX     X   X   XXXX    X X X   XXXXX     X       X     X   X   X X X
	//	  X     X  XX   X       X   X   X   X   X   X   X   X     X       X     X   X   X  XX
	//	XXXXX   X   X   X        XXX    X   X   X   X   X   X     X     XXXXX    XXX    X   X
	// ######################################################################
	/// <summary>
	/// Funkcja konfigurująca podstawowy tekst okna informacji.
	/// </summary>
	/// <param name="title"> Tytuł okna. </param>
	/// <param name="content"> Tekst. </param>
	/// <param name="height"> Wysokość okna. </param>

	public void setInformations( string title, string content, int height ) {
		info_title		=	title;
		info_about		=	content;
		info_size		=	height;
	}

	/// <summary>
	/// Ustawia funckje na najechanie kursorem na przycisku wybranych akcje w oknie narzędzi.
	/// </summary>
	/// <param name="enterButton"> Funkcja najechania kursorem. </param>
	/// <param name="exitButton"> Funkcja opuszczenia kursorem. </param>

	public void setInformationsHover( ButtonBehaviour.CustomButtonAction enterButton, ButtonBehaviour.CustomButtonAction exitButton ) {
		if ( enterButton != null ) { button_informations.GetComponent<ButtonBehaviour>().setOnMouseOver( enterButton ); }
		if ( exitButton != null ) { button_informations.GetComponent<ButtonBehaviour>().setOnMouseExit( exitButton ); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja pokazująca informacje
	/// </summary>

	private void showInformations() {
		component_informations.GetComponent<DescriptionBox>().Init( info_title, info_about, info_size );
	}

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	X        X X      X       X  
	//	XXX       X       X       X  
	//	X        X X      X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	/// <summary>
	/// Ustawia funkcję na przycisk wyjścia.
	/// </summary>
	/// <param name="functionEnd"> Funkcja zamykająca. </param>
	/// <param name="args"> Argumenty funkcji. </param>

	public void setExit( ActionEnd functionEnd, object[] args ) {
		this.functionEnd	=	functionEnd;
		this.objectsEnd		=	args;
	}

	/// <summary>
	/// Ustawia funckje na najechanie kursorem na przycisku wybranych akcje w oknie narzędzi.
	/// </summary>
	/// <param name="enterButton"> Funkcja najechania kursorem. </param>
	/// <param name="exitButton"> Funkcja opuszczenia kursorem. </param>

	public void setExitHover( ButtonBehaviour.CustomButtonAction enterButton, ButtonBehaviour.CustomButtonAction exitButton ) {
		if ( enterButton != null ) { button_exit.GetComponent<ButtonBehaviour>().setOnMouseOver( enterButton ); }
		if ( exitButton != null ) { button_exit.GetComponent<ButtonBehaviour>().setOnMouseExit( exitButton ); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonywana po naciśnięciu na przycisk wyjścia
	/// </summary>

	private void onExitClick() {
		string[]	str_data	=	{ "Kończenie", "Czy napewno chcesz zakończyć zadanie?", "Tak, zakończ", "Nie, kontynuuj" };
		component_messageQBox.GetComponent<MessageQBox>().Init( str_data, onExitYes, null, objectsEnd );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonywana po zaakceptowaniu wyjścia
	/// </summary>
	/// <param name="args">Arguments.</param>

	private void onExitYes( object[] args ) {
		if ( functionEnd != null ) { functionEnd( args ); }
	}

	// ######################################################################
	//	 XXX    XXXXX   X   X   XXXXX   XXXX 
	//	X   X     X     X   X   X       X   X
	//	X   X     X     XXXXX   XXX     XXXX 
	//	X   X     X     X   X   X       X   X
	//	 XXX      X     X   X   XXXXX   X   X
	// ######################################################################
	/// <summary>
	/// Ustwienie pozycji okna paska narzędzi
	/// </summary>
	/// <param name="value"> Wartość 0 - dół, 1 - góra. </param>

	public void contentPosition( float value ) {
		if ( value < 0.0f || value > 1.0f ) { return; }
		transform.GetChild(1).GetComponent<ScrollRect>().verticalScrollbar.value	=	value;
	}

	// ######################################################################

}

// ################################################################################