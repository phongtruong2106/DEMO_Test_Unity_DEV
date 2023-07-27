using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Seed : MonoBehaviour
{
    public int id;
    public float waits = 5f;
    [SerializeField] private GameObject crops; 
    [SerializeField] private GameObject deviceFram;
    [SerializeField] private TMP_Text textDisplay;
    public int seedType; 
    public static int waitCount = 40;
    private int counter = 0;
    private float waitUpgrade;


     private void Start()
     {
          deviceFram = GameObject.Find("Fram Device");
          //id = Product.whichSeed;
          StartCoroutine(Wait());
          seedType = id;
          seedType = Animal.instance.animalType;
     }

    private IEnumerator Wait()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(waitUpgrade); // Chờ 10 phút
            waitUpgrade = Mathf.Max(0.1f, waits * (1f - deviceFram.GetComponent<framDevice>().numberOfFruitsUpgradeCount));
            counter++; // Tăng giá trị biến đếm lên 1
            textDisplay.text = counter.ToString(); // Hiển thị giá trị x / 40 lên Text

            if (counter >= waitCount)
            {
                GameObject newCrops = Instantiate(crops, transform.position, transform.rotation);
                Destroy(this.gameObject);
                yield break;
            }
        }
    }

     public void PlantSeed(int seedId)
    {
        // Thực hiện các xử lý cần thiết với id cây trồng được truyền vào
        // Ví dụ: tạo một cây trồng mới với id tương ứng, hoặc thay đổi các thuộc tính của cây trồng hiện tại dựa trên id
    }
}
