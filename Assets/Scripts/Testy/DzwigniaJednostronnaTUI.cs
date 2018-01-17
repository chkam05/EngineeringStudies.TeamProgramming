using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ################################################################################

public class DzwigniaJednostronnaTUI : MonoBehaviour {

	// PRIVATE VARIABLES
	private	string			str_teoria				=	"    Brak teorii. ";
	private string			str_wzory				=	"";

	private	string			str_star				=	"    Uruchom bądź zakończ symulację. ";
	private string			str_achv				=	"    Pokaż okno postępów. ";
	private string			str_info				=	"    Pokaż informacje na temat tego zadania. ";
	private	string			str_exit				=	"    Wyjdź z trybu symulacji, podsumowując swój wynik. ";
	private	string			str_next				=	"    Następna część testu: Dźwignia dwustronna - część 1. ";

	private int				size_wzory				=	10 * 32;

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

	public	GameObject		fieldinput_M;
	public	GameObject		fieldinput_Lenght;
	public	GameObject		fieldinput_Force;
	public	GameObject		fieldinput_Distance;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	void Start () {
		Time.timeScale	=	0.0f;

		component_settings.GetComponent<SettingsBox>().Setup();
		component_description.GetComponent<DescriptionBox>().Init( "Wzory", str_wzory, size_wzory );
		component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );

		component_toolbar.GetComponent<ToolBarBox>().setStartStopHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setAchivmnetsHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setInformationsHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setExitHover( onButtonEnter, onButtonExit );

		component_toolbar.GetComponent<ToolBarBox>().setStartStop( functionStart, functionStop, null, null );
		component_toolbar.GetComponent<ToolBarBox>().setInformations( "Wzory", str_wzory, size_wzory );

		fieldinput_M.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_M.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_M.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
        fieldinput_Lenght.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
        fieldinput_Lenght.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
        fieldinput_Lenght.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
        fieldinput_Distance.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
        fieldinput_Distance.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
        fieldinput_Distance.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );

		component_welcome.GetComponent<WelcomeBox>().Init( "Dźwignia jednostronna", "Test z dźwigni - część 1." );
		component_statusbar.GetComponent<StatusBarBehaviour>().setNextFunction( onNextClick, str_next );
        Init();
    }

    // ----------------------------------------------------------------------
    private void Init() {
        fieldinput_M.GetComponent<InputField>().text			=	(1.0f).ToString();
        fieldinput_Lenght.GetComponent<InputField>().text		=	(20.0f).ToString();
        fieldinput_Distance.GetComponent<InputField>().text 	=	(1.0f).ToString();
		fieldinput_Force.GetComponent<InputField>().text		=	"";

		setData();
    }

    // ----------------------------------------------------------------------
    void Update () {
		//
	}

	// ######################################################################
	//	X   X   XXXXX   X   X   XXXXX
	//	XX  X   X        X X      X  
	//	X X X   XXX       X       X  
	//	X  XX   X        X X      X  
	//	X   X   XXXXX   X   X     X  
	// ######################################################################

	private void onNextClick( object[] args ) {
		SceneManager.LoadScene( "Dzwignia dwustronna Test" );
	}

	// ######################################################################
	//	XXX     X   X   XXXXX   XXXXX    XXX    X   X    XXXX
	//	X  X    X   X     X       X     X   X   XX  X   X    
	//	XXXX    X   X     X       X     X   X   X X X    XXX 
	//	X   X   X   X     X       X     X   X   X  XX       X
	//	XXXX     XXX      X       X      XXX    X   X   XXXX
	// ######################################################################

	private void onButtonEnter( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button.name == "Button StartStop" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_star );
		} else if ( current_button.name == "Button Achivments" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_achv );
		} else if ( current_button.name == "Button Informations" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_info );
		} else if ( current_button.name == "Button Exit" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_exit );

		} else if ( current_button == fieldinput_M.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		} else if ( current_button == fieldinput_Lenght.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		} else if ( current_button == fieldinput_Distance.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		} else {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );
		}
	}

	// ----------------------------------------------------------------------
	private void onButtonExit( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }

		component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );
	}

	// ######################################################################
	//	XXXXX   X   X   XXXX    X   X   XXXXX      XXX      XXX    X   X
	//	  X     XX  X   X   X   X   X     X        X  X    X   X    X X 
	//	  X     X X X   XXXX    X   X     X        XXXX    X   X     X  
	//	  X     X  XX   X       X   X     X        X   X   X   X    X X 
	//	XXXXX   X   X   X        XXX      X        XXXX     XXX    X   X
	// ######################################################################

	private void onInputBox( object[] args ) {
		if ( Time.timeScale > 0.0f ) { return; }

		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == fieldinput_M.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_M };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		} else if ( current_button == fieldinput_Lenght.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_Lenght };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );

		} else if ( current_button == fieldinput_Distance.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Długość:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_Distance };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		}
	}

	// ----------------------------------------------------------------------
	private void onInputBoxYes( string text, object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<InputField>() == null ) { return; }
		var	input_field			=	args[0] as GameObject;

        float convert;
        if (!float.TryParse(text, out convert)) { return; }

		input_field.GetComponent<InputField>().text	= convert.ToString();
        setData();
    }

	// ######################################################################
	//	 XXXX    XXX    X   X   XXXXX
	//	X       X   X   XX XX   X    
	//	X  XX   XXXXX   X X X   XXX  
	//	X   X   X   X   X   X   X    
	//	 XXXX   X   X   X   X   XXXXX
	// ######################################################################

	public void functionStart( object[] args ) {
		Time.timeScale			=	1.0f;

        float mass = float.Parse(fieldinput_M.GetComponent<InputField>().text);
        float length = float.Parse(fieldinput_Lenght.GetComponent<InputField>().text);
        float distance = float.Parse(fieldinput_Distance.GetComponent<InputField>().text);
        float force = module_game.GetComponent<DzwigniaJednostronnaTest>().calculateF(length, distance, mass);
        updateData(force);
    }

	// ----------------------------------------------------------------------
	public void functionStop( object[] args ) {
		Time.timeScale	=	0.0f;
		module_game.GetComponent<DzwigniaJednostronnaTest>().resetPosition();
		fieldinput_Force.GetComponent<InputField>().text	=	"";
	}

    // ----------------------------------------------------------------------
    public void setData() {
        float mass = float.Parse(fieldinput_M.GetComponent<InputField>().text);
        float length = float.Parse(fieldinput_Lenght.GetComponent<InputField>().text);
        float distance = float.Parse(fieldinput_Distance.GetComponent<InputField>().text);

        module_game.GetComponent<DzwigniaJednostronnaTest>().calculate_Block(length, distance, mass);
		module_game.GetComponent<DzwigniaJednostronnaTest>().Init();
    }

    // ----------------------------------------------------------------------
    public void updateData( float force ) {
        fieldinput_Force.GetComponent<InputField>().text = force.ToString();
    }

    // ######################################################################

}

// ################################################################################