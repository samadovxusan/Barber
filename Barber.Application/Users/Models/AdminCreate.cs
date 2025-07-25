﻿using System.Text.Json.Serialization;
using Barber.Domain.Enums;

namespace Barber.Application.Users.Models;

public class AdminCreate
{
    public string FullName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    [JsonIgnore]
    public Role Roles { get; set; } = Role.Admin;
}