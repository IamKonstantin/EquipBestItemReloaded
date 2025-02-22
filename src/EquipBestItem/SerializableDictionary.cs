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


//    public override bool Equals(object obj) => Equals(obj);
    //public override bool Equals(object obj)
    //{
    //    //var meBased = (Dictionary<TKey, TValue>)this;
    //    //return meBased.Equals(obj);

    //    //var objBased = (Dictionary<TKey, TValue>)obj;
    //    return base.Equals(obj);
        

    //    //var objBased2 = obj as Dictionary<TKey, TValue>;
    //    //if (objBased2 == null)
    //    //    return false;
    //    //else
    //    //    return base.Equals(objBased2);
    //}

    public override int GetHashCode() => base.GetHashCode();
}
