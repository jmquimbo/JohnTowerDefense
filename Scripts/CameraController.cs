using UnityEngine;

public class CameraController : MonoBehaviour
{
    //private bool doMovement = true;
    private float panSpeed = 50f;
    private float panBorderThickness = 5f;
    private float scrollSpeed = 5f;
    private float minY = 10f;
    private float maxY = 100f;
    private float minX = -100f;
    private float maxX = -60f;
    private float minZ = 70f;
    private float maxZ = 110f;
    // Update is called once per frame

    

    void Update()
    {

        if(GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        /*if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;*/
        
       

        if (Input.GetKey(KeyCode.UpArrow) ||  Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
           
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        //////
        //
        /*
        if (Input.GetKey("p"))
        {

            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if ( Input.GetKey("o") )
        {

            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        */
            ////
        /////
        ///

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        if (pos.y >= 10f && pos.y <= 15f) 
        {
            minZ = 55f;
            minX = -150f;
        }
      


        float horizontalInput = Input.GetAxis("Horizontal");

        pos.x += horizontalInput * 1000 * scrollSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}
