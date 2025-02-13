﻿using Microsoft.ComponentDetection.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace Microsoft.ComponentDetection.Common.Tests
{
    [TestClass]
    public class EnvironmentVariableServiceTests
    {
        public const string MyEnvVar = nameof(MyEnvVar);
        private EnvironmentVariableService testSubject;

        [TestInitialize]
        public void TestInitialize()
        {
            testSubject = new EnvironmentVariableService();
            Environment.SetEnvironmentVariable(EnvironmentVariableServiceTests.MyEnvVar, "true");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableServiceTests.MyEnvVar, null);
        }

        [TestMethod]
        public void DoesEnvironmentVariableExist_ChecksAreCaseInsensitive()
        { 
            Assert.IsFalse(testSubject.DoesEnvironmentVariableExist("THIS_ENVIRONMENT_VARIABLE_DOES_NOT_EXIST"));

            Assert.IsTrue(testSubject.DoesEnvironmentVariableExist(MyEnvVar));
            Assert.IsTrue(testSubject.DoesEnvironmentVariableExist(MyEnvVar.ToLower()));
            Assert.IsTrue(testSubject.DoesEnvironmentVariableExist(MyEnvVar.ToUpper()));
        }
        
        [TestMethod]
        public void GetEnvironmentVariable_returnNullIfVariableDoesNotExist()
        {
            Assert.IsNull(testSubject.GetEnvironmentVariable("NonExistentVar"));
        }

        [TestMethod]
        public void GetEnvironmentVariable_returnCorrectValue()
        {
            string envVariableKey = nameof(envVariableKey);
            string envVariableValue = nameof(envVariableValue);
            Environment.SetEnvironmentVariable(envVariableKey, envVariableValue);
            var result = testSubject.GetEnvironmentVariable(envVariableKey);
            Assert.IsNotNull(result);
            Assert.AreEqual(envVariableValue, result);
            Environment.SetEnvironmentVariable(envVariableKey, null);
        }

        [TestMethod]
        public void IsEnvironmentVariableValueTrue_returnsTrueForValidKey_caseInsensitive()
        {
            string envVariableKey1 = nameof(envVariableKey1);
            string envVariableKey2 = nameof(envVariableKey2);
            Environment.SetEnvironmentVariable(envVariableKey1, "True");
            Environment.SetEnvironmentVariable(envVariableKey2, "tRuE");
            var result1 = testSubject.IsEnvironmentVariableValueTrue(envVariableKey1);
            var result2 = testSubject.IsEnvironmentVariableValueTrue(envVariableKey1);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Environment.SetEnvironmentVariable(envVariableKey1, null);
            Environment.SetEnvironmentVariable(envVariableKey2, null);
        }

        [TestMethod]
        public void IsEnvironmentVariableValueTrue_returnsFalseForValidKey_caseInsensitive()
        {
            string envVariableKey1 = nameof(envVariableKey1);
            string envVariableKey2 = nameof(envVariableKey2);
            Environment.SetEnvironmentVariable(envVariableKey1, "False");
            Environment.SetEnvironmentVariable(envVariableKey2, "fAlSe");
            var result1 = testSubject.IsEnvironmentVariableValueTrue(envVariableKey1);
            var result2 = testSubject.IsEnvironmentVariableValueTrue(envVariableKey1);
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
            Environment.SetEnvironmentVariable(envVariableKey1, null);
            Environment.SetEnvironmentVariable(envVariableKey2, null);
        }

        [TestMethod]
        public void IsEnvironmentVariableValueTrue_returnsFalseForInvalidAndNull()
        {
            string envVariableKey1 = nameof(envVariableKey1);
            string nonExistentKey = nameof(nonExistentKey);
            Environment.SetEnvironmentVariable(envVariableKey1, "notABoolean");
            var result1 = testSubject.IsEnvironmentVariableValueTrue(envVariableKey1);
            var result2 = testSubject.IsEnvironmentVariableValueTrue(nonExistentKey);
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
            Environment.SetEnvironmentVariable(envVariableKey1, null);
        }
    }
}
