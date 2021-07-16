using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;

namespace Tests
{
    public class Plane_Test
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        //checks that the plane as been spawned by making sure the plane can move
        public IEnumerator isPlaneSpawned_Test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<PlaneMovement>();
            var planeController = plane.gameObject.AddComponent<CharacterController>();

            var propelPlane = plane.PropelPlane(10.0f);
            planeController.Move(propelPlane * Time.deltaTime);
            //act
            yield return new WaitForSeconds(3);

            //assert
            Assert.Greater(plane.gameObject.transform.position.z, 0.0f);
            //grabage collection
            gameObject = null;
            plane = null;
            planeController = null;
            propelPlane = new Vector3(0,0,0);
        }

        //checks that the helper function is working
        [UnityTest]
        public IEnumerator isHelperActive_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<PlaneMovement>();
            var planeController = plane.gameObject.AddComponent<CharacterController>();

            plane.TurnHelperOn();

            // Use yield to skip a frame.
            //act
            yield return new WaitForFixedUpdate();

            //assert
            Assert.AreEqual(plane.isHelperOn, true);

            //garbage collection
            gameObject = null;
            plane = null;
            planeController = null;
        }

        //check if plane can accelerate forward
        [UnityTest]
        public IEnumerator isSpeedNormal_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<PlaneMovement>();
            var planeController = plane.gameObject.AddComponent<CharacterController>();

            var propelPlane = plane.PropelPlane(10.0f);
            planeController.Move(propelPlane * Time.deltaTime);
            plane.AcceleratePlane(20);
            // Use yield to skip a frame.
            //act
            yield return new WaitForSeconds(5.0f);
            //assert
            Assert.Greater(plane.currSpeed, 15.0f);
            //garbage collection
            gameObject = null;
            plane = null;
            planeController = null;
            propelPlane = new Vector3(0, 0, 0);
        }

        //checks if the dive helper works, by seeing if the plane corrects itself
        [UnityTest]
        public IEnumerator isHelperDiveWorking_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<PlaneMovement>();
            var planeController = plane.gameObject.AddComponent<CharacterController>();
            plane.AcceleratePlane(20);
            plane.GroundHelper();
            // Use yield to skip a frame.
            //act
            yield return new WaitForSeconds(5);

            //assert
            Assert.AreEqual(plane.isAboutToCollide, false);

            gameObject = null;
            plane = null;
            planeController = null;
        }
        //checks if the plane slows down when the helper is activated
        [UnityTest]
        public IEnumerator isHelperTurnWorking_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<PlaneMovement>();
            var planeController = plane.gameObject.AddComponent<CharacterController>();
            var propelPlane = plane.PropelPlane(10.0f);
            planeController.Move(propelPlane * Time.deltaTime);

            plane.TurnSpeedReducer();

            // Use yield to skip a frame.
            //act
            yield return new WaitForSeconds(6.0f);

            //assert
            Assert.LessOrEqual(plane.currSpeed, 15.0f);

            gameObject = null;
            plane = null;
            planeController = null;
        }

        //checks if the plane stalls if the speed is slowed down to 0
        [UnityTest]
        public IEnumerator isStallWorking_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<PlaneMovement>();
            var planeController = plane.gameObject.AddComponent<CharacterController>();

            plane.currSpeed = 0;

            var currYPos = plane.transform.position.y;

            // Use yield to skip a frame.
            //act
            yield return new WaitForSeconds(3.0f);

            var newYPos = plane.transform.position.y;
            //assert
            Assert.AreNotEqual(currYPos, newYPos);

            gameObject = null;
            plane = null;
            planeController = null;
        }
    }
}
