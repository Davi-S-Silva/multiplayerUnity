using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UiItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("contato: "+ pointerEventData.ToString());
        Destroy(transform.gameObject);
    }
}
