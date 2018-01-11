using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ################################################################################
//	XXXXX   XXXXX    XXXX   XXXXX      X   X   XXXXX
//	  X     X       X         X        X   X     X  
//	  X     XXX      XXX      X        X   X     X
//	  X     X           X     X        X   X     X  
//	  X     XXXXX   XXXX      X         XXX    XXXXX
// ################################################################################

public class DedicatedTestScene : MonoBehaviour {

	// PRIVATE VARIABLES:
	// ...

	// PUBLIC VARIABLES:
	public	GameObject			component_toolbar;
	public	GameObject			component_description;
	public	GameObject			component_achivments;
	public	GameObject			component_settings;
	public	GameObject			component_input;
	public	GameObject			component_messageQbox;
	public	GameObject			component_messageIbox;
	public	GameObject			component_ending;
	public	GameObject			component_statusbar;

	public	GameObject			button_description_test;
	public	GameObject			button_achivments_test;
	public	GameObject			button_settings_test;
	public	GameObject			button_input_test;
	public	GameObject			button_messageQbox_test;
	public	GameObject			button_messageIbox_test;
	public	GameObject			button_winning_test;
	public	GameObject			button_lossing_test;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	void Start () {
		component_achivments.GetComponent<AchivmentBox>().Setup();
		achivmentBoxSetup();
		component_settings.GetComponent<SettingsBox>().Setup();

		button_description_test.GetComponent<Button>().onClick.AddListener( descriptionBoxInit );
		button_achivments_test.GetComponent<Button>().onClick.AddListener( achivmentBoxInit );
		button_settings_test.GetComponent<Button>().onClick.AddListener( settingsBoxInit );
		button_input_test.GetComponent<Button>().onClick.AddListener( inputBoxInit );
		button_messageQbox_test.GetComponent<Button>().onClick.AddListener( messageQBoxInit );
		button_messageIbox_test.GetComponent<Button>().onClick.AddListener( messageIBoxInit );
		button_winning_test.GetComponent<Button>().onClick.AddListener( endingBoxInitWin );
		button_lossing_test.GetComponent<Button>().onClick.AddListener( endingBoxInitLoose );

		component_toolbar.GetComponent<ToolBarBox>().setStartStop(
			delegate { component_messageIbox.GetComponent<MessageIBox>().Init( new string[] { "Debug", "Funkcja została uruchomiona." }, null, null ); },
			delegate { component_messageIbox.GetComponent<MessageIBox>().Init( new string[] { "Debug", "Funkcja została zakończona." }, null, null ); },
			null, null );
		component_toolbar.GetComponent<ToolBarBox>().setExit(
			delegate { component_ending.GetComponent<EndingBox>().Init(
				new string[] { "Debug", "Test zakończony powodzeniem", "Powrót do menu" }, 
				delegate { SceneManager.LoadScene( "Main Menu" ); },
				EndingBox.EndingType.gold, null ); },
			null );
	}

	// ######################################################################
	//	XXXX    XXXXX    XXXX    XXX    XXXX    XXXXX   XXXX    XXXXX   XXXXX    XXX    X   X
	//	 X  X   X       X       X   X   X   X     X     X   X     X       X     X   X   XX  X
	//	 X  X   XXX      XXX    X       XXXX      X     XXXX      X       X     X   X   X X X
	//	 X  X   X           X   X   X   X   X     X     X         X       X     X   X   X  XX
	//	XXXX    XXXXX   XXXX     XXX    X   X   XXXXX   X         X     XXXXX    XXX    X   X
	//
	//	XXX      XXX    X   X      XXXXX   XXXXX    XXXX   XXXXX
	//	X  X    X   X    X X         X     X       X         X  
	//	XXXX    X   X     X          X     XXX      XXX      X  
	//	X   X   X   X    X X         X     X           X     X  
	//	XXXX     XXX    X   X        X     XXXXX   XXXX      X  
	// ######################################################################

	private void descriptionBoxInit() {
		string	title		=	"Porada na zadanie";
		string	content		=	"Zadanie będzie polegało na ... \n" 
							+	"\n"
							+	"do zadania będą potrzebne następujące informacje ... \n"
							+	"\n"
							+	"możesz użyć następujących wzorów ... \n"
							+	"\n"
							+	"Życzę powodzenia!";

		component_description.GetComponent<DescriptionBox>().Init( title, content, 256 );
	}

	// ######################################################################
	//	XXXXX   XXXXX    XXXX   XXXXX
	//	  X     X       X         X  
	//	  X     XXX      XXX      X  
	//	  X     X           X     X  
	//	  X     XXXXX   XXXX      X  
	//
	//	 XXX     XXX    X   X   XXXXX   X   X   X   X   XXXXX   X   X   XXXXX    XXXX      XXX      XXX    X   X
	//	X   X   X   X   X   X     X     X   X   XX XX   X       XX  X     X     X          X  X    X   X    X X 
	//	XXXXX   X       XXXXX     X     X   X   X X X   XXX     X X X     X      XXX       XXXX    X   X     X  
	//	X   X   X   X   X   X     X      X X    X   X   X       X  XX     X         X      X   X   X   X    X X 
	//	X   X    XXX    X   X   XXXXX     X     X   X   XXXXX   X   X     X     XXXX       XXXX     XXX    X   X
	// ######################################################################

