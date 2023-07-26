using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Animal : MonoBehaviour
{
    public int id;
    public float waits = 5f;
    [SerializeField] private GameObject crops;
    [SerializeField] private GameObject deviceFram;
    public static int waitCount = 40;
    private int counter = 0;
    private Coroutine waitCoroutine;
    private float waitUpgrade;
    public int cropType; 

    private void Start()
    {
        deviceFram = GameObject.Find("Fram Device");
        //id = Product.whichSeed;
        waitCoroutine = StartCoroutine(Wait());
        cropType = id;
    }

    private IEnumerator Wait()
    {
        // Thực hiện biến đếm từ lúc bắt đầu
        while (true)
        {   
            yield return new WaitForSecondsRealtime(waitUpgrade); // Chờ 10 phút
            waitUpgrade = Mathf.Max(0.1f, waits * (1f - deviceFram.GetComponent<framDevice>().numberOfFruitsUpgradeCount));
            counter++; // Tăng giá trị biến đếm lên 1

            if (counter >= waitCount)
            {
               GameObject newCrops = Instantiate(crops, transform.position, transform.rotation);
               counter = 0;
               yield break;
               
            } 
        }
    }

    private void RestartWaitCoroutine()
    {
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
        }
        waitCoroutine = StartCoroutine(Wait());
    }
    public void FeedSeed(int seedId)
    {
        // Thực hiện các xử lý cần thiết với id cây trồng được truyền vào
        // Ví dụ: tạo một cây trồng mới với id tương ứng, hoặc thay đổi các thuộc tính của cây trồng hiện tại dựa trên id
    }
}
