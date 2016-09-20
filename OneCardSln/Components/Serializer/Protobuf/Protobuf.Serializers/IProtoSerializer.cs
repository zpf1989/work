namespace OneCardSln.Components.Serialize.Protobuf.Serializers
{
    using OneCardSln.Components.Serialize.Protobuf.Compiler;
    using OneCardSln.Components.Serialize.Protobuf.Protobuf;
    using System;

    internal interface IProtoSerializer
    {
        void EmitRead(CompilerContext ctx, Local entity);
        void EmitWrite(CompilerContext ctx, Local valueFrom);
        object Read(object value, ProtoReader source);
        void Write(object value, ProtoWriter dest);

        Type ExpectedType { get; }

        bool RequiresOldValue { get; }

        bool ReturnsValue { get; }
    }
}

