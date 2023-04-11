using Study402Online.BuildingBlocks.LocalMessage;

namespace LocalMessage.Test;

record TestMessage : IMessage
{
    public string Value { get; set; }
}
