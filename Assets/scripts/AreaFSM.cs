using System.Collections.Generic;
using UnityEngine;

public class AreaFSM : MonoBehaviour
{
    [SerializeField] private Renderer floorRenderer;
    public Color normalColor = Color.white;
    public Color highlightColor = new Color(1f, 0.5f, 0f);
    public GameObject enemyPrefab;
    public GameObject healthBoxPrefab;
    public List<GameObject> collectables = new List<GameObject>();

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
                RemoveCollectables();
                RespawnEntitys();
                SetState(0);
                break;
        }
    }

    private void RemoveCollectables()
    {
        foreach (GameObject collectable in collectables)
        {
            if (collectable == null)
            {
                continue;
            }
            Destroy(collectable);
        }

        collectables.Clear();
    }

    private void ReleaseEnemys()
    {
        foreach (GameObject enemy in enemiesToLaunch)
        {
            if (enemy == null)
            {
                continue;
            }
            enemy.SetActive(true);
        }

        enemiesToLaunch.Clear();
    }

    private void RespawnEntitys()
    {
        Vector3 position = transform.position;
        GameObject enemy;

        for (int i = 0; i < UnityEngine.Random.Range(3, 5); i++)
        {
            if (i % 2 == 0)
            {
                collectables.Add(Instantiate(healthBoxPrefab, position + UtilsRandomPosition() * 1.5f, Quaternion.identity));
            }
            enemy = Instantiate(enemyPrefab, position + UtilsRandomPosition(), Quaternion.identity);
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