	private void achivmentBoxSetup() {
		for ( int i=0; i<5; i++ ) {
			int							random				=	Random.Range( 100, 1000 );
			AchivmentBox.AchivmentType	achivment_type		=	(AchivmentBox.AchivmentType) Random.Range( 0, System.Enum.GetValues(typeof(AchivmentBox.AchivmentType)).Length );
			component_achivments.GetComponent<AchivmentBox>().addAchivment( "Achivment#" + random.ToString(), randomString(32), null );
			component_achivments.GetComponent<AchivmentBox>().changeAchivmentStatus( i, achivment_type );
		}
	}

	// ----------------------------------------------------------------------
	private void achivmentBoxInit() {
		component_achivments.GetComponent<AchivmentBox>().Init();
	}

	// ######################################################################
	//	XXXXX   XXXXX    XXXX   XXXXX
	//	  X     X       X         X  
	//	  X     XXX      XXX      X  
	//	  X     X           X     X  
	//	  X     XXXXX   XXXX      X  
	//
	//	 XXXX   XXXXX   XXXXX   XXXXX   XXXXX   X   X    XXXX    XXXX      XXX      XXX    X   X
	//	X       X         X       X       X     XX  X   X       X          X  X    X   X    X X 
	//	 XXX    XXX       X       X       X     X X X   X  XX    XXX       XXXX    X   X     X  
	//	    X   X         X       X       X     X  XX   X   X       X      X   X   X   X    X X 
	//	XXXX    XXXXX     X       X     XXXXX   X   X    XXXX   XXXX       XXXX     XXX    X   X
	// ######################################################################

	private void settingsBoxInit() {
		component_settings.GetComponent<SettingsBox>().Init( false );
	}

	// ######################################################################
	//	XXXXX   XXXXX    XXXX   XXXXX
	//	  X     X       X         X  
	//	  X     XXX      XXX      X  
	//	  X     X           X     X  
	//	  X     XXXXX   XXXX      X  
	//
	//	XXXXX   X   X   XXXX    X   X   XXXXX      XXX      XXX    X   X
	//	  X     XX  X   X   X   X   X     X        X  X    X   X    X X 
	//	  X     X X X   XXXX    X   X     X        XXXX    X   X     X  
	//	  X     X  XX   X       X   X     X        X   X   X   X    X X 
	//	XXXXX   X   X   X        XXX      X        XXXX     XXX    X   X
	// ######################################################################

	private void inputBoxInit() {
		string[]	str_data	=	{ "Wprowadzanie danych", "Podaj swoje imię", "Akceptuj", "Zamknij" };
		component_input.GetComponent<InputBox>().Init( str_data, ContentType.Standard, inputBoxOK, inputBoxCancel, null );
	}

	// ----------------------------------------------------------------------
	private void inputBoxOK( string text, object[] args ) { Debug.Log( "InputBox zakończył działanie z wynikiem " + text ); Debug.Log( args ); }
	private void inputBoxCancel( object[] args ) { Debug.Log( "InputBox zakończył działanie bez zwracania wyniku" ); Debug.Log( args ); }

	// ######################################################################
	//	XXXXX   XXXXX    XXXX   XXXXX
	//	  X     X       X         X  
	//	  X     XXX      XXX      X  
	//	  X     X           X     X  
	//	  X     XXXXX   XXXX      X  
	//
	//	X   X   XXXXX    XXXX    XXXX    XXX     XXXX   XXXXX       XXX    XXX      XXX    X   X
	//	XX XX   X       X       X       X   X   X       X          X   X   X  X    X   X    X X 
	//	X X X   XXX      XXX     XXX    XXXXX   X  XX   XXX        X X X   XXXX    X   X     X  
	//	X   X   X           X       X   X   X   X   X   X          X  XX   X   X   X   X    X X 
	//	X   X   XXXXX   XXXX    XXXX    X   X    XXXX   XXXXX       XXX    XXXX     XXX    X   X
	// ######################################################################

	private void messageQBoxInit() {
		string[]	str_data	=	{ "Usuwanie plików", "Czy na pewno chcesz usunąć ten plik?", "Usuń plik", "Nie usuwaj" };
		component_messageQbox.GetComponent<MessageQBox>().Init( str_data, messageQBoxYes, messageQBoxNo, null );
	}

