using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour, InteractableInterface
{
    private enum direction
    {
        left,
        right
    }
    [SerializeField] private direction triggerDirection;

    public GameObject rudder;
    private AudioSource audioSource;
    private bool spinCoolDown = false;
    private GameObject[] obstacles;
    [SerializeField] private GameObject obstacleParent;
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        if (!spinCoolDown) SpinWheel();
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = rudder.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void getObstacles(){
        obstacles = new GameObject[obstacleParent.transform.childCount];
        for (int i = 0; i < obstacleParent.transform.childCount; i++)
        {
            obstacles[i] = obstacleParent.transform.GetChild(i).gameObject;
        }
    }



    public void SpinWheel()
    {
        audioSource.Play();
        StartCoroutine(SpinWheelCoroutine());
    }

    private IEnumerator SpinWheelCoroutine()
    {
        float time = 0;
        while(time < 2)
        {
            moveObstacles();
            if(triggerDirection == direction.left)
            {
                rudder.transform.Rotate(Vector3.left, 2f);
            }
            else
            {
                rudder.transform.Rotate(Vector3.right, 2f);
            }
            time += Time.deltaTime;
            yield return null;
        }
        if (time >= 2) {
            spinCoolDown = false;
            audioSource.Stop();
        }
    }

    private void moveObstacles(){
        getObstacles(); 
        foreach (GameObject obstacle in obstacles)
        {
            if (triggerDirection == direction.left) obstacle.transform.Translate(new Vector3(0,-0.14f,0));
            else obstacle.transform.Translate(new Vector3(0,0.14f,0));
        }
    }
    

}
