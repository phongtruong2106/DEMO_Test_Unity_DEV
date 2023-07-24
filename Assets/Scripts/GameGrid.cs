using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<SerializableVector3> objectPositions;
    public List<SerializableQuaternion> objectRotations;
    public List<string> objectTags;

    // Phương thức khởi tạo
    public SaveData()
    {
        objectPositions = new List<SerializableVector3>();
        objectRotations = new List<SerializableQuaternion>();
        objectTags = new List<string>();
    }
}

[System.Serializable]
public struct SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public SerializableVector3(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public struct SerializableQuaternion
{
    public float x, y, z, w;

    public SerializableQuaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public SerializableQuaternion(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    public Quaternion ToQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
}

public class GameGrid : MonoBehaviour
{
    public int columnLength , rowlength;
    public int fiedsPrice;
    public float x_Space, z_Space;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject[] currentGrid;
    [SerializeField] private GameObject hitted;
    [SerializeField] private GameObject[] gridObjects;
    [SerializeField] private GameObject field;
    [SerializeField] private GameObject crops;
    [SerializeField] private GameObject seend;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject goldSystem;
    [SerializeField] private GameObject[] seed;
    [SerializeField] private GameObject seedStroge;

    [System.NonSerialized]
    public SaveData saveData;

    public bool gotGrid;
    public bool creatingFields;
    public bool harvern;
     public bool harvernmilk;
    private bool isPlanting;

    public Texture2D basicCursor, fieldCursor, seedCurror;

    public CursorMode cursorMode = CursorMode.Auto;

    public Vector2 hospot = Vector2.zero;

    private RaycastHit _Hit;
    private int fieldsCount = 0;
    

    private void Awake() {
        Cursor.SetCursor(basicCursor, hospot, cursorMode);
    }

    private void Start() {
        // Đọc trạng thái của creatingFields và harvern từ PlayerPrefs
        creatingFields = PlayerPrefs.GetInt("CreatingFields", 0) == 1;
        harvern = PlayerPrefs.GetInt("Harvern", 0) == 1;

        // // Khởi tạo các thông tin khác nếu cần
        // float gold = PlayerPrefs.GetFloat("Gold", 0f);
        // goldSystem.GetComponent<GoldSystem>().gold = gold;

        // Khởi tạo grid và lưu các đối tượng grid vào mảng gridObjects
        gridObjects = new GameObject[columnLength * rowlength];
        for (int i = 0; i < columnLength * rowlength; i++)
        {
            GameObject gridObj = Instantiate(grass, new Vector3(x_Space + (x_Space * (i % columnLength)), 0, z_Space + (z_Space * (i / columnLength))), Quaternion.identity);
            gridObjects[i] = gridObj;
        }
        // Khôi phục dữ liệu các đối tượng clone sau khi đã được lưu
        LoadObjectPositions();
    }

    private void Update() 
    {
        if(gotGrid == false)
        {
            currentGrid = GameObject.FindGameObjectsWithTag("Grid");
            gotGrid = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),  out _Hit))
            {
                if(creatingFields == true)
                {
                    if(_Hit.transform.tag == "Grid" && goldSystem.GetComponent<GoldSystem>().gold >= fiedsPrice)
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(field, hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);

                        goldSystem.GetComponent<GoldSystem>().gold -= fiedsPrice;
                    }
                }
               if (harvern == true)
                {
                    if (_Hit.transform.tag == "crops")
                    {
                        hitted = _Hit.transform.gameObject;
                        Crops cropsComponent = hitted.GetComponent<Crops>();
                        if (cropsComponent != null)
                        {
                            cropsComponent.Harvest();
                            Destroy(hitted);
                            Instantiate(field, hitted.transform.position, Quaternion.identity);
                            print("get crops +1"); //Update in next e
                            Debug.Log("a.lo");
                        }
                    }
                }
                if (harvernmilk == true)
                {
                    if (_Hit.transform.tag == "Animal")
                    {
                        hitted = _Hit.transform.gameObject;
                        Crops cropsComponent = hitted.GetComponent<Crops>();
                        if (cropsComponent != null)
                        {
                            cropsComponent.Harvest();
                            Destroy(hitted);
                            Debug.Log("a.lo");
                        }
                    }
                }
                if(Product.isSowing == true)
                {
                    if(_Hit.transform.tag == "field" && goldSystem.GetComponent<GoldSystem>().gold >= Product.currentProductPrice)
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(seed[Product.whichSeed], hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);

                        goldSystem.GetComponent<GoldSystem>().gold -= Product.currentProductPrice;
                    }
                }
               if (BarnSeed.isSowing == true)
                {
                    if (_Hit.transform.tag == "field" && seedStroge.GetComponent<SeedStroge>().seedCount[BarnSeed.whichSeed] > 0)
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(seed[BarnSeed.whichSeed], hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);
                        seedStroge.GetComponent<SeedStroge>().seedCount[BarnSeed.whichSeed] -= 1;
                    }
                    
                }
                if(BarnSeed.isfeed == true)
                {
                     // Kiểm tra xem có ít nhất hai ruộng để gieo hạt giống
                    if (_Hit.transform.tag == "Grid" && CountFields() >= 2)
                    {
                        // Gieo hạt giống vào hai ruộng kề nhau
                        hitted = _Hit.transform.gameObject;
                        Instantiate(seed[BarnSeed.whichSeed], hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);
                        seedStroge.GetComponent<SeedStroge>().seedCount[BarnSeed.whichSeed] -= 1;

                        // Đặt lại đếm số ruộng
                        ResetFields();
                    }
                }
                if(creatingFields == false && BarnSeed.isSowing == false&& harvern ==false && harvernmilk ==false)
                {
                    if(_Hit.transform.tag == "field")
                    {
                        hitted = _Hit.transform.gameObject;
                        Instantiate(plane, hitted.transform.position, Quaternion.identity);
                        Destroy(hitted);
                        print("get crops +1"); //Update in next e
                    }
                }
               
            }
        }

        //
        if(creatingFields == true)
        {
            Cursor.SetCursor(fieldCursor, hospot, cursorMode);
            Product.isSowing = false;
        }
        if(harvern == true)
        {
            Cursor.SetCursor(fieldCursor, hospot, cursorMode);
            Product.isSowing = false;
        }

        if(Shop.beInShop == true)
        {
            creatingFields = false;
            Cursor.SetCursor(basicCursor, hospot, cursorMode);
        }
        if(SeedStroge.beInBarnSeed == true)
        {
            creatingFields = false;
            Cursor.SetCursor(basicCursor, hospot, cursorMode);
        }
        if(Product.isSowing == true)
        {
            creatingFields = false;
            Cursor.SetCursor(seedCurror, hospot, cursorMode);
        }
        if(BarnSeed.isSowing == true)
        {
            creatingFields = false;
            Cursor.SetCursor(seedCurror, hospot, cursorMode);
        }
        if(BarnSeed.isfeed == true)
        {
            creatingFields = false;
            Cursor.SetCursor(seedCurror, hospot, cursorMode);
        }
        if(Input.GetMouseButtonDown(1))
        {
            ClearCursor();
        }
    }
    public void CreateFields()
    {
        creatingFields = true;
    }
    public void Harven()
    {
        harvern =true;
    }

    public void HarvenMilk()
    {
        harvernmilk =true;
    }

    public void returnToNormality()
    {
        creatingFields = false;
        harvern =false;
        harvernmilk = false; 
    }

    public void ClearCursor()
    {
        creatingFields = false;
        Product.isSowing = false;
        BarnSeed.isSowing = false;
        BarnSeed.isfeed = false;
        harvern =false;
        harvernmilk =false;

        Cursor.SetCursor(basicCursor, hospot, cursorMode);
      
    }
    // Phương thức hỗ trợ để đếm số lượng ruộng được chọn liên tiếp
    private int CountFields()
    {
        return ++fieldsCount;
    }

    // Phương thức hỗ trợ để đặt lại đếm số lượng ruộng về số không
    private void ResetFields()
    {
        fieldsCount = 0;
    }

    private void OnApplicationQuit()
    {
        // Lưu trạng thái của creatingFields và harvern vào PlayerPrefs
        PlayerPrefs.SetInt("CreatingFields", creatingFields ? 1 : 0);
        PlayerPrefs.SetInt("Harvern", harvern ? 1 : 0);

        // Lưu các thông tin khác nếu cần
        // PlayerPrefs.SetFloat("Gold", goldSystem.GetComponent<GoldSystem>().gold);
        // ...

        // Lưu dữ liệu các đối tượng
        SaveObjectPositions();
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    private void SaveObjectPositions()
    {
        // Tạo đối tượng SaveData
        saveData = new SaveData();

        // Lưu trữ thông tin vị trí và thông tin khác của các đối tượng
        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objects)
        {
            if (obj.CompareTag("field") || obj.CompareTag("crops") || obj.CompareTag("Seed"))
            {
                saveData.objectPositions.Add(new SerializableVector3(obj.transform.position));
                saveData.objectRotations.Add(new SerializableQuaternion(obj.transform.rotation));
                saveData.objectTags.Add(obj.tag);
                // Thêm các thông tin khác bạn muốn lưu trữ của các đối tượng
            }
        }

        // Lưu dữ liệu các đối tượng vào PlayerPrefs
        string jsonData = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("ObjectData", jsonData);
    }

    private void LoadObjectPositions()
    {
        // Đọc dữ liệu đối tượng từ PlayerPrefs và chuyển đổi từ JSON thành đối tượng SaveData
        string jsonData = PlayerPrefs.GetString("ObjectData", "");
        saveData = JsonUtility.FromJson<SaveData>(jsonData);

        // Debug: Check the count of saved data and tag data
        Debug.Log("Saved object count: " + saveData.objectPositions.Count);
        Debug.Log("Saved tag count: " + saveData.objectTags.Count);

        // Khôi phục thông tin vị trí và thông tin khác của các đối tượng
        for (int i = 0; i < saveData.objectPositions.Count; i++)
        {
            // Tạo một GameObject từ Prefab tương ứng với tag đã lưu trữ
            GameObject prefab = null;
            if (saveData.objectTags[i] == "field")
                prefab = field;
            else if (saveData.objectTags[i] == "crops")
                prefab = crops; // Thay "crops" bằng Prefab của đối tượng crops
            else if (saveData.objectTags[i] == "Seed")
                prefab = seend; // Thay "Seed" bằng Prefab của đối tượng Seed

            // Nếu có Prefab tương ứng, tạo đối tượng và gán thông tin vị trí và quay
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, saveData.objectPositions[i].ToVector3(), saveData.objectRotations[i].ToQuaternion());
                // Thêm các thông tin khác bạn muốn lưu trữ của các đối tượng vào đây nếu cần
            }
        }
    }

    // // Add the public method for deleting saved data
    public void DeleteSavedData()
    {
        // Clear the PlayerPrefs data related to saving and loading
        PlayerPrefs.DeleteKey("CreatingFields");
        PlayerPrefs.DeleteKey("Harvern");
        PlayerPrefs.DeleteKey("ObjectData"); // Delete the saved object data

        // Reset any other variables or data that need to be reset after data deletion
        // For example, you might want to destroy all existing crops, seeds, and animals in the scene
        

        // Display a message or perform any other actions to indicate that the data was deleted
        Debug.Log("Saved data deleted!");
    }
}