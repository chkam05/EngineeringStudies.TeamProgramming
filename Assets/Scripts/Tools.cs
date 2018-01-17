using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################
//	XXXXX    XXX     XXX    X        XXXX
//	  X     X   X   X   X   X       X    
//	  X     X   X   X   X   X        XXX 
//	  X     X   X   X   X   X           X
//	  X      XXX     XXX    XXXXX   XXXX 
// ################################################################################

public static class Tools {

	// PRIVATE VARIABLES:
	private static	float[]		score			=	new float[3] { 50.0f, 70.0f, 90.0f };

	// PUBLIC VARIABLES:
	//	...

	// ######################################################################
	//	XXXX     XXX    XXXXX    XXX 
	//	 X  X   X   X     X     X   X
	//	 X  X   XXXXX     X     XXXXX
	//	 X  X   X   X     X     X   X
	//	XXXX    X   X     X     X   X
	// ######################################################################

	public	static	readonly	string[]	score_titles		=	{
		"Test: Bloki",
		"Test: Równia pochyła",
		"Test: Dźwignia"
	};

	public	static	readonly	string[]	ending_colors		=	{
		"#F0D60F",
		"#BFBFBF",
		"#8E5F1A",
		"#5A1F08"
	};

	public	static	readonly	string[]	achivment_colors	=	{
		"#007624",
		"#000000",
		"#A7000D"
	};

	// ######################################################################
	//	X   X    XXX    XXXXX   X   X
	//	XX XX   X   X     X     X   X
	//	X X X   XXXXX     X     XXXXX
	//	X   X   X   X     X     X   X
	//	X   X   X   X     x     X   X 
	// ######################################################################
	/// <summary>
	/// Konwersja z radianów na stopnie. 
	/// </summary>
	/// <returns> Stopnie. </returns>
	/// <param name="radian"> Radiany. </param>

	public static float radianToDegree( float radian ) {
		return ( radian * ( 180.0f / Mathf.PI ) );
	}

	/// <summary>
	/// Konwersja ze stopni na radiany.
	/// </summary>
	/// <returns> Radiany. </returns>
	/// <param name="radian"> Stopnie. </param>

	public static float degreeToRadian( float degree ) {
		return ( Mathf.PI * degree / 180.0f );
	}

	// ######################################################################
	//	 XXX     XXX    X        XXX    XXXX     XXXX
	//	X   X   X   X   X       X   X   X   X   X    
	//	X       X   X   X       X   X   XXXX     XXX 
	//	X   X   X   X   X       X   X   X   X       X
	//	 XXX     XXX    XXXXX    XXX    X   X   XXXX 
	// ######################################################################
	/// <summary>
	/// Konwersja kodu HTML na kolor
	/// </summary>
	/// <returns> Kolor </returns>
	/// <param name="html"> Kod HTML koloru. </param>

	public static Color HTMLToColor( string html ) {
		Color	output	=	new Color( 1.0f, 1.0f, 1.0f, 1.0f );
		ColorUtility.TryParseHtmlString( html, out output );
		return	output;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ustawia przeźroczystość koloru.
	/// </summary>
	/// <returns> Zwraca kolor. </returns>
	/// <param name="color"> Kolor. </param>
	/// <param name="alpha255"> Przeźroczystość 0 - 255. </param>

	public static Color setAplha( Color color, int alpha255 ) {
		Color	output	=	color;
		output.a		=	(alpha255 / 255 );
		return	output;
	}

	// ######################################################################
	//	 XXXX    XXX     XXX    XXXX    XXXXX
	//	X       X   X   X   X   X   X   X    
	//	 XXX    X       X   X   XXXX    XXX  
	//	    X   X   X   X   X   X   X   X    
	//	XXXX     XXX     XXX    X   X   XXXXX
	// ######################################################################
	/// <summary>
	/// Oblicza punktację.
	/// </summary>
	/// <returns> Ocena. </returns>
	/// <param name="points"> Ilość uzyskanych punktów </param>
	/// <param name="maxPoints"> Maksymalna ilość punktów. </param>

	public static float scalePoints( float points, float maxPoints ) {
		//	maxPoints	=	score_max
		//	points		=	return
		//	maxPoints * return	=	score_max * points
		//	return	=	( score_max * points ) / maxPoints

		return	(( 100.0f * points ) / maxPoints);
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Konwersja punktacji na oceny.
	/// </summary>
	/// <returns> Wynik w postaci typu zakończenia. </returns>
	/// <param name="points"> Punktacja. </param>

	public static EndingBox.EndingType scoreResult( float points ) {
		if ( (points) >= score[2] ) { return EndingBox.EndingType.gold; }
		else if ( (points) >= score[1] ) { return EndingBox.EndingType.silver; }
		else if ( (points) >= score[0] ) { return EndingBox.EndingType.bronze; }
		else { return EndingBox.EndingType.fail; }
	}

	// ######################################################################
	//	 XXXX    XXX     XXX    XXXX    XXXXX       XXXX    XXX    X   X   XXXXX
	//	X       X   X   X   X   X   X   X          X       X   X   X   X   X    
	//	 XXX    X       X   X   XXXX    XXX         XXX    XXXXX   X   X   XXX  
	//	    X   X   X   X   X   X   X   X              X   X   X    X X    X    
	//	XXXX     XXX     XXX    X   X   XXXXX      XXXX    X   X     X     XXXXX
	// ######################################################################
	/// <summary>
	/// Zapisuje punktacje tekstu dla wybranego gracza.
	/// </summary>
	/// <param name="playerID"> ID gracza. </param>
	/// <param name="indexCompetition"> Indeks testu. </param>
	/// <param name="score"> Punktacja. </param>

	public static void saveScore( int playerID, int indexCompetition, float score ) {
		string	key_competition		=	"data_player" + playerID.ToString() + "achivment" + indexCompetition.ToString() + "SCORE";

		if ( PlayerPrefs.GetFloat( key_competition, 0.0f ) < score ) {
			PlayerPrefs.SetFloat( key_competition, score );
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Pobiera informacje o teście.
	/// </summary>
	/// <returns> Punktacja. </returns>
	/// <param name="playerID"> ID gracza. </param>
	/// <param name="indexCompetition"> Indeks testu. </param>

	public static float loadScore( int playerID, int indexCompetition ) {
		string	key_competition		=	"data_player" + playerID.ToString() + "achivment" + indexCompetition.ToString() + "SCORE";

		return PlayerPrefs.GetFloat( key_competition, 0.0f );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Sprawdza czy zakończono wszystkie testy pozytywnie.
	/// </summary>
	/// <returns> True - tak, False - nie. </returns>

	public static bool checkScoreComplete() {
		int		playerID	=	PlayerPrefs.GetInt( "data_playerLastID", 0 );
		
		for ( int i=0; i<Tools.score_titles.Length; i++ ) {
			float	current_score	=	Tools.loadScore( playerID, i );
			if ( current_score < 50 ) {
				return false;
			}
		}

		return true;
	}

	// ######################################################################
	
}

// ################################################################################