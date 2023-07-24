using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crops : MonoBehaviour
{
    public int id;

    public int numberOfFruits;
    private float elapsedTime;
    private const float HourInSeconds = 1f;

    [SerializeField] private GameObject barn;


    private void Update() {
         // Bắt đầu tính thời gian khi đối tượng "crops" được bắt đầu
        elapsedTime += Time.deltaTime;
        
        // Nếu đã trôi qua 1 giờ, thực hiện thu hoạch và xóa đối tượng "crops"
        if (elapsedTime >= HourInSeconds)
        {
            Harvest();
            Destroy(gameObject);
        }
    }

    private void Start() {
        barn = GameObject.Find("Barn");
        
    }
    
    public void Harvest()
    {
        barn.GetComponent<Barn>().countProduct[id] += numberOfFruits;
    }

    
}
