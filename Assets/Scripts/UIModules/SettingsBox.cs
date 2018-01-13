using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	XXXX     XXX    XXXXX    XXX 
//	 X  X   X   X     X     X   X
//	 X  X   XXXXX     X     XXXXX
//	 X  X   X   X     X     X   X
//	XXXX    X   X     X     X   X
// ################################################################################

/*
		data_musicMusic
		data_musicSound
		data_musicDialog

		data_playersCount
		data_player( int )ID
		data_player( data_player( int )ID )NAME";
		data_player( data_player( int )ID )SURNAME";
		data_player( data_player( int )ID )DAY";
		data_player( data_player( int )ID )MOTH
		data_player( data_player( int )ID )YEAR

		data_playerLastIndex
		data_playerLastID

		data_player( data_player( int )ID )achivment( X )SCORE
*/

// ################################################################################
//	 XXXX   XXXXX   XXXXX   XXXXX   XXXXX   X   X    XXXX    XXXX      XXX      XXX    X   X
//	X       X         X       X       X     XX  X   X       X          X  X    X   X    X X 
//	 XXX    XXX       X       X       X     X X X   X  XX    XXX       XXXX    X   X     X  
//	    X   X         X       X       X     X  XX   X   X       X      X   X   X   X    X X 
//	XXXX    XXXXX     X       X     XXXXX   X   X    XXXX   XXXX       XXXX     XXX    X   X
// ################################################################################

public class SettingsBox : MonoBehaviour {

	// PRIVATE VARIABLES:
	private	GameObject			active_page					=	null;
	private	bool				in_game						=	false;
	private bool				changePlayer_detector		=	true;
	private	int					year_init					=	1900;
	private	int					changePlayer_current		=	0;
	private	int					changePlayer_currentID		=	0;

	private	List<GameObject>	score_items					=	null;

	// PUBLIC VARIABLES:
	public	GameObject			button_save;
	public	GameObject			button_close;

	public	GameObject			button_player;
	public	GameObject			button_sound;
	public	GameObject			button_score;
	public	GameObject			button_informations;

	public	GameObject			container_player;
	public	GameObject			container_sound;
	public	GameObject			container_score;
	public	GameObject			container_informations;

	public	GameObject			dropdown_player;
	public	GameObject			button_addPlayer;
	public	GameObject			button_removePlayer;
	public	GameObject			button_updatePlayer;
	public	GameObject			text_playerID;
	public	GameObject			inputfield_playerName;
	public	GameObject			inputfield_playerSurname;
	public	GameObject			dropdown_playerDay;
	public	GameObject			dropdown_playerMonth;
	public	GameObject			dropdown_playerYear;

	public	GameObject			slider_music;
	public	GameObject			slider_sound;
	public	GameObject			slider_dialog;

	public	GameObject			item_score;
	public	GameObject			container_scoreItems;
	
	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//	  X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	
	public void Setup() {
		button_player.GetComponent<Button>().onClick.AddListener( openPlayerPage );
		button_sound.GetComponent<Button>().onClick.AddListener( openSoundPage );
		button_score.GetComponent<Button>().onClick.AddListener( openScorePage );
		button_informations.GetComponent<Button>().onClick.AddListener( openInfoPage );

		button_save.GetComponent<Button>().onClick.AddListener( onSaveButtonClick );
		button_close.GetComponent<Button>().onClick.AddListener( onCloseButtonClick );

		dropdown_player.GetComponent<ButtonBehaviour>().setOnMouseClick( detectPlayerChange );
		dropdown_player.GetComponent<Dropdown>().onValueChanged.AddListener( changePlayer );
		dropdown_playerYear.GetComponent<Dropdown>().onValueChanged.AddListener( changeYear );
		dropdown_playerMonth.GetComponent<Dropdown>().onValueChanged.AddListener( changeMonth );
		button_addPlayer.GetComponent<Button>().onClick.AddListener( addPlayer );
		button_removePlayer.GetComponent<Button>().onClick.AddListener( removePlayer );
		button_updatePlayer.GetComponent<Button>().onClick.AddListener( updatePlayer );
	}

