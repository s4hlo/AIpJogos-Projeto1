using UnityEngine;
using System.Collections.Generic;

public class PlayerSignalSender : MonoBehaviour
{

    public float detectionRadius = 5.0f;

    private List<AreaFSM> areasChace = new List<AreaFSM>();

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        List<AreaFSM> activeAreas = new List<AreaFSM>();
        foreach (Collider collider in colliders)
        {

            if (collider.CompareTag("Area"))
            {
                AreaFSM area = collider.GetComponent<AreaFSM>();

                activeAreas.Add(area);

                area.SetState(1);

                if (areasChace.Contains(area) == false)
                {
                    areasChace.Add(area);
                }
            }
        }

        for (int i = areasChace.Count - 1; i >= 0; i--)
        {
            AreaFSM area = areasChace[i];
            if (!activeAreas.Contains(area))
            {
                area.SetState(2);
                areasChace.RemoveAt(i);
            }
        }

    }
}
