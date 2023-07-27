using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{
    public int amountOfStorage;

    public int[] id;
    public int[] countProduct;

    public int numberOfProducts;
    public int pageNumber;

    [SerializeField] private GameObject barnWindow;
    [SerializeField] private GameObject[] products;
    [SerializeField] private PriceTable priceTable;
   

    private void Start() {
        for(int i = 0; i <= numberOfProducts; i++)
            {
                products[i].SetActive(false);
            }
            Refresh();
    }

    private void Update() {
       if(ProductBarn.isSowing == true)
        {
            Close();
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
                products[i].GetComponent<ProductBarn>().id = id[i];
                products[i].SetActive(true);
            }
        }
    }

    public int GetSellingPriceById(int id)
    {
        for (int i = 0; i < priceTable.id.Length; i++)
        {
            if (priceTable.id[i] == id)
            {
                return priceTable.sellingPrice[i];
            }
        }
        return 0; // Nếu không tìm thấy mã số, trả về 0 hoặc giá trị mặc định khác tùy theo yêu cầu
    }


    public void Close()
    {
        barnWindow.SetActive(false);
    }

    public void Open()
    {
        barnWindow.SetActive(true);
    }
    public void UpdateProductCount(int id, int newCount)
    {
        countProduct[id] = newCount;
    }
    public void SaveCountProduct()
    {
        for (int i = 0; i < numberOfProducts; i++)
        {
            PlayerPrefs.SetInt("Barn_CountProduct_" + i, countProduct[i]);
        }
    }

    public void LoadCountProduct()
    {
        for (int i = 0; i < numberOfProducts; i++)
        {
            countProduct[i] = PlayerPrefs.GetInt("Barn_CountProduct_" + i, 0);
        }
    }
    
}
