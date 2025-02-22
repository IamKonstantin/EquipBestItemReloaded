using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.Core;
using TaleWorlds.Library;


namespace EquipBestItem
{
    /// <summary>
    /// This class serves as a static helper for the codebase. Some methods and fields cannot
    /// be accessed by directly through code, but it can done with C# reflection. These functions provided
    /// by this class reduces the need to write reflection code to access methods and fields.
    /// </summary>
    public static class Helper
    {

        /// <summary>
        /// Invokes the method of a given object and returns a object
        /// </summary>
        /// <param name="o">Object</param>
        /// <param name="methodName">Name of the method</param>
        /// <param name="args">Method arguments</param>
        /// <returns>nullable object</returns>
        /// <exception cref="MBException">Occurs when invoking them method fails</exception>
        internal static object GetMethod(this object o, string methodName, params object[] args)
        {
            // Get method info from the given type
            var methodInfo = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            // Run the method on the object if we have the method info
            // Otherwise, return null since there is no method info
            if (methodInfo != null)
            {
                // Note: Errors can occur while invoking the method if
                // the method has incorrect arguments or the method itself
                // causes an error
                return methodInfo.Invoke(o, args);
            }
            return null;
        }

        /// <summary>
        /// Returns a field based on the given object and field name
        /// </summary>
        /// <param name="o">Object where to get the field</param>
        /// <param name="fieldName">Object's field name</param>
        /// <returns>Nullable object</returns>
        /// <exception cref="MBException">Occurs when getting value fails</exception>
        internal static object GetField(this object o, string fieldName)
        {
            // Get field info from the given type
            var fieldInfo = o.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Get field value from the object if we have the field info
            // Otherwise, return null since there is no field info
            if (fieldInfo != null)
            {
                // Note: Errors can occur when getting value from the object
                // if the object causes an error when getting value
                return fieldInfo.GetValue(o);
            }
            return null;
        }

        /// <summary>
        /// Serializes data into a XML file
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="fileName">Name of file</param>
        /// <param name="data">Data to be serialized</param>
        /// <exception cref="MBException">Occurs if serialization fails</exception>
        public static void Serialize<T>(string fileName, T data)
        {
            TextWriter writer = null;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            
            // Add prefix and namespace for the serializer to work
            ns.Add("", "");

            try
            {
                // Opens a XML file using the file name
                writer = new StreamWriter(fileName);

                // Create XML serializer for the given data type
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                // Serialize the data to the XML file
                serializer.Serialize(writer, data, ns);
            }
            finally
            {
                // Closes the file if file was open before
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Returns the data by deserializing XML file using the file name
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="fileName">XML file name</param>
        /// <returns>Deserialized data</returns>
        /// <exception cref="MBException">Occurs if deserialization fails</exception>
        public static T Deserialize<T>(string fileName)
        {
            XmlReader xmlReader = null;
            StreamReader streamReader = null;
            T data = default(T);
            if (!File.Exists(fileName))
                return data;
            try
            {
                using (streamReader = new StreamReader(fileName))
                {
                    xmlReader = XmlReader.Create(streamReader);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    data = (T)serializer.Deserialize(xmlReader);
                }
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
            }
            return data;
        }

        public static void displayException(System.Exception ex, String message = "Exception caught.")
        {
            if (ex == null)
                return;
            message += "\n";
            message += ex.Message;
            var stackTrace = new System.Diagnostics.StackTrace(ex, true);
            message += "\nStack trace:\n";
            message += stackTrace.ToString();
            log(message, true);
        }

        private static string _filePathLog = Path.Combine(BasePath.Name, "Modules", "EquipBestItem", "ModuleData", "log.txt");
        public static void log(String text, bool error = false)
        {
            if (text == null)
                return;

            String timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            String messageFile = String.Format(CultureInfo.InvariantCulture, "{0} {1}: {2}", timestamp, error ? "ERROR" : "INFO", text);
            File.AppendAllText(_filePathLog, messageFile);

            int newLinePos = text.IndexOf('\n');
            String messageGame = String.Format(CultureInfo.InvariantCulture, "EquipBestItem: {0}: {1}{2}",
                error ? "ERROR" : "INFO",
                newLinePos >= 0 ? text.Substring(0, newLinePos) : text,
                newLinePos >= 0 ? "" : "...");
            if (messageGame[messageGame.Length - 1] != '\n')
                messageGame += '\n';
            InformationManager.DisplayMessage(new InformationMessage(messageGame, new Color(error ? 1f : 0f, 0f, 0f)));
        }
    }
}
