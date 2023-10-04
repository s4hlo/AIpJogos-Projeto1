using System.Collections.Generic;
using UnityEngine;

public class AreaFSM : MonoBehaviour
{
    [SerializeField] private Renderer floorRenderer;
    public Color normalColor = Color.white;
    public Color highlightColor = new Color(1f, 0.5f, 0f);
    public GameObject myPrefab;

    public List<GameObject> enemiesToLaunch = new List<GameObject>();

    private enum State
    {
        WaitingForPlayer,
        PlayerIsClose,
        Reseting,
    }

    private State currentState = State.WaitingForPlayer; //TODO use this

    private void Start()
    {
        SetState(2);
    }

    public void Update()
    {
        Debug.Log(gameObject.name + " " + currentState);
    }

    public void SetState(int state)
    {
        switch (state)
        {
            case 0:
                currentState = State.WaitingForPlayer;
                SetFloorColor(normalColor);
                break;
            case 1:
                currentState = State.PlayerIsClose;
                SetFloorColor(highlightColor);
                ReleaseEnemys();
                break;
            case 2:
                currentState = State.Reseting;
                RespawnEnemys();
                // RespawnCollectables();
                SetState(0);
                break;
        }
    }

    private void ReleaseEnemys()
    {
        foreach (GameObject enemy in enemiesToLaunch)
        {
            if (enemy == null) {
                continue;
            } 
            enemy.SetActive(true);
        }

        enemiesToLaunch.Clear();
    }

    private void RespawnEnemys()
    {
        Vector3 position = transform.position;
        GameObject enemy;
        for (int i = 0; i < UnityEngine.Random.Range(3, 5); i++)
        {
            enemy = Instantiate(myPrefab, position + UtilsRandomPosition(), Quaternion.identity);
            enemy.SetActive(false);
            enemy.GetComponent<EnemyMoviment>().player = GameObject.Find("Player").transform;
            enemiesToLaunch.Add(enemy);
        }
    }

    private Vector3 UtilsRandomPosition()
    {
        Vector3 Unit = new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5));
        return Unit;
    }

    private void SetFloorColor(Color color)
    {
        floorRenderer.material.color = color;
    }

}
