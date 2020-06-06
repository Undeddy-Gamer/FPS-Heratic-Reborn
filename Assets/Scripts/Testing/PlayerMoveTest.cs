//using System.Collections;
//using System.Collections.Generic;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;

//namespace Tests
//{
//    //public class PlayerMoveTest
//    //{

//    //    private MovementController playerMovement;
//    //    private GameObject playerGameObject;
//    //    private GameObject groundGameObject;

//    //    [SetUp]
//    //    public void Setup()
//    //    {
//    //        //get the game object we are testing
//    //        playerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

//    //        //get the script we are testing
//    //        playerMovement = playerGameObject.GetComponent<MovementController>();

//    //        //setup a test ground so the player can walk
//    //        groundGameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
//    //        groundGameObject.transform.position = new Vector3(playerGameObject.transform.position.x, playerGameObject.transform.position.y - 2f, playerGameObject.transform.position.z);
//    //    }

//    //    [TearDown]
//    //    public void TearDown()
//    //    {
//    //        //Destroy objects created
//    //        Object.Destroy(playerGameObject.gameObject);
//    //        Object.Destroy(groundGameObject.gameObject);
//    //    }



//    //    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
//    //    // `yield return null;` to skip a frame.
//    //   /* [UnityTest]
//    //    public IEnumerator PlayerMoveTestWithEnumeratorPasses()
//    //    {

//    //        //get value to test before function changes
//    //        Vector3 tempPos = playerGameObject.transform.position;

//    //        //run function you are testing
//    //        playerMovement.MoveV2(.5f, .5f);

//    //        //wait ingame time
//    //        yield return new WaitForSeconds(1.1f);

//    //        //do a check
//    //        // Use the Assert class to test conditions.
//    //        Assert.AreNotEqual(playerGameObject.transform.position, tempPos);


//    //        // Use yield to skip a frame.
//    //        //yield return null;
//    //    }*/

//    //    // Test gravity movewment 
//    //    [UnityTest]
//    //    public IEnumerator PlayerYAsixTest()
//    //    {
//    //        //get value to test before function changes
            
//    //        Vector3 tempPos = playerGameObject.transform.position;

//    //        //run function you are testing
//    //        playerMovement.MoveV2(0f, 0f, true);
            

//    //        //wait ingame time
//    //        yield return new WaitForSeconds(1.1f);

//    //        //do a check
//    //        // Use the Assert class to test conditions.
//    //        Assert.AreNotEqual(playerGameObject.transform.position.y, tempPos.y);

//    //    }


//    //    //Test left right movement
//    //    [UnityTest]        
//    //    public IEnumerator PlayerMoveXAxisTest()
//    //    {
//    //        //get value to test before function changes
//    //        Vector3 tempPos = playerGameObject.transform.position;

//    //        //run function you are testing
//    //        playerMovement.MoveV2(.5f, .5f, false);

//    //        //wait ingame time
//    //        yield return new WaitForSeconds(2f);

//    //        //do a check
//    //        // Use the Assert class to test conditions.
//    //        Assert.AreNotEqual(playerGameObject.transform.position.x, tempPos.x);

//    //    }

//    //    //text forward/back movement
//    //    [UnityTest]
//    //    public IEnumerator PlayerMoveZAxisTest()
//    //    {
//    //        //get value to test before function changes
//    //        Vector3 tempPos = playerGameObject.transform.position;

//    //        //run move function over time
//    //        float StartTime = Time.time;
//    //        while (Time.time < StartTime + 2f)
//    //        { 

//    //            playerMovement.MoveV2(1, 1, false);
//    //            yield return null;
//    //        }


//    //        //do a check
//    //        // Use the Assert class to test conditions.
//    //        Assert.AreNotEqual(playerGameObject.transform.position.z, tempPos.z);

//    //    }


//    //    // To check is null on gameobject
//    //    //UnityEngine.Assertions.Assert.IsNull(gameObject);
//    //}
//}
