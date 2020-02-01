using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public PieceInfo info;
    public PieceState state;
    public bool filled = false;


    public void SetState(PieceState state)
    {
        info = state.info;
        this.state = state;
        GetComponent<SpriteRenderer>().sprite = state.info.sprite;
        transform.Rotate(Vector3.forward * 90 * state.rotation);
    }

    public void Fill()
    {
        filled = true;
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 1f, 0.5f);
    }

    
}
