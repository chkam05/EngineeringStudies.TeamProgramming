using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################

public class RowniaPochylaUI : MonoBehaviour {

	// PRIVATE VARIABLES
	private	string			str_teoria				=	"    Brak teorii. ";
	private string			str_wzory				=	" G - Ciężar \n"
													+	" m - masa \n"
													+	" g - przyspieszenie ziemskie = 9,81 [m/s^2] \n"
													+	" alpha - kąt nachylenia równi do podłoża \n"
													+	" N - siła reakcji podłoża \n"
													+	"\n"
													+	" G = m*g \n"
													+	" a = g * ( sin( alpha ) - u cos( alpha ) ) \n"
													+	"\n"
													+	" W przypadku kiedy tarcie wynosi 0 otrzymujemy: \n"
													+	" a = sin( alpha ) * g \n";

	private	string			str_star				=	"    Uruchom bądź zakończ symulację. ";
	private string			str_achv				=	"    Pokaż okno postępów. ";
	private string			str_info				=	"    Pokaż informacje na temat tego zadania. ";
	private	string			str_exit				=	"    Wyjdź z trybu symulacji, podsumowując swój wynik. ";

	private	string			str_input_M				=	"    Wprowadź masę objektu spadającego na równie pochyłą. ";
	private	string			str_input_Friction		=	"    Współczynnik tarcia podłoża (Tarcia wspoczynkowego). ";
	private	string			str_input_Alpha			=	"    Wprowadź kąt alpha nachylenia równi pochyłej. ";
	private string			str_input_base			=	"    Wprowadź długość podstawy równi pochyłej. ";

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

	public	GameObject		fieldinput_M;
	public	GameObject		fieldinput_Friction;
	public	GameObject		fieldinput_Alpha;
	public	GameObject		fieldinput_base;
	public	GameObject		fieldinput_G;
	public	GameObject		fieldinput_a;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	void Start () {
		functionStop( null );

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
		fieldinput_Friction.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_Friction.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_Friction.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		fieldinput_Alpha.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_Alpha.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_Alpha.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		fieldinput_base.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldinput_base.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldinput_base.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );

		Init();
	}
	
	// ----------------------------------------------------------------------
	private void Init() {
		float	alpha_angle		=	30.0f;
		float	base_a			=	16.0f;
		float	cube_mass		=	10.0f;
		float	friction		=	0.4f;

		fieldinput_M.GetComponent<InputField>().text			=	cube_mass.ToString();
		fieldinput_Friction.GetComponent<InputField>().text		=	friction.ToString();
		fieldinput_Alpha.GetComponent<InputField>().text		=	alpha_angle.ToString();
		fieldinput_base.GetComponent<InputField>().text			=	base_a.ToString();
		setData( alpha_angle, base_a, cube_mass, friction );
	}

	// ----------------------------------------------------------------------
	void Update () {
		//
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
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_input_M );
		} else if ( current_button == fieldinput_Friction.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		} else if ( current_button == fieldinput_Alpha.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_input_Alpha );
		} else if ( current_button == fieldinput_base.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_input_base );
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
			string[]	str_texts	=	{ "Zmienna M:", str_input_M, "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_M };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		} else if ( current_button == fieldinput_Friction.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna u:", str_input_Friction, "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_Friction };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );

		} else if ( current_button == fieldinput_Alpha.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna Alpha:", str_input_Alpha, "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_Alpha };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		} else if ( current_button == fieldinput_base.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Długość podstawy:", str_input_base, "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldinput_base };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		}
	}

	// ----------------------------------------------------------------------
	private void onInputBoxYes( string text, object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<InputField>() == null ) { return; }
		var	input_field			=	args[0] as GameObject;

		input_field.GetComponent<InputField>().text	=	text;
		float	alpha_angle		=	float.Parse( fieldinput_Alpha.GetComponent<InputField>().text );
		float	friction		=	float.Parse( fieldinput_Friction.GetComponent<InputField>().text );
		float	base_a			=	float.Parse( fieldinput_base.GetComponent<InputField>().text );
		float	cube_mass		=	float.Parse( fieldinput_M.GetComponent<InputField>().text );
		setData( alpha_angle, base_a, cube_mass, friction );
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
		float	alpha_angle		=	float.Parse( fieldinput_Alpha.GetComponent<InputField>().text );
		float	friction		=	float.Parse( fieldinput_Friction.GetComponent<InputField>().text );
		float	cube_mass		=	float.Parse( fieldinput_M.GetComponent<InputField>().text );	 

		//module_game.GetComponent<RowniaPochylaGame>().hideGhost();
		fieldinput_G.GetComponent<InputField>().text	=	module_game.GetComponent<RowniaPochylaGame>().calculateG( cube_mass ).ToString() + " [kg * m/s^2]";
		fieldinput_a.GetComponent<InputField>().text	=	module_game.GetComponent<RowniaPochylaGame>().calculatea( alpha_angle, friction ).ToString() + " [m/s^2]";
		component_toolbar.GetComponent<ToolBarBox>().contentPosition( 0.0f );
	}

	// ----------------------------------------------------------------------
	public void functionStop( object[] args ) {
		Time.timeScale	=	0.0f;
		module_game.GetComponent<RowniaPochylaGame>().resetCubeData();
		//module_game.GetComponent<RowniaPochylaGame>().showGhost();

		fieldinput_G.GetComponent<InputField>().text	=	"";
		fieldinput_a.GetComponent<InputField>().text	=	"";
	}

	// ----------------------------------------------------------------------
	public void setData( float alpha_angle, float base_a, float cube_mass, float friction ) {
		module_game.GetComponent<RowniaPochylaGame>().importData( alpha_angle, base_a, cube_mass, friction );
		module_game.GetComponent<RowniaPochylaGame>().setGhost( alpha_angle );
		//module_game.GetComponent<RowniaPochylaGame>().showGhost();
	}

	// ######################################################################

}

// ################################################################################