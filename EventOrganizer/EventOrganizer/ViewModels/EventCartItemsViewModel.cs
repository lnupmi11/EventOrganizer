﻿using EventOrganizer.DAL.Models;
using System.Collections.Generic;

namespace EventOrganizer.ViewModels
{
    public class EventCartItemsViewModel
    {
        public IEnumerable<EventCartItem> EventCartItems { get; set; }
    }
}