using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public static class Tests {

	// PRIVATE VARIABLES:
	private	static	int		count_exercises		=	0;
	private	static	bool[]	state_exercises		=	null;

	// ######################################################################
	/// <summary>
	/// Funkcja ustwiająca liczbę zadań na test.
	/// </summary>
	/// <param name="count"> Ilość zadań. </param>

	public static void setExercises( int count ) {
		state_exercises	=	new bool[count];
		for ( int i = 0; i < count; i++ ) {
			state_exercises[i]	=	false;
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja pobiera ilość zadań.
	/// </summary>
	/// <returns> Ilość zadań. </returns>

	public static int getExercisesCount() {
		return count_exercises;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja ustawia zakończenie danego zadania.
	/// </summary>
	/// <param name="exercise"> Numer zadania. </param>
	/// <param name="complete"> True - zkończone, False - nie zakończone. </param>

	public static void setExercise( int exercise, bool complete ) {
		if ( exercise >= 0 && exercise < count_exercises ) {
			state_exercises[ exercise ] = complete;
		}
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Obliczenie i zapis uzyskanych punków
	/// </summary>
	/// <returns> Punktacja. </returns>
	/// <param name="indexCompetition"> Numer testu. </param>

	public static float setScore( int indexCompetition ) {
		int		playerID	=	PlayerPrefs.GetInt( "data_playerLastID", 0 );
		float	score		=	0.0f;

		for ( int i = 0; i < count_exercises; i++ ) {
			if ( state_exercises[i] ) { score = score + 1.0f; }
		}

		score	=	(score / count_exercises) * 100.0f;
		Tools.saveScore( playerID, indexCompetition, score );
		return score;
	}

	// ----------------------------------------------------------------------
	/// <summary>
	/// Funkcja czyszcząca punktację.
	/// </summary>

	public static void clearScore() {
		count_exercises		=	0;
		state_exercises		=	null;
	}

	// ######################################################################
	
}

// ################################################################################