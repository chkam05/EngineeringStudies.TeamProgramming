using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	 XXX     XXX    X   X    XXXX    XXX    X       XXXXX
//	X   X   X   X   XX  X   X       X   X   X       X    
//	X       X   X   X X X    XXX    X   X   X       XXX  
//	X   X   X   X   X  XX       X   X   X   X       X    
//	 XXX     XXX    X   X   XXXX     XXX    XXXXX   XXXXX
// ################################################################################

public class ConsoleBox : MonoBehaviour {

	// PRIVATE VARIABLES:
	private	int				text_height			=	20;
	private	bool			echo_off			=	false;

	// PUBLIC VARIABLES:
	public	GameObject		scrollbar_text;
	public	GameObject		container_text;
	public	GameObject		text_text;
	public	GameObject		inputfield_text;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	public void	Init() {
		gameObject.SetActive( true );
	}

	// ----------------------------------------------------------------------
	void Update() {
		if ( inputfield_text.GetComponent<InputField>().isFocused && inputfield_text.GetComponent<InputField>().text != "" && Input.GetKey( KeyCode.Return ) ) {
			string	temp_command								=	inputfield_text.GetComponent<InputField>().text;
			inputfield_text.GetComponent<InputField>().text		=	"";
			proceedCommand( temp_command );
		}
	}

	// ----------------------------------------------------------------------
	private void closeCommands() {
		gameObject.SetActive( false );
	}

	// ######################################################################
	//	XXXXX   XXXXX   X   X   XXXXX
	//	  X     X        X X      X  
	//	  X     XXX       X       X  
	//	  X     X        X X      X  
	//	  X     XXXXX   X   X     X  
	// ######################################################################

	private void proceedCommand( string command ) {
		if ( command == "" ) { return; }
		if ( echo_off == false ) { addLine( "> " + command ); }

		if ( command.ToLower().Substring( 0 ) == "list players" ) {
			commandLISTPLAYERS();

		} else if ( command.ToLower().Substring( 0 ) == "show players" ) {
			commandSHOWPLAYERS();

		} else if ( command.Length >= 4 && command.ToLower().Substring( 0, 4 ) == "help" ) {
			commandHELP();

		} else if ( command.Length >= 4 && command.ToLower().Substring( 0, 4 ) == "exit" ) {
			closeCommands();

		} else if ( command.Length >= 9 && command.ToLower().Substring( 0, 9 ) == "echo -off" ) {
			echo_off			=	!echo_off;
			if ( echo_off ) { commandECHO( "[ i ] Wyłączono wyświetlanie komend." ); }
			else { commandECHO( "[ i ] Włączono wyświetlanie komend." ); }
			commandECHO( "" );

		} else if ( command.Length >= 4 && command.ToLower().Substring( 0, 4 ) == "echo" ) {
			string	argument	=	command.Substring( 5 );
			commandECHO( argument );

		} else if ( command.Length >= 11 && command.ToLower().Substring( 0, 11 ) == "show player" ) {
			string	argument	=	command.Substring( 12 );
			commandSHOWPLAYER( argument );

		} else if ( command.Length >= 15 && command.ToLower().Substring( 0, 15 ) == "show achivments" ) {
			string	argument	=	command.Substring( 16 );
			commandSHOWACHIVMENTS( argument );

		} else {
			addLine( "[ ! ] Nieznana komenda" );
		}
	}

	// ######################################################################
	//	X   X   XXXXX
	//	X   X     X  
	//	X   X     X  
	//	X   X     X  
	//	 XXX    XXXXX
	// ######################################################################

	private void addLine( string line ) {
		string	temp							=	text_text.GetComponent<Text>().text;
		text_text.GetComponent<Text>().text		=	temp + "\n" + line;
		setUI();
	}

	// ----------------------------------------------------------------------
	private void setUI() {
		var		temp											=	container_text.GetComponent<RectTransform>().sizeDelta;
		container_text.GetComponent<RectTransform>().sizeDelta	=	new Vector2( temp.x, temp.y + text_height );
		scrollbar_text.GetComponent<Scrollbar>().value			=	0;
	}

	// ######################################################################
	//	 XXX     XXX    X   X   X   X    XXX    X   X   XXXX     XXXX
	//	X   X   X   X   XX XX   XX XX   X   X   XX  X    X  X   X    
	//	X       X   X   X X X   X X X   XXXXX   X X x    X  X    XXX 
	//	X   X   X   X   X   X   X   X   X   X   X  XX    X  X       X
	//	 XXX     XXX    X   X   X   X   X   X   X   X   XXXX    XXXX 
	// ######################################################################

