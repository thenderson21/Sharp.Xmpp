namespace Sharp.Xmpp.Extensions.XEP_0045
{
    /// <summary>
    /// Describes how long a conference room will exist for.
    /// </summary>
    public enum RoomPersistence
    {
        /// <summary>
        /// A room that is destroyed if the last occupant exits.
        /// </summary>
        Temporary,

        /// <summary>
        /// A room that is not destroyed if the last occupant exits.
        /// </summary>
        Persistent
    }
}
