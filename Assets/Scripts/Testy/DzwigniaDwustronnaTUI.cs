using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ################################################################################

public class DzwigniaDwustronnaTUI : MonoBehaviour {

	// PRIVATE VARIABLES
	private	string			str_teoria				=	"    Brak teorii. ";
	private string			str_wzory				=	"";

	private	string			str_star				=	"    Uruchom bądź zakończ symulację. ";
	private string			str_achv				=	"    Pokaż okno postępów. ";
	private string			str_info				=	"    Pokaż informacje na temat tego zadania. ";
	private	string			str_exit				=	"    Wyjdź z trybu symulacji, podsumowując swój wynik. ";
	private	string			str_next				=	"    Następna część testu: Dźwignia dwustronna - część 2. ";
	private	string			str_back				=	"    Poprzednia część testu: Dźwignia jednostronna. ";

	private int				size_wzory				=	10 * 32;
	private	float			cube1_x;
	private	float			cube2_x;

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

	public	GameObject		fieldinput_M1;
    public	GameObject		fieldinput_M2;
	public	GameObject		fieldinput_length;
	public	GameObject		fieldinput_triangle;
	public	GameObject		fieldinput_distance1;
    public	GameObject		fieldinput_distance2;
    public	GameObject		fieldinput_force1;
    public	GameObject		fieldinput_force2;

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

		fieldinput_M1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_M1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_M1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
        fieldinput_M2.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver(onButtonEnter);
        fieldinput_M2.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick(onInputBox);
        fieldinput_M2.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit(onButtonExit);
        fieldinput_length.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver(onButtonEnter);
        fieldinput_length.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick(onInputBox);
        fieldinput_length.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit(onButtonExit);
        fieldinput_triangle.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver(onButtonEnter);
        fieldinput_triangle.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick(onInputBox);
        fieldinput_triangle.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit(onButtonExit);
        fieldinput_distance1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver(onButtonEnter);
        fieldinput_distance1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick(onInputBox);
        fieldinput_distance1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit(onButtonExit);
        fieldinput_distance2.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver(onButtonEnter);
        fieldinput_distance2.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick(onInputBox);
        fieldinput_distance2.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit(onButtonExit);

		component_statusbar.GetComponent<StatusBarBehaviour>().setNextFunction( onNextClick, str_next );
		component_statusbar.GetComponent<StatusBarBehaviour>().setPreviousFunction( onPreviousClick, str_back );
		component_welcome.GetComponent<WelcomeBox>().Init( "Dźwignia Dwustronna", "Test z dźwigni - część 2." );
        Init();
    }

    // ----------------------------------------------------------------------
    private void Init() {
        fieldinput_M1.GetComponent<InputField>().text			=	(1.0f).ToString();
        fieldinput_M2.GetComponent<InputField>().text			=	(1.0f).ToString();
        fieldinput_length.GetComponent<InputField>().text		=	(25.0f).ToString();
        fieldinput_triangle.GetComponent<InputField>().text		=	(0.0f).ToString();
        fieldinput_distance1.GetComponent<InputField>().text	=	(11.0f).ToString();
        fieldinput_distance2.GetComponent<InputField>().text	=	(11.0f).ToString();

        setData();
    }

	// ######################################################################
	//	X   X   XXXXX   X   X   XXXXX
	//	XX  X   X        X X      X  
	//	X X X   XXX       X       X  
	//	X  XX   X        X X      X  
	//	X   X   XXXXX   X   X     X  
	// ######################################################################

	private void onPreviousClick( object[] args ) {
		SceneManager.LoadScene( "Dzwignia dwustronna2 Test" );
	}

	private void onNextClick( object[] args ) {
		SceneManager.LoadScene( "Dzwignia jednostronna Test" );
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

		} else if ( current_button == fieldinput_M1.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		} else if ( current_button == fieldinput_M2.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		} else if ( current_button == fieldinput_length.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		} else if ( current_button == fieldinput_triangle.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
        } else if (current_button == fieldinput_distance1.transform.GetChild(3).gameObject) {
            component_statusbar.GetComponent<StatusBarBehaviour>().setInformations("");
        } else if (current_button == fieldinput_distance2.transform.GetChild(3).gameObject) {
            component_statusbar.GetComponent<StatusBarBehaviour>().setInformations("");
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

		if ( current_button == fieldinput_M1.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_M1 };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		} else if ( current_button == fieldinput_M2.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_M2 };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );

		} else if ( current_button == fieldinput_length.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_length };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		} else if ( current_button == fieldinput_triangle.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Długość:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_triangle };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		} else if ( current_button == fieldinput_distance1.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Długość:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_distance1 };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		} else if ( current_button == fieldinput_distance2.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Długość:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_distance2 };
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
    }

	// ----------------------------------------------------------------------
	public void functionStop( object[] args ) {
		Time.timeScale	=	0.0f;
		module_game.GetComponent<DzwigniaDwustronnaTest>().resetPosition();

		fieldinput_distance1.GetComponent<InputField>().text	=	cube1_x.ToString();
		fieldinput_distance2.GetComponent<InputField>().text	=	cube2_x.ToString();
		fieldinput_force1.GetComponent<InputField>().text		=	"";
        fieldinput_force2.GetComponent<InputField>().text		=	"";
	}

    // ----------------------------------------------------------------------
    public void setData() {
        float length	=	float.Parse(fieldinput_length.GetComponent<InputField>().text);
        float triangle	=	float.Parse(fieldinput_triangle.GetComponent<InputField>().text);
        float m1		=	float.Parse(fieldinput_M1.GetComponent<InputField>().text);
        float m2		=	float.Parse(fieldinput_M2.GetComponent<InputField>().text);
        float d1		=	float.Parse(fieldinput_distance1.GetComponent<InputField>().text);
        float d2		=	float.Parse(fieldinput_distance2.GetComponent<InputField>().text);

		cube1_x			=	d1;
		cube2_x			=	d2;

        module_game.GetComponent<DzwigniaDwustronnaTest>().calculate_Block( length, triangle, m1, m2, d1, d2 );
		module_game.GetComponent<DzwigniaDwustronnaTest>().Init();
    }

    // ----------------------------------------------------------------------
    public object[] getData() {
        float length = float.Parse(fieldinput_length.GetComponent<InputField>().text);
        float triangle = float.Parse(fieldinput_triangle.GetComponent<InputField>().text);
        float m1 = float.Parse(fieldinput_M1.GetComponent<InputField>().text);
        float m2 = float.Parse(fieldinput_M2.GetComponent<InputField>().text);
        float d1 = float.Parse(fieldinput_distance1.GetComponent<InputField>().text);
        float d2 = float.Parse(fieldinput_distance2.GetComponent<InputField>().text);
        
		return new object[] { length, triangle, m1, m2, d1, d2 };
    }

    // ----------------------------------------------------------------------
    public void updateData( float force1, float force2, float d1, float d2 )
    {
        fieldinput_distance1.GetComponent<InputField>().text	=   d1.ToString();
        fieldinput_distance2.GetComponent<InputField>().text	=   d2.ToString();
        fieldinput_force1.GetComponent<InputField>().text		=	force1.ToString();
        fieldinput_force2.GetComponent<InputField>().text		=	force2.ToString();
    }

    // ######################################################################

}

// ################################################################################