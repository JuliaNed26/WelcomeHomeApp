﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHomeModels.Models;

namespace WelcomeHomeModels.Repositories
{
    public interface IEventsRepository //: IDisposable
    {
        IEnumerable<Event> GetEvents();

        Event? GetEventById(int id);

        //IEnumerable<Event> GetEventsByType(int typeId);

        void AddEvent(Event newEvent);

        void DeleteEvent(int id);

        void UpdateEvent(int id, Event editedEvent);

        //IEnumerable<Volunteer> GetEventHosts(int id);
    }
}
