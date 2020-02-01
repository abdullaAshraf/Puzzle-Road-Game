using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public PieceInfo info;
    public PieceState state;

    Vector3 startPosition;
    Transform validSocket;

    // Start is called before the first frame update
    void Start()
    {
        state = new PieceState(info);
        GetComponent<SpriteRenderer>().sprite = state.info.sprite;
    }

    void Rotate()
    {
        state.rotation = (state.rotation + 1) % 4;
        transform.Rotate(Vector3.forward * 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Socket")
        {
            Socket socket = collision.transform.GetComponent<Socket>();
            if (!socket.filled && socket.state.isSame(state))
                validSocket = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == validSocket)
            validSocket = null;
    }

    private void OnMouseDown()
    {
        startPosition = transform.position;
    }

    void OnMouseUp()
    {
        double distance = Vector3.Distance(transform.position, startPosition);
        if (distance < 2)
        {
            Rotate();
        }
        else
        {
            if (validSocket != null)
            {
                //match found
                validSocket.GetComponent<Socket>().Fill();
            }
        }
        transform.position = startPosition;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }
}
