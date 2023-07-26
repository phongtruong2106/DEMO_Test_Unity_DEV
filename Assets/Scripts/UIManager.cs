using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ChangeSelectedobj(string _id)
    {
        GameGrid.instance.SelectedObject =_id;
    }
}
