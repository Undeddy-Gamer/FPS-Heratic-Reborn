using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerMoveTest
    {

        private FPS.Player.Movement playerMovement;
        private GameObject playerGameObject;
        private GameObject groundGameObject;

        [SetUp]
        public void Setup()
        {
            //get the game object we are testing
            playerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

            //get the script we are testing
            playerMovement = playerGameObject.GetComponent<FPS.Player.Movement>();

            //groundGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Level1Map"));
            groundGameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            groundGameObject.transform.position = new Vector3(playerGameObject.transform.position.x - 1, playerGameObject.transform.position.y - 1.1f, playerGameObject.transform.position.z -1);
        }

        [TearDown]
        public void TearDown()
        {
            //Destroy objects created
            Object.Destroy(playerGameObject.gameObject);
            Object.Destroy(groundGameObject.gameObject);
        }



        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayerMoveTestWithEnumeratorPasses()
        {

            //get value to test before function changes
            Vector3 tempPos = playerGameObject.transform.position;

            //run function you are testing
            playerMovement.MoveV2(.5f, .5f);

            //wait ingame time
            yield return new WaitForSeconds(1.1f);

            //do a check
            // Use the Assert class to test conditions.
            Assert.AreNotEqual(playerGameObject.transform.position, tempPos);


            // Use yield to skip a frame.
            //yield return null;
        }


        [UnityTest]
        public IEnumerator PlayerMoveVerticalTestWithEnumeratorPasses()
        {
            //get value to test before function changes
            Vector3 tempPos = playerGameObject.transform.position;

            //run function you are testing
            playerMovement.MoveV2(.5f, .5f);

            //wait ingame time
            yield return new WaitForSeconds(1.1f);

            //do a check
            // Use the Assert class to test conditions.
            Assert.AreNotEqual(playerGameObject.transform.position.y, tempPos.y);

        }

        [UnityTest]
        public IEnumerator PlayerMoveXTestWithEnumeratorPasses()
        {
            //get value to test before function changes
            Vector3 tempPos = playerGameObject.transform.position;

            //run function you are testing
            playerMovement.MoveV2(.5f, .5f);

            //wait ingame time
            yield return new WaitForSeconds(2f);

            //do a check
            // Use the Assert class to test conditions.
            Assert.AreNotEqual(playerGameObject.transform.position.x, tempPos.x);

        }


        [UnityTest]
        public IEnumerator PlayerMoveZTestWithEnumeratorPasses()
        {
            //get value to test before function changes
            Vector3 tempPos = playerGameObject.transform.position;

            //run function you are testing
            float StartTime = Time.time;
            while (Time.time < StartTime + 2f)
            { 
                playerMovement.MoveV2(1f, 1f);
                yield return null;
            }

            //wait ingame time
            //yield return new WaitForSeconds(2f);

            //do a check
            // Use the Assert class to test conditions.
            Assert.AreNotEqual(playerGameObject.transform.position.z, tempPos.z);

        }


        // To check is null on gameobject
        //UnityEngine.Assertions.Assert.IsNull(gameObject);
    }
}
