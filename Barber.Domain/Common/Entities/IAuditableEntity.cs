namespace Barber.Domain.Common.Entities;

/// <summary>
/// Represents an interface for auditable entities, defining properties related to creation and modification timestamps.
/// </summary>
public interface IAuditableEntity : IEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// Can be null if the entity has never been modified.
    /// </summary>
    public DateTimeOffset? ModifiedTime { get; set; }
}