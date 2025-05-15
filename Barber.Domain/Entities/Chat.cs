namespace Barber.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ChatGroup { get; set; } = string.Empty;
    public int CHatterId { get; set; }
}