using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class framDevice : MonoBehaviour
{
    [SerializeField] private GameObject devicefram;
    [SerializeField] private Text textproductivity;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject goldSystem;

    public int numberOfFruitsUpgradeCount = 0;
    private const int MaxUpgradeCount = 5;
    public float UpgradePercentage = 0.1f;
    public int price = 500;

     private void Start()
    {
        upgradeButton.onClick.AddListener(Upgrade);
        goldSystem = GameObject.Find("Gold System");
    }

    private void Update()
    {
        // Hiển thị thông tin sau nâng cấp
        int upgradePercentageInt = Mathf.RoundToInt(UpgradePercentage * numberOfFruitsUpgradeCount * 100f);
        textproductivity.text = "productivity: " + upgradePercentageInt + "%";
    }

    public void Upgrade()
    {
        if (numberOfFruitsUpgradeCount < MaxUpgradeCount)
        {

            if (goldSystem.GetComponent<GoldSystem>().gold >= price)
            {
                numberOfFruitsUpgradeCount++;
                goldSystem.GetComponent<GoldSystem>().gold -= price; // Deduct 500 gold
                // Nếu đã đạt tối đa nâng cấp, vô hiệu hóa nút Upgrade
                if (numberOfFruitsUpgradeCount == MaxUpgradeCount)
                {
                    upgradeButton.interactable = false;
                }
                Debug.Log("up +1");

                
                // Lưu số lần nâng cấp vào PlayerPrefs
                PlayerPrefs.SetInt("DeviceUpgradeCount", numberOfFruitsUpgradeCount);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Not enough gold to upgrade!");
            }
        }
    }

    public void OpenDevice()
    {
        devicefram.SetActive(true);
    }

    public void CloseDevice()
    {
        devicefram.SetActive(false);
    }
}
