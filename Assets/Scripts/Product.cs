using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject goldSystem;
    private SeedStroge seedStroge;
    public int id;
    public string productName;
    public int price;
    public Text nameText, priceText;
    
    public static bool placeSeeds;
    public static int whichSeed;

    public static bool isSowing;

    public static int currentProductPrice;
    public int purchaseAmount = 10; 

   
    private void Start() {
        shop = GameObject.Find("Shop");
        goldSystem = GameObject.Find("Gold System");
        seedStroge = GameObject.Find("Seed Stroge").GetComponent<SeedStroge>(); // Lấy tham chiếu tới SeedStroge từ GameObject
    }

    private void Update() {
        nameText.text = "" + productName;
        priceText.text = price + " $ /10";

        productName = shop.GetComponent<Shop>().productName[id];
        price = shop.GetComponent<Shop>().price[id];
    }

    public void Buy()
    {
        int totalCost = price * purchaseAmount; // Tính tổng số vàng cần phải trả

        if (goldSystem.GetComponent<GoldSystem>().gold >= totalCost)
        {
             placeSeeds = true;
             whichSeed = id;
             currentProductPrice = price;

            //isSowing = true;

            // Truyền số lượng sản phẩm mua vào lớp SeedStroge thông qua id
                seedStroge.seedCount[id] += purchaseAmount;

                // Lặp lại việc mua sản phẩm purchaseAmount lần
                for (int i = 0; i < purchaseAmount; i++)
                {
                    goldSystem.GetComponent<GoldSystem>().gold -= price; // Trừ số vàng sau mỗi lần mua
                }

        }
    }
}