	// ----------------------------------------------------------------------
	public void Init( bool in_game ) {
		this.in_game	=	in_game;
		showBox();
		if ( in_game ) { openSoundPage(); } else { openPlayerPage(); }
	}

	// ----------------------------------------------------------------------
	void Update() {
		
	}

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX   XXXX     XXX     XXX    XXXXX   XXXXX    XXX    X   X
	//	  X     XX  X     X     X       X   X   X   X   X   X     X       X     X   X   XX  X
	//	  X     X X X     X     XXX     XXXX    XXXXX   X         X       X     X   X   X X X
	//	  X     X  XX     X     X       X   X   X   X   X   X     X       X     X   X   X  XX
	//	XXXXX   X   X     X     XXXXX   X   X   X   X    XXX      X     XXXXX    XXX    X   X
	// ######################################################################

	private void clearPage() {
		container_player.SetActive( false );
		container_sound.SetActive( false );
		container_score.SetActive( false );
		container_informations.SetActive( false );
	}

	// ----------------------------------------------------------------------
	private void openPlayerPage() {
		if ( in_game ) { return; }
		clearPage();
		prepareYears();
		loadPlayersData();
		container_player.SetActive( true );
		active_page		=	container_player;
	}

	// ----------------------------------------------------------------------
	private void openSoundPage() {
		clearPage();
		loadSoundData();
		container_sound.SetActive( true );
		active_page		=	container_sound;
	}

	// ----------------------------------------------------------------------
	private void openScorePage() {
		clearPage();
		loadScoreData();
		container_score.SetActive( true );
		active_page		=	container_score;
	}

	// ----------------------------------------------------------------------
	private void openInfoPage() {
		clearPage();
		container_informations.SetActive( true );
		active_page		=	container_informations;
	}

	// ----------------------------------------------------------------------
	private void onSaveButtonClick() {
		if ( active_page == null ) { return; }
		if ( active_page == container_player ) { saveCurrentPlayer(); }
		if ( active_page == container_sound ) { saveSoundData(); }
	}

	// ----------------------------------------------------------------------
	private void onCloseButtonClick() {
		hideBox();
	}

	// ######################################################################
	//	XXXX    X        XXX    X   X   XXXXX   XXXX 
	//	X   X   X       X   X    X X    X       X   X
	//	XXXX    X       XXXXX     X     XXX     XXXX 
	//	X       X       X   X     X     X       X   X
	//	X       XXXXX   X   X     X     XXXXX   X   X
	//
	//	X   X    XXX    X   X    XXX     XXXX   XXXXX   XXXX 
	//	XX XX   X   X   XX  X   X   X   X       X       X   X
	//	X X X   XXXXX   X X X   XXXXX   X  XX   XXX     XXXX 
	//	X   X   X   X   X  XX   X   X   X   X   X       X   X
	//	X   X   X   X   X   X   X   X    XXXX   XXXXX   X   X
	// ######################################################################

	private int randomPlayerID() {
		return Random.Range( 0, int.MaxValue );
	}

	// ----------------------------------------------------------------------

	private void loadPlayersData() {
		int 			players_count		=	PlayerPrefs.GetInt( "data_playersCount", 1 );
		List<int>		players_id			=	new List<int>();
		List<string>	players_title		=	new List<string>();

		for ( int i = 0; i < players_count; i++ ) {
			string	key_playerID		=	"data_player" + i.ToString() + "ID";
			int		data_playerID		=	PlayerPrefs.GetInt( key_playerID, 0 );

			string	key_playerName		=	"data_player" + data_playerID.ToString() + "NAME";
			string	key_playerSurname	=	"data_player" + data_playerID.ToString() + "SURNAME";
			string	data_playerName		=	PlayerPrefs.GetString( key_playerName, "Gracz" );
			string	data_playerSurname	=	PlayerPrefs.GetString( key_playerSurname, "Domyślny" );

			players_id.Add( data_playerID );
			players_title.Add( data_playerName + " " + data_playerSurname );
			updatePlayersList( players_title );
		}

		string	key_playerLastIndex		=	"data_playerLastIndex";
		string	key_playerLastID		=	"data_playerLastID";
		changePlayer_current			=	PlayerPrefs.GetInt( key_playerLastIndex, 0 );
		changePlayer_currentID			=	PlayerPrefs.GetInt( key_playerLastID, 0 );
		changePlayer_detector			=	true;
		changePlayer( changePlayer_current );
	}

