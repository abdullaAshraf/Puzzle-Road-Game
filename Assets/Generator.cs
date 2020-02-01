using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<PieceInfo> shapes;
    public PieceInfo edgeIn, edgeOut;
    public float startX = 0, startY = 0;
    float x, y;
    float pieceDim = 1.2f;
    public int rowSize;
    int curRowSize = 0;
    public GameObject socketPrefab;
    List<GameObject> sockets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        x = startX;
        y = startY;
        for(int i=0; i<30; i++)
            GenerateNext();
    }

    void GenerateNext()
    {
        GameObject socket = Instantiate(socketPrefab, new Vector3(x, y, 0), Quaternion.identity);
        RandomisePiece(socket);
        if (RandomiseFilled())
            socket.GetComponent<Socket>().Fill();
        else
            socket.GetComponent<SpriteRenderer>().color = RandomiseColor();
        sockets.Add(socket);
        curRowSize++;
        if (curRowSize == rowSize)
        {
            x = startX;
            y += pieceDim;
            curRowSize = 0;
        }
        else
            x += pieceDim;
    }

    Color RandomiseColor()
    {
        float darkness = Random.Range(0.6f, 1f);
        return new Color(darkness, darkness, darkness);
    }

    bool RandomiseFilled()
    {
        return Random.Range(0f,1f) > 0.5f;
    }

    void RandomisePiece(GameObject socketObject)
    {
        int index = sockets.Count;
        List<PieceState> options = new List<PieceState>();
        for (int shape = 0; shape < shapes.Count; shape++)
        {
            for (int rotation = 0; rotation < 4; rotation++)
            {
                PieceState state = new PieceState(shapes[shape], rotation);
                bool valid = true;
                if (curRowSize > 0)
                {
                    if (!sockets[index - 1].GetComponent<Socket>().state.isMatch(state, 'R'))
                        valid = false;
                }
                if (index >= rowSize)
                {
                    if (!sockets[index - rowSize].GetComponent<Socket>().state.isMatch(state, 'T'))
                        valid = false;
                }
                if (valid)
                    options.Add(state);
            }
        }
        PieceState piece = options[Random.Range(0, options.Count)];
        if (curRowSize == 0)
        {
            GameObject socket = Instantiate(socketPrefab, new Vector3(x-pieceDim, y, 0), Quaternion.identity);
            socket.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            if (piece.edge(0) == -1)
                socket.GetComponent<Socket>().SetState(new PieceState(edgeOut,2));
            else
                socket.GetComponent<Socket>().SetState(new PieceState(edgeIn, 1));
        }
        if (curRowSize == rowSize -1)
        {
            GameObject socket = Instantiate(socketPrefab, new Vector3(x+pieceDim, y, 0), Quaternion.identity);
            socket.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            if (piece.edge(2) == -1)
                socket.GetComponent<Socket>().SetState(new PieceState(edgeOut, 0));
            else
                socket.GetComponent<Socket>().SetState(new PieceState(edgeIn, 3));
        }
        socketObject.GetComponent<Socket>().SetState(piece);
    }
}
