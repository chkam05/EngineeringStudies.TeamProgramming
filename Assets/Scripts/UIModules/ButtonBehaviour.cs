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
	/// <summary>
	/// Funkcja sprawdzająca kliknięcia.
	/// </summary>

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
	/// <summary>
	/// Funkcja sprawdzająca czy kursor najechał na objekt UI.
	/// </summary>
	/// <param name="eventData">Event data.</param>

	public void OnPointerEnter( PointerEventData eventData ) {
		if ( onMouseOver != null ) { onMouseOver( new object[] { gameObject, eventData } ); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja sprawdzająca czy kursor kliknął na objekt UI.
	/// </summary>
	/// <param name="eventData">Event data.</param>

	public void OnPointerClick( PointerEventData eventData ) {
		if ( doubleClick && onMouseDoubleClick != null ) {
			onMouseDoubleClick( new object[] { gameObject, eventData } );
		} else if ( onMouseClick != null ) {
			onMouseClick( new object[] { gameObject, eventData } );
		}

		doubleClick		=	!doubleClick;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja sprawdzająca czy kursor nacisnął na objekt UI.
	/// </summary>
	/// <param name="eventData">Event data.</param>

	public void OnPointerDown( PointerEventData eventData ) {
		if ( onMouseDownClick != null ) { onMouseDownClick( new object[] { gameObject, eventData } ); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja sprawdzająca czy kursor puścił objekt UI.
	/// </summary>
	/// <param name="eventData">Event data.</param>

	public void OnPointerUp( PointerEventData eventData ) {
		if ( onMouseUpRelease != null ) { onMouseUpRelease( new object[] { gameObject, eventData } ); }
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja sprawdzająca czy kursor opuścił objekt UI.
	/// </summary>
	/// <param name="eventData">Event data.</param>

	public void OnPointerExit( PointerEventData eventData ) {
		if ( onMouseExit != null ) { onMouseExit( new object[] { gameObject, eventData } ); }
	}

	// ######################################################################
	/// <summary>
	/// Ustawienie funkcji która ma się wykonać po najechaniu kursorem na objekt UI.
	/// </summary>
	/// <param name="function">Function.</param>

	public void setOnMouseOver( CustomButtonAction function ) {
		onMouseOver			=	function;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ustawienie funkcji która ma się wykonać po kliknięciu kursorem na objekt UI.
	/// </summary>
	/// <param name="function">Function.</param>

	public void setOnMouseClick( CustomButtonAction function ) {
		onMouseClick		=	function;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ustawienie funkcji która ma się wykonać po podwójnym klinięciu kursorem na objekt UI.
	/// </summary>
	/// <param name="function">Function.</param>

	public void setOnMouseDoubleClick( CustomButtonAction function ) {
		onMouseDoubleClick	=	function;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ustawienie funkcji która ma się wykonać po naciśnięciu kursorem na objekt UI.
	/// </summary>
	/// <param name="function">Function.</param>

	public void setOnMouseDown( CustomButtonAction function ) {
		onMouseDownClick	=	function;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ustawienie funkcji która ma się wykonać po puszczeniu kursorea z objektu UI.
	/// </summary>
	/// <param name="function">Function.</param>

	public void setOnMouseUp( CustomButtonAction function ) {
		onMouseUpRelease	=	function;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Ustawienie funkcji która ma się wykonać po opuszczeniu kursora z objektu UI.
	/// </summary>
	/// <param name="function">Function.</param>

	public void setOnMouseExit( CustomButtonAction function ) {
		onMouseExit			=	function;
	}

	// ######################################################################

}

// ################################################################################