	// ----------------------------------------------------------------------

	private void saveCurrentPlayer() {
		string	key_playerLastIndex		=	"data_playerLastIndex";
		string	key_playerLastID		=	"data_playerLastID";

		PlayerPrefs.SetInt( key_playerLastIndex, changePlayer_current );
		PlayerPrefs.SetInt( key_playerLastID, changePlayer_currentID );
	}

	// ----------------------------------------------------------------------

	private void updatePlayersList( List<string> players_title ) {
		dropdown_player.GetComponent<Dropdown>().ClearOptions();
		dropdown_player.GetComponent<Dropdown>().AddOptions( players_title );
	}

	// ######################################################################
	private void detectPlayerChange( object[] args ) {
		//Debug.Log( "detector" );
		changePlayer_detector		=	true;
	}

	// ----------------------------------------------------------------------

	private void changePlayer( int index ) {
		if ( changePlayer_detector ) {
			//Debug.Log( "reactor" );
			changePlayer_detector								=	false;
			dropdown_player.GetComponent<Dropdown>().value		=	index;

			string	key_playerID			=	"data_player" + index.ToString() + "ID";
			int		data_playerID			=	PlayerPrefs.GetInt( key_playerID, 0 );

			changePlayer_current			=	index;
			changePlayer_currentID			=	data_playerID;

			string	key_playerName			=	"data_player" + data_playerID.ToString() + "NAME";
			string	key_playerSurname		=	"data_player" + data_playerID.ToString() + "SURNAME";
			string	key_playerDay			=	"data_player" + data_playerID.ToString() + "DAY";
			string	key_playerMonth			=	"data_player" + data_playerID.ToString() + "MOTH";
			string	key_playerYear			=	"data_player" + data_playerID.ToString() + "YEAR";

			text_playerID.GetComponent<Text>().text						=	"ID Gracza: " + data_playerID;
			inputfield_playerName.GetComponent<InputField>().text		=	PlayerPrefs.GetString( key_playerName, "Gracz" );
			inputfield_playerSurname.GetComponent<InputField>().text	=	PlayerPrefs.GetString( key_playerSurname, "Domyślny" );
			setDate( PlayerPrefs.GetInt( key_playerDay, 0 ), PlayerPrefs.GetInt( key_playerMonth, 0 ), PlayerPrefs.GetInt( key_playerYear, 0 ) );
		}
	}

	// ----------------------------------------------------------------------
	private void addPlayer() {
		int		players_count		=	PlayerPrefs.GetInt( "data_playersCount", 1 );
		int		players_newCount	=	players_count+1;
		int		player_index		=	players_count;
		int		player_id			=	randomPlayerID();

		string	key_playerID		=	"data_player" + player_index.ToString() + "ID";
		string	key_playerName		=	"data_player" + player_id.ToString() + "NAME";
		string	key_playerSurname	=	"data_player" + player_id.ToString() + "SURNAME";
		string	key_playerDay		=	"data_player" + player_id.ToString() + "DAY";
		string	key_playerMonth		=	"data_player" + player_id.ToString() + "MOTH";
		string	key_playerYear		=	"data_player" + player_id.ToString() + "YEAR";

		PlayerPrefs.SetInt( "data_playersCount", players_newCount );
		PlayerPrefs.SetInt( key_playerID, player_id );
		PlayerPrefs.SetString( key_playerName, inputfield_playerName.GetComponent<InputField>().text );
		PlayerPrefs.SetString( key_playerSurname, inputfield_playerSurname.GetComponent<InputField>().text );
		PlayerPrefs.SetInt( key_playerDay, dropdown_playerDay.GetComponent<Dropdown>().value );
		PlayerPrefs.SetInt( key_playerMonth, dropdown_playerMonth.GetComponent<Dropdown>().value );
		PlayerPrefs.SetInt( key_playerYear, dropdown_playerYear.GetComponent<Dropdown>().value );

		loadPlayersData();
	}

