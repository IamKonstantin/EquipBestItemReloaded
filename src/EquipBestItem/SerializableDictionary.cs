using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;



[XmlRoot("dictionary"), Serializable]
public class SerializableDictionary<TKey, TValue>
    : Dictionary<TKey, TValue>, IXmlSerializable
{
    #region IXmlSerializable Members
    public System.Xml.Schema.XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(System.Xml.XmlReader reader)
    {
        if (reader == null)
            throw new ArgumentNullException(nameof(reader), "123");
        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        bool wasEmpty = reader.IsEmptyElement;
        reader.Read();

        if (wasEmpty)
            return;

        while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
        {
            reader.ReadStartElement("item");
            TKey key = (TKey)keySerializer.Deserialize(reader);
            TValue value = (TValue)valueSerializer.Deserialize(reader);
            this.Add(key, value);
            reader.ReadEndElement();
            reader.MoveToContent();
        }
        reader.ReadEndElement();
    }

    public void WriteXml(System.Xml.XmlWriter writer)
    {
        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
        emptyNamespaces.Add("", "");

        foreach (TKey key in this.Keys)
        {
            writer.WriteStartElement("item");
            keySerializer.Serialize(writer, key, emptyNamespaces);
            TValue value = this[key];
            valueSerializer.Serialize(writer, value, emptyNamespaces);
            writer.WriteEndElement();
        }
    }
    #endregion

    public static bool operator ==(SerializableDictionary<TKey, TValue> left,
                              SerializableDictionary<TKey, TValue> right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(SerializableDictionary<TKey, TValue> left,
                                   SerializableDictionary<TKey, TValue> right)
    {
        return !(left == right);
    }

    public override bool Equals(object obj)
    {
        var other = obj as SerializableDictionary<TKey, TValue>;
        if (other == null) return false;

        if (Count != other.Count) return false;

        var comparer = EqualityComparer<TValue>.Default;
        foreach (var pair in this)
        {
            if (!other.TryGetValue(pair.Key, out TValue value) ||
                !comparer.Equals(pair.Value, value))
            {
                return false;
            }
        }
        return true;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 19;
            foreach (var pair in this)
            {
                hash = hash * 31 + (pair.Key?.GetHashCode() ?? 0);
                hash = hash * 31 + (pair.Value?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}
