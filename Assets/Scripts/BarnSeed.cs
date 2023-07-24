using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarnSeed : MonoBehaviour
{
    [SerializeField] private GameObject seedStroge;
    public int id;
    public static int seedCount; // Số lượng cây trồng trong nhà kho

    public Text seeddCount; // Thêm đối tượng Text để hiển thị số lượng cây trồng
    public static bool placeSeeds;
    public static int whichSeed;

    public static bool isSowing;
    public static bool isfeed;
    public static int currentSeedCount;
    private int purchaseAmount; 
    private static int selectedSeedId;
    private void Start()
    {
        seedStroge = GameObject.Find("Seed Stroge");
    }
    private void Update()
    {
        seedCount = seedStroge.GetComponent<SeedStroge>().seedCount[id]; // Cập nhật giá trị seedCount từ SeedStroge
        seeddCount.text = seedCount.ToString(); // Hiển thị số lượng cây trồng lên Text
        purchaseAmount = seedCount;
    }
    public void Plant()
    {    
        // Kiểm tra xem có đủ số lượng cây trồng để trồng không
        if (seedCount > 0)
        { 
            placeSeeds = true;
            isSowing = true;
            whichSeed = id;   
            // Trừ đi 1 cây trồng khi trồng (Plant) dựa vào id  
        }  
    }
     public void Feed()
    {    
        // Kiểm tra xem có đủ số lượng cây trồng để trồng không
        if (seedCount > 0)
        { 
            placeSeeds = true;
            isfeed = true;
            whichSeed = id;   
            // Trừ đi 1 cây trồng khi trồng (Plant) dựa vào id  
        }  
    }
}