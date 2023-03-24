namespace Aboba.Attributes.Server
{
  public readonly struct AttributeModifier
  {
    public string AttributeId { get; }
    
    public float Value { get; }
    
    public AttributeOperationType AttributeOperationType { get; }

    public override int GetHashCode() => AttributeId.GetHashCode();

    public override string ToString() => $"{AttributeId}: {Value}";
    
    public AttributeModifier(string attributeId, float value, AttributeOperationType attributeOperationType)
    {
      AttributeId = attributeId;
      Value = value;
      AttributeOperationType = attributeOperationType;
    }
  }
}