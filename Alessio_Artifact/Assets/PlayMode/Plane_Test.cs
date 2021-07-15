using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Plane_Test
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        //sut_when_then
        public IEnumerator isPlaneSpawned_Test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<Plane>();

            // Use yield to skip a frame.
            //act
            yield return new WaitForFixedUpdate();

            //assert
            Assert.AreEqual(10.0f, plane.North);
        }
        [UnityTest]
        public IEnumerator isLandingGearOn_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<Plane>();

            // Use yield to skip a frame.
            //act
            yield return new WaitForFixedUpdate();

            //assert
            Assert.AreEqual(10.0f, plane.North);
        }
        [UnityTest]
        public IEnumerator isHelperActive_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<Plane>();

            // Use yield to skip a frame.
            //act
            yield return new WaitForFixedUpdate();

            //assert
            Assert.AreEqual(10.0f, plane.North);
        }

        [UnityTest]
        public IEnumerator isSpeedNormal_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<Plane>();

            // Use yield to skip a frame.
            //act
            yield return new WaitForFixedUpdate();

            //assert
            Assert.AreEqual(10.0f, plane.North);
        }
        [UnityTest]
        public IEnumerator isHelperDiveWorking_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<Plane>();

            // Use yield to skip a frame.
            //act
            yield return new WaitForFixedUpdate();

            //assert
            Assert.AreEqual(10.0f, plane.North);
        }
        [UnityTest]
        public IEnumerator isHelperTurnWorking_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<Plane>();

            // Use yield to skip a frame.
            //act
            yield return new WaitForFixedUpdate();

            //assert
            Assert.AreEqual(10.0f, plane.North);
        }
        [UnityTest]
        public IEnumerator isHelperStallWorking_test()
        {
            // Use the Assert class to test conditions.

            //arrange
            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<Plane>();

            // Use yield to skip a frame.
            //act
            yield return new WaitForFixedUpdate();

            //assert
            Assert.AreEqual(10.0f, plane.North);
        }
    }
}
