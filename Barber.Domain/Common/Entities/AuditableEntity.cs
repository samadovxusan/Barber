namespace Barber.Domain.Common.Entities;

/// <summary>
/// Represents an abstract class that serves as a base for auditable entities in the system. This class extends the Entity class and implements the IAuditableEntity interface.
/// </summary>
public abstract class AuditableEntity : Entity, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created. This value is typically set automatically by the
    /// system when the entity is first persisted.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated. This value is typically updated automatically
    /// by the system whenever changes are made to the entity's properties.
    /// </summary>
    public DateTimeOffset? ModifiedTime { get; set; }
}