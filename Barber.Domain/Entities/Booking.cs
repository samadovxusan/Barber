﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Barber.Domain.Common.Entities;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class Booking:AuditableEntity
{
    public DateOnly Date { get; set; }
    public TimeSpan AppointmentTime { get; set; }
    public Status Status { get; set; } = Enums.Status.Pending;
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid BarberId { get; set; }
    public Barber? Barber { get; set; }
    public bool Confirmed { get; set; } = false;
    public string ServiceId { get; set; } = string.Empty;
    [NotMapped]
    public Guid[] ServiceIdsArray { get; set; } = Array.Empty<Guid>();
}
