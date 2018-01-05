using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	XXXXX   XXXXX   X   X   XXXXX
//	  X     X        X X      X  
//	  X     XXX       X       X  
//	  X     X        X X      X  
//	  X     XXXXX   X   X     X  
//
//	XXXX     XXX    X       X    
//	X   X   X   X   X       X    
//	XXXX    X   X   X       X    
//	X   X   X   X   X       X    
//	X   X    XXX    XXXXX   XXXXX
// ################################################################################

public class TextRoll : MonoBehaviour {

	// PRIVATE VARIABLES:
	private	string				initText		=	"";
	private	string				activeText		=	"";

	private float				timer			=	0.0f;
	private	float				timerTick		=	0.15f;

	// PUBLIC VARIABLES:
	// ...

	// ######################################################################
	void Start () {
		initText		=	gameObject.GetComponent<Text>().text;
		resetText();
	}
	
	// ----------------------------------------------------------------------
	void Update () {
		timer			+=	Time.deltaTime;

		if ( timer >= timerTick ) {
			timer		=	0.0f;
			activeText	=	activeText.Substring( 1 );
		}

		if ( activeText.Length == 0 ) {
			resetText();
		}

		showText();
	}
	
	// ######################################################################
	public void setText( string text ) {
		initText		=	text;
		resetText();
	}

	// ----------------------------------------------------------------------
	private void showText() {
		gameObject.GetComponent<Text>().text	=	activeText;
	}

	// ----------------------------------------------------------------------
	private void resetText() {
		activeText		=	initText;
		timer			=	0.0f;
	}

	// ######################################################################

}

// ################################################################################