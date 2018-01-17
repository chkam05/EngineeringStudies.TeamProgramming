using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	XXXXX   X   X   XXXX    XXXXX   X   X    XXXX      XXX      XXX    X   X
//	X       XX  X    X  X     X     XX  X   X          X  X    X   X    X X 
//	XXX     X X X    X  X     X     X X X   X  XX      XXXX    X   X     X  
//	X       X  XX    X  X     X     X  XX   X   X      X   X   X   X    X X 
//	XXXXX   X   X   XXXX    XXXXX   X   X    XXXX      XXXX     XXX    X   X
// ################################################################################

public class EndingBox : MonoBehaviour {

	// ######################################################################
	public enum EndingType {
		gold,
		silver,
		bronze,
		fail
	}
	// ######################################################################

	// PRIVATE VARIABLES:
	EndingBoxOKAction		ActionOK;
	private	object[]		action_objects;

	// PUBLIC VARIABLES:
	public	GameObject		text_title;
	public	GameObject		text_subtitle;
	public	GameObject		rawimage_goblet;
	public	GameObject		button_ok;

	public	delegate void	EndingBoxOKAction( object[] args );

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	/// <summary>
	/// Funkcja konfigurująca okno zakończenia.
	/// </summary>
	/// <param name="texts"> Teksty, (Tytuł, Podtytuł, Przycisk OK). </param>
	/// <param name="functionOK"> Funkcja dla przycisku OK. </param>
	/// <param name="endingType"> Typ zakończenia. </param>
	/// <param name="args"> Argumenty dla funkcji. </param>

	public void Init( string[] texts, EndingBoxOKAction functionOK, EndingType endingType, object[] args ) {
		clearBox();
		if ( texts.Length > 0 && texts[0] != null ) { text_title.GetComponent<Text>().text								=	texts[0]; }
		if ( texts.Length > 1 && texts[1] != null ) { text_subtitle.GetComponent<Text>().text							=	texts[1]; }
		if ( texts.Length > 2 && texts[2] != null ) { button_ok.transform.GetChild(0).GetComponent<Text>().text			=	texts[2]; }
		ActionOK				=	functionOK;
		action_objects			=	args;

		rawimage_goblet.GetComponent<RawImage>().texture		=	Resources.Load( "Images/icon_goblet" ) as Texture;
		Color	endingColor										=	new Color( 1.0f, 1.0f, 1.0f, 1.0f );

		switch ( endingType ) {
		case EndingType.gold:
			ColorUtility.TryParseHtmlString( Tools.ending_colors[0], out endingColor );
			break;
		case EndingType.silver:
			ColorUtility.TryParseHtmlString( Tools.ending_colors[1], out endingColor );
			break;
		case EndingType.bronze:
			ColorUtility.TryParseHtmlString( Tools.ending_colors[2], out endingColor );
			break;
		case EndingType.fail:
			ColorUtility.TryParseHtmlString( Tools.ending_colors[3], out endingColor );
			rawimage_goblet.GetComponent<RawImage>().texture	=	Resources.Load( "Images/icon_crack" ) as Texture;
			break;
		default:
			endingColor											=	new Color( 0.0f, 0.0f, 0.0f, 0.0f );
			rawimage_goblet.GetComponent<RawImage>().texture	=	Resources.Load( "Images/empty" ) as Texture;
			break;	
		}

		rawimage_goblet.GetComponent<RawImage>().color			=	endingColor;
		showBox();
	}
	
	// ######################################################################
	//	X   X    XXX    X   X    XXX     XXXX   XXXXX   XXXX 
	//	XX XX   X   X   XX  X   X   X   X       X       X   X
	//	X X X   XXXXX   X X X   XXXXX   X  XX   XXX     XXXX 
	//	X   X   X   X   X  XX   X   X   X   X   X       X   X
	//	X   X   X   X   X   X   X   X    XXXX   XXXXX   X   X
	// ######################################################################
	/// <summary>
	/// Czyści okno zakończenia.
	/// </summary>

	private void clearBox() {
		ActionOK														=	null;
		text_title.GetComponent<Text>().text							=	"WYGRANA";
		text_subtitle.GetComponent<Text>().text							=	"Zakończono z punktacją 85/100";
		rawimage_goblet.GetComponent<RawImage>().texture				=	Resources.Load( "Images/empty" ) as Texture;
		rawimage_goblet.GetComponent<RawImage>().color					=	new Color( 0.0f, 0.0f, 0.0f, 0.0f );
		button_ok.transform.GetChild(0).GetComponent<Text>().text		=	"OK";
		button_ok.GetComponent<Button>().onClick.RemoveAllListeners();
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Pokazuje okno zakończenia.
	/// </summary>

	private void showBox() {
		button_ok.GetComponent<Button>().onClick.AddListener( onButtonOKClick );
		gameObject.SetActive( true );
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Zamyka okno zakończenia.
	/// </summary>

	private void hideBox() {
		gameObject.SetActive( false );
	}

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX   XXXX     XXX     XXX    XXXXX   XXXXX    XXX    X   X
	//	  X     XX  X     X     X       X   X   X   X   X   X     X       X     X   X   XX  X
	//	  X     X X X     X     XXX     XXXX    XXXXX   X         X       X     X   X   X X X
	//	  X     X  XX     X     X       X   X   X   X   X   X     X       X     X   X   X  XX
	//	XXXXX   X   X     X     XXXXX   X   X   X   X    XXX      X     XXXXX    XXX    X   X
	// ######################################################################
	/// <summary>
	/// Funkcja wykonująca się po naciśnięciu przycisku OK.
	/// </summary>

	private void onButtonOKClick() {
		hideBox();
		if ( ActionOK != null ) { ActionOK( action_objects ); }
	}

	// ######################################################################

}

// ################################################################################