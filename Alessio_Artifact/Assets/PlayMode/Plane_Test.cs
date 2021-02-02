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
        public IEnumerator North()
        {
            // Use the Assert class to test conditions.

            var gameObject = new GameObject();
            var plane = gameObject.AddComponent<Plane>();
            
            // Use yield to skip a frame.
            yield return new WaitForSeconds(2);

            Assert.AreEqual(10.0f, plane.North);
        }
    }
}
