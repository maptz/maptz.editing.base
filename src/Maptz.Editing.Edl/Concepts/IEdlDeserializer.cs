using System.Collections.Generic;
namespace Maptz.Editing.Edl
{
    /// <summary>
    /// A service used to deserialize an Edl. 
    /// </summary>
    public interface IEdlDeserializer
    {
        ///<summary>Reads a set of edl entries from an EDL.</summary>
        ///<param name="str">The 'str' parameter</param>
        ///<returns>returns [REPLACE]</returns>
        IEnumerable<IEdlEntry> Read(string str);

    }
}