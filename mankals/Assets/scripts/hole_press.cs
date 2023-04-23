using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hole_press : MonoBehaviour
{
    //where in the array
    public int x_val = 0;
    public int y_val = 0;
    public int size = 0;
    public manager manager;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void change_number()
    {
        this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "" + size;
        return;
    }
    private void OnMouseDown()
    {

        manager.pressed(x_val, y_val, size);
    }
}
