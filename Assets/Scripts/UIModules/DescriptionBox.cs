using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	XXXX    XXXXX    XXXX    XXX    XXXX    XXXXX   XXXX    XXXXX   XXXXX    XXX    X   X
//	 X  X   X       X       X   X   X   X     X     X   X     X       X     X   X   XX  X
//	 X  X   XXX      XXX    X       XXXX      X     XXXX      X       X     X   X   X X X
//	 X  X   X           X   X   X   X   X     X     X         X       X     X   X   X  XX
//	XXXX    XXXXX   XXXX     XXX    X   X   XXXXX   X         X     XXXXX    XXX    X   X
//
//	XXX      XXX    X   X
//	X  X    X   X    X X
//	XXXX    X   X     X  
//	X   X   X   X    X X 
//	XXXX     XXX    X   X
// ################################################################################

public class DescriptionBox : MonoBehaviour {

	// PRIVATE VARIABLES:
	//	...

	// PUBLIC VARIABLES:
	public	GameObject				button_close;
	public	GameObject				container_text;
	public	GameObject				text_title;
	public	GameObject				text_content;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//	  X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	public void Init ( string title, string text, int size ) {
		button_close.GetComponent<Button>().onClick.RemoveAllListeners();
		button_close.GetComponent<Button>().onClick.AddListener( closeBox );

		float	width		=	container_text.GetComponent<RectTransform>().sizeDelta.x;
		float	height		=	(float) size;
		container_text.GetComponent<RectTransform>().sizeDelta	=	new Vector2( width, height );
		
		text_title.GetComponent<Text>().text	=	title;
		text_content.GetComponent<Text>().text	=	text;

		openBox();
	}
	
	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX   XXXX     XXX     XXX    XXXXX   XXXXX    XXX    X   X
	//	  X     XX  X     X     X       X   X   X   X   X   X     X       X     X   X   XX  X
	//	  X     X X X     X     XXX     XXXX    XXXXX   X         X       X     X   X   X X X
	//	  X     X  XX     X     X       X   X   X   X   X   X     X       X     X   X   X  XX
	//	XXXXX   X   X     X     XXXXX   X   X   X   X    XXX      X     XXXXX    XXX    X   X
	// ######################################################################

	private void openBox() {
		gameObject.SetActive( true );
	}

	// ----------------------------------------------------------------------
	private void closeBox() {
		gameObject.SetActive( false );
	}

	// ######################################################################

}

// ################################################################################