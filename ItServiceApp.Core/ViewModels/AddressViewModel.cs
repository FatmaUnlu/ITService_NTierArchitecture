﻿
using ItServiceApp.Core.Entities;
using System;

namespace ItServiceApp.Core.ViewModels
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        public string Line { get; set; }
        public string PostCode { get; set; }
        public AddressTypes AddressType { get; set; }
        public int StateId { get; set; }
        public string UserId { get; set; }


    }
}
