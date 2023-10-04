using System.Collections.Generic;
using UnityEngine;

public class AreaFSM : MonoBehaviour
{
    [SerializeField] private Renderer floorRenderer;
    public Color normalColor = Color.white;
    public Color highlightColor = new Color(1f, 0.5f, 0f);
    public GameObject enemyPrefab;
    public GameObject healthBoxPrefab;
    public GameObject ammoBoxPrefab;
    public List<GameObject> collectables = new List<GameObject>();

    public List<GameObject> enemiesToLaunch = new List<GameObject>();

    private enum State
    {
        WaitingForPlayer,
        PlayerIsClose,
        Reseting,
    }

    private State currentState = State.WaitingForPlayer;

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
            //enable enemy rigidbody
            enemy.GetComponent<Rigidbody>().isKinematic = false;
            // enemy.SetActive(true);
        }

        enemiesToLaunch.Clear();
    }

    private void RespawnEntitys()
    {
        Vector3 position = transform.position;
        GameObject enemy;

        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            if (i % 2 == 0)
            {
                collectables.Add(Instantiate(healthBoxPrefab, position + UtilsRandomPosition() * 1.5f, Quaternion.identity));
                collectables.Add(Instantiate(ammoBoxPrefab, position + UtilsRandomPosition() * 1.5f, Quaternion.identity));
            }
            enemy = Instantiate(enemyPrefab, position + UtilsRandomPosition(), Quaternion.identity);
            //disable enemy rigidbody
            enemy.GetComponent<Rigidbody>().isKinematic = true;
            // enemy.SetActive(false);
            enemy.GetComponent<EnemyMoviment>().player = GameObject.Find("Player").transform;
            enemiesToLaunch.Add(enemy);
        }
    }

    private Vector3 UtilsRandomPosition()
    {
        Vector3 Unit = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        return Unit;
    }

    private void SetFloorColor(Color color)
    {
        floorRenderer.material.color = color;
    }

}
