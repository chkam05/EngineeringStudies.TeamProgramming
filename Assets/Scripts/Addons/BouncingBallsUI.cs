using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################

public class BouncingBallsUI : MonoBehaviour {

	// PRIVATE VARIABLES
	private	string			str_teoria				=	"";
	private string			str_wzory				=	"I oto są tajemnice świata... \n"
													+	"Wielka fizyka która go oplata! \n"
													+	"Kontroler jej matematyką się zwie, \n"
													+	"w oparciu o to, ta gra mogła rozwinąć się. \n"
													+	"\n"
													+	"Wszystkie zadania ukończyłeś w mig, \n"
													+	"bloczki, równie, oraz dźwig. \n"
													+	"Nadszedł w końcu pożegnania czas, \n"
													+	"lecz to nie wszystko co zostało teraz. \n"
													+	"\n"
													+	"Zostawiam nie małą lecz wielką rzecz, \n"
													+	"nad którą siedziałem parę dni wstecz \n"
													+	"Zabawa was czeka, to nie jest wyzwanie \n"
													+	"i zapraszam na z piłkami zabawianie.";

	private	string			str_star				=	"    Uruchom bądź zakończ symulację. ";
	private string			str_achv				=	"    Pokaż okno postępów. ";
	private string			str_info				=	"    Pokaż informacje na temat tego zadania. ";
	private	string			str_exit				=	"    Wyjdź z trybu symulacji, podsumowując swój wynik. ";

	private	string			str_input_M				=	"    Wprowadź masę piłki. ";
	private	string			str_input_Friction		=	"    Współczynnik tarcia, jak bardzo chropowata będzie piłka. ";
	private	string			str_input_Bounciess		=	"    Wprowadź wartość odbicia, jak bardzo piłka ma przypominać kauczuk. ";
	private string			str_input_g				=	"    Wybierz czy grawitacja ma być aktywna, czy wszystko odbywać się będzie w próżni. ";
	private string			str_input_a				=	"    Wprowadź wartość przyspieszenia dla przeciągania myszką. ";

	private int				size_wzory				=	12 * 32;
	
	private	int				current_ball			=	0;
	private	bool			toggle_Gravity_e		=	false;

	// PUBLIC VARIABLES
	public	GameObject		module_game;

	public	GameObject		component_toolbar;
	public	GameObject		component_statusbar;
	public	GameObject		component_description;
	public	GameObject		component_inputBox;
	public	GameObject		component_messageQBox;
	public	GameObject		component_messageIBox;
	public	GameObject		component_settings;
	public	GameObject		component_welcome;

	public	GameObject		text_currentBall;
	public	GameObject		button_previousBall;
	public	GameObject		button_nextBall;
	public	GameObject		button_removeBall;
	public	GameObject		button_addBall;

	public	GameObject		fieldinput_M;
	public	GameObject		fieldinput_Friction;
	public	GameObject		fieldinput_Bouncies;
	public	GameObject		fieldinput_a;

	public	GameObject		toggle_Gravity;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	/// <summary>
	/// KOnfiguracja komponentów UI
	/// </summary>

	void Start () {
		functionStop( null );

		component_settings.GetComponent<SettingsBox>().Setup();
		component_description.GetComponent<DescriptionBox>().Init( "Zakończenie", str_wzory, size_wzory );
		component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );

		component_toolbar.GetComponent<ToolBarBox>().setStartStopHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setAchivmnetsHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setInformationsHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setExitHover( onButtonEnter, onButtonExit );

		component_toolbar.GetComponent<ToolBarBox>().setStartStop( functionStart, functionStop, null, null );
		component_toolbar.GetComponent<ToolBarBox>().setInformations( "Zakończenie", str_wzory, size_wzory );

