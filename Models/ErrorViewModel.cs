using System;

// Some text comment - kv G
//comment 2.0 - kv h
namespace BookCave.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}