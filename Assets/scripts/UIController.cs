using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text healthText;
    public Text ammoText;

    private (int,int) status = (100,30); // Exemplo de valor inicial de saúde

    void Update()
    {
        // pega os sinais de saúde e munição do player
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().GetStatus();

        // Atualiza o texto da saúde e da munição com os valores atuais
        healthText.text = "Health: " + status.Item1.ToString();
        ammoText.text = "Ammo: " + status.Item2.ToString();
    }
}
