using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
   public int id;
   
   public float waits = 5f;

   [SerializeField] private GameObject crops;


   private void Start() {
        id = Product.whichSeed;
        StartCoroutine(Wait());
   }

   private IEnumerator Wait()
   {
        yield return new WaitForSeconds(waits);

        GameObject newCrops = Instantiate(crops, transform.position, transform.rotation);
        newCrops.GetComponent<Crops>().id = id;
        Destroy(this.gameObject);
   }
}
