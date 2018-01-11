using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ################################################################################
//	XXXXX   X   X   XXXX    X   X   XXXXX      XXX      XXX    X   X
//	  X     XX  X   X   X   X   X     X        X  X    X   X    X X 
//	  X     X X X   XXXX    X   X     X        XXXX    X   X     X  
//	  X     X  XX   X       X   X     X        X   X   X   X    X X 
//	XXXXX   X   X   X        XXX      X        XXXX     XXX    X   X
// ################################################################################

public enum ContentType {
	Standard,
	AutoCorrected,
	IntegerNumber,
	DecimalNumber,
	Alphanumeric,
	Name,
	EmailAddress,
	Password,
	Pin
}

public class InputBox : MonoBehaviour {

	// PRIVATE VARIABLES:
	InputBoxReturnAction	ActionOK;
	InputBoxCancelAction	ActionCancel;
	private	object[]		action_objects;

	// PUBLIC VARIABLES:
	public	GameObject		text_title;
	public	GameObject		text_subtitle;
	public	GameObject		inputfield;
	public	GameObject		button_ok;
	public	GameObject		button_cancel;

	public	delegate void	InputBoxReturnAction( string output, object[] args );
	public	delegate void	InputBoxCancelAction( object[] args );

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################
	
	public void Init( string[] texts, ContentType contentType, InputBoxReturnAction functionOK, InputBoxCancelAction functionCancel, object[] args ) {
		clearBox();
		if ( texts.Length > 0 && texts[0] != null ) { text_title.GetComponent<Text>().text								=	texts[0]; }
		if ( texts.Length > 1 && texts[1] != null ) { text_subtitle.GetComponent<Text>().text							=	texts[1]; }
		if ( texts.Length > 2 && texts[2] != null ) { button_ok.transform.GetChild(0).GetComponent<Text>().text			=	texts[2]; }
		if ( texts.Length > 3 && texts[3] != null ) { button_cancel.transform.GetChild(0).GetComponent<Text>().text		=	texts[3]; }
		setContent( contentType );
		ActionOK		=	functionOK;
		ActionCancel	=	functionCancel;
		action_objects	=	args;
		showBox();
	}
	
	// ######################################################################
	//	X   X    XXX    X   X    XXX     XXXX   XXXXX   XXXX 
	//	XX XX   X   X   XX  X   X   X   X       X       X   X
	//	X X X   XXXXX   X X X   XXXXX   X  XX   XXX     XXXX 
	//	X   X   X   X   X  XX   X   X   X   X   X       X   X
	//	X   X   X   X   X   X   X   X    XXXX   XXXXX   X   X
	// ######################################################################

	private void clearBox() {
		ActionOK														=	null;
		ActionCancel													=	null;
		text_title.GetComponent<Text>().text							=	"Wprowadzanie tekstu";
		text_subtitle.GetComponent<Text>().text							=	"Wpisz ponieżej tekst który ma zostać użyty.";
		inputfield.GetComponent<InputField>().text						=	"";
		button_ok.transform.GetChild(0).GetComponent<Text>().text		=	"OK";
		button_cancel.transform.GetChild(0).GetComponent<Text>().text	=	"Anuluj";
		button_ok.GetComponent<Button>().onClick.RemoveAllListeners();
		button_cancel.GetComponent<Button>().onClick.RemoveAllListeners();
	}

	// ----------------------------------------------------------------------
	private void showBox() {
		button_ok.GetComponent<Button>().onClick.AddListener( onButtonOKClick );
		button_cancel.GetComponent<Button>().onClick.AddListener( onButtonCancelClick );
		gameObject.SetActive( true );
	}

	// ----------------------------------------------------------------------
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

	private void onButtonOKClick() {
		hideBox();
		string	output		=	inputfield.GetComponent<InputField>().text;
		if ( ActionOK != null ) { ActionOK( output, action_objects ); }
	}

	// ----------------------------------------------------------------------
	private void onButtonCancelClick() {
		hideBox();
		if ( ActionCancel != null ) { ActionCancel( action_objects ); }
	}

	// ######################################################################
	//	 XXX     XXX    X   X   XXXXX   XXXXX   X   X   XXXXX
	//	X   X   X   X   XX  X     X     X       XX  X     X  
	//	X       X   X   X X X     X     XXX     X X X     X  
	//	X   X   X   X   X  XX     X     X       X  XX     X  
	//	 XXX     XXX    X   X     X     XXXXX   X   X     X  
	// ######################################################################

	private void setContent( ContentType contentType ) {
		switch( contentType ) {
		case ContentType.Standard:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.Standard;
			break;
		case ContentType.AutoCorrected:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.Autocorrected;
			break;
		case ContentType.DecimalNumber:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.DecimalNumber;
			break;
		case ContentType.IntegerNumber:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.IntegerNumber;
			break;
		case ContentType.Alphanumeric:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.Alphanumeric;
			break;
		case ContentType.EmailAddress:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.EmailAddress;
			break;
		case ContentType.Name:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.Name;
			break;
		case ContentType.Password:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.Password;
			break;
		case ContentType.Pin:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.Pin;
			break;
		default:
			inputfield.GetComponent<InputField>().contentType	=	InputField.ContentType.Standard;
			break;
		}
	}

	// ######################################################################

}

// ################################################################################