	private void commandHELP() {
		addLine( "" );
		addLine( "[ help ]" );
		addLine( "Wyświetla pomoc i listę dostępnych komend." );
		addLine( "[ echo ] text" );
		addLine( "Wypisuje na ekranie podany text." );
		addLine( "[ echo -off ]" );
		addLine( "Nie wyświetla wypisywanych poleceń." );
		addLine( "[ exit ] text" );
		addLine( "Zamyka okno poleceń." );
		addLine( "[ list players ]" );
		addLine( "Wyświetla listę ID graczy." );
		addLine( "[ show players ]" );
		addLine( "Wyświetla listę graczy z danymi." );
		addLine( "[ show player ] id" );
		addLine( "Wyświetla dane gracza o podanym ID." );
		addLine( "[ show achivments ] id" );
		addLine( "Wyświetla wszystkie postępy gracza." );
		addLine( "" );
	}

	// ----------------------------------------------------------------------
	private void commandECHO( string argument ) {
		addLine( argument );
		addLine( "" );
	}

	// ----------------------------------------------------------------------
	private void commandLISTPLAYERS() {
		int	players_count	=	PlayerPrefs.GetInt( "data_playersCount", 0 );
		
		for ( int i = 0; i < players_count; i++ ) {
			int	argument	=	PlayerPrefs.GetInt( "data_player"+i.ToString()+"ID", 0 );

			addLine( argument > 0 ? "ID_" + i.ToString() + ": " + argument.ToString() : "[ ! ] Błędny wpis gracza w bazie danych." );
		}
	}

	// ----------------------------------------------------------------------
	private void commandSHOWPLAYERS() {
		int	players_count	=	PlayerPrefs.GetInt( "data_playersCount", 0 );
		
		for ( int i = 0; i < players_count; i++ ) {
			int	argument	=	PlayerPrefs.GetInt( "data_player"+i.ToString()+"ID", 0 );

			if ( argument > 0 ) {
				addLine( "ID_" + i.ToString() + ": " + argument.ToString() );
				commandSHOWPLAYER( argument.ToString() );
			} else {
				addLine( "[ ! ] Błędny wpis gracza w bazie danych." );
			}
		}

		addLine( "" );
	}

	// ----------------------------------------------------------------------
	private void commandSHOWPLAYER( string argument ) {
		int		parse				=	0;
		string	argument_imie		=	PlayerPrefs.GetString( "data_player" + argument + "NAME", "null" );
		string	argument_nazwisko	=	PlayerPrefs.GetString( "data_player" + argument + "SURNAME", "null" );
		int		argument_day		=	PlayerPrefs.GetInt( "data_player" + argument + "DAY", 0 );
		int		argument_month		=	PlayerPrefs.GetInt( "data_player" + argument + "MOTH", 0 );
		int		argument_year		=	PlayerPrefs.GetInt( "data_player" + argument + "YEAR", 0 );

		if ( !int.TryParse( argument, out parse ) || argument_imie == "null" ) {
			addLine( "[ ! ] Błędne ID gracza." );
			addLine( "" );
			return;
		} else {
			addLine( "IMIE:     " + argument_imie );
			addLine( "NAZWISKO: " + argument_nazwisko );
			addLine( "DATA UR:  " + (argument_day+1).ToString() + "." + (argument_month+1).ToString() + "." + (argument_year+1900).ToString() );
		}

		addLine( "" );
	}

	// ----------------------------------------------------------------------
	private void commandSHOWACHIVMENTS( string argument ) {
		int		parse				=	0;
		int		count				=	Tools.score_titles.Length;
		string	argument_imie		=	PlayerPrefs.GetString( "data_player" + argument + "NAME", "null" );
		string	argument_nazwisko	=	PlayerPrefs.GetString( "data_player" + argument + "SURNAME", "null" );

		if ( !int.TryParse( argument, out parse ) || argument_imie == "null" ) {
			addLine( "[ ! ] Błędne ID gracza." );
			addLine( "" );
			return;

		} else {
			addLine( argument_imie + " " + argument_nazwisko + ":" );
			for ( int i = 0; i < count; i++ ) {
				float	score			=	PlayerPrefs.GetFloat( "data_player" + argument + "achivment" + i.ToString() + "SCORE", -1.0f );
				if ( score < 0.0f ) {
					addLine( Tools.score_titles[i] + ": brak wyniku." );
				} else {
					addLine( Tools.score_titles[i] + ": " + score.ToString() );
				}
			}
		}

		addLine( "" );
	}

	// ######################################################################

}

// ################################################################################