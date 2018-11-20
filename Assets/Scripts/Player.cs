using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	#region Variables
	public GameObject 	X_Piece, // must be public to be assigned in the editor
						O_Piece; // must be public to be assigned in the editor
	static char 		p1Shape,
						p2Shape;
	static bool			AIflag,
						AITurnFirst,
						FirstAIturn,
						altTurns,
						gameWon;
	static int			AIlevel;
	static PlayerClass	P1,
						P2;
	static string 		player1Name,
						player2Name;
	static string[] 	AInames;
	public const int 	maxTurns 	= 25,
						numAInames 	= 3;
	public const float 	pieceHeightFromBoard = 0.1f;
	#endregion

	#region Functions
	public Player() {}

	// Constructer called from main menu just prior to loading Gameplay scene
	public Player (string p1name, string p2name, bool AI, bool AIfirst, int AIlvl) {
		player1Name = p1name;
		player2Name = p2name;
		AIflag 		= AI;		// True if AI opponent has been chosen
		AITurnFirst = AIfirst;	// True if AI has been chosen to go first
		AIlevel 	= AIlvl;	// 0 = easy, 1 = normal, 2 = hard
	}

	void Start () {
		AInames		= new string[numAInames] {"Easy Bob", "Cunning Clive", "Master Shifu"};
		gameWon		= false;
		p1Shape		= 'x';
		p2Shape		= 'o';
		altTurns	= true;		// True = P1, false = P2/AI
		CreateObjects();
	}

	// Update checks for state change every frame
	void Update () 
	{
		// Detects release of the left mouse button
		if(Input.GetMouseButtonUp(0)) 
		{
			if(altTurns) 
			{
				if(P1.GetMove())
					altTurns = false;
			}
			else if (!altTurns) 
			{
				if(P2.GetMove())
					altTurns = true;
			}
		}

		CheckWinConditions();

		/* This is how the AI takes its first move. It only occurs
		 * once per game, and only if the AI has been chosen to go first*/
		if(AITurnFirst) 
		{
			AITurnFirst = false;
			
			if(P2.GetMove())
				altTurns = true;
		}
	}

	void CreateObjects() 
	{
		P1 = new PlayerClass(X_Piece, p1Shape);
		
		if(AIflag) 
		{
			if(AIlevel == 0) {
				P2 = new AIClass(O_Piece, p2Shape);
			}
			else if (AIlevel == 1) {
				P2 = new AIMediumClass(O_Piece, p2Shape);
			}
			else if (AIlevel == 2) {
				P2 = new AIHardClass(O_Piece, p2Shape);
			}
		}
		else
			P2 = new PlayerClass(O_Piece, p2Shape);
	}

	public static bool IsAIFirst() {
		return AITurnFirst;
	}

	public static bool WhoseTurn() {
		return altTurns;
	}

	public static string GetPlayer1Name() {
		return player1Name;
	}

	public static string GetPlayer2Name() {
		if(AIflag) {
			return AInames[AIlevel]; }
		else {
			return player2Name; }
	}

	public static void CheckWinConditions() {
		if(gameWon || GameInfo.IsItADraw()) {
			if(GameInfo.IsItADraw()) {
				Debug.Log("No one wins!");
			}
			EndScreen.ExecuteHistorySystem();
			Application.LoadLevel("End"); // Load game over sceen
		}
	}

	public static void CheckWinState() 
	{
		// Checks both win state arrays for a win condition
		for(int i = 0; i < 28; i++) {
			if(BoardArray.GetWCSX(i) == 4) {
				gameWon = true;
				GameInfo.SetWinner(GetPlayer1Name());
				Debug.Log(player1Name + " Wins!");
				break;
			};
			if(BoardArray.GetWCSO(i) == 4) {
				gameWon = true;
				GameInfo.SetWinner(GetPlayer2Name());
				Debug.Log(player2Name + " Wins!");
				break;
			};
		};
	}
	#endregion
}