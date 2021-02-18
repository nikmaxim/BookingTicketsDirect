using System;
using System.Collections.Generic;
using System.Text;

namespace BookingTicketsDirect
{
    class Person
    {
        public int Id { get; set; }
        public string CarriageStr { get; set; }
        public string PlaceStr { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public List<string> DocumentTypes { get; set; }
        public string DocumentInfo { get; set; }
        
        public string DocumentTypesStr { get; set; }
        public int ServiceBeddingStr { get; set; }
        public int ServiceWaterStr { get; set; }
    }
}
