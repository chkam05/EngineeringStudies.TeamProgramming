using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	X   X   XXXXX   X        XXX     XXX    X   X   XXXXX      XXX      XXX    X   X
//	X   X   X       X       X   X   X   X   XX XX   X          X  X    X   X    X X 
//	X X X   XXX     X       X       X   X   X X X   XXX        XXXX    X   X     X  
//	XX XX   X       X       X   X   X   X   X   X   X          X   X   X   X    X X 
//	X   X   XXXXX   XXXXX    XXX     XXX    X   X   XXXXX      XXXX     XXX    X   X
// ################################################################################

public class WelcomeBox : MonoBehaviour {

	// PRIVATE VARIABLES:
	private	bool			text_showanimation		=	false;
	private bool			text_hideanimation		=	false;
	private	bool			text_awaitanimation		=	false;
	private	bool			background_animation	=	false;

	private	float			text_alpha				=	0.0f;
	private	float			background_alpha		=	1.0f;
	private float			await_timer				=	2.5f;
	private float			color_jump				=	0.01f;

	// PUBLIC VARIABLES:
	public	GameObject		text_title;
	public	GameObject		text_subtitle;
	public	GameObject		panel_background;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	
	public void Init( string title, string subtitle ) {
		text_title.GetComponent<Text>().text		=	title;
		text_subtitle.GetComponent<Text>().text		=	subtitle;

		text_alpha			=	0.0f;
		background_alpha	=	1.0f;
		text_showanimation	=	true;
		setTextColor( text_alpha );
		setBackgroundColor( background_alpha );
		gameObject.SetActive( true );
	}

	void Update() {
		if ( text_showanimation ) { textShowAnimation(); }
		if ( text_hideanimation ) { textHideAnimation(); }
		if ( text_awaitanimation ) { awaitAnimation(); }
		if ( background_animation ) { backgroundAnimation(); }
	}
	
	// ######################################################################
	//	 XXX    X   X   XXXXX   X   X    XXX    XXXXX   XXXXX    XXX    X   X
	//	X   X   XX  X     X     XX XX   X   X     X       X     X   X   XX  X
	//	XXXXX   X X X     X     X X X   XXXXX     X       X     X   X   X X X
	//	X   X   X  XX     X     X   X   X   X     X       X     X   X   X  XX
	//	X   X   X   X   XXXXX   X   X   X   X     X     XXXXX    XXX    X   X
	// ######################################################################

	public void textShowAnimation() {
		text_alpha	+=	color_jump;

		setTextColor( text_alpha );
		if ( text_alpha >= 1.0f ) {
			text_showanimation		=	false;
			text_awaitanimation		=	true;
		}
	}

	// ----------------------------------------------------------------------
	public void awaitAnimation() {
		await_timer	+=	-color_jump;

		if ( await_timer <= 0.0f ) {
			text_awaitanimation		=	false;
			text_hideanimation		=	true;
		}
	}

	// ----------------------------------------------------------------------
	public void textHideAnimation() {
		text_alpha	+=	-color_jump;

		setTextColor( text_alpha );
		if ( text_alpha <= 0.0f ) {
			text_hideanimation		=	false;
			background_animation	=	true;
		}
	}

	// ----------------------------------------------------------------------
	public void backgroundAnimation() {
		background_alpha	+=	-color_jump;

		setBackgroundColor( background_alpha );
		if ( background_alpha <= 0.0f ) {
			background_animation	=	false;

			gameObject.SetActive( false );
		}
	}

	// ######################################################################
	//	 XXX     XXX    X        XXX    XXXX 
	//	X   X   X   X   X       X   X   X   X
	//	X       X   X   X       X   X   XXXX 
	//	X   X   X   X   X       X   X   X   X
	//	 XXX     XXX    XXXXX    XXX    X   X
	// ######################################################################

	private void setTextColor( float alpha ) {
		text_title.GetComponent<Text>().color		=	new Color( 1.0f, 1.0f, 1.0f, alpha );
		text_subtitle.GetComponent<Text>().color	=	new Color( 1.0f, 1.0f, 1.0f, alpha );
	}

	private void setBackgroundColor( float alpha ) {
		panel_background.GetComponent<Image>().color	=	new Color( 0.0f, 0.0f, 0.0f, alpha );
	}

	// ######################################################################

}

// ################################################################################