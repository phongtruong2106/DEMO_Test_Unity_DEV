using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarnSeed : MonoBehaviour
{
    [SerializeField] private GameObject seedStroge;
    public int id;
    public int seedCount; // Số lượng cây trồng trong nhà kho

    public Text seeddCount; // Thêm đối tượng Text để hiển thị số lượng cây trồng

    private void Start()
    {
        seedStroge = GameObject.Find("Seed Stroge");
    }

    private void Update()
    {
        seedCount = seedStroge.GetComponent<SeedStroge>().seedCount[id]; // Cập nhật giá trị seedCount từ SeedStroge
        seeddCount.text = seedCount.ToString(); // Hiển thị số lượng cây trồng lên Text
    }

    public void Plant()
    {
        if (seedCount > 0)
        {
            // Trừ đi 1 cây trồng khi trồng (Plant)
            seedCount--;

            // Tạo cây trồng mới và thêm vào trò chơi
            // GameObject newPlant = Instantiate(see[id], transform.position, Quaternion.identity);

            // Cập nhật thông tin cho cây trồng mới (nếu cần)
            // Ví dụ: newPlant.GetComponent<Crops>().id = id;

            // Thêm code để tạo cây trồng mới vào trò chơi (nếu cần)

            // Cập nhật thông tin về số lượng cây trồng trong nhà kho (nếu cần)
        }
    }
}