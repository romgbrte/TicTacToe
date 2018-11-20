using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour 
{
	#region Variables
	// Variables for screen and button size
	private float screenHeight,
				  screenWidth,
				  buttonHeight,
				  buttonWidth;

	// Variables for scores screen
	private Vector2 scrollViewVector = Vector2.zero;
	private string 	scoresHeading	 = "Player Name\r\nWins/Losses/Draws\r\n\r\n";
	private float 	vScrollbarValue;

	// Variables that indicates different screens
	static bool MenuScreen 		= true,
				StartScreen 	= false,
				ScoresScreen 	= false,
				AboutScreen 	= false,
				ReturningP1		= false,
				ReturningP2		= false,
				NewP1Screen		= false,
				NewP2Screen		= false,
				AIScreen		= false,
				IsThereAI 		= false,
				DoesP1GoFirst 	= false,
				DoesAIGoFirst 	= false,
				EnterName		= false,
				IsPlayer1		= false,
				IsPlayer2		= false;

	static string[] returningPlayers;
	static string	stringToEdit,
					tempName,
				  	player1Name,
				  	player2Name,
				  	playerHistory;

	static int	selGridInt = -1,
				AIDifficultyLevel = 0; // 0 easy, 1 normal, 2 hard

	static GUIStyle scoreLabelText = new GUIStyle();
	#endregion
				

	// Sets screen and button size
	void Start () 
	{
		screenHeight 	= Screen.height;
		screenWidth 	= Screen.width;
		buttonHeight 	= screenHeight * 0.1f;
		buttonWidth 	= screenWidth * 0.4f;
		InitializeStrings();
		InitializePlayerHistory();
		scoreLabelText.alignment = TextAnchor.UpperCenter;
	}
		
	// Utilizes Unity GUI class
	void OnGUI()
	{
		if (MenuScreen) 
		{
			mainMenu ();
		}
		else if (ReturningP1) 
		{
			startMenu ();
			returningPlayer1Menu ();
		}
		else if (ReturningP2) 
		{
			startMenu ();
			returningPlayer2Menu ();
		}
		else if (AIScreen) {
			startMenu();
			AIMenu();
		}
		else if (StartScreen) 
		{
			startMenu ();
		}
		else if (ScoresScreen) 
		{
			scoresMenu ();
		}
		else if (AboutScreen) 
		{
			aboutMenu ();
		}
		else if (NewP1Screen) 
		{
			startMenu ();
			newPlayer1Menu ();
		}
		else if (NewP2Screen) 
		{
			startMenu ();
			newPlayer2Menu ();
		}


	}

	// Displays main menu screen
	void mainMenu()
	{
		// Button that directs to "Start" screen
		if (GUI.Button (new Rect ((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.4f, buttonWidth, buttonHeight), "Start")) 
		{
			MenuScreen		= false;
			StartScreen		= true;
			ScoresScreen	= false;
			AboutScreen		= false;
			Application.LoadLevel ("Start");
		}

		// Button that directs to "Scores" screen
		if (GUI.Button (new Rect ((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.5f, buttonWidth, buttonHeight), "Scores")) 
		{
			MenuScreen		= false;
			StartScreen		= false;
			ScoresScreen	= true;
			AboutScreen		= false;
			Application.LoadLevel ("Scores");
		}

		// Button that directs to "About" screen
		if (GUI.Button (new Rect ((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.6f, buttonWidth, buttonHeight), "About")) 
		{
			MenuScreen		= false;
			StartScreen		= false;
			ScoresScreen	= false;
			AboutScreen		= true;
			Application.LoadLevel ("About");
		}

		// Button that quits game
		if (GUI.Button (new Rect ((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.7f, buttonWidth, buttonHeight), "Quit")) 
		{
			Application.Quit ();
		}
	}

	void startMenu()
	{
		//Player 1 options box
		GUI.Box (new Rect (screenWidth * 0.1f, screenHeight * 0.05f, screenWidth * 0.25f, screenHeight * 0.5f), "Player1");

		//Player 1 has the ability to create a username
		if (GUI.Button (new Rect (screenWidth * 0.11f, screenHeight * 0.1f, buttonWidth * 0.58f, buttonHeight * 0.5f), "New User")) 
		{
			MenuScreen		= false;
			StartScreen		= false;
			ScoresScreen	= false;
			AboutScreen		= false;
			NewP1Screen		= true;
			newPlayer1Menu();
			//player1Name 	= tempName;
		}
		
		// Player 1 has the ability to choose from a list of saved names
		if (GUI.Button (new Rect (screenWidth * 0.11f, screenHeight * 0.15f, buttonWidth * 0.58f, buttonHeight * 0.5f), "Returning User")) 
		{
			ReturningP1 = true;
			//ResetAIFlags();
		}

		//Player 1 has the ability to play as a guest
		if (GUI.Button (new Rect (screenWidth * 0.11f, screenHeight * 0.2f, buttonWidth * 0.58f, buttonHeight * 0.5f), "Guest")) 
		{
			player1Name = "Guest";
			IsPlayer1	= true;
			ResetAIFlags();
		}

		//Player 1 will be able to choose an AI as a player/opponent
		/*if (GUI.Button (new Rect (screenWidth * 0.11f, screenHeight * 0.25f, buttonWidth * 0.58f, buttonHeight * 0.5f), "AI")) 
		{
			//IsThereAI = true;
		}*/

		//Ready flag
		if (IsPlayer1) 
		{
			GUI.Box (new Rect (screenWidth * 0.11f, screenHeight * 0.35f, buttonWidth * 0.58f, buttonHeight * 0.6f), player1Name + " Ready!!");
		}
		
		//Player 2 options text box
		GUI.Box (new Rect (screenWidth * 0.65f, screenHeight * 0.05f, screenWidth * 0.25f, screenHeight * 0.5f), "Player2");
		
		//Player 2 has the ability to create a username		
		if (GUI.Button (new Rect (screenWidth * 0.66f, screenHeight * 0.1f, buttonWidth * 0.58f, buttonHeight * 0.5f), "New User")) 
		{
			MenuScreen		= false;
			StartScreen		= false;
			ScoresScreen	= false;
			AboutScreen		= false;
			NewP2Screen		= true;
			newPlayer2Menu();
			//player2Name 	= tempName;
		}
		
		//Player 2 has the ability to choose from a list of saved names
		if (GUI.Button (new Rect (screenWidth * 0.66f, screenHeight * 0.15f, buttonWidth * 0.58f, buttonHeight * 0.5f), "Returning User")) 
		{
			ReturningP2 = true;
		}

		//Player 2 has the ability to play as a guest
		if (GUI.Button (new Rect (screenWidth * 0.66f, screenHeight * 0.2f, buttonWidth * 0.58f, buttonHeight * 0.5f), "Guest"))
		{
			player2Name = "Guest";
			IsPlayer2	= true;
			ResetAIFlags();
		}

		//Player 2 will be chosen as an AI oppenent
		if (GUI.Button (new Rect (screenWidth * 0.66f, screenHeight * 0.25f, buttonWidth * 0.58f, buttonHeight * 0.5f), "A.I.")) 
		{
			IsThereAI = true;
			AIScreen = true;
			IsPlayer2 = true;
		}

		//Ready flag 2
		if (IsPlayer2) 
		{
			GUI.Box (new Rect (screenWidth * 0.66f, screenHeight * 0.35f, buttonWidth * 0.58f, buttonHeight * 0.6f), player2Name + " Ready!!");
		}
		
		// Menu button box
		GUI.Box (new Rect (screenWidth * 0.1f, screenHeight * 0.6f, screenWidth * 0.25f, screenHeight * 0.25f), "");
	
		//Player can return to the main menu
		if (GUI.Button (new Rect (screenWidth * 0.11f, screenHeight * 0.62f, buttonWidth * 0.58f, buttonHeight * 2.0f), "Main Menu")) 
		{
			StartScreen		= false;
			ScoresScreen	= false;
			ResetAIFlags();
			AboutScreen		= false;
			NewP1Screen		= false;
			NewP2Screen		= false;
			MenuScreen		= true;
			//ResetAIFlags();
			Application.LoadLevel ("Menu");
		}

		//Start button box
		GUI.Box (new Rect (screenWidth * 0.65f, screenHeight * 0.6f, screenWidth * 0.25f, screenHeight * 0.25f), "");

		//Player will be directed to the game screen
		if(IsPlayer1 && IsPlayer2 && (DoesP1GoFirst || DoesAIGoFirst)) {
			if (GUI.Button (new Rect (screenWidth * 0.66f, screenHeight * 0.63f, buttonWidth * 0.58f, buttonHeight * 0.7f), "Play!")) 
			{
				Debug.Log(player1Name + ", " + player2Name + ", " + IsThereAI + ", " + DoesAIGoFirst + ", " + AIDifficultyLevel);
				//Creates the Player class and initializes it based on choices made on the main menu
				new Player(player1Name, player2Name, IsThereAI, DoesAIGoFirst, AIDifficultyLevel);
				ResetMatchToMatchFlags();
				/*StartScreen		= false;
				ScoresScreen	= false;
				AboutScreen		= false;
				NewP1Screen		= false;
				NewP2Screen		= false;
				MenuScreen		= false;*/
				Application.LoadLevel ("Gameplay");
				IsPlayer1		= false;
				IsPlayer2		= false;
			}
		}

		//Player can quit the game
		if (GUI.Button (new Rect (screenWidth * 0.66f, screenHeight * 0.73f, buttonWidth * 0.58f, buttonHeight * 0.7f), "Quit")) {
			Application.Quit ();
		}

		//Radio Buttons for which player goes first
		GUI.Box (new Rect(screenWidth * 0.4f, screenHeight * 0.65f, screenWidth * 0.2f, screenHeight * 0.2f), "Who goes first?");
		/*Only one of these options can be selected at a time, and
		 * selecting the unselected one deselects the currently selected one*/
		DoesP1GoFirst = GUI.Toggle (new Rect(screenWidth * 0.43f, screenHeight * 0.7f, 100, 30), DoesP1GoFirst, "Player 1");
		if (DoesP1GoFirst == true) {
			DoesAIGoFirst = false;
		}
		if(IsThereAI) {
			DoesAIGoFirst = GUI.Toggle (new Rect (screenWidth * 0.43f, screenHeight * 0.75f, 100, 30), DoesAIGoFirst, "AI");
			if (DoesAIGoFirst == true) {
				DoesP1GoFirst = false;
			}	
		}
	}

	// Enable player to create a username
	void newPlayer1Menu()
	{
		// Make a background box
		GUI.Box(new Rect(screenWidth * 0.375f, screenHeight * 0.05f, screenWidth * 0.25f, screenHeight * 0.5f), "New User");

		// Make standard text saying enter name
		GUI.Label (new Rect (screenWidth * 0.4f, screenHeight * 0.135f, buttonWidth * 0.65f, buttonHeight * 0.5f), "P1 Name (no commas):");

		EnterName = GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.3f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Enter");
		//Create text inputbox.
		stringToEdit = GUI.TextField(new Rect(screenWidth * 0.4f, screenHeight * 0.2f, buttonWidth * 0.5f, buttonHeight * 0.4f), stringToEdit,25);

		for(int i = 0; i < stringToEdit.Length; i++) {
			if(stringToEdit[i] == ',') {
				stringToEdit = ""; 
			}
		}

		//create Enter button to input text
		if (EnterName && (stringToEdit == player2Name || stringToEdit == "Master Shifu" || stringToEdit == "Cunning Clive" || stringToEdit == "Easy Bob" || stringToEdit == "Guest")) {
			stringToEdit = "Invalid name";
		}
		else if(EnterName && stringToEdit != player2Name && stringToEdit != "Invalid name" && stringToEdit != "" && stringToEdit != " ")
		{
			player1Name = stringToEdit;
			NewP1Screen	= false;
			StartScreen = true;
			stringToEdit = "";
			ResetAIFlags();
			IsPlayer1	= true;
			//Application.LoadLevel("Start");
		}
		
		// Return to previous screen
		if(GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.4f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Return")) 
		{
			NewP1Screen	= false;
			StartScreen = true;
			//Application.LoadLevel("Start");
		}
	}

	// Enable player to create a username
	void newPlayer2Menu()
	{
		// Make a background box
		GUI.Box(new Rect(screenWidth * 0.375f, screenHeight * 0.05f, screenWidth * 0.25f, screenHeight * 0.5f), "New User");
		
		// Make standard text saying enter name
		GUI.Label (new Rect (screenWidth * 0.4f, screenHeight * 0.135f, buttonWidth * 0.65f, buttonHeight * 0.5f), "P2 Name (no commas):");
		
		EnterName = GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.3f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Enter");
		//Create text inputbox.
		stringToEdit = GUI.TextField(new Rect(screenWidth * 0.4f, screenHeight * 0.2f, buttonWidth * 0.5f, buttonHeight * 0.4f), stringToEdit,25);

		for(int i = 0; i < stringToEdit.Length; i++) {
			if(stringToEdit[i] == ',') {
				stringToEdit = "";
			}
		}

		//create Enter button to input text
		if (EnterName && (stringToEdit == player2Name || stringToEdit == "Master Shifu" || stringToEdit == "Cunning Clive" || stringToEdit == "Easy Bob" || stringToEdit == "Guest")){
			stringToEdit = "Invalid name";
		}
		else if(EnterName && stringToEdit != player1Name && stringToEdit != "Invalid name" && stringToEdit != "" && stringToEdit != " ") 
		{
			player2Name = stringToEdit;
			NewP2Screen	= false;
			StartScreen = true;
			stringToEdit = "";
			ResetAIFlags();
			IsPlayer2	= true;
			//Application.LoadLevel("Start");
		}
		
		// Return to previous screen
		if(GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.4f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Return")) 
		{
			NewP2Screen	= false;
			StartScreen = true;
			//Application.LoadLevel("Start");
		}
	}

	void returningPlayer1Menu()
	{
		// Make a background box
		GUI.Box(new Rect(screenWidth * 0.375f, screenHeight * 0.05f, screenWidth * 0.25f, screenHeight * 0.5f), "Returning P1");

		scrollViewVector = GUI.BeginScrollView(new Rect(screenWidth * 0.4f, screenHeight * 0.1f, screenWidth * 0.21f, screenHeight * 0.28f), scrollViewVector, new Rect(0, 0, 200, 1500));

		// selGridInt will hold the array index of the name selected from the returning player menu
		if((selGridInt = GUI.SelectionGrid( new Rect(0, 0, screenWidth * 0.19f, screenHeight * 0.75f), selGridInt, returningPlayers, 1)) > 0) {
			if(returningPlayers[selGridInt] == "Master Shifu" || returningPlayers[selGridInt] == "Cunning Clive" ||
			   returningPlayers[selGridInt] == "Easy Bob" || returningPlayers[selGridInt] == player2Name) {
				selGridInt = -1;
			}
		}

		GUI.EndScrollView ();
		//create Enter button to input text
		if(selGridInt != -1) 
		{
			player1Name = returningPlayers[selGridInt];
			ReturningP1	= false;
			ReturningP2	= false;
			selGridInt = -1;
			ResetAIFlags();
			StartScreen = true;
			IsPlayer1	= true;
			//Application.LoadLevel("Start");
		}
		
		// Return to previous screen
		if(GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.4f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Return")) 
		{
			ReturningP1	= false;
			StartScreen = true;
			//Application.LoadLevel("Start");
		}
	}

	void returningPlayer2Menu()
	{
		// Make a background box
		GUI.Box(new Rect(screenWidth * 0.375f, screenHeight * 0.05f, screenWidth * 0.25f, screenHeight * 0.5f), "Returning P2");
		
		scrollViewVector = GUI.BeginScrollView(new Rect(screenWidth * 0.4f, screenHeight * 0.1f, screenWidth * 0.21f, screenHeight * 0.28f), scrollViewVector, new Rect(0, 0, 200, 1500));
		
		// selGridInt will hold the array index of the name selected from the returning player menu
		//selGridInt = GUI.SelectionGrid(new Rect(0, 0, screenWidth * 0.19f, screenHeight * 0.75f), selGridInt, returningPlayers, 1);

		if((selGridInt = GUI.SelectionGrid( new Rect(0, 0, screenWidth * 0.19f, screenHeight * 0.75f), selGridInt, returningPlayers, 1)) > 0) {
			if(returningPlayers[selGridInt] == "Master Shifu" || returningPlayers[selGridInt] == "Cunning Clive" ||
			   returningPlayers[selGridInt] == "Easy Bob" || returningPlayers[selGridInt] == player1Name) {
				selGridInt = -1;
			}
		}
		
		GUI.EndScrollView ();
		//create Enter button to input text
		if(selGridInt != -1) 
		{
			player2Name = returningPlayers[selGridInt];
			ReturningP1	= false;
			ReturningP2	= false;
			selGridInt = -1;
			ResetAIFlags();
			StartScreen = true;
			IsPlayer2	= true;
			//Application.LoadLevel("Start");
		}
		
		// Return to previous screen
		if(GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.4f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Return")) 
		{
			ReturningP2	= false;
			StartScreen = true;
			//Application.LoadLevel("Start");
		}
	}

	// Displays scores
	void scoresMenu()
	{
		scrollViewVector = GUI.BeginScrollView(new Rect(screenWidth * 0.3f, screenHeight * 0.25f, screenWidth * 0.6f, screenHeight * 0.6f), scrollViewVector, new Rect(0, 0, 400, 2000));
		/*Vector2 = GUI.BeginScrollView(new Rect(scrollbox locx, scrollbox locy, scrollbox width, scrollbox height),
		 * Vector2, 
		 * new Rect(text area locx, text area locy, text area width, text area height));*/

		playerHistory = GUI.TextArea (new Rect (-screenWidth * 0.05f, 0, screenWidth * 0.5f, screenHeight), playerHistory, scoreLabelText);

		GUI.EndScrollView ();
		//Debug.Log("after scroll view");

		if (GUI.Button (new Rect ((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.85f, buttonWidth, buttonHeight), "Main Menu")) 
		{
			ScoresScreen = false;
			MenuScreen = true;
			Application.LoadLevel ("Menu");
		}
	}

	// Displays team info
	void aboutMenu()
	{
		if (GUI.Button (new Rect ((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.85f, buttonWidth, buttonHeight), "Main Menu")) 
		{
			AboutScreen = false;
			MenuScreen = true;
			Application.LoadLevel ("Menu");
		}
	}


	//Displays AI options
	void AIMenu()
	{
		GUI.Box(new Rect(screenWidth * 0.375f, screenHeight * 0.05f, screenWidth * 0.25f, screenHeight * 0.25f), "AI Difficulty");

		if(GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.1f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Easy")) 
		{
			AIDifficultyLevel = 0;
			player2Name = "Easy Bob";
			//Debug.Log("From AIMenu - Diff lvl 0  &&  AIDifficultyLevel = " + AIDifficultyLevel);
			//PlayerPrefs.SetString ("currentPlayer2", "Easy Bob");
			//AIScreen = false;
			StartScreen = true;
			AIScreen = false;
			//Application.LoadLevel ("Start");
		}
		if(GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.15f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Normal")) 
		{
			AIDifficultyLevel = 1;
			player2Name = "Cunning Clive";
			//Debug.Log("From AIMenu - Diff lvl 1  &&  AIDifficultyLevel = " + AIDifficultyLevel);
			//PlayerPrefs.SetString ("currentPlayer2", "Cunning Clive");
			//AIScreen = false;
			StartScreen = true;
			AIScreen = false;
			//Application.LoadLevel ("Start");
		}
		if(GUI.Button(new Rect(screenWidth * 0.4f, screenHeight * 0.2f, buttonWidth * 0.5f, buttonHeight * 0.4f), "Hard")) 
		{
			AIDifficultyLevel = 2;
			player2Name = "Master Shifu";
			//Debug.Log("From AIMenu - Diff lvl 2  &&  AIDifficultyLevel = " + AIDifficultyLevel);
			//PlayerPrefs.SetString ("currentPlayer2", "Master Shifu");
			//AIScreen = false;
			StartScreen = true;
			AIScreen = false;
			//Application.LoadLevel ("Start");
		}
	}

	void InitializeStrings() {
		stringToEdit	= "";
		player1Name		= "";
		player2Name		= "";
		playerHistory	= "";
	}

	void PopulateReturningPlayers() {
		int cr = History.GetCurrentRecords();
		returningPlayers = new string[cr];
		for(int i = 0; i < cr; i++) {
			returningPlayers[i] = History.GetPlayerHistoryNames(i);
		}
	}

	void InitializePlayerHistory() {
		History.PopulatePlayerHistory();
		PopulateReturningPlayers();
		playerHistory = History.GetPlayerHistoryEntry();
		playerHistory = scoresHeading + playerHistory;
	}

	void ResetAIFlags() {
		if(IsThereAI) {
			IsThereAI = false;
		}
		if(DoesAIGoFirst) {
			DoesAIGoFirst = false;
		}
	}

	// Shouldn't need to call this, probably just for debugging
	void ResetMatchToMatchFlags() {
		MenuScreen 		= true;
		StartScreen 	= false;
		ScoresScreen 	= false;
		AboutScreen 	= false;
		NewP1Screen		= false;
		NewP2Screen		= false;
		AIScreen		= false;
		IsThereAI 		= false;
		DoesP1GoFirst 	= false;
		DoesAIGoFirst 	= false;
		stringToEdit	= "";
		player1Name		= "";
		player2Name		= "";
		playerHistory	= "";
		AIDifficultyLevel = 0;
	}
}
