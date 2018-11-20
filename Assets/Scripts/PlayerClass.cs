using UnityEngine;
using System.Collections;

public class PlayerClass : MonoBehaviour {

	#region Variables
	protected GameObject	gamePiece;
	protected GameObject	gameBoardSpace;
	protected Transform		myTransform;
	protected char			shape, opponentShape;
	protected char[,]		theBoard = new char[5,5];
	#endregion

	#region Funcions
	public PlayerClass() {}

	public PlayerClass(GameObject piece, char pShape) {
		gamePiece	= piece;
		shape		= pShape;
		if (shape == 'o')
			opponentShape = 'x';
		else if (shape == 'x')
			opponentShape = 'o';
	}

	// Update is called once per frame
	public virtual bool GetMove () {

		// Cast a ray towards the game board originating from mouse position during a given frame
		Ray			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit	hit;

		// If a GameObject is hit, get references to its location
		if(Physics.Raycast(ray, out hit, 10)) {
			myTransform		= hit.transform.gameObject.transform;
			gameBoardSpace	= hit.transform.gameObject;
		};

		// Stores location of the clicked game piece for use below 
		Vector3 boardLocation = new Vector3(myTransform.position.x,
		                                    myTransform.position.y + Player.pieceHeightFromBoard,
		                                    myTransform.position.z);

		// Check if object is occupied by a game piece. If vacant, place piece of that player
		if(gameBoardSpace.tag == "Unoccupied" && !GameInfo.IsItADraw()) {
			
			// Create player's game piece in game space
			Instantiate(gamePiece, boardLocation, Quaternion.identity);

			// Tag player-clicked space as occupied
			if(shape == 'x') {
				gameBoardSpace.tag = "x_Occupied";
			}
			else if(shape == 'o') {
				gameBoardSpace.tag = "o_Occupied";
			}

			// Get a reference to the GameObject that was clicked
			BoardSpace thisSpace = (BoardSpace)gameBoardSpace.GetComponent("BoardSpace");

			// Call the function of the clicked piece to update win state information
			thisSpace.UpdateSpaceState(shape);

			Player.CheckWinState();

			// Alternate player name UI colors to denote whose turn it is
			GameInfo.ChangeColor();

			// Increment turn counter
			GameInfo.IncrementTurnCount();

			return true;
		}
		
		return false;
	}
	#endregion
}