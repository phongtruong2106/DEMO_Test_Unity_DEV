using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  

public class Shop : MonoBehaviour
{
    public int[] id;
    public string[] productName;
    public int[] price;
    public int numberOfProducts;

    public int pageNumber;
    
    [SerializeField] private GameObject shopWindow;
    [SerializeField] private GameObject[] products;

    public static bool beInShop;

    private void Start() {
        for(int i = 0; i <= numberOfProducts; i++)
        {
            products[i].SetActive(false);
        }
        Refresh();
    }

    public void OpenShop()
    {
        shopWindow.SetActive(true);
        beInShop = true;
        Refresh();
    }
    public void CloseShop()
    {
        shopWindow.SetActive(false);
        beInShop = false;
    }

    private void Update() {
        if(Product.isSowing == true)
        {
            CloseShop();
        }
    }

    public void Refresh()
    {
        for(int i = 0; i < numberOfProducts; i++)
        {
            products[i].SetActive(false);
        }
        if(pageNumber == 1)
        {
            for(int i =0; i< numberOfProducts; i++)
            {
                products[i].GetComponent<Product>().id =id[i];
                products[i].SetActive(true);
            }
        }
    }

    public void left()
    {

    }

    public void right()
    {

    }
}
