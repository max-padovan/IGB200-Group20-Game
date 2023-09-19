using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class watertank : MonoBehaviour
{
    //MeshRenderer mesh;
    //Color original = Color.white;
    //Color hoverCol = Color.gray;
    public GameObject waterUI;
    public camera cam;
    public water waterManager;

    public Slider slider;
    public Text value;
    public Text dailyGain;

public GameObject UIManager;

    public void setMaxStorage(int N)
    {
        slider.maxValue = N;    
    }
    public void setWaterStorage(int N)
    {
        slider.value = N;
        value.text = waterManager.currentWaterStorage + "/" + waterManager.maxWaterStorage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        setMaxStorage(waterManager.maxWaterStorage);
        setWaterStorage(waterManager.currentWaterStorage);
        //shedUI = GameObject.FindGameObjectWithTag("shedUI"); not gonna work if it starts toggled inactive, do it manually for now :)
        //mesh = GetComponent<MeshRenderer>();
        //original = mesh.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        //setWaterStorage(currentWaterStorage);
        UIManager.GetComponent<UIManager>().ESCOffWater();
    }

    void OnMouseOver()
    {
        //mesh.material.color = hoverCol; //later change to an outline or glow - didn't bother with placeholder
        UIManager.GetComponent<UIManager>().ClickOnWater();
    }
}