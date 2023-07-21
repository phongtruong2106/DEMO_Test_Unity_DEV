using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{
    [SerializeField] private GameObject priceTable;

    public int amountOfStorage;

    public int[] id;
    public string[] productName;
    public int[] count;
    public int[] sellingPrice;

    public int numberOfProducts;
    public GameObject shop;
    public GameObject barnWindow;


    private void Start() {
        numberOfProducts = 5;
    }

    private void Update() {
        for(int i = 0; i< amountOfStorage; i++)
        {
            sellingPrice[i] = priceTable.GetComponent<PriceTable>().sellingPrice[id[i]];
            productName[i] = shop.GetComponent<Shop>().productName[id[i]];
        }
    }

    public void Close()
    {
        barnWindow.SetActive(false);
    }

    public void Open()
    {
        barnWindow.SetActive(true);
    }
}