		button_previousBall.GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		button_previousBall.GetComponent<ButtonBehaviour>().setOnMouseClick( onButtonPreviousClick );
		button_previousBall.GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		button_nextBall.GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		button_nextBall.GetComponent<ButtonBehaviour>().setOnMouseClick( onButtonNextClick );
		button_nextBall.GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		button_removeBall.GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		button_removeBall.GetComponent<ButtonBehaviour>().setOnMouseClick( onButtonRemoveClick );
		button_removeBall.GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		button_addBall.GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		button_addBall.GetComponent<ButtonBehaviour>().setOnMouseClick( onButtonAddClick );
		button_addBall.GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );

		fieldinput_M.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_M.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_M.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		fieldinput_Friction.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_Friction.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_Friction.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		fieldinput_Bouncies.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_Bouncies.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_Bouncies.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		fieldinput_a.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_a.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_a.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );

		toggle_Gravity.GetComponent<ButtonBehaviour>().setOnMouseDown( onToggleEnter );
		toggle_Gravity.GetComponent<ButtonBehaviour>().setOnMouseDown( onToggleDown );
		toggle_Gravity.GetComponent<ButtonBehaviour>().setOnMouseUp( onToggleUp );
		toggle_Gravity.GetComponent<ButtonBehaviour>().setOnMouseDown( onToggleExit );

		component_welcome.GetComponent<WelcomeBox>().Init( "Skaczące piłki", "A teraz czas na zabawę ;)" );

		Init();
	}
	
	// ----------------------------------------------------------------------
	/// <summary>
	/// Pzypisanie danych wejściowych komponentowi warstwy 3D
	/// </summary>

	private void Init() {
		updateGravity( module_game.GetComponent<BouncingBalls>().getGravity() );
		updateSpeed( module_game.GetComponent<BouncingBalls>().getSpeed() );
		updateBall( current_ball );
	}

	// ######################################################################
	//	XXX      XXX    X       X        XXXX
	//	X  X    X   X   X       X       X    
	//	XXXX    X   X   X       X        XXX 
	//	X   X   X   X   X       X           X
	//	XXXX    X   X   XXXXX   XXXXX   XXXX 
	//
	//	XXX     X   X   XXXXX   XXXXX    XXX    X   X    XXXX
	//	X  X    X   X     X       X     X   X   XX  X   X    
	//	XXXX    X   X     X       X     X   X   X X X    XXX 
	//	X   X   X   X     X       X     X   X   X  XX       X
	//	XXXX     XXX      X       X      XXX    X   X   XXXX
	// ######################################################################
	/// <summary>
	/// Wybranie poprzedniej piłki.
	/// </summary>
	/// <param name="args">Arguments.</param>

	private void onButtonPreviousClick( object[] args ) {
		if ( current_ball <= 0 ) { return; }
		updateBall( current_ball - 1 );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Wybranie następnej piłki.
	/// </summary>
	/// <param name="args">Arguments.</param>

	private void onButtonNextClick( object[] args ) {
		if ( current_ball >= module_game.GetComponent<BouncingBalls>().balls.Count ) { return; }
		updateBall( current_ball + 1 );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Usunięcie wybranej piłki.
	/// </summary>
	/// <param name="args">Arguments.</param>

	private void onButtonRemoveClick( object[] args ) {
		if ( module_game.GetComponent<BouncingBalls>().balls.Count <= 1 ) { return; }
		module_game.GetComponent<BouncingBalls>().destroyBall( current_ball );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Dodanie nowej piłki.
	/// </summary>
	/// <param name="args">Arguments.</param>

	private void onButtonAddClick( object[] args ) {
		if ( module_game.GetComponent<BouncingBalls>().balls.Count >= 10 ) { return; }
		module_game.GetComponent<BouncingBalls>().addBall();
	}

	// ######################################################################
	//	XXX      XXX    X       X        XXXX
	//	X  X    X   X   X       X       X    
	//	XXXX    XXXXX   X       X        XXX 
	//	X   X   X   X   X       X           X
	//	XXXX    X   X   XXXXX   XXXXX   XXXX 
	// ######################################################################
	/// <summary>
	/// Pobranie informacji o piłce.
	/// </summary>
	/// <param name="index"> Numer piłki. </param>

	public void updateBall( int index ) {
		if ( index >= 0 && index < module_game.GetComponent<BouncingBalls>().balls.Count ) {
			current_ball		=	index;
			object[]	data	=	module_game.GetComponent<BouncingBalls>().getData( index );
			
			text_currentBall.GetComponent<Text>().text				=	"Ball " + current_ball.ToString();
			fieldinput_M.GetComponent<InputField>().text			=	((float) data[0]).ToString();
			fieldinput_Friction.GetComponent<InputField>().text		=	((float) data[1]).ToString();
			fieldinput_Bouncies.GetComponent<InputField>().text		=	((float) data[2]).ToString();
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Przypisanie konfiguracji wybranej piłce.
	/// </summary>
	/// <param name="index"> Numer piłki. </param>

	private void setBallData( int index ) {
		if ( index >= 0 && index < module_game.GetComponent<BouncingBalls>().balls.Count ) {
			float	mass		=	float.Parse( fieldinput_M.GetComponent<InputField>().text );
			float	friction	=	float.Parse( fieldinput_Friction.GetComponent<InputField>().text );
			float	bounciess	=	float.Parse( fieldinput_Bouncies.GetComponent<InputField>().text );

			module_game.GetComponent<BouncingBalls>().setData( index, mass, friction, bounciess );
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Pobranie informacji o grawitacji
	/// </summary>
	/// <param name="gravity"> True - jest, False - brak. </param>

	public void updateGravity( bool gravity ) {
		toggle_Gravity.GetComponent<Toggle>().isOn	=	gravity;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Pobranie informacji o przyspieszeniu piłek.
	/// </summary>
	/// <param name="speed"> Przyspieszenie piłek. </param>

	public void updateSpeed( float speed ) {
		fieldinput_a.GetComponent<InputField>().text	=	speed.ToString();
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ustawienie grawitacji.
	/// </summary>

	private void setGravity() {
		bool	gravity		=	toggle_Gravity.GetComponent<Toggle>().isOn;
		module_game.GetComponent<BouncingBalls>().setGravity( !gravity );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ustawienie szybkości.
	/// </summary>

	private void setSpeed() {
		float	speed	=	float.Parse( fieldinput_a.GetComponent<InputField>().text );
		module_game.GetComponent<BouncingBalls>().setSpeed( speed );
	}

	// ######################################################################
	//	XXX     X   X   XXXXX   XXXXX    XXX    X   X    XXXX
	//	X  X    X   X     X       X     X   X   XX  X   X    
	//	XXXX    X   X     X       X     X   X   X X X    XXX 
	//	X   X   X   X     X       X     X   X   X  XX       X
	//	XXXX     XXX      X       X      XXX    X   X   XXXX
	// ######################################################################
	/// <summary>
	/// Funkcja wykonująca się po najechaniu kursorem na przycisk
	/// </summary>
	/// <param name="args"> Przycisk. </param>

	private void onButtonEnter( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == button_previousBall ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "    Poprzednia piłka" );
		} else if ( current_button == button_nextBall ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "    Następna piłka" );
		} else if ( current_button == button_removeBall ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "    Usuń piłkę" );
		} else if ( current_button == button_addBall ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "    Dodaj piłkę" );

		} else if ( current_button.name == "Button StartStop" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_star );
		} else if ( current_button.name == "Button Achivments" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_achv );
		} else if ( current_button.name == "Button Informations" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_info );
		} else if ( current_button.name == "Button Exit" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_exit );

		} else if ( current_button == fieldinput_M.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_input_M );
		} else if ( current_button == fieldinput_Friction.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_input_Friction );
		} else if ( current_button == fieldinput_Bouncies.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_input_Bounciess );
		} else if ( current_button == fieldinput_a.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_input_a );

		} else {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonująca się po opuszceniu kursora z przycisku
	/// </summary>
	/// <param name="args"> Przycisk. </param>

	private void onButtonExit( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }

		component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );
	}

	// ######################################################################
	//	XXXXX    XXX     XXXX    XXXX   X       XXXXX    XXXX
	//	  X     X   X   X       X       X       X       X    
	//	  X     X   X   X  XX   X  XX   X       XXX      XXX 
	//	  X     X   X   X   X   X   X   X       X           X
	//	  X      XXX     XXXX    XXXX   XXXXX   XXXXX   XXXX
	// ######################################################################
	/// <summary>
	/// Funkcja wykonująca się po najechaniu kursorem na komponent checkbox
	/// </summary>
	/// <param name="args"> CheckBox. </param>

	private void onToggleEnter( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Toggle>() == null ) { return; }
		var	current_toggle	=	args[0] as GameObject;

		if ( current_toggle == toggle_Gravity ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_input_g );
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonująca się po wybraniu przez kursor komponentu checkbox
	/// </summary>
	/// <param name="args"> CheckBox. </param>

	private void onToggleDown( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Toggle>() == null ) { return; }
		var	current_toggle	=	args[0] as GameObject;

		if ( current_toggle == toggle_Gravity ) {
			toggle_Gravity_e	=	true;
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonująca się po puszceniu kursora z komponentu checkbox
	/// </summary>
	/// <param name="args"> CheckBox. </param>

	private void onToggleUp( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Toggle>() == null ) { return; }
		var	current_toggle	=	args[0] as GameObject;

		if ( current_toggle == toggle_Gravity ) {
			toggle_Gravity_e	=	false;
			setGravity();
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonująca się po opuszceniu kursora z komponentu checkbox
	/// </summary>
	/// <param name="args"> CheckBox. </param>

	private void onToggleExit( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Toggle>() == null ) { return; }

		component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );
	}

	// ######################################################################
	//	XXXXX   X   X   XXXX    X   X   XXXXX      XXX      XXX    X   X
	//	  X     XX  X   X   X   X   X     X        X  X    X   X    X X 
	//	  X     X X X   XXXX    X   X     X        XXXX    X   X     X  
	//	  X     X  XX   X       X   X     X        X   X   X   X    X X 
	//	XXXXX   X   X   X        XXX      X        XXXX     XXX    X   X
	// ######################################################################
	/// <summary>
	/// Funckja wykonująca się po otwarciu InputBoxów
	/// </summary>
	/// <param name="args"> Arguments. </param>

	private void onInputBox( object[] args ) {
		//if ( Time.timeScale > 0.0f ) { return; }

		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == fieldinput_M.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna M:", str_input_M, "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_M };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );

		} else if ( current_button == fieldinput_Friction.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna Tarcia:", str_input_Friction, "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_Friction };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );

		} else if ( current_button == fieldinput_Bouncies.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna Odbicia:", str_input_Bounciess, "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_Bouncies };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );

		} else if ( current_button == fieldinput_a.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna przyspieszenia:", str_input_a, "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_a };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );

		}

	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja wykonująca się po zaakceptowaniu danych z InputBoxów
	/// </summary>
	/// <param name="text"> Tekst zwrotny. </param>
	/// <param name="args"> InputBox. </param>

	private void onInputBoxYes( string text, object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<InputField>() == null ) { return; }
		var	input_field			=	args[0] as GameObject;

		float	convert;
		if ( !float.TryParse( text, out convert ) ) { return; }

		if ( input_field == fieldinput_Bouncies ) {
			if ( convert < 0.0f || convert > 1.0f ) { return; }
			setBallData( current_ball );

		} else  if ( input_field == fieldinput_a ) {
			setSpeed();

		} else {
			setBallData( current_ball );

		}

		input_field.GetComponent<InputField>().text		=	convert.ToString();
	}

	// ######################################################################
	//	 XXXX    XXX    X   X   XXXXX
	//	X       X   X   XX XX   X    
	//	X  XX   XXXXX   X X X   XXX  
	//	X   X   X   X   X   X   X    
	//	 XXXX   X   X   X   X   XXXXX
	// ######################################################################
	/// <summary>
	/// Uruchomienie symulacji.
	/// </summary>
	/// <param name="args">Arguments.</param>

	public void functionStart( object[] args ) {
		Time.timeScale			=	1.0f;
		
		component_toolbar.GetComponent<ToolBarBox>().contentPosition( 0.0f );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Zakończenie symulacji.
	/// </summary>
	/// <param name="args">Arguments.</param>

	public void functionStop( object[] args ) {
		Time.timeScale	=	0.0f;
		module_game.GetComponent<BouncingBalls>().resetSimulation();
	}

	// ######################################################################

}

// ################################################################################