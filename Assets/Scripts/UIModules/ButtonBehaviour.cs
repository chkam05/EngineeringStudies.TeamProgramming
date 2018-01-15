using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ################################################################################
//	XXX     X   X   XXXXX   XXXXX    XXX    X   X
//	X  X    X   X     X       X     X   X   XX  X
//	XXXX    X   X     X       X     X   X   X X X
//	X   X   X   X     X       X     X   X   X  XX
//	XXXX     XXX      X       X      XXX    X   X
//
//	XXX     XXXXX   X   X    XXX    X   X   XXXXX    XXX    X   X   XXXX 
//	X  X    X       X   X   X   X   X   X     X     X   X   X   X   X   X
//	XXXX    XXX     XXXXX   XXXXX   X   X     X     X   X   X   X   XXXX 
//	X   X   X       X   X   X   X    X X      X     X   X   X   X   X   X
//	XXXX    XXXXX   X   X   X   X     X     XXXXX    XXX     XXX    X   X
// ################################################################################

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {

	// PRIVATE VARIABLES:
	private	CustomButtonAction		onMouseOver;
	private CustomButtonAction		onMouseClick;
	private CustomButtonAction		onMouseDoubleClick;
	private CustomButtonAction		onMouseDownClick;
	private CustomButtonAction		onMouseUpRelease;
	private CustomButtonAction		onMouseExit;

	private	bool					doubleClick				=	false;
	private float					doubleTimer				=	0.0f;
	private	float					doubleLimiter			=	1.0f;

	// PUBLIC VARIABLES:
	public	delegate void			CustomButtonAction( object[] args );

	// ######################################################################
	void Update () {
		if ( doubleClick ) {
			doubleTimer			+=	Time.deltaTime;
			if ( doubleTimer > doubleLimiter ) {
				doubleClick		=	false;
				doubleTimer		=	0.0f;
			}
		}
	}

	// ----------------------------------------------------------------------
	public void OnPointerEnter( PointerEventData eventData ) {
		if ( onMouseOver != null ) { onMouseOver( new object[] { gameObject, eventData } ); }
	}

	// ----------------------------------------------------------------------
	public void OnPointerClick( PointerEventData eventData ) {
		if ( doubleClick && onMouseDoubleClick != null ) {
			onMouseDoubleClick( new object[] { gameObject, eventData } );
		} else if ( onMouseClick != null ) {
			onMouseClick( new object[] { gameObject, eventData } );
		}

		doubleClick		=	!doubleClick;
	}

	// ----------------------------------------------------------------------
	public void OnPointerDown( PointerEventData eventData ) {
		if ( onMouseDownClick != null ) { onMouseDownClick( new object[] { gameObject, eventData } ); }
	}

	// ----------------------------------------------------------------------
	public void OnPointerUp( PointerEventData eventData ) {
		if ( onMouseUpRelease != null ) { onMouseUpRelease( new object[] { gameObject, eventData } ); }
	}

	// ----------------------------------------------------------------------
	public void OnPointerExit( PointerEventData eventData ) {
		if ( onMouseExit != null ) { onMouseExit( new object[] { gameObject, eventData } ); }
	}

	// ######################################################################
	public void setOnMouseOver( CustomButtonAction function ) {
		onMouseOver			=	function;
	}

	// ----------------------------------------------------------------------
	public void setOnMouseClick( CustomButtonAction function ) {
		onMouseClick		=	function;
	}

	// ----------------------------------------------------------------------
	public void setOnMouseDoubleClick( CustomButtonAction function ) {
		onMouseDoubleClick	=	function;
	}

	// ----------------------------------------------------------------------
	public void setOnMouseDown( CustomButtonAction function ) {
		onMouseDownClick	=	function;
	}

	// ----------------------------------------------------------------------
	public void setOnMouseUp( CustomButtonAction function ) {
		onMouseUpRelease	=	function;
	}

	// ----------------------------------------------------------------------
	public void setOnMouseExit( CustomButtonAction function ) {
		onMouseExit			=	function;
	}

	// ######################################################################

}

// ################################################################################