	// ----------------------------------------------------------------------
	private void updatePlayer() {
		//string	key_playerID		=	"data_player" + changePlayer_current.ToString() + "ID";
		string	key_playerName		=	"data_player" + changePlayer_currentID.ToString() + "NAME";
		string	key_playerSurname	=	"data_player" + changePlayer_currentID.ToString() + "SURNAME";
		string	key_playerDay		=	"data_player" + changePlayer_currentID.ToString() + "DAY";
		string	key_playerMonth		=	"data_player" + changePlayer_currentID.ToString() + "MOTH";
		string	key_playerYear		=	"data_player" + changePlayer_currentID.ToString() + "YEAR";
		
		PlayerPrefs.SetString( key_playerName, inputfield_playerName.GetComponent<InputField>().text );
		PlayerPrefs.SetString( key_playerSurname, inputfield_playerSurname.GetComponent<InputField>().text );
		PlayerPrefs.SetInt( key_playerDay, dropdown_playerDay.GetComponent<Dropdown>().value );
		PlayerPrefs.SetInt( key_playerMonth, dropdown_playerMonth.GetComponent<Dropdown>().value );
		PlayerPrefs.SetInt( key_playerYear, dropdown_playerYear.GetComponent<Dropdown>().value );

		loadPlayersData();
	}

	// ----------------------------------------------------------------------
	private void removePlayer() {
		int		players_count			=	PlayerPrefs.GetInt( "data_playersCount", 1 );
		if ( players_count <= 1 ) { return; }
		int		players_countNew		=	players_count-1;

		string	key_playerID		=	"data_player" + changePlayer_current.ToString() + "ID";
		int		player_id			=	PlayerPrefs.GetInt( key_playerID, 0 );

		string	key_playerName		=	"data_player" + player_id.ToString() + "NAME";
		string	key_playerSurname	=	"data_player" + player_id.ToString() + "SURNAME";
		string	key_playerDay		=	"data_player" + player_id.ToString() + "DAY";
		string	key_playerMonth		=	"data_player" + player_id.ToString() + "MOTH";
		string	key_playerYear		=	"data_player" + player_id.ToString() + "YEAR";

		PlayerPrefs.DeleteKey( key_playerName );
		PlayerPrefs.DeleteKey( key_playerSurname );
		PlayerPrefs.DeleteKey( key_playerDay );
		PlayerPrefs.DeleteKey( key_playerMonth );
		PlayerPrefs.DeleteKey( key_playerYear );

		arrangePlayers( changePlayer_current );
		PlayerPrefs.SetInt( "data_playersCount", players_countNew );
		loadPlayersData();
	}

	// ----------------------------------------------------------------------
	private void arrangePlayers( int index ) {
		int		players_count			=	PlayerPrefs.GetInt( "data_playersCount", 1 );

		for ( int i = index; i < players_count; i++ ) {
			if ( i+1 < players_count ) {
				string	key_playerID	=	"data_player" + i.ToString() + "ID";
				string	key_playerIDN	=	"data_player" + (i+1).ToString() + "ID";
				PlayerPrefs.SetInt( key_playerID, PlayerPrefs.GetInt( key_playerIDN, 0 ) );
			} else {
				string	key_playerIDN		=	"data_player" + i.ToString() + "ID";
				PlayerPrefs.DeleteKey( key_playerIDN );
			}
		}
	}

	// ######################################################################
	private void setDate( int dayData, int monthData, int yearData ) {
		dropdown_playerYear.GetComponent<Dropdown>().value		=	yearData;
		dropdown_playerMonth.GetComponent<Dropdown>().value		=	monthData;
		prepareDay( monthData+1, yearData + year_init );
		dropdown_playerDay.GetComponent<Dropdown>().value		=	dayData;
	}

