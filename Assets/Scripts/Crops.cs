using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crops : MonoBehaviour
{
    public int id;
    public int cropType; // Thêm thuộc tính cropType để lưu thông tin loại cây

    public int numberOfFruits;
    private float elapsedTime;
    private const float HourInSeconds = 3600f;

    [SerializeField] private GameObject barn;
    [SerializeField] private GameObject fieldPrefab; // Prefab của đối tượng "field"
    [SerializeField] private GameObject deviceFram;

    private int numberUpgrade;
    private void Start()
    {
        barn = GameObject.Find("Barn");
        deviceFram = GameObject.Find("Fram Device");
        cropType = id;
    }
    private void Update()
    {
        // Bắt đầu tính thời gian khi đối tượng "crops" được bắt đầu
        elapsedTime += Time.deltaTime;

        // Nếu đã trôi qua 1 giờ, thực hiện thu hoạch và xóa đối tượng "crops"
        if (elapsedTime >= HourInSeconds)
        {
                Harvest();
                // Instantiate đối tượng "field" tại vị trí hiện tại của "crops"
                Instantiate(fieldPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
        }
        numberUpgrade = Mathf.RoundToInt(numberOfFruits * (1f + deviceFram.GetComponent<framDevice>().numberOfFruitsUpgradeCount));

    }

    public void Harvest()
    {
        barn.GetComponent<Barn>().countProduct[id] += numberUpgrade;
    }
    
}
