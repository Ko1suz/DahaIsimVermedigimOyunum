using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class AutoMapGenerator : MonoBehaviour
{

    public int offsetX = 2;         // the offset so that we don't get any weird errors

    // these are used for checking if we need to instantiate stuff
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    public bool hasATopBuddy = false;
    public bool hasABotBuddy = false;
    public Transform playerTransform;
	public Rigidbody2D PlayerRb;

    float distance;
    private float spriteWidth = 0f;     // the width of our element
    private Camera cam;
    private Transform myTransform;


    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        HaritaOlusturucu();



    }

    private void HaritaOlusturucu()
    {
        // does it still need buddies? If not do nothing
        if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            // calculate the cameras extend (half the width) of what the camera can see in world coordinates
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;


            // calculate the x position where the camera can see the edge of the sprite (element)
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;


            // checking if we can see the edge of the element and then calling MakeNewBuddy if we can
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }

        if (hasATopBuddy == false || hasABotBuddy == false)
        {
            float camVerticalExtend = cam.orthographicSize * Screen.height / Screen.width;


            float edgeVisiblePositionTop = (myTransform.position.y + spriteWidth / 2) + camVerticalExtend;
            float edgeVisiblePositionBot = (myTransform.position.y - spriteWidth / 2) + camVerticalExtend;

            if (cam.transform.position.y >= edgeVisiblePositionTop - offsetX && hasATopBuddy == false)
            {
                MakeNewBuddy2(1);
                hasATopBuddy = true;
            }
            else if (cam.transform.position.y <= edgeVisiblePositionBot + offsetX && hasABotBuddy == false)
            {
                MakeNewBuddy2(-1);
                hasABotBuddy = true;
            }
        }
    }

    // a function that creates a buddy on the side required
    void MakeNewBuddy(int rightOrLeft)
    {
        // calculating the new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        // instantating our new body and storing him in a variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;
        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<AutoMapGenerator>().hasALeftBuddy = true;
        }
        else if (rightOrLeft < 0)
        {
            newBuddy.GetComponent<AutoMapGenerator>().hasARightBuddy = true;
        }
		
    }

    void DeleteNewBuddy(int rightOrLeft)
    {
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
    }

    void MakeNewBuddy2(int topOrBot)
    {
        // calculating the new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x, myTransform.position.y + spriteWidth * topOrBot, myTransform.position.z);
        // instantating our new body and storing him in a variable
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        newBuddy.parent = myTransform.parent;
        if (topOrBot < 0)
        {
            newBuddy.GetComponent<AutoMapGenerator>().hasATopBuddy = true;
        }
        else if (topOrBot > 0)
        {
            newBuddy.GetComponent<AutoMapGenerator>().hasABotBuddy = true;
        }
        
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    // 	if (other.gameObject.tag == "PlayerViewTrigger")
    // 	{
    // 		this.gameObject.SetActive(true);
    // 	}
    // }

    // private void OnTriggerExit2D(Collider2D other) {
    // 	if (other.gameObject.tag == "PlayerViewTrigger")
    // 	{
    // 		this.gameObject.SetActive(false);
    // 	}
    // }

}