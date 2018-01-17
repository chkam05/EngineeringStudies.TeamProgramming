using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class CubeDetectCollision : MonoBehaviour {

	private	GameObject	object_collider		=	null;
	private bool		collision_active	=	false;
	private	float		timer				=	0.0f;
	private float		time_release		=	50.0f;

	// ######################################################################
	/// <summary>
	///	Funckja oczekująca czas przed zwróceniem informacji o kolizji.
	/// </summary>

	void Update() {
		if ( collision_active ) {
			timer	=	timer + Time.timeScale;
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Oczekwianie na kolizję
	/// </summary>
	/// <param name="col"> Objekt zderzający. </param>

	void OnCollisionEnter( Collision col ) {
		if ( col.gameObject != null ) {
			object_collider		=	col.gameObject;
			collision_active	=	true;
			timer				=	0.0f;
		}
	}

	// ######################################################################
	/// <summary>
	/// Ustawia czas oczekiwania na zwrócenie informacji.
	/// </summary>
	/// <param name="time"> Czas oczekiwania. </param>

	public void setTimeRelease( float time ) {
		this.time_release	=	time;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Po odczekanym czasie zwraca objekt zderzenia.
	/// </summary>
	/// <returns>The collision.</returns>

	public GameObject getCollision() {
		if ( collision_active && timer >= this.time_release ) {
			var	temp_obj			=	object_collider;
			this.collision_active	=	false;
			this.timer				=	0.0f;
			return temp_obj;
		} else {
			return null;
		}
	}

	// ######################################################################

}

// ################################################################################