using UnityEngine;
using System.Collections;

// Require a character controller to be attached to the same game object
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

// The speed when walking
public static float walkSpeed = 70.0f;

float inAirControlAcceleration = 5.0f;

// How high do we jump when pressing jump and letting go immediately
float jumpHeight = 5f;

// The gravity for the character
public static float gravity = 90.0f;
// The gravity in controlled descent mode
float speedSmoothing = 5.0f;

bool canJump = true;

private float jumpRepeatTime = 0.05f;
private float jumpTimeout = 0.15f;

// The current move direction in x-z
private Vector3 moveDirection = Vector3.zero;
// The current vertical speed
private float verticalSpeed = 0.0f;
// The current x-z move speed
private float moveSpeed = 0.0f;

// The last collision flags returned from controller.Move
private CollisionFlags collisionFlags; 

// Are we jumping? (Initiated with jump button and not grounded yet)
private bool jumping = false;
private bool jumpingReachedApex = false;

// Is the user pressing any keys?
private bool isMoving = false;
// Last time the jump button was clicked down
private float lastJumpButtonTime = -10.0f;
// Last time we performed a jump
private float lastJumpTime = -1.0f;

private Vector3 inAirVelocity = Vector3.zero;

private bool isControllable = true;

private CharacterController controller;

void Awake ()
{
	moveDirection = transform.TransformDirection(Vector3.forward);
	controller = GetComponent<CharacterController>();
	walkSpeed = 70.0f;
	gravity = 90.0f;
}


void UpdateSmoothedMovementDirection ()
{
	Transform cameraTransform = Camera.main.transform;
	bool grounded = IsGrounded();
	
	// Forward vector relative to the camera along the x-z plane	
	Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
	forward.y = 0;
	forward = forward.normalized;

	// Right vector relative to the camera
	// Always orthogonal to the forward vector
	Vector3 right = new Vector3(forward.z, 0, -forward.x);

	// var v = Input.GetAxisRaw("Vertical");
	float v = 0.5f;
	float h = Input.GetAxisRaw("Horizontal");
	
	isMoving = Mathf.Abs (h) > 0.1 || Mathf.Abs (v) > 0.1;
		
	// Target direction relative to the camera
	Vector3 targetDirection = new Vector3(h * right.x * 0.3f + v * forward.x, h * right.y * 0.3f + v * forward.y, h * right.z * 0.3f + v * forward.z);

	float horizontal = 0.05f;
	if(!grounded)
		horizontal = Input.GetAxisRaw("Horizontal");
	
	// Grounded controls
	if (grounded)
	// if(true)
	{

		// We store speed and direction seperately,
		// so that when the character stands still we still have a valid forward direction
		// moveDirection is always normalized, and we only update it if there is user input.
		if (targetDirection != Vector3.zero)
		{
			// If we are really slow, just snap to the target direction
			if (moveSpeed < walkSpeed * 0.9 && grounded)
			{
				moveDirection = targetDirection.normalized;
			}
			// Otherwise smoothly turn towards it
			else
			{
				moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 0, 1000);
				moveDirection = moveDirection.normalized;
			}
		}
		
		// Smooth the speed based on the current target direction
		float curSmooth = speedSmoothing * Time.deltaTime;
		
		// Choose target speed
		//* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
		float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);
		targetSpeed *= walkSpeed;
		
		moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
	}
	// In air controls
	else
	{
		if (isMoving)
			inAirVelocity = targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
			if(horizontal != 0) 
				inAirVelocity = new Vector3(inAirVelocity.x * Mathf.Abs(horizontal) * 300, inAirVelocity.y, inAirVelocity.z);
	}	
}


void ApplyJumping ()
{
	// Prevent jumping too fast after each other
	if (lastJumpTime + jumpRepeatTime > Time.time)
		return;

	if (IsGrounded()) {
		// Jump
		// - Only when pressing the button down
		// - With a timeout so you can press the button slightly before landing		
		if (canJump && Time.time < lastJumpButtonTime + jumpTimeout) {
			verticalSpeed = CalculateJumpVerticalSpeed (jumpHeight);
			SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
		}
	}
}


void ApplyGravity ()
{
	if (isControllable)	// don't move player at all if not controllable.
	{
		
		// When we reach the apex of the jump we send out a message
		if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0)
		{
			jumpingReachedApex = true;
			SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
		}
	
		if (IsGrounded ())
			verticalSpeed = 0.0f;
		else
			verticalSpeed -= gravity * Time.deltaTime;
	}
}

float CalculateJumpVerticalSpeed(float targetJumpHeight)
{
	// From the jump height and gravity we deduce the upwards speed 
	// for the character to reach at the apex.
	return Mathf.Sqrt(2 * targetJumpHeight * gravity);
}

void DidJump ()
{
	jumping = true;
	jumpingReachedApex = false;
	lastJumpTime = Time.time;
	lastJumpButtonTime = -10;
}

void Update() {
	
	if (!isControllable)
	{
		// kill all inputs if not controllable.
		Input.ResetInputAxes();
	}

	if (Input.GetButtonDown ("Jump"))
	{
		lastJumpButtonTime = Time.time;
	}

	UpdateSmoothedMovementDirection();
	
	// Apply gravity
	// - extra power jump modifies gravity
	// - controlledDescent mode modifies gravity
	ApplyGravity ();

	// Apply jumping logic
	ApplyJumping ();
	
	// Calculate actual motion
	Vector3 movement = moveDirection * moveSpeed + new Vector3 (0, verticalSpeed, 0) + inAirVelocity;
	movement *= Time.deltaTime;
	
	// Move the controller
	collisionFlags = controller.Move(movement);
	
	// We are in jump mode but just became grounded
	if (IsGrounded())
	{
		inAirVelocity = Vector3.zero;
		if (jumping)
		{
			jumping = false;
			SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
		}
	}
}

void OnControllerColliderHit(ControllerColliderHit hit)
{
//	Debug.DrawRay(hit.point, hit.normal);
	if (hit.moveDirection.y > 0.01) 
		return;
}

bool IsGrounded () {
	return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
}

}