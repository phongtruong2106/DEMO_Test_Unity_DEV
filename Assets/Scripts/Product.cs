using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject goldSystem;
    public int id;
    public string productName;
    public int price;
    public Text nameText, priceText;
    
    public static bool placeSeeds;
    public static int whichSeed;

   
    private void Start() {
        shop = GameObject.Find("Shop");

        goldSystem = GameObject.Find("Gold System");
    }

    private void Update() {
        nameText.text = "" + productName;
        priceText.text = price + " $";

        productName = shop.GetComponent<Shop>().productName[id];
        price = shop.GetComponent<Shop>().price[id];
    }

    public void Buy()
    {
        if(goldSystem.GetComponent<GoldSystem>().gold >= price)
        {
            placeSeeds = true;
            whichSeed = id;
            goldSystem.GetComponent<GoldSystem>().gold -= price;
        }
    }

}
