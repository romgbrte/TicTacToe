using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {

	#region Variables
	static float 	screenHeight,
					screenWidth,
					buttonWidth,
					buttonHeight,
					labelWidth,
					labelHeight;
	static GUIStyle winLabelText	= new GUIStyle();
	static string[] winMsgArr 		= new string[numMsgs] {" so best!", " is King of the Wooooorld!",
													" is better than the other guy!",
													" is liek O.K. i guess i mean wutevs...",
													".rar", " is Tic Tac Toe-tally AWESOME :D",
													": YOU DEFEATED"};
	static string	player1Name,
					player2name,
					winner;
	static bool draw;
	static string winMsg;
	const int numMsgs = 7;
	#endregion

	#region Functions
	void Start () {
		screenHeight 			= Screen.height;
		screenWidth 			= Screen.width;
		buttonWidth				= screenWidth/4;
		buttonHeight			= screenHeight/12;
		labelWidth				= screenWidth/2;
		labelHeight				= screenHeight/5;
		winLabelText.fontSize	= 36;
		winLabelText.normal.textColor = Color.yellow;
		GUI.backgroundColor		= Color.gray;
		winLabelText.alignment	= TextAnchor.UpperCenter;
		SetWinMsg();
		Debug.Log("Instantiated EndScreen");
	}
	//Label Template -->  GUI.Label (new Rect(loc x, loc y, size x, size y), "contents");

	void OnGUI () {

		GUI.Label (new Rect((screenWidth/2) - (labelWidth/2),
		                    (screenHeight/2) - (labelHeight/0.75f),
		                    labelWidth, labelHeight), GameInfo.GetWinner() + winMsg, winLabelText);

		if(GUI.Button (new Rect((screenWidth/2) - (buttonWidth/2), // Retry button
		                        (screenHeight/2) - (buttonHeight/1.25f),
		                        buttonWidth, buttonHeight), "Retry?")) {
			Application.LoadLevel("Gameplay");
		};

		if(GUI.Button (new Rect((screenWidth/2) - (buttonWidth/2), // Main menu button
		                        (screenHeight/2) + (buttonHeight/1.25f),
		                        buttonWidth, buttonHeight), "Main Menu")) {
			Application.LoadLevel("Menu");
		};
	}

	public static void ExecuteHistorySystem() {
		History.PopulatePlayerHistory();
		History.UpdatePlayerHistory(Player.GetPlayer1Name(), GameInfo.GetWinner());
		History.UpdatePlayerHistory(Player.GetPlayer2Name(), GameInfo.GetWinner());
		History.DisplayArray();
		History.WriteHistoryFile();
	}

	void SetWinMsg() {
		if(GameInfo.IsItADraw()) {
			winMsg = "Everybody loses!!! :(";
		}
		else {
			System.Random msgNum = new System.Random ();
			winMsg = winMsgArr[msgNum.Next(numMsgs)];
		}
	}
	#endregion
}
