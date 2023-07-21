    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SeedStroge : MonoBehaviour
    {
        public int[] id;
        public int[] seedCount; // Tham chiếu đến lớp BarnSeed để quản lý số lượng cây trồng trong nhà kho
        public int numberOfSeed;

        public int pageNumber; // Prefab của trường trồng (nơi cây trồng được trồng)

        [SerializeField] private GameObject BarnSeedWindow;
        [SerializeField] private GameObject[] seed;

        public static bool beInBarnSeed;

        private void Start() {
            for(int i = 0; i <= numberOfSeed; i++)
            {
                seed[i].SetActive(false);
            }
            Refresh();

        }
        public void OpenBard()
        {
            BarnSeedWindow.SetActive(true);
            beInBarnSeed = true;
            Refresh();
        }
        public void CloseBard()
        {
            BarnSeedWindow.SetActive(false);
            beInBarnSeed = false;
        }

        private void Update() {
            if(Product.isSowing == true)
            {
                CloseBard();
            }
        }
         public void left()
        {

        }

        public void right()
        {

    }

    public void Refresh()
    {
        for(int i = 0; i < numberOfSeed; i++)
        {
            seed[i].SetActive(false);
        }
        if(pageNumber == 1)
        {
            for(int i =0; i< numberOfSeed; i++)
            {
                seed[i].GetComponent<BarnSeed>().id =id[i];
                seed[i].SetActive(true);
            }
        }
    }
       
    }
