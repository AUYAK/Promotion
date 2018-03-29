using Domain.Entities;
using System;

namespace WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public String ReturnUrl { get; set; }
     }
}