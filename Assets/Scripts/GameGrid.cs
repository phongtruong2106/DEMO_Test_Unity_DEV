using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int columnLength , rowlength;
    public int fiedsPrice;
    public float x_Space, z_Space;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject[] currentGrid;
    [SerializeField] private GameObject hitted;
    [SerializeField] private GameObject field;
    [SerializeField] private GameObject goldSystem;
    public bool gotGrid;
    public bool creatingFields;

    public Texture2D basicCursor, fieldCursor;

    public CursorMode cursorMode = CursorMode.Auto;

    public Vector2 hospot = Vector2.zero;

    private RaycastHit _Hit;


    private void Awake() {
        Cursor.SetCursor(basicCursor, hospot, cursorMode);
    }

    private void Start() {
        for(int i = 0 ; i< columnLength*rowlength; i++)
        {
            Instantiate(grass, new Vector3(x_Space + (x_Space * (i %columnLength)), 0 ,z_Space + (z_Space * (i/columnLength))), Quaternion.identity);
        }
    }

    private void Update() {
        if(gotGrid == false)
        {
            currentGrid = GameObject.FindGameObjectsWithTag("Grid");
            gotGrid = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),  out _Hit))
            {
                if(creatingFields == true)
                {
                    if(_Hit.transform.tag == "Grid" && goldSystem.GetComponent<GoldSystem>().gold >= fiedsPrice)
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(field, hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);

                        goldSystem.GetComponent<GoldSystem>().gold -= fiedsPrice;
                    }
                }
            }
        }

        //
        if(creatingFields == true)
        {
            Cursor.SetCursor(fieldCursor, hospot, cursorMode);
        }
    }

    public void CreateFields()
    {
        creatingFields = true;
    }

    public void returnToNormality()
    {
        creatingFields = false;
    }
}