	// ----------------------------------------------------------------------
	private void prepareYears() {
		List<string>	years		=	new List<string>();
		int				year_end	=	2017;
		int.TryParse( System.DateTime.Now.ToString( "yyyy" ), out year_end );
		
		for ( int i=year_init; i <= year_end; i++ ) {
			years.Add( i.ToString() );
		}

		dropdown_playerYear.GetComponent<Dropdown>().ClearOptions();
		dropdown_playerYear.GetComponent<Dropdown>().AddOptions( years );

		int				month_now	=	1;
		int				day_now		=	1;
		int.TryParse( System.DateTime.Now.ToString( "MM" ), out month_now );
		int.TryParse( System.DateTime.Now.ToString( "dd" ), out day_now );

		prepareDay( year_end, month_now );
		dropdown_playerYear.GetComponent<Dropdown>().value		=	(year_end - year_init);
		dropdown_playerMonth.GetComponent<Dropdown>().value		=	(month_now - 1);
		dropdown_playerDay.GetComponent<Dropdown>().value		=	(day_now - 1);
	}

	// ----------------------------------------------------------------------
	private void prepareDay( int month, int year ) {
		List<string>	days		=	new List<string>();
		int				days_init	=	1;
		int				days_end	=	30;

		if ( month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12 ) {
			days_end		=	31;
		} else if ( month == 2 ) {
			if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) {
				days_end	=	29;
			} else {
				days_end	=	28;
			}
		}

		for ( int i=days_init; i<=days_end; i++ ) {
			days.Add( i.ToString() );
		}

