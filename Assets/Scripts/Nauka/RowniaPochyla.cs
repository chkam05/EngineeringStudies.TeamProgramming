using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################

public class RowniaPochyla : MonoBehaviour {

	// PRIVATE VARIABLES
	private	string			str_teoria				=	"    Brak teorii. ";

	private string			str_wzory				=	" G - Ciężar \n"
													+	" m - masa \n"
													+	" g - przyspieszenie ziemskie = 9,81 [m/s^2] \n"
													+	" alpha - kąt nachylenia równi do podłoża \n"
													+	" n - siła reakcji podłoża \n"
													+	"\n"
													+	" G = m*g \n"
													+	" a = sin( alpha ) * g \n";

	private int				size_wzory				=	7 * 32;

	// PUBLIC VARIABLES
	public	GameObject		component_toolbar;
	public	GameObject		component_statusbar;
	public	GameObject		component_description;
	public	GameObject		component_messageQBox;
	public	GameObject		component_messageIBox;
	public	GameObject		component_settings;

	public	GameObject		fieldinput_M;
	public	GameObject		fieldinput_Alpha;
	public	GameObject		fieldinput_G;
	public	GameObject		fieldinput_a;

	void Start () {
		component_settings.GetComponent<SettingsBox>().Setup();
		component_description.GetComponent<DescriptionBox>().Init( "Wzory", str_wzory, size_wzory );
		component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );
		component_toolbar.GetComponent<ToolBarBox>().setInformations( "Wzory", str_wzory, size_wzory );
	}
	
	void Update () {
		//
	}

}

// ################################################################################