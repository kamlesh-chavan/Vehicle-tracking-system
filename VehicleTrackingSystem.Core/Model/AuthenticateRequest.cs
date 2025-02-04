﻿using System.ComponentModel.DataAnnotations;

namespace VehicleTrackingSystem.Core.Model
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
