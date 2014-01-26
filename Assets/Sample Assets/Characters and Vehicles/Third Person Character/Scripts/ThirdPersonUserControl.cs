using UnityEngine;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour {
	
	public bool walkByDefault = false;                  // toggle for walking state
	public bool lookInCameraDirection = true;           // should the character be looking in the same direction that the camera is facing
	
    private Transform cam;                              // A reference to the main camera in the scenes transform
	private Vector3 lookPos;                            // The position that the character should be looking towards
    private ThirdPersonCharacter character;             // A reference to the ThirdPersonCharacter on the object

	public bool InputEnabled = false;

	// Use this for initialization
	void Start ()
	{
        // get the transform of the main camera
		cam = Camera.main.transform;

        // get the third person character ( this should never be null due to require component )
		character = GetComponent<ThirdPersonCharacter>();
	}

	// Fixed update is called in sync with physics
	void FixedUpdate ()
	{
		bool crouch;
		bool jump;
		float h;
		float v;

		if (InputEnabled)
		{
			// read inputs
			crouch = Input.GetKey(KeyCode.C);
			jump = CrossPlatformInput.GetButton("Jump");
			h = CrossPlatformInput.GetAxis("Horizontal");
			v = CrossPlatformInput.GetAxis("Vertical");
		}
		else
		{
			crouch = false;
			jump = false;
			h = 0;
			v = 0;
		}

		// calculate move direction to pass to character
		// Vector3 camForward = Vector3.Scale (cam.forward, new Vector3(1,0,1)).normalized;
		Vector3 camForward = new Vector3 (1,0,0);
		Vector3 move = v * camForward + h * new Vector3(0,0,-1);	

		#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8)
		// On standalone builds, walk/run speed is modified by a key press.
		bool walkToggle = Input.GetKey(KeyCode.LeftShift);
		// We select appropriate speed based on whether we're walking by default, and whether the walk/run toggle button is pressed:
		float walkMultiplier = (walkByDefault ? walkToggle ? 1 : 0.5f : walkToggle ? 0.5f : 1);
		move *= walkMultiplier;
		#endif

		// On mobile, it's controlled in analogue fashion by the v input value, and therefore needs no special handling.


		// calculate the head look target position
	    lookPos = lookInCameraDirection && cam != null
	                  ? transform.position + cam.forward * 100
	                  : transform.position + transform.forward * 100;

	    // pass all parameters to the character control script
		character.Move( move, crouch, jump, lookPos );
	}

	public void IntroTurnAround()
	{
		Vector3 camForward = Vector3.Scale (cam.forward, new Vector3(1,0,1)).normalized;
		Vector3 move = 0.3f * camForward + 0.2f * cam.right;	

		// pass all parameters to the character control script
		character.Move(move, false, false, transform.position + transform.forward * 100 );

	}
}
