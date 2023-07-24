using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Animal : MonoBehaviour
{
   public int id;
   public float waits = 5f;
   [SerializeField] private GameObject crops;

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

               if (counter >= waitCount)
               {
                    GameObject newCrops = Instantiate(crops, transform.position, transform.rotation);
                    yield break;
               }
          }
   }
}
