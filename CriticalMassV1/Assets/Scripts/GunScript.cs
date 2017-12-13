using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;


public class GunScript : MonoBehaviour
{
    public float range;
    public float Grow, Shrink;
    public float GrowTime, Shrinktime, MassSubMod;
    public float PSshrink, PSgrow, PSgrowtime, PSshrinktime;
    public float mass;
    public float StaffpointLOC;
    public GameObject gameManager;
    public GameObject CurrentItem, CurrentItemVisible, SpawnPoint, HeldItemSmall, HeldItemVisible;
    public Camera fpsCam;
    public Vector3 CurrentSize, PminSize, avgSize, maxSize;
    LayerMask ScaleLayer_mask = -8;
    LayerMask Button_mask = -9;
    Vector3 minSize;
    public Slider Size;
    public bool InVol, TooSmol, TooLorge, ControlLayers;
    public bool holdObjSmall, Lev;
    public GameObject Player;
    public Image Down, Up;
    public Sprite Pgrow, Pshrink, Ogrow, Oshrink;
    [SerializeField] Animator StaffANIM;
    public GameObject Staff;
    [SerializeField] bool TranGP, TranGO, TranSP, TranSO, IsGrowing, IsShrinking, IsGrowingP, IsShrinkingP;
    public UnityEvent UpSound, DownSound, ShowCube, PlaceCube, FreezeCube, PlayerUP, PlayerDown, Wipe;
    public bool EGrowing, EShrinking, EventRunning;
    public LineRenderer LineRend;
    GameObject HitObject;
    public GameObject StaffPoint, StaffVis;
    bool isShooting;
    Vector3 OBJLOC;
    void Start()
    {
       LineRend.enabled = false;
       minSize = new Vector3(.2f, .2f, .2f);
       ControlLayers = false;
       StaffANIM = Staff.GetComponent<Animator>();
    }
    void Update()
    {
        TranGP = Staff.GetComponent<AnimationControlStaff>().ATranGP;
        TranGO = Staff.GetComponent<AnimationControlStaff>().ATranGO;
        TranSP = Staff.GetComponent<AnimationControlStaff>().ATranSP;
        TranSO = Staff.GetComponent<AnimationControlStaff>().ATranSO;
        mass = gameManager.GetComponent<GameManager>().MassAvailable;
        CurrentSize = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        PminSize = new Vector3(.23f, .23f, .23f);
        avgSize = new Vector3(1f, 1f, 1f);
        maxSize = new Vector3(1.74f, 1.74f, 1.74f);        
        CheckSize();
        SetControlLayers();
        CheckControlLayer();
        Size.value = CurrentSize.magnitude;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            Debug.Log("Wheel");
            StaffPoint.gameObject.transform.localPosition = new Vector3(StaffPoint.gameObject.transform.localPosition.x, StaffPoint.gameObject.transform.localPosition.y, StaffPoint.gameObject.transform.localPosition.z + StaffpointLOC);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            Debug.Log("Wheel");
            StaffPoint.gameObject.transform.localPosition = new Vector3(StaffPoint.gameObject.transform.localPosition.x, StaffPoint.gameObject.transform.localPosition.y, StaffPoint.gameObject.transform.localPosition.z - StaffpointLOC);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ObjectHold();
            FreezeCube.Invoke();
            Debug.Log("HoldObj");
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("EndOBJHOLD");         
        }
        else
        {
            Debug.Log("NoObject");
        }     
        PlaceItemDeactivated();
        LaserCast();
    }
    void PlaceItemDeactivated()
    {
        if (holdObjSmall == true)
        { 
            if (Input.GetKeyDown(KeyCode.F))
            {
                HeldItemSmall.SetActive(true);
                ShowCube.Invoke();
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (Lev == false)
            {
                HeldItemSmall.GetComponent<Rigidbody>().isKinematic = false;
                HeldItemSmall.transform.parent = null;
                HeldItemSmall.transform.localScale = new Vector3(.2f, .2f, .2f);
                holdObjSmall = false;
                PlaceCube.Invoke();
            }

            if (Lev == true)
            {
                HeldItemSmall.transform.parent = null;
             
                HeldItemSmall.transform.localScale = new Vector3(.2f, .2f, .2f);
                HeldItemSmall.transform.eulerAngles = new Vector3(0f, 0f, 0f);               
                holdObjSmall = false;
                PlaceCube.Invoke();
            }
            else
            {
                Debug.Log("NoObject");
            }
        }
    }
    void SetControlLayers()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            ControlLayers = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ControlLayers = false;
        }
    }
    void CheckControlLayer()
    {if (ControlLayers == false)
        {
            FireMainGun();
            Down.sprite = Oshrink;
            Up.sprite = Ogrow;
        }

        if (ControlLayers == true)
        {
            ScaleShrinkGrow();
            Down.sprite = Pshrink;
            Up.sprite = Pgrow;
        }
    }
    public void FireMainGun()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            UpSound.Invoke();
         
        }

        if (Input.GetButton("Fire1"))
        {
            StaffANIM.SetBool("TranGrowO", true);
            StaffANIM.SetBool("IsGrowingOBJ", true);
          
            if (TranGO == true)
            {
                if (mass > 0f)
                {
                    ShootGrow();              
                    
                }
                if (mass <= 0f)
                {
                    Debug.Log("NoMass");
                    StaffANIM.SetBool("IsGrowingOBJ", false);
                }
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StaffANIM.SetBool("IsGrowingOBJ", false);
            StaffANIM.SetBool("TranGrowO", false);
            Wipe.Invoke();
            isShooting = false;
            OBJLOC = StaffPoint.transform.localPosition;

        }
        if (Input.GetButtonDown("Fire2"))
        {
            DownSound.Invoke();
           

        }

        if (Input.GetButton("Fire2"))
        {
            StaffANIM.SetBool("TranShrinkO", true);
            StaffANIM.SetBool("IsShrinkingOBJ", true);
            if (TranSO == true)
            {
                ShootShrink();
                           
            }
        }
        if(Input.GetButtonUp("Fire2"))
        {

            StaffANIM.SetBool("IsShrinkingOBJ", false);
            StaffANIM.SetBool("TranShrinkO", false);
            EShrinking = false;
          
            Wipe.Invoke();
            isShooting = false;
            OBJLOC = new Vector3(0f, 0f, 0f);
        }
    }
    void ShootGrow()
    {
        RaycastHit GrowHit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out GrowHit, range, ScaleLayer_mask.value))
        {
            isShooting = true;
            HitObject = GrowHit.transform.gameObject;
            OBJLOC = HitObject.transform.position;
            gameManager.GetComponent<GameManager>().MassAvailable -= GrowTime * MassSubMod;
            GrowHit.collider.gameObject.transform.localScale = Vector3.Lerp(GrowHit.collider.gameObject.transform.localScale, new Vector3(Grow, Grow, Grow), Time.deltaTime * GrowTime);
            Debug.Log(GrowHit.transform.name);
          
        }
        else
        {    
            Debug.Log("Not Hit Skybox");
        }
    }
    void ShootShrink()
    {
        RaycastHit hit2;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit2, range, ScaleLayer_mask.value))
        {
            isShooting = true;
            HitObject = hit2.transform.gameObject;
            OBJLOC = HitObject.transform.position;
            hit2.collider.gameObject.transform.localScale = Vector3.Lerp(hit2.collider.gameObject.transform.localScale, new Vector3(Shrink, Shrink, Shrink), Time.deltaTime * Shrinktime);
            Debug.Log(hit2.transform.name);
            Vector3 HitSize = hit2.transform.localScale;
            CurrentItem = hit2.transform.gameObject;
            EShrinking = true;
        

            if (HitSize.magnitude <= minSize.magnitude && holdObjSmall == false)
            {
                HeldItemSmall = CurrentItem;
                SetHoldObject();
            }
            if (HitSize.magnitude > minSize.magnitude)
            {
                gameManager.GetComponent<GameManager>().MassAvailable += GrowTime * 3f;
            }
            if (HitSize.magnitude <= minSize.magnitude && holdObjSmall == true)
            {
                CurrentItem.transform.localScale = minSize;

            }
        }
        else
        {
            Debug.Log("Not Hit Skybox");
        }
    }
    void SetHoldObject()
    {  
        HeldItemSmall.transform.parent = SpawnPoint.transform;
        HeldItemSmall.transform.localPosition = SpawnPoint.transform.localPosition;
        HeldItemSmall.transform.localRotation = Quaternion.identity;
        HeldItemSmall.GetComponent<Rigidbody>().isKinematic = true;
        HeldItemSmall.SetActive(false);
        Debug.Log("minSize");
        holdObjSmall = true;
       
    }
    public void ScaleShrinkGrow()
    {
        if (Input.GetButton("Fire1"))
        {
            StaffANIM.SetBool("TranGrowP", true);
            StaffANIM.SetBool("IsGrowingP", true);
            if (TranGP == true)
            { 
                if (InVol == false)
                {
                    if (mass > 0f)
                    {
                        Player.transform.localScale = Vector3.Lerp(Player.transform.localScale, new Vector3(PSgrow, PSgrow, PSgrow), Time.deltaTime * PSgrowtime);
                        
                        if (CurrentSize.magnitude <= maxSize.magnitude)
                        {
                            gameManager.GetComponent<GameManager>().MassAvailable -= (GrowTime * Time.deltaTime);
                        }
                        if (CurrentSize.magnitude >= maxSize.magnitude)
                        {
                            Debug.Log("TooLorge");
                            
                            
                        }
                    }
                    if (mass <= 0f)
                    {
                        Debug.Log("NoMass");
                    }
                }


            }


        }
       
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerUP.Invoke();
            if (TooLorge == true)
            {

                Debug.Log("DeadByLorge");
                SceneManager.LoadScene("Died");
                

            }
        }
        if (Input.GetButtonUp("Fire1"))
        {

            StaffANIM.SetBool("IsGrowingP", false);
            StaffANIM.SetBool("TranGrowP", false);
            Wipe.Invoke();
        }
   

            if (Input.GetButton("Fire2"))
        {
            StaffANIM.SetBool("TranShrinkP", true);
            StaffANIM.SetBool("IsShrinkingP", true);
            if (InVol == false && TranSP == true)
            {
                Player.transform.localScale = Vector3.Lerp(Player.transform.localScale, new Vector3(PSshrink, PSshrink, PSshrink), Time.deltaTime * PSshrinktime);
                if (CurrentSize.magnitude < avgSize.magnitude)
                {

                    if (CurrentSize.magnitude >= PminSize.magnitude)
                    {
                        gameManager.GetComponent<GameManager>().MassAvailable += (GrowTime * Time.deltaTime) * 1.25f;

                    }
                    if (CurrentSize.magnitude <= PminSize.magnitude)
                    {

                        Debug.Log("smol");


                    }
                }
            }

        }


        if (Input.GetButtonDown("Fire2"))
        {
            PlayerDown.Invoke();
            if (TooSmol == true)
            {

                Debug.Log("DeadBySmol");
                SceneManager.LoadScene("Died");

            }

        }
        if (Input.GetButtonUp("Fire2"))
        {
            Wipe.Invoke();
            StaffANIM.SetBool("TranShrinkP", false);
            StaffANIM.SetBool("IsShrinkingP", false);


        }


    }
    void CheckSize()
    {
        if (CurrentSize.magnitude > PminSize.magnitude)
        {
            TooSmol = false;
        }

        if (CurrentSize.magnitude < maxSize.magnitude)
        {
            TooLorge = false;
        }
        if (CurrentSize.magnitude <= PminSize.magnitude)
        {


            TooSmol = true;


        }
        if (CurrentSize.magnitude >= maxSize.magnitude)
        {

            TooLorge = true;

        }
    }
    void ObjectHold()
    {
        RaycastHit hitO;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitO, range, ScaleLayer_mask.value))
        {
            if (hitO.transform.gameObject.GetComponent<Rigidbody>().isKinematic == true)
            {

                hitO.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                Lev = false;
            }
            else
            {

               hitO.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                
                Lev = true;
            }


        }
    }
    void LaserCast()
    {
        if (isShooting == true && ControlLayers == false)
        {
            LineRend.enabled = true;
            LineRend.SetPosition(0, StaffVis.transform.position);
            LineRend.SetPosition(1, OBJLOC);


        }
        if (isShooting == false)
        {
            LineRend.enabled = false;
            Debug.Log("NotShoot");

        }

    }   
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("ShrinkVolume"))
        {

            Player.transform.localScale = Vector3.Lerp(Player.transform.localScale, new Vector3(Shrink, Shrink, Shrink), Time.deltaTime * GrowTime);
            InVol = true;

        }
        if (col.CompareTag("GrowVolume"))
        {

            Player.transform.localScale = Vector3.Lerp(Player.transform.localScale, new Vector3(Grow, Grow, Grow), Time.deltaTime * GrowTime);
            InVol = true;
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("ShrinkVolume") || col.CompareTag("GrowVolume"))
        {
            InVol = false;


        }
    }
 
}



