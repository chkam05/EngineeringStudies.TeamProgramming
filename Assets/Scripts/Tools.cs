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
	private static	float		score_max		=	100.0f;

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
	//	 XXX     XXX    X        XXX    XXXX     XXXX
	//	X   X   X   X   X       X   X   X   X   X    
	//	X       X   X   X       X   X   XXXX     XXX 
	//	X   X   X   X   X       X   X   X   X       X
	//	 XXX     XXX    XXXXX    XXX    X   X   XXXX 
	// ######################################################################

	public static Color HTMLToColor( string html ) {
		Color	output	=	new Color( 1.0f, 1.0f, 1.0f, 1.0f );
		ColorUtility.TryParseHtmlString( html, out output );
		return	output;
	}

	// ----------------------------------------------------------------------

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

	public static void scoreSet( float minBronze, float minSilver, float minGold, float maxPoints ) {
		score[0]	=	minBronze;
		score[1]	=	minSilver;
		score[2]	=	minGold;
		score_max	=	maxPoints;
	}

	// ----------------------------------------------------------------------
	public static float scalePoints( float points, float maxPoints ) {
		//	maxPoints	=	score_max
		//	points		=	return
		return	(( score_max * points ) / maxPoints);
	}

	// ----------------------------------------------------------------------
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

	public static void saveScore( int playerID, int indexCompetition, float score ) {
		string	key_competition		=	"data_player" + playerID.ToString() + "achivment" + indexCompetition.ToString() + "SCORE";

		if ( PlayerPrefs.GetFloat( key_competition, 0.0f ) < score ) {
			PlayerPrefs.SetFloat( key_competition, score );
		}
	}

	// ----------------------------------------------------------------------
	public static float loadScore( int playerID, int indexCompetition ) {
		string	key_competition		=	"data_player" + playerID.ToString() + "achivment" + indexCompetition.ToString() + "SCORE";

		return PlayerPrefs.GetFloat( key_competition, 0.0f );
	}

	// ######################################################################
	
}

// ################################################################################