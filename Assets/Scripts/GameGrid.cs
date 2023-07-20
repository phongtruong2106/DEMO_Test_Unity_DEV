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
    [SerializeField] private GameObject seed;
    public bool gotGrid;
    public bool creatingFields;

    public Texture2D basicCursor, fieldCursor, seedCurror;

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
                if(Product.isSowing == true)
                {
                    if(_Hit.transform.tag == "field" && goldSystem.GetComponent<GoldSystem>().gold >= Product.currentProductPrice)
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(seed, hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);

                        goldSystem.GetComponent<GoldSystem>().gold -= Product.currentProductPrice;
                    }
                }
                if(creatingFields == false && Product.isSowing == false)
                {
                    if(_Hit.transform.tag == "crops")
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(field, hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);
                        print("get crops +1"); //Update in next e
                    }
                }
            }
        }

        //
        if(creatingFields == true)
        {
            Cursor.SetCursor(fieldCursor, hospot, cursorMode);
            Product.isSowing = false;
        }

        if(Shop.beInShop == true)
        {
            creatingFields = false;
            Cursor.SetCursor(basicCursor, hospot, cursorMode);
        }
        if(Product.isSowing == true)
        {
            creatingFields = false;

            Cursor.SetCursor(seedCurror, hospot, cursorMode);
        }
        if(Input.GetMouseButtonDown(1))
        {
            ClearCursor();
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

    public void ClearCursor()
    {
        creatingFields = false;
        Product.isSowing = false;

        Cursor.SetCursor(basicCursor, hospot, cursorMode);
    }
}
