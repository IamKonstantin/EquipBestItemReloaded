using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using EquipBestItem;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject
{

    [TestClass]
    public class TestSerializableDictionary
    {
        public enum TestEnum
        {
            TestMyLove,
            TestIsGreatToo
        }

        [Serializable]
        public class TestClass
        {
            public String Name { get; set; }

            public TestClass()
            {

            }

            public TestClass(string name)
            {
                Name = name;
            }

            public override bool Equals(object obj)
            {
                var other = obj as TestClass;
                if (other == null)
                    return false;
                return Name == other.Name;
            }
        }
        public TestSerializableDictionary()
        {
        }

        bool AreEqualByContent<TKey, TValue>(IDictionary<TKey, TValue> left, IDictionary<TKey, TValue> right)
        {
            if (left.Count != right.Count)
                return false;
            foreach (var key in left.Keys)
            {
                if (!right.TryGetValue(key, out var rightValue) ||
                    !EqualityComparer<TValue>.Default.Equals(left[key], rightValue))
                {
                    return false;
                }
            }
            return true;
        }

        [TestMethod]
        public void Test03CompatibilityEqualOperators()
        {
            int i = 0;
            var buildInStringToInt = new Dictionary<String, int>();
            buildInStringToInt["One"] = 3;
            var buildInStringToIntEqualInContent = new Dictionary<String, int>();
            buildInStringToIntEqualInContent["One"] = 3;

            Assert.IsTrue(AreEqualByContent(buildInStringToInt, buildInStringToIntEqualInContent));

            Assert.IsTrue(buildInStringToInt == buildInStringToInt);
            Assert.IsFalse(buildInStringToInt != buildInStringToInt);
            Assert.IsFalse(buildInStringToInt == buildInStringToIntEqualInContent);
            Assert.IsTrue(buildInStringToInt != buildInStringToIntEqualInContent);

            var serializableStringToInt = new SerializableDictionary<String, int>();
            serializableStringToInt["One"] = 3;
            var serializableStringToIntEqualInContent = new SerializableDictionary<String, int>();
            serializableStringToIntEqualInContent["One"] = 3;

            Assert.IsTrue(AreEqualByContent(serializableStringToInt, serializableStringToIntEqualInContent));

            Assert.IsTrue(serializableStringToInt == serializableStringToInt);
            Assert.IsFalse(serializableStringToInt != serializableStringToInt);
            Assert.IsFalse(serializableStringToInt == serializableStringToIntEqualInContent);
            Assert.IsTrue(serializableStringToInt != serializableStringToIntEqualInContent);

            Assert.IsTrue(AreEqualByContent(buildInStringToInt, serializableStringToInt));

            Assert.IsTrue(buildInStringToInt != serializableStringToInt);
            Assert.IsFalse(buildInStringToInt == serializableStringToInt);
        }

        [TestMethod]
        public void Test02CompatibilityEqualsFunction()
        {
            var buildInStringToInt = new Dictionary<String, int>();
            buildInStringToInt["One"] = 3;
            var buildInStringToIntEqualInContent = new Dictionary<String, int>();
            buildInStringToIntEqualInContent["One"] = 3;

            Assert.IsFalse(buildInStringToInt.Equals(buildInStringToIntEqualInContent));

            var serializableStringToInt = new SerializableDictionary<String, int>();
            serializableStringToInt["One"] = 3;
            var serializableStringToIntEqualInContent = new SerializableDictionary<String, int>();
            serializableStringToIntEqualInContent["One"] = 3;

            Assert.IsFalse(serializableStringToInt.Equals(serializableStringToIntEqualInContent));
        }

        [TestMethod]
        public void Test00СompatibilitySquareBracketsInsert()
        {

            var buildInStringToInt = new Dictionary<String, int>();
            buildInStringToInt["One"] = 3;
            var serialiableStringToInt = new SerializableDictionary<String, int>();
            serialiableStringToInt["One"] = 3;

            Assert.AreEqual(buildInStringToInt.Count, serialiableStringToInt.Count);
            foreach (var key in buildInStringToInt.Keys)
            {
                Assert.IsTrue(serialiableStringToInt.ContainsKey(key));
                Assert.AreEqual(buildInStringToInt[key], serialiableStringToInt[key]);
            }
        }

        [TestMethod]
        public void Test01СompatibilityCurveBracketsConstructor()
        {

            var buildInStringToInt = new Dictionary<String, int>
            {
                { "One", 3 },
                { "Two", 4 }
            };
            var serialiableStringToInt = new SerializableDictionary<String, int>
            {
                { "One", 3 },
                { "Two", 4 }
            };

            Assert.AreEqual(buildInStringToInt.Count, serialiableStringToInt.Count);
            foreach (var key in buildInStringToInt.Keys)
            {
                Assert.IsTrue(serialiableStringToInt.ContainsKey(key));
                Assert.AreEqual(buildInStringToInt[key], serialiableStringToInt[key]);
            }
        }

        [TestMethod]
        public void Test10SerializableDictionaryStringToInt()
        {
            var stringToInt = new SerializableDictionary<String, int>
            {
                { "One", 3 },
                { "Two", 4 }
            };

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<String, int>));
            serializer.Serialize(writer, stringToInt, ns);
            String serializedXml = writer.ToString();

            string expectedXml = 
@"<?xml version=""1.0"" encoding=""utf-16""?>
<dictionary>
  <item>
    <string>One</string>
    <int>3</int>
  </item>
  <item>
    <string>Two</string>
    <int>4</int>
  </item>
</dictionary>";

            Assert.AreEqual(serializedXml, expectedXml);

            StringReader streamReader = new StringReader(serializedXml);
            XmlReader xmlReader = XmlReader.Create(streamReader);
            var readWritenStringToInt = (SerializableDictionary<String, int>)serializer.Deserialize(xmlReader);

            Assert.IsTrue(AreEqualByContent(readWritenStringToInt, stringToInt));
        }


        [TestMethod]
        public void Test11SerializableDictionaryEnumToClass()
        {
            var enumToClass = new SerializableDictionary<TestEnum, TestClass>
            {
                { TestEnum.TestMyLove, new TestClass("Svana") },
                { TestEnum.TestIsGreatToo, new TestClass("Arwa") }
            };

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<TestEnum, TestClass>));
            serializer.Serialize(writer, enumToClass, ns);
            String serializedXml = writer.ToString();

            string expectedXml =
@"<?xml version=""1.0"" encoding=""utf-16""?>
<dictionary>
  <item>
    <TestEnum>TestMyLove</TestEnum>
    <TestClass>
      <Name>Svana</Name>
    </TestClass>
  </item>
  <item>
    <TestEnum>TestIsGreatToo</TestEnum>
    <TestClass>
      <Name>Arwa</Name>
    </TestClass>
  </item>
</dictionary>";

            Assert.AreEqual(serializedXml, expectedXml);

            StringReader streamReader = new StringReader(serializedXml);
            XmlReader xmlReader = XmlReader.Create(streamReader);
            var readWritenEnumToClass = (SerializableDictionary<TestEnum, TestClass>)serializer.Deserialize(xmlReader);

            Assert.IsTrue(AreEqualByContent(readWritenEnumToClass, enumToClass));
        }
    }

    [TestClass]
    public class TestSerialize
    {
        public TestSerialize()
        {
        }

        [TestMethod]
        public void TestSaveSettingsFromInstace()
        {
            SettingsLoader.Instance.SaveSettings();
            SettingsLoader.Instance.SaveCharacterSettings();
        }

        [TestMethod]
        public void TestSaveClassSettings()
        {
            
            ClassSettings cs = new ClassSettings();
        }
    }
}
