﻿namespace Web.LMS.Service.dto;
public class StaffDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
}
