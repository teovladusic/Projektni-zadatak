﻿using System.ComponentModel.DataAnnotations;

namespace Projektni_Zadatak_Project_Service.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        [Required]
        public int MakeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }
    }
}