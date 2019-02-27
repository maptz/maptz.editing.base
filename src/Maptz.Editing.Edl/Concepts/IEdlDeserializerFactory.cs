namespace Maptz.Editing.Edl
{
    /// <summary>
    /// A factory for getting IEdlDeserializer instances. 
    /// </summary>
    public interface IEdlDeserializerFactory
    {
        /// <summary>
        /// Gets an IEdlDeserializerService for a specific type of EDL. 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        IEdlDeserializer GetEdlDeserializer(string edlMode);
    }
}