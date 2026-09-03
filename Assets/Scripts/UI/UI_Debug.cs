using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_Debug : MonoBehaviour
{
    
    public VisualTreeAsset uxml;
    public StyleSheet uss;
    VisualElement root;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        //root.Query("FPS").
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
