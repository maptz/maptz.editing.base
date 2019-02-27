using System;
using System.Collections.Generic;

namespace Maptz.Editing.Edl
{

    /// <summary>
    /// The default IEdlDeserializerServiceFactory. 
    /// </summary>
    public class DefaultEdlDeserializerServiceFactory : IEdlDeserializerFactory
    {

        public DefaultEdlDeserializerServiceFactory(IDictionary<string, Func<IEdlDeserializer>> dict)
        {
            this.DeserializerDictionary = dict;
        }

        public DefaultEdlDeserializerServiceFactory()
        {
            this.DeserializerDictionary = new Dictionary<string, Func<IEdlDeserializer>>();
            this.DeserializerDictionary.Add(EdlModes.CMX3600, () => new CMX3600Deserializer());
            this.DeserializerDictionary.Add(EdlModes.GVG, () => new GVGEdlReaderService());
        }

        private IDictionary<string, Func<IEdlDeserializer>> DeserializerDictionary { get; }

        /// <summary>
        /// Gets an EdlDeserializer based on the EdlMode. 
        /// </summary>
        /// <param name="edlMode"></param>
        /// <returns></returns>
        public IEdlDeserializer GetEdlDeserializer(string edlMode)
        {
            if (this.DeserializerDictionary.ContainsKey(edlMode))
                return this.DeserializerDictionary[edlMode]();
            throw new NotSupportedException($"The EDLMode '{edlMode}' is not supported.");
        }
    }
}