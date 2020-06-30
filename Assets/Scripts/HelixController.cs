using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

//The helixController responsible for the helix rotation,and all the updates for the helix parts. It also laods a
//new level anytime it needed to.
public class HelixController : MonoBehaviour
{
  //public  Rigidbody r;
    SerialPort sp = new SerialPort("COM3", 9600);

    public const int PLATFORMS_PARTS = 12;
    private const int FIRST_LEVEL = 0;

    private Vector2 lastRotPos;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixPlatformPrefab;
    public GameObject goalPrefab;

    public List<Level> allLevels = new List<Level>();
    private float helixDistance;
    private List<GameObject> spawnedPlatforms= new List<GameObject>();
  
    private void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    //set all he helix values and load the first level
    void Awake()
    {
        
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y+0.1f);
        LoadLevel(FIRST_LEVEL);
    }


    //get the movement angle from the sensor and rotate the helix accordingly
    void Update()
    {

        if (sp.IsOpen)
        {
            try
            {
                moveObject(sp.ReadByte());
             
            }
            catch (System.Exception)
            {
            }
        }
    }

    void moveObject(int direction)
    {

        if (direction == 2)
        {
            transform.Rotate(Vector3.up * 13f);
        }
        if (direction == 1)
        {
            transform.Rotate(Vector3.up * -13f);
        }
    }

    //load a new level, get all the platforms prepared for the new helix, it also responsible for all the design.  
    public void LoadLevel(int levelNumber)
    {   //get a level from the list we built
        Level level = allLevels[levelNumber];

        if (level == null)
        {
            Debug.LogError("No Level" + levelNumber);
            return;
        }


        //change the background color
        Camera.main.backgroundColor = allLevels[levelNumber].levelBackgroundColor;

        //change the ball color
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allLevels[levelNumber].levelBallColor;



        //change the ball trail color

        FindObjectOfType<BallController>().GetComponent<TrailRenderer>().startColor = allLevels[levelNumber].levelBallColor;

        FindObjectOfType<BallController>().GetComponent<TrailRenderer>().endColor = allLevels[levelNumber].levelBallColor;

        //change the goal color

        foreach (Transform t in goalPrefab.GetComponentInChildren<Transform>())
        {
            t.GetComponent<Renderer>().material.color = allLevels[levelNumber].levelBallColor;
          
        }

        //change the spash color
        FindObjectOfType<BallController>().Splash_prefab.GetComponent<SpriteRenderer>().color = allLevels[levelNumber].levelBallColor;

        //change the helix color
        this.GetComponent<Renderer>().material.color = allLevels[levelNumber].levelHelixColor;


        //change text color
        FindObjectOfType<UIManager>().changeColor();

        //reset the helix rotation
        transform.localEulerAngles = startRotation;

        //destroy the old platforms if there were any
        foreach(GameObject go in spawnedPlatforms)
            Destroy(go);

        
      


        float platformDistance = helixDistance / level.platforms.Count;
        float spawnPosY = topTransform.localPosition.y;

        for( int i=0; i<level.platforms.Count; i++)
        {
            spawnPosY -= platformDistance;
            //create a new platform within scene
            GameObject platform = Instantiate(helixPlatformPrefab, transform);
            
            platform.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedPlatforms.Add(platform);

            //Creating the platform gaps
            int partsToDisabled = PLATFORMS_PARTS - level.platforms[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            
            while(disabledParts.Count<partsToDisabled)
            {
                //deactivate parts of the platform randomly
                GameObject randomPart = platform.transform.GetChild(Random.Range(0, platform.transform.childCount)).gameObject;
                if(!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            //Change left parts color

            List<GameObject> leftParts = new List<GameObject>();

            foreach(Transform t in platform.transform)
            {
                t.GetComponent<Renderer>().material.color = allLevels[levelNumber].levelPlatformColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }

            //creating the death parts
            List<GameObject> deathParts= new List<GameObject>();
            while (deathParts.Count < level.platforms[i].deathPartCount)
            {
                GameObject randomDeathPart = leftParts[Random.Range(0, leftParts.Count)];
                if(!deathParts.Contains(randomDeathPart))
                {
                    randomDeathPart.gameObject.AddComponent<DeathPart>();
                    randomDeathPart.gameObject.GetComponent<Renderer>().material.color = allLevels[levelNumber].deathPartColor;
                    deathParts.Add(randomDeathPart);
                }
            }


        }

        
    }
}
