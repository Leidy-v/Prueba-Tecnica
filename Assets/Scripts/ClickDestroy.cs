using UnityEngine;

public class ClickDestroy : MonoBehaviour
{
    public SpawnManager spawnManager;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.CompareTag("Enemy"))
                {

                    hit.collider.gameObject.SetActive(false);

                    ClickDestroy clickedScript = hit.collider.GetComponent<ClickDestroy>();
                    if (clickedScript != null && clickedScript.spawnManager != null)
                    {
                        clickedScript.spawnManager.UpdateEnemyCounter();
                    }
                }
            }
        }
    }
} 