	// ----------------------------------------------------------------------
	private void messageQBoxYes( object[] args ) { Debug.Log( "MessageQBox zakończył działanie z wyborem YES" ); Debug.Log( args ); }
	private void messageQBoxNo( object[] args ) { Debug.Log( "MessageQBox zakończył działanie z wyborem NO" ); Debug.Log( args ); }

	// ######################################################################
	//	XXXXX   XXXXX    XXXX   XXXXX
	//	  X     X       X         X  
	//	  X     XXX      XXX      X  
	//	  X     X           X     X  
	//	  X     XXXXX   XXXX      X  
	//
	//	X   X   XXXXX    XXXX    XXXX    XXX     XXXX   XXXXX      XXXXX   XXX      XXX    X   X
	//	XX XX   X       X       X       X   X   X       X            X     X  X    X   X    X X 
	//	X X X   XXX      XXX     XXX    XXXXX   X  XX   XXX          X     XXXX    X   X     X  
	//	X   X   X           X       X   X   X   X   X   X            X     X   X   X   X    X X 
	//	X   X   XXXXX   XXXX    XXXX    X   X    XXXX   XXXXX      XXXXX   XXXX     XXX    X   X
	// ######################################################################

	private void messageIBoxInit() {
		string[]	str_data	=	{ "Błąd", "Wystąpił nieoczekiwany błąd.", "Zrozumiałem" };
		component_messageIbox.GetComponent<MessageIBox>().Init( str_data, messageIBoxOK, null );
	}

	// ----------------------------------------------------------------------
	private void messageIBoxOK( object[] args ) { Debug.Log( "MessageIBox zakończył działanie" ); Debug.Log( args ); }

	// ######################################################################
	//	XXXXX   XXXXX    XXXX   XXXXX
	//	  X     X       X         X  
	//	  X     XXX      XXX      X  
	//	  X     X           X     X  
	//	  X     XXXXX   XXXX      X  
	//
	//	XXXXX   X   X   XXXX    XXXXX   X   X    XXXX      XXX      XXX    X   X
	//	X       XX  X    X  X     X     XX  X   X          X  X    X   X    X X 
	//	XXX     X X X    X  X     X     X X X   X  XX      XXXX    X   X     X  
	//	X       X  XX    X  X     X     X  XX   X   X      X   X   X   X    X X 
	//	XXXXX   X   X   XXXX    XXXXX   X   X    XXXX      XXXX     XXX    X   X
	// ######################################################################

	private void endingBoxInitWin() {
		int			random		=	Random.Range( 0, 3 );
		
		switch( random ) {
		case 0:
			string[]	str_data0	=	{ "WYGRANA", "Zakończono zadanie z wynikiem 95%", "Zrozumiałem" };
			component_ending.GetComponent<EndingBox>().Init( str_data0, endingBoxOK, EndingBox.EndingType.gold, null );
			break;
		case 1:
			string[]	str_data1	=	{ "WYGRANA", "Zakończono zadanie z wynikiem 72%", "Zrozumiałem" };
			component_ending.GetComponent<EndingBox>().Init( str_data1, endingBoxOK, EndingBox.EndingType.silver, null );
			break;
		case 2:
			string[]	str_data2	=	{ "WYGRANA", "Zakończono zadanie z wynikiem 56%", "Zrozumiałem" };
			component_ending.GetComponent<EndingBox>().Init( str_data2, endingBoxOK, EndingBox.EndingType.bronze, null );
			break;
		default:
			string[]	str_data3	=	{ "WYGRANA", "Zakończono zadanie z wynikiem 50%", "Zrozumiałem" };
			component_ending.GetComponent<EndingBox>().Init( str_data3, endingBoxOK, EndingBox.EndingType.bronze, null );
			break;
		}
	}

	// ----------------------------------------------------------------------
	private void endingBoxInitLoose() {
		string[]	str_data	=	{ "PRZEGRANA", "Niestety zadanie nie zostało ukończone. \nRezultat końcowy 28%", "Zrozumiałem" };
		component_ending.GetComponent<EndingBox>().Init( str_data, endingBoxOK, EndingBox.EndingType.fail, null );
	}

	// ----------------------------------------------------------------------
	private void endingBoxOK( object[] args ) { Debug.Log( "EndingBox zakończył działanie" ); Debug.Log( args ); }

	// ######################################################################
	//	XXXXX    XXX     XXX    X        XXXX
	//	  X     X   X   X   X   X       X    
	//	  X     X   X   X   X   X        XXX 
	//	  X     X   X   X   X   X           X
	//	  X      XXX     XXX    XXXXX   XXXX 
	// ######################################################################

	private string randomString( int length ) {
		string	gryphls		=	"abcdefghijklmnopqrstuvwxyz0123456789";
		string	output		=	"";
		for ( int i=0; i<length; i++ ) { output += gryphls[Random.Range( 0, gryphls.Length )]; }
		return output;
	}

	// ######################################################################

}

// ################################################################################