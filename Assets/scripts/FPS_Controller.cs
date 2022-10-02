using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPS_Controller : CharacterStats 
{
    [Header("Player Camera")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [Header("User Interface")]
    public healthBarScript healthBar;
    public AmmoUIScript ammoUIScript;
    public GameObject PickUp_Ui;
    public Image PickUp_gunPreview;

    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public bool isCrouching = false;

    // private void Awake() {
    //     PickUp_gunPreview = PickUp_Ui.GetComponentInChildren<Image>(true);
    // }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        healthBar.SetMaxHealth(MaxHealth.GetValue());
        healthBar.SetHealth(CurrentHealth);
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
#region pickUp guns
        if (Physics.CheckSphere(transform.position,pickUpRange,universal_vars.instance.PickablesLayer))
        {
            Collider Closest=null;
            float _distance=float.MaxValue;
            foreach (Collider item in Physics.OverlapSphere(transform.position,pickUpRange,universal_vars.instance.PickablesLayer))
            {
                if (!item.GetComponent<Gun_base>().IsEquiped&&Vector3.Distance(transform.position,item.transform.position)<_distance)
                {
                    Closest=item;
                    _distance = Vector3.Distance(transform.position,item.transform.position);
                }
            }
            if (Closest!=null)
            {
                PickUp_Ui.SetActive(true);
                Texture2D _preview =UnityEditor.AssetPreview.GetAssetPreview(Closest.gameObject);
                if (_preview!=null)
                {
                    PickUp_gunPreview.sprite=Sprite.Create(_preview,new Rect(0,0,_preview.width,_preview.height),new Vector2(_preview.width/2,_preview.height/2));
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Gun_base _gun =Closest.gameObject.GetComponent<Gun_base>();
                    PickUp_Ui.SetActive(false);
                    if (selectedWeapon!=null)
                    {
                        selectedWeapon.Drop();
                    }
                    if (selectedWeapon==primaryWeapon)
                    {
                        if (selectedWeapon!=null)
                        {
                            primaryWeapon.ActivateWeapon(true);
                        }
                        primaryWeapon=_gun;
                        selectedWeapon=_gun;
                    }
                    else{
                        if (selectedWeapon!=null)
                        {
                            secondaryWeapon.ActivateWeapon(true);
                        }
                        secondaryWeapon=_gun;
                        selectedWeapon=_gun;
                    }
                    Closest.SendMessage("PickUp",GunContainer);
                }
            }
        }
        else{
            PickUp_Ui.SetActive(false);
        }
#endregion
#region switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (primaryWeapon!=null)
            {
                primaryWeapon.ActivateWeapon(true);
            }
            if (secondaryWeapon!=null)
            {
                secondaryWeapon.ActivateWeapon(false);
            }
            selectedWeapon=primaryWeapon;
            if (selectedWeapon==null)
            {
                ammoUIScript.SetCount(0);
            }
            else{
                ammoUIScript.SetCount(selectedWeapon.current_mag_capacity);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (primaryWeapon!=null)
            {
                primaryWeapon.ActivateWeapon(false);
            }
            if (secondaryWeapon!=null)
            {
                secondaryWeapon.ActivateWeapon(true);
            }
            selectedWeapon=secondaryWeapon;
            if (selectedWeapon==null)
            {
                ammoUIScript.SetCount(0);
            }
            else{
                ammoUIScript.SetCount(selectedWeapon.current_mag_capacity);
            }
        }
#endregion
#region shot weapon
        if (selectedWeapon!=null)
        {
           if (Input.GetMouseButtonDown(0)&&selectedWeapon.singleFire)
            {
                
                selectedWeapon.shoot();
            }
            if (Input.GetMouseButton(0)&&!selectedWeapon.singleFire)
            {
                selectedWeapon.shoot();
            }
#endregion
            if (Input.GetKeyDown(KeyCode.Q))
            {
                selectedWeapon.Drop();
                if (selectedWeapon==primaryWeapon)
                {
                    primaryWeapon=null;
                }
                else
                {
                    secondaryWeapon=null;
                }
                selectedWeapon=null;
            }
            if(Input.GetKeyDown(KeyCode.R)){
                
                selectedWeapon.reload();
            }
        }
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isCrouching ? crouchWalkingSpeed.GetValue() : (isRunning ? runningSpeed.GetValue() : walkingSpeed.GetValue())) * Input.GetAxis("Vertical") : 0;

        float curSpeedY = canMove ? (isCrouching ? crouchWalkingSpeed.GetValue() : (isRunning ? runningSpeed.GetValue() : walkingSpeed.GetValue())) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed.GetValue();
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= universal_vars.instance.Gravity * Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && canMove && characterController.isGrounded)
        {
            isCrouching=true;
            
        }
        if(isCrouching&& canMove && characterController.isGrounded){
            playerCamera.transform.localPosition=Vector3.Lerp(playerCamera.transform.localPosition,new Vector3(0,-0.5f,0),Time.deltaTime*5f);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && canMove && characterController.isGrounded)
        {
            isCrouching=false;
        }
            
        if(!isCrouching&& canMove && characterController.isGrounded){
            playerCamera.transform.localPosition=Vector3.Lerp(playerCamera.transform.localPosition,new Vector3(0,0.64f,0),Time.deltaTime*5f);
        }



        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        if (selectedWeapon==null)
        {
            ammoUIScript.SetCount(0);
        }
        else{
            ammoUIScript.SetCount(selectedWeapon.current_mag_capacity);
        }
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBar.SetHealth(CurrentHealth);
    }
}
