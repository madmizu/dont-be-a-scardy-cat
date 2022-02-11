using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    // 
    public float minGroundNormalY = .65f;

    // Used to scale gravity with a float
        public float gravityModifier = 1f;

    // this is where we will store input from outside the class that refers to where our object is trying to move.
        protected Vector2 targetVelocity;
    // boolean variable to determine if an object is grounded or not.
        protected bool grounded;
    // variable to set the coordinates of an object when it is 'grounded' on the y axis
        protected Vector2 groundNormal;
    // In 'movement', we move an object based on the values calculated by setting the position of the object's rigidbody 2D. We declare a rigidbody 2D variable to store a reference to the rigidbody 2D that is attached to the game object. See 'OnEnable' function.
        protected Rigidbody2D rb2d;
    // protected because other classes will inherit from physics object and I want them to be able to access velocity from only within the class (not accessible from other classes)
        protected Vector2 velocity;
    // Needed for rb2d.cast function - see 'movement' function
        protected ContactFilter2D contactFilter;
    // Needed for rb2d.cast function - see 'movement' function. Calling the array 'hitBuffer' and making it a value of a new array with a length of 16
        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    // Once rb2d.cast runs, we have our results and we want to copy our array of contacts ('hitBuffer') to a new list of RayCastHit2D objects. List is called 'hitBufferList'. This list is equal to a new list of RaycastHit2D objects with a length of 16.
        protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);
    // We only want to apply collision if the distance we are attempting to move is greater than a minimum value. Here, we are storing the minimum value in a float called 'minMoveDistance'
    // List class represents a list of objects, when can be accessed by index (i.e. an array)
        protected const float minMoveDistance = 0.001f;
    // This shell makes sure our object never gets stuck inside another collider. It adds additional padding to 'distance' in the rb2d.cast function (see movement function)
        protected const float shellRadius = 0.001f;

    // This function gets and stores a rigidbody 2D component reference.
        void OnEnable()
        {
            rb2d = GetComponent<Rigidbody2D> ();
        }

    void Start () 
    {
        // We are NOT checking collision against 'triggers'
            contactFilter.useTriggers = false;
        // SetLayerMask fuction gets a layer mask from the project settings for Physics 2D
            // We are using this function to have our code refer to our Project settings for Physics2D to determine what layers we would check collision against.
            // In Unity, go to 'edit' => 'Project Settings' => Physics2D to see the Layer Collision Matrix
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    // This will be overwritten the the platformer controller
    void Update () 
    {
        // zero out target velocity during each frame
        targetVelocity = Vector2.zero;
        ComputeVelocity ();    
    }


    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        // gravity default value (in the Physics2D system) multiplied by the time it took to render the last frame (Time.deltaTime)
            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        // after gravity is added, we set x velocity to equal our target velocity.
            velocity.x = targetVelocity.x;
        // always consider grounded as false after gravity modifier is set.
            grounded = false;
         // apply velocity to object to move it. This will determine the object's new location. This Vector2 is called 'deltaPosition' (i.e. the change in position) is equal to velocity multiplied by the time it took to render the last frame (Time.deltaTime)
            Vector2 deltaPosition = velocity * Time.deltaTime;
        // This stores the direction that we are trying to move along the ground. This help for calculating the angle based on groundNormal to have our player be able to move on a slope.
            Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);
        // We have a new position for our oject (deltaPosition). We want to use that new position to move our object. This Vector2 will be passed to the movement function. 
            Vector2 move = moveAlongGround * deltaPosition.x;
            // First the X
            Movement (move, false);
            // Value of 'move' for vertical movement
                move = Vector2.up * deltaPosition.y;
            // Then the y
            Movement (move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        // Using magnitude of Vector2 'move' to determine the distance we move. Store this in a float called 'distance'
        float distance = move.magnitude;

        // We only want to apply collision if the distance we are attempting to move is greater than a minimum value (saved in float variable called 'minMoveDistance'). This will prevent objects from constantly checking collision when they are standing still on a collision surface.
            if (distance > minMoveDistance) 
            {
                // using rb2d.cast to check if any of the attached colliders of our rb2d would overlap with anything in the next frame. 
                // public int Cast(Vector2 direction, ContactFilter2D, RaycastHit2D[] results, float distance = Mathf.Infinity);
                    // 'move' = direction: Vector representing direction to cast each Collider2D shape.
                        // We use 'move' and cast the object's collider shape in the next frame, then we are using logic to determine: in the next frame is this collider shape going to overlap with another collider. 
                    // results: An array of RayCastHit2D[] objects where we store info about the contacts that are detected.
                    // 'distance' = distance: How far from our object's collider are we going to cast that collider shape in the next frame
                    // 'contactFilter' = contactFilter: Allows us to filter the results in the RayCastHit2D array - we can choose what results we want to filter by setting variables here in the contactFilter.
                // rb2d.cast will RETURN and int => The number of results (i.e. all the contacts that were made during the collision casted into the next frame)
                // we store the return value (the number of results) in an int called 'count'
                    int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
                // We only want to copy the indexes of the hitBuffer array that actually hit (or make contact) with something. Just the current, active contacts.
                // Clear the list so we are not using old data
                hitBufferList.Clear ();


                // Loop over the 'hitBuffer' array with (hitBuffer [i]) and we're using 'count' to only copy over the indexes that have something in them out of the default length of 16. Remember, 'count' is the return of rb2d.cast, wich is the number of results. We then add (copy) each of those indexes to the hit Bufferlist. This is now a list of objects that will overlap with the object's physics object collider
                    for (int i = 0; i < count; i++) {
                    // This line and the if statement will check if a platform has a PlatformEffector2D.
                    // If it does, it will allow the player to jump up from underneath, but not fall through
                    // the top surface
                    PlatformEffector2D platform = hitBuffer[i].collider.GetComponent<PlatformEffector2D>();
                    
                    if(!platform||(hitBuffer[i].normal == Vector2.up && velocity.y < 0 && yMovement)){

                        hitBufferList.Add (hitBuffer [i]);
                        }
                    }
                    // Loop over the new hitBufferList and compare 'normal' to a minimum ground, normal value.
                    // .Count is the total number of elements in the List
                    for (int i = 0; i < hitBufferList.Count; i++) 
                    {
                        // Checking the normal for each index
                        Vector2 currentNormal = hitBufferList [i].normal;
                        // We are trying to determine if the object is grounded. If the angle of the object that we are colliding with means it is a flat surface that we can stand on (so, not a wall) we can only set the player to grounded when it is on a 'normal' ground. 
                        // plyer cannot jump to a vertical wall and collide with it. It needs to me a horizontal ground
                            // This will not allow players to slide down slopes
                            // if you want your player to move on a steep slope, you can extend the controller or use a maximum allowed slope and design based on that slope maximum. 
                        if (currentNormal.y > minGroundNormalY) 
                        {
                            // if this is true, grounded = true
                            grounded = true;
                            //if we are moving along the y axis, we need to add a variable for our groundNormal. 
                            if (yMovement) 
                            {
                                groundNormal = currentNormal;
                                currentNormal.x = 0;
                            }
                        }
                    // getting the difference between the velocity and the currentNormal to determine if we need to subtract from the velocity to prevent pllayer from entering into another collider. 
                    // Example: Slope ceiling that you jump towards, we dont want the player to immediately stop and fall, nor do we want the player's head to go through the ceiling. Instead we want the player to slide against that ceiling while continuing the horizontal movement.
                    // calculate this using Vector2.Dot 

                            float projection = Vector2.Dot (velocity, currentNormal);
                    // check if projection returns negative number, set velocity to equal velocity minus the projection multiplied by the currentNormal to cancel out the velocity that would be stopped by the collision.
                            if (projection < 0) 
                            {
                                velocity = velocity - projection * currentNormal;
                            }
                    // if distance is less than modifiedDistance, then we make distance equal to the modifiedDistance
                    // If the movement will cause a collision, the distance will now equal the modifiedDistance and stop at the collision point. 
                    float modifiedDistance = hitBufferList [i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }


            }
        // Setting the object's position. Add movement vector to the position of the rb2d for every frame.
        rb2d.position = rb2d.position + move.normalized * distance;
    }

}

/* 
Notes:

- Arrays vs. Lists
    - An array must be declared 


*/
