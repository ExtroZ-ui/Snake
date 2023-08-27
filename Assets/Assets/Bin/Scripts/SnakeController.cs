using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour
{

    // Настройки
    public float MoveSpeed = 5; // Скорость движения змеи
    public float SteerSpeed = 180; // Скорость поворота змеи
    public int BodyCount = 1; // Жир змейки
    public float BodySpeed = 5; // Скорость движения частей тела змеи
    public int Gap = 10; // Расстояние между частями тела змеи

    public Rigidbody SnakeHead;

    public AppleSpawner appleSpawnerController;

    // Ссылки
    public GameObject BodyPrefab; // Префаб для создания частей тела змеи

    // Списки
    private List<GameObject> BodyParts = new List<GameObject>(); // Список частей тела змеи
    private List<Vector3> PositionsHistory = new List<Vector3>(); // Список истории позиций змеи


    public Text textScore;
    private int score;
    public MenuController menuController;



    // Start вызывается перед первым кадром
    void Start()
    {

        for (int i = 0; i < BodyCount; i++)
        {
            GrowSnake();
        }
    }

    // Update вызывается каждый кадр
    void Update()
    {
        // Движение вперед
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // Поворот
        float steerDirection = Input.GetAxis("Horizontal"); // Возвращает значение -1, 0 или 1
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        // Сохранение истории позиций
        PositionsHistory.Insert(0, transform.position);

        // Движение частей тела
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Движение части тела в направлении точки по пути змеи
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;

            // Поворот части тела в направлении точки по пути змеи
            body.transform.LookAt(point);

            index++;
        }

    }

    private void GrowSnake()
    {
        GameObject LastBody = gameObject;

        if (BodyParts.Count > 0)
        {
            LastBody = BodyParts.Last().gameObject;
        }

        // Создание экземпляра части тела и
        // добавление его в список
        GameObject body = Instantiate(BodyPrefab, LastBody.transform.position, LastBody.transform.rotation);
        BodyParts.Add(body);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Apple>()) // Проверка на подбор яблок
        {
            GrowSnake();
            Destroy(other.gameObject);
            appleSpawnerController.countGrayApple++;
            appleSpawnerController.SpawnerApples();

            score++;
            textScore.text = score.ToString();
        }
        else if (other.GetComponent<AppleBonus>())
        {
            Destroy(other.gameObject);
            appleSpawnerController.UI_timer.SetActive(false);
            score += 10;
            textScore.text = score.ToString();
        }

        if (other.GetComponent<BodySnakeController>() && BodyParts.Count >= 4)
        {
            appleSpawnerController.countGrayApple = 0;
            RestartScene();

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<Plan>())
        {
            SnakeHead.isKinematic = false;
            appleSpawnerController.countGrayApple = 0;
            RestartScene();
        }
    }

    private void RestartScene()
    {
        PlayerPrefs.SetInt("Money", MenuController.money += score);
        score = 0;
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
