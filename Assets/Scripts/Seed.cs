using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Seed : MonoBehaviour
{
   public int id;
   public float waits = 5f;
   [SerializeField] private GameObject crops;
   [SerializeField] private TMP_Text textDisplay;

   public static int waitCount = 40;

   private int counter = 0;


   private void Start() {
        id = Product.whichSeed;
        StartCoroutine(Wait());
   }

   private IEnumerator Wait()
   {
          while (true)
          {
               yield return new WaitForSecondsRealtime(waits); // Chờ 10 phút
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
}