		dropdown_playerDay.GetComponent<Dropdown>().ClearOptions();
		dropdown_playerDay.GetComponent<Dropdown>().AddOptions( days );
	}

	// ----------------------------------------------------------------------
	private void changeYear( int index ) {
		int		year_data		=		dropdown_playerYear.GetComponent<Dropdown>().value;
		int		month_data		=		dropdown_playerMonth.GetComponent<Dropdown>().value;
		
		prepareDay( month_data+1, year_data+year_init );
	}

	// ----------------------------------------------------------------------
	private void changeMonth( int index ) {
		int		year_data		=		dropdown_playerYear.GetComponent<Dropdown>().value;
		int		month_data		=		dropdown_playerMonth.GetComponent<Dropdown>().value;

		prepareDay( month_data+1, year_data+year_init );
	}

	// ######################################################################
	//	 XXXX    XXX    X   X   X   X   XXXX 
	//	X       X   X   X   X   XX  X    X  X
	//	 XXX    X   X   X   X   X X X    X  X
	//	    X   X   X   X   X   X  XX    X  X
	//	XXXX     XXX     XXX    X   X   XXXX 
	//
	//	X   X    XXX    X   X    XXX     XXXX   XXXXX   XXXX 
	//	XX XX   X   X   XX  X   X   X   X       X       X   X
	//	X X X   XXXXX   X X X   XXXXX   X  XX   XXX     XXXX 
	//	X   X   X   X   X  XX   X   X   X   X   X       X   X
	//	X   X   X   X   X   X   X   X    XXXX   XXXXX   X   X
	// ######################################################################

	private void loadSoundData() {
		slider_music.GetComponent<Slider>().value	=	PlayerPrefs.GetFloat( "data_musicMusic", 50.0f );
		slider_sound.GetComponent<Slider>().value	=	PlayerPrefs.GetFloat( "data_musicSound", 50.0f );
		slider_dialog.GetComponent<Slider>().value	=	PlayerPrefs.GetFloat( "data_musicDialog", 50.0f );
	}

	// ----------------------------------------------------------------------
	private void saveSoundData() {
		PlayerPrefs.SetFloat( "data_musicMusic", slider_music.GetComponent<Slider>().value );
		PlayerPrefs.SetFloat( "data_musicSound", slider_sound.GetComponent<Slider>().value );
		PlayerPrefs.SetFloat( "data_musicDialog", slider_dialog.GetComponent<Slider>().value );
	}

	// ######################################################################
	//	 XXXX    XXX     XXX    XXXX    XXXXX
	//	X       X   X   X   X   X   X   X    
	//	 XXX    X       X   X   XXXX    XXX  
	//	    X   X   X   X   X   X   X   X    
	//	xXXX     XXX     XXX    X   X   XXXXX
	//
	//	X   X    XXX    X   X    XXX     XXXX   XXXXX   XXXX 
	//	XX XX   X   X   XX  X   X   X   X       X       X   X
	//	X X X   XXXXX   X X X   XXXXX   X  XX   XXX     XXXX 
	//	X   X   X   X   X  XX   X   X   X   X   X       X   X
	//	X   X   X   X   X   X   X   X    XXXX   XXXXX   X   X
	// ######################################################################

	private void loadScoreData() {
		deleteRepresentationScoreItems();
		score_items			=	new List<GameObject>();

		int		playerID	=	PlayerPrefs.GetInt( "data_playerLastID", 0 );
		int 	start_y		=	56;
		int		end_y		=	16;
		//float	x			=	item_score.GetComponent<RectTransform>().sizeDelta.x;
		float	y			=	item_score.GetComponent<RectTransform>().sizeDelta.y;
		float	width		=	container_scoreItems.GetComponent<RectTransform>().sizeDelta.x;
		float	height		=	( 8 + y ) * Tools.score_titles.Length + start_y + end_y;

		container_scoreItems.GetComponent<RectTransform>().sizeDelta	=	new Vector2( width, height );
		
		for ( int i=0; i<Tools.score_titles.Length; i++ ) {
			float	top				=	( 8 + y ) * i + start_y;
			float	offset_MaxX		=	-16;
			float	offset_MaxY		=	-top;
			float	offset_MinX		=	16;
			float	offset_MinY		=	-top - y;
			//Debug.Log( "offsetMin :: " + i + " :: " + offset_MinX + " :: " + offset_MinY );
			//Debug.Log( "offsetMax :: " + i + " :: " + offset_MaxX + " :: " + offset_MaxY );

			GameObject	item		=	Instantiate( item_score, container_scoreItems.transform );
			item.GetComponent<RectTransform>().offsetMin			=	new Vector2( offset_MinX, offset_MinY );
			item.GetComponent<RectTransform>().offsetMax			=	new Vector2( offset_MaxX, offset_MaxY );
			//item.GetComponent<RectTransform>().sizeDelta			=	new Vector2( x, y );
			
			float	score			=	Tools.loadScore( playerID, i );
			item.transform.GetChild(0).GetComponent<RawImage>().texture		=	Resources.Load( "Images/icon_goblet" ) as Texture;
			item.transform.GetChild(1).GetComponent<Text>().text			=	Tools.score_titles[i];
			item.transform.GetChild(2).GetComponent<Text>().text			=	score.ToString() + "%";

			switch( Tools.scoreResult( score ) ) {
				case EndingBox.EndingType.gold:
					item.transform.GetChild(0).GetComponent<RawImage>().color	=	Tools.HTMLToColor( Tools.ending_colors[0] );
					break;
				case EndingBox.EndingType.silver:
					item.transform.GetChild(0).GetComponent<RawImage>().color	=	Tools.HTMLToColor( Tools.ending_colors[1] );
					break;
				case EndingBox.EndingType.bronze:
					item.transform.GetChild(0).GetComponent<RawImage>().color	=	Tools.HTMLToColor( Tools.ending_colors[2] );
					break;
				default:
					item.transform.GetChild(0).GetComponent<RawImage>().texture		=	Resources.Load( "Images/icon_crack" ) as Texture;
					item.transform.GetChild(0).GetComponent<RawImage>().color	=	Tools.HTMLToColor( Tools.ending_colors[3] );
					break;
			}
			
			score_items.Add( item );
		}
	}

	// ----------------------------------------------------------------------
	private void deleteRepresentationScoreItems() {
		if ( score_items != null ) {
			foreach( GameObject item in score_items ) { GameObject.Destroy( item ); }
			score_items.Clear();
			score_items		=	null;
		}
	}

	// ######################################################################
	//	X   X   XXXXX   X   X   XXXX     XXX    X   X
	//	X   X     X     XX  X    X  X   X   X   X   X
	//	X X X     X     X X X    X  X   X   X   X X X
	//	XX XX     X     X  XX    X  X   X   X   XX XX
	//	X   X   XXXXX   X   X   XXXX     XXX    X   X
	// ######################################################################

	private void showBox() {
		gameObject.SetActive( true );
	}

	// ----------------------------------------------------------------------
	private void hideBox() {
		gameObject.SetActive( false );
	}
	
	// ######################################################################
}

// ################################################################################