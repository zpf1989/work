namespace MyNet.Components.Serialize.Protobuf.Protobuf 
{
    using System;

    public interface IExtensible
    {
        IExtension GetExtensionObject(bool createIfMissing);
    }
}

