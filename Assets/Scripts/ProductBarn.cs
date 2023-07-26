using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductBarn : MonoBehaviour
{
    [SerializeField] private GameObject barn;
    [SerializeField] private PriceTable priceTable;
    public int id;
    public int countProduct;
    public Text productCount;
    public static bool placeSeeds;
    public static int whichSeed;
    public static bool isSowing;
    public int purchaseAmount = 1;

    private void Start()
    {
        barn = GameObject.Find("Barn");
        priceTable = GameObject.Find("Price Table").GetComponent<PriceTable>(); // Get the PriceTable component
    }

    private void Update()
    {
        productCount.text = countProduct.ToString();
        countProduct = barn.GetComponent<Barn>().countProduct[id];
    }

    public void Sell()
    {
        int sellingPrice = GetSellingPriceById(id); // Get the selling price from the PriceTable

        FindObjectOfType<GoldSystem>().gold += sellingPrice;

        // Reduce the product count in the barn
        barn.GetComponent<Barn>().countProduct[id] -= purchaseAmount;
    }

    public void SellAll()
    {
        int sellingPrice = GetSellingPriceById(id); // Get the selling price from the PriceTable

        FindObjectOfType<GoldSystem>().gold += (countProduct * sellingPrice); // Sell all products and receive the total amount of gold

        // Set the product count in the barn to 0 (sold all products)
        barn.GetComponent<Barn>().countProduct[id] = 0;
    }

    private int GetSellingPriceById(int id)
    {
        for (int i = 0; i < priceTable.id.Length; i++)
        {
            if (priceTable.id[i] == id)
            {
                return priceTable.sellingPrice[i];
            }
        }
        return 0; // If the id is not found in the PriceTable, return 0 or a default value as needed
    }
}
