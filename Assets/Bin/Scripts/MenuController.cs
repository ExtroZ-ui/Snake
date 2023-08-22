using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public SnakeController snake_script;
    public AppleSpawner apple_script;

    public Camera[] cameras;

    public GameObject MenuUI;
    public GameObject GameUI;

    [SerializeField]
    public Text TextMoney;
    static public int money;

    public void Start(){
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
            Debug.Log(money.ToString());
            TextMoney.text = money.ToString();

            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }

    public void MenuStart() // Запуск при кнопке Start 
    {
        snake_script.enabled = true;
        apple_script.enabled = true;
        cameras[0].enabled = false;
        cameras[1].enabled = true;
        MenuUI.SetActive(false);
        GameUI.SetActive(true);

    }
}
