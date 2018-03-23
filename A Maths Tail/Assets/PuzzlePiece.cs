using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {
    public bool locked = false;
    public bool idle = true;
    int timesClicked = 0;
    public KeyCode pieceDown;
    public GameObject pieceSocket;
    public bool mouseClicked = false;
    public float yDiff;
    public float xPos;
    public float yPos;
    public Vector2 inventoryPosition;
    public float newYPosition;

	// Use this for initialization
	void Start () {
        xPos = transform.position.x;
        yPos = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        inventoryControl();
        if (!locked && !idle)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objectPosition;
            if (Input.GetKeyDown(pieceDown) && timesClicked > 1) {
                float socketX = pieceSocket.gameObject.transform.position.x;
                float pieceX = gameObject.transform.position.x;
                float diffX = Mathf.Abs(socketX - pieceX);
                float socketY = pieceSocket.gameObject.transform.position.y;
                float pieceY = gameObject.transform.position.y;
                float diffY = Mathf.Abs(socketY - pieceY);
                if (diffX < 1.89 && diffY < 1.895) {
                    transform.position = pieceSocket.gameObject.transform.position;
                    locked = true;
                    mouseClicked = false;
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    pieceSocket.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    BarnBuilding.piecesRemaining--;
                } else {
                    transform.position = new Vector2(xPos, yPos + yDiff);
                    mouseClicked = false;
                    idle = true;
                    timesClicked = 0;
                    BarnBuilding.wrongGuesses++;
                }
            }
        }
        
    }

    //If idle, change to moving, store current place on the screen
    void OnMouseDown()
    {
        idle = false;
        timesClicked++;
        inventoryPosition = transform.position;
    }

    //Change pieces position depending on how the mouse was moved
    void inventoryControl()
    {
        if (!locked)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && yDiff > -13)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 2.233333f);
                yDiff -= 2.233333f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && yDiff < 13)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 2.233333f);
                yDiff += 2.233333f;
            }
        }
    }

    //Move piece according to whether the up and down buttons are clicked
    public void MovePiece(bool moveUp)
    {
        if (!locked)
        {
            if (moveUp && yDiff > -13)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 2.233333f);
                yDiff -= 2.233333f;
            }
            else if (!moveUp && yDiff < 13)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 2.233333f);
                yDiff += 2.233333f;
            }
        }
    }
}
