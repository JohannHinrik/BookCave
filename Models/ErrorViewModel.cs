using System;

// Some text comment - kv G
// komment númer tvö - G 

namespace BookCave